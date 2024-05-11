﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using KeePass;
using KeePass.App;
using KeePass.App.Configuration;
using KeePass.Forms;
using KeePass.Resources;
using KeePass.UI;
using KeePass.Util;
using KeePass.Util.Spr;
using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;
using KeePassLib.Utility;
using PluginTools;
using PluginTranslation;

namespace PasswordChangeAssistant
{
  public partial class PCADialog : Form, IGwmWindow
  {
    #region members
    private static MethodInfo m_miEncodeForCommandLine;

    private PwInputControlGroup m_icgNewPassword = new PwInputControlGroup();
    private OpenWithMenu m_dynOpenUrl;
    private ToolStripMenuItem tsmiOpen = new ToolStripMenuItem();
    private ToolStripMenuItem tsmiCopy = new ToolStripMenuItem();

    private List<string> m_Sequences = new List<string>();
    private PwEntry m_peCtxEntry = null;

    private string sURL = string.Empty;

    private PwProfile m_Profile = null;
    public string Profile { get { return m_Profile == null ? string.Empty : m_Profile.Name; } }

    public ExpiryControlGroup EntryExpiry = new ExpiryControlGroup();
    public ProtectedString NewPassword { get { return tbPasswordNew.TextEx; } }
    public string Sequence { get { return rtbSequence.Text; } }

    public string PCAURL { get { return tbURL2.Text; } }

    private Action<object, CancelEventArgs> m_OnProfilesOpening = null;
    private PCAInitData m_pcadata = null;
    public PCAInitData PCAData { get { return m_pcadata; } }
    #endregion

    public bool CanCloseWithoutDataLoss { get { return true; } }


    #region PCA form (load, show, close, ...)
    static PCADialog()
    {
      try
      {
        Type t = Program.MainForm.GetType().Assembly.GetType("KeePass.Util.Spr.SprEncoding");
        m_miEncodeForCommandLine = t.GetMethod("EncodeForCommandLine", BindingFlags.Static | BindingFlags.NonPublic);
      }
      catch { }
    }

    public PCADialog()
    {
      InitializeComponent();
    }

    private void PCADialog_Load(object sender, EventArgs e)
    {
      //Create input control group for new password
      m_icgNewPassword.Attach(tbPasswordNew, cbToggleNewPassword, m_lblPasswordRepeat, tbPasswordNewRepeat, m_lblQuality,
        pbNewPasswordQuality, lNewPasswordQualityInfo, toolTip1, this, cbToggleNewPassword.Checked, false);
      m_icgNewPassword.ContextDatabase = Program.MainForm.DocumentManager.SafeFindContainerOf(m_pcadata.Entry);
      m_icgNewPassword.ContextEntry = m_pcadata.Entry;

      bool bForceHide = !AppPolicy.Current.UnhidePasswords;
      AceColumn colPw = Program.Config.MainWindow.FindColumn(AceColumnType.Password);
      bool bShowPassword = (colPw != null) ? !colPw.HideWithAsterisks : false;
      bShowPassword &= !bForceHide;
      if (Program.Config.UI.Hiding.SeparateHidingSettings)
        cbToggleNewPassword.Checked = (Program.Config.UI.Hiding.HideInEntryWindow || bForceHide);
      else
        cbToggleNewPassword.Checked = !bShowPassword;
      cbToggleOldPassword.Checked = cbToggleNewPassword.Checked;
      tbPasswordOld.EnableProtection(cbToggleOldPassword.Checked);
      cbToggleOldPassword.Image = cbToggleNewPassword.Image;
      cbToggleOldPassword.Text = string.Empty;
      tbPasswordOld.ReadOnly = true;
    }

    private void PCADialog_Shown(object sender, EventArgs e)
    {
      rtbSequence_TextChanged(null, null);
      PwProfile PPSProfile = null;
      string PPSProfileName = m_pcadata.Entry.CustomDataGetSafe(Config.ProfileLastUsedProfile);
      if (PPSProfileName == Config.ProfileAutoGenerated)
        PPSProfile = Program.Config.PasswordGenerator.AutoGeneratedPasswordsProfile;
      else
        PPSProfile = PwGeneratorUtil.GetAllProfiles(false).Find(X => X.Name == PPSProfileName);
      if (PPSProfile != null)
      {
        m_Profile = PPSProfile;
        CreateNewPassword(PPSProfile);
      }
      else CreateNewPassword(PwProfile.DeriveFromPassword(m_pcadata.OldPassword));
      HandleURLFields();
    }

    private void PCADialog_FormClosed(object sender, FormClosedEventArgs e)
    {
      tsmiOpen.Dispose();
      tsmiCopy.Dispose();
      if (m_dynOpenUrl != null) m_dynOpenUrl.Destroy();
      SprEngine.FilterPlaceholderHints.Remove(Config.PlaceholderOldPW);
      SprEngine.FilterPlaceholderHints.Remove(Config.PlaceholderNewPW);
    }
    #endregion

    public void Init(PCAInitData pcadata, Action<object, CancelEventArgs> eProfilesOpening)
    {
      #region Translations
      Text = PluginTranslate.PluginName;
      tbURL2.PromptText = PluginTranslate.URL2Hint;
      gPasswords.Text = PluginTranslate.ChangePassword; ;
      lOldPassword.Text = PluginTranslate.OldPW;
      lNewPassword.Text = PluginTranslate.NewPW;
      bCancel.Text = KPRes.Cancel;
      bChangePassword.Text = PluginTranslate.ButtonChangePW;
      bOldPasswordCopy.Text = bNewPasswordCopy.Text = KPRes.Copy;
      bSequence.Text = bOldPasswordType.Text = bNewPasswordType.Text = KPRes.AutoType;
      gSequence.Text = PluginTranslate.PCASequence;
      bSequenceEdit.Text = KPRes.EditCmd;
      bSequenceEdit.Width = bNewPasswordCopy.Width = bOldPasswordCopy.Width = Math.Max(bNewPasswordCopy.Width, bSequenceEdit.Width);
      bSequence.Left = bOldPasswordType.Left = bNewPasswordType.Left = bOldPasswordCopy.Left + bOldPasswordCopy.Width + 10;
      KeePassLib.Translation.KPFormCustomization kpfc = Program.Translation.Forms.Find(x => x.FullName == typeof(PwEntryForm).FullName);
      if (kpfc != null)
      {
        kpfc.ApplyTo(this);
        m_cbExpires.Text = StrUtil.RemoveAccelerator(m_cbExpires.Text);
        m_lblPasswordRepeat.Text = StrUtil.RemoveAccelerator(m_lblPasswordRepeat.Text);
        m_lblQuality.Text = StrUtil.RemoveAccelerator(m_lblQuality.Text);
      }
      if (!m_lblQuality.Text.EndsWith(":")) m_lblQuality.Text += ":";
      #endregion

      m_OnProfilesOpening = eProfilesOpening;
      m_pcadata = pcadata;

      m_peCtxEntry = new PwEntry(true, true);
      foreach (var kvp in m_pcadata.Entry.CustomData)
        m_peCtxEntry.CustomData.Set(kvp.Key, kvp.Value);
      m_peCtxEntry.Strings = m_pcadata.Entry.Strings;

      InitSequences();
      rtbSequence.Text = m_pcadata.PCASequence;

      #region Handle SecureTextBoxEx
      SecureTextBoxEx.InitEx(ref tbPasswordOld);
      SecureTextBoxEx.InitEx(ref tbPasswordNew);
      SecureTextBoxEx.InitEx(ref tbPasswordNewRepeat);

      tbPasswordOld.TextEx = pcadata.OldPassword;
      tbPasswordOld.ReadOnly = true;

      ToolTip ttPwGen = new ToolTip();
      #endregion

      SprEngine.FilterPlaceholderHints.Add(Config.PlaceholderOldPW);
      SprEngine.FilterPlaceholderHints.Add(Config.PlaceholderNewPW);

      #region password profile button - context menu, integration of PasswordProfileSync, ...
      ttPwGen.SetToolTip(GeneratePW, KPRes.GeneratePassword);
      GeneratePW.Image = UIUtil.CreateDropDownImage(Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_Key_New")));

      bChangePassword.Image = Config.ScaleImage(Resources.pca);

      //Password profile dropdown
      UpdateProfilesContextMenu();
      #endregion

      Program.Translation.ApplyTo("KeePass.Forms.PwEntryForm.m_ctxDefaultTimes", m_ctxDefaultTimes.Items);
      bExpiry.Image = UIUtil.CreateDropDownImage(Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_History")));
      InitExpiryDate();

      #region Show entry title - shortened if required
      string title = m_pcadata.Title;
      string user = m_pcadata.User;
      bool titleShortened = false, userShortened = false;
      while ((title.Length + user.Length) > 60)
      {
        if (title.Length > 30)
        {
          title = title.Substring(0, title.Length - 1);
          titleShortened = true;
        }
        else
        {
          user = user.Substring(0, user.Length - 1);
          userShortened = true;
        }
      }
      if (titleShortened)
        title += "...";
      if (userShortened)
        user += "...";
      if (string.IsNullOrEmpty(user))
        lEntry.Text = string.Format(PluginTranslate.EntryInfoNoUsername, KPRes.Entry, title);
      else
        lEntry.Text = string.Format(PluginTranslate.EntryInfo, KPRes.Entry, title, user);
      #endregion

      Tools.GlobalWindowManager(this);
    }

    public void CleanupEx()
    {
      if (EntryExpiry != null) EntryExpiry.Release();
      if (m_icgNewPassword != null) m_icgNewPassword.Release();
    }

    private void InitExpiryDate()
    {
      EntryExpiry.Attach(m_cbExpires, dtExpire);
      dtExpire.Format = DateTimePickerFormat.Custom;
      if (m_pcadata.SetExpiry)
      {
        EntryExpiry.Value = m_pcadata.Expiry.ToLocalTime();
        EntryExpiry.Checked = m_pcadata.Expires;
        return;
      }
      else if (m_pcadata.Expires)
      {
        if (Program.Config.Defaults.NewEntryExpiresInDays > -1)
          EntryExpiry.Value = DateTime.Now.AddDays(Program.Config.Defaults.NewEntryExpiresInDays).ToLocalTime();
        else
          EntryExpiry.Value = DateTime.Now.AddMonths(6).ToLocalTime();
        EntryExpiry.Checked = true;
      }
//      else return;

      //Let's see whether PEDCalc is installed and active
      CheckPEDCalc();

      //Let's see whether KeePassCPOEc is installed and active
      CheckKeePassCPOE();
    }

    private void CheckKeePassCPOE()
    {
      KeePassCPEOStub kpcpoe = new KeePassCPEOStub((KeePass.Plugins.Plugin)Tools.GetPluginInstance("KeePassCPEO"));
      if (!kpcpoe.Loaded) return;
      if (kpcpoe.CustomOptions.Count == 0) return;
      m_ctxDefaultTimes.Items.Add(new ToolStripSeparator());
      foreach (var co in kpcpoe.CustomOptions)
      {
        var tsmi = new ToolStripMenuItem() { Text = co.ToString(), Tag = co };
        tsmi.Click += OnKPCPOEClick;
        m_ctxDefaultTimes.Items.Add(tsmi);
      }
    }

    private void OnKPCPOEClick(object sender, EventArgs e)
    {
      var tsmi = sender as ToolStripMenuItem;
      var x = tsmi.Tag.GetType().GetProperties();
      var y = tsmi.Tag.GetType().GetProperty("Years").GetValue(tsmi.Tag, null);
      var m = tsmi.Tag.GetType().GetProperty("Months").GetValue(tsmi.Tag, null);
      var d = tsmi.Tag.GetType().GetProperty("Days").GetValue(tsmi.Tag, null);

      if (y == null || m == null || d == null) return;
      var dt = Program.MainForm.GetSelectedEntry(true).ExpiryTime;
      dt = dt.AddYears((int)y);
      dt = dt.AddMonths((int)y);
      dt = dt.AddDays((int)d);
      EntryExpiry.Value = dt.ToLocalTime();
    }

    private void CheckPEDCalc()
    {
      List<string> lMsg = new List<string>();
      try
      {
        PwEntry pe = Program.MainForm.GetSelectedEntry(true);
        if (pe == null)
        {
          lMsg.Add("Error identifying selected entry");
          return;
        }
        PEDCalcStub pedcalc = new PEDCalcStub((KeePass.Plugins.Plugin)Tools.GetPluginInstance("PEDCalc"));
        if (!pedcalc.Loaded)
        {
          lMsg.Add("PEDCalc not found");
          return;
        }
        lMsg.Add("PEDCalc found");
        if (!pedcalc.Active)
        {
          lMsg.Add("PEDCalc inactive");
          return;
        }
        if (!pedcalc.AdjustExpiryDateRequired(pe))
        {
          lMsg.Add("PEDCalc result: Off, no recalculation neccessary");
          return;
        }
        object pedNewExpireDate;
        DateTime dtNewExpireDate = pedcalc.GetNewExpiryDateUtc(pe, out pedNewExpireDate);
        EntryExpiry.Value = dtNewExpireDate.ToLocalTime();
        lMsg.Add("PEDCalc result: " + pedNewExpireDate.ToString());
        lMsg.Add("New expiry date:" + dtNewExpireDate.ToLocalTime().ToString());
      }
      finally { PluginDebug.AddInfo("Adjust expiry date according to PEDCalc", 0, lMsg.ToArray()); }
    }

    #region URL handling
    private const string PlhTargetUri = @"{OW_URI}";
    private void OnOpenUrl(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(sURL)) return;
      string sDereferenced = m_pcadata.GetDereferenced(sURL);
      try
      {
        if ((sender as ToolStripMenuItem).Tag != null)
        {
          var it = (sender as ToolStripMenuItem).Tag;
          int iFilePathType = (int)it.GetType().GetProperty("FilePathType").GetValue(it, null);
          string strApp = (string)it.GetType().GetProperty("FilePath").GetValue(it, null);

          if (iFilePathType == 0) //OwFilePathType.Executable
            WinUtil.OpenUrlWithApp(sDereferenced, m_pcadata.Entry, strApp);
          else if (iFilePathType == 1) //OwFilePathType.ShellExpand
          {
            string str = string.Empty;
            if (m_miEncodeForCommandLine != null)
              str = strApp.Replace(PlhTargetUri, (string)m_miEncodeForCommandLine.Invoke(null, new object[] { sDereferenced }));
            else
              str = strApp.Replace(PlhTargetUri, sDereferenced);
            WinUtil.OpenUrl(str, m_pcadata.Entry, true);
          }
        }
        else
        {
          Tools.OpenUrl(sDereferenced);
        }
      }
      catch { }
    }

    private void OnCopyUrl(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(sURL)) return;
      string sDereferenced = m_pcadata.GetDereferenced(sURL);
      if (ClipboardUtil.CopyAndMinimize(sDereferenced, true, this, m_pcadata.Entry, Program.MainForm.ActiveDatabase))
        Program.MainForm.StartClipboardCountdown();
    }

    private void EntryURLClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      LinkLabel llURL = sender as LinkLabel;
      sURL = string.Empty;
      if (llURL == lURL)
        sURL = e.Link.LinkData as string;
      else if (llURL == lURL2)
        sURL = tbURL2.Text;
      if (string.IsNullOrEmpty(sURL)) return;
      llURL.ContextMenuStrip.Show(llURL, Cursor.Position.X - llURL.PointToScreen(Point.Empty).X, llURL.Height);
    }

    private string GetDisplayUrl(string url, int MaxDisplayChars)
    {
      if (url.Length <= MaxDisplayChars) return url;
      return url.Substring(0, MaxDisplayChars / 2) + "..." + url.Substring(url.Length - MaxDisplayChars / 2);
    }

    private void HandleURLFields()
    {
      #region URL context menu
      tsmiOpen.Text = KPRes.OpenCmd;
      tsmiOpen.Image = (Image)Program.Resources.GetObject("B16x16_FTP");
      tsmiOpen.Click += OnOpenUrl;
      ctxOpenWith.Items.Insert(0, tsmiOpen);
      Dictionary<string, string> translations = Program.Translation.SafeGetStringTableDictionary("KeePass.Forms.MainForm.m_ctxPwList");
      string translated = string.Empty;
      if (translations.TryGetValue("m_ctxEntryCopyUrl", out translated))
        tsmiCopy.Text = translated;
      else
        tsmiCopy.Text = KPRes.Copy;
      tsmiCopy.Image = (Image)Program.Resources.GetObject("B16x16_EditCopyUrl");
      tsmiCopy.Click += OnCopyUrl;
      ctxOpenWith.Items.Insert(1, tsmiCopy);

      #region Add additional "Open with" entries
      ToolStripMenuItem tsmiOpenWith = new ToolStripMenuItem();
      m_dynOpenUrl = new OpenWithMenu(tsmiOpenWith);
      //Show dropdown to have the entries created
      try
      {
        //Use reflection first
        //Use 'ShowDropDown' as fallback, won't work on Mono
        MethodInfo mi_OnMenuOpening = m_dynOpenUrl.GetType().GetMethod("OnMenuOpening", BindingFlags.Instance | BindingFlags.NonPublic);
        if (mi_OnMenuOpening != null)
        {
          mi_OnMenuOpening.Invoke(m_dynOpenUrl, new object[] { null, null });
        }
        else
        {
          tsmiOpenWith.ShowDropDown();
          tsmiOpenWith.HideDropDown();
        }
      }
      catch { }
      while (tsmiOpenWith.DropDownItems.Count > 0)
      {
        ToolStripItem x = tsmiOpenWith.DropDownItems[0];
        tsmiOpenWith.DropDownItems.Remove(x);
        if (!string.IsNullOrEmpty(m_pcadata.PCAURL) && (x is ToolStripMenuItem))
        {
          ToolStripMenuItem newItem = new ToolStripMenuItem(x.Text, x.Image);
          newItem.Tag = x.Tag;
          newItem.Click += OnOpenUrl;
          x = newItem;
        }
        ctxOpenWith.Items.Add(x);
      };
      tsmiOpenWith.Dispose();
      #endregion
      #endregion

      lURL.Links.Clear();
      lURL.Text = KPRes.Url + ": " + KPRes.Empty;
      if (!string.IsNullOrEmpty(m_pcadata.MainURL))
      {
        //string url = CompileUrl(m_pcadata.MainURL);
        lURL.Links.Add(KPRes.Url.Length + 2, m_pcadata.MainURL.Length, m_pcadata.MainURL);
        lURL.ContextMenuStrip.Opening += CtxOpenWith_Opening;
        lURL.Text = KPRes.Url + ": " + GetDisplayUrl(m_pcadata.MainURL, 60);
      }

      lURL2.Links.Clear();
      lURL2.Text = KPRes.Url + ": ";
      tbURL2.Left = lURL2.Left + lURL2.Width;
      tbURL2.Width = ClientSize.Width - tbURL2.Left - lURL2.Left;
      if (!string.IsNullOrEmpty(m_pcadata.PCAURL)) tbURL2.Text = m_pcadata.PCAURL;
    }

    private void CtxOpenWith_Opening(object sender, CancelEventArgs e)
    {
      bool bEnabled = !WinUtil.IsCommandLineUrl(sURL);
      foreach (var x in ctxOpenWith.Items)
      {
        if (!(x is ToolStripMenuItem)) continue;
        if (x == tsmiOpen || x == tsmiCopy) continue;
        (x as ToolStripMenuItem).Enabled = bEnabled;
      }
    }
    #endregion

    #region PCA url textbox
    private void tbURL2_TextChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbURL2.Text))
      {
        if (lURL2.LinkArea.Length == 0) return;
        lURL2.LinkArea = new LinkArea(0, 0);
        return;
      }
      if (lURL2.LinkArea.Length != 0) return;
      lURL2.LinkArea = new LinkArea(0, lURL2.Text.Length - 2);
    }
    #endregion

    #region password generation
    private byte[] m_pbEntropy = null;
    private void CreateNewPassword(PwProfile prof)
    {
      if (prof == null) prof = PwProfile.DeriveFromPassword(m_pcadata.OldPassword);
      if (prof.CollectUserEntropy && (m_pbEntropy == null)) m_pbEntropy = EntropyForm.CollectEntropyIfEnabled(prof);
      ProtectedString psNew;
      PwGenerator.Generate(out psNew, prof, m_pbEntropy, Program.PwGeneratorPool);
      m_icgNewPassword.SetPassword(psNew, true);
    }

    private void GeneratePWClick(object sender, EventArgs e)
    {
      ctxPWGen.Show(GeneratePW, 0, GeneratePW.Height);
    }

    private void OnPWGenOpen(object sender, EventArgs e)
    {
      ProtectedString ps = m_icgNewPassword.GetPasswordEx();
      PwProfile opt = PwProfile.DeriveFromPassword(ps);

      PwGeneratorForm pgf = new PwGeneratorForm();
      pgf.InitEx((!ps.IsEmpty ? opt : null), true, false);

      if (pgf.ShowDialog() == DialogResult.OK)
      {
        CreateNewPassword(pgf.SelectedProfile);
        m_Profile = pgf.SelectedProfile;
      }
      UpdateProfilesContextMenu();
      UIUtil.DestroyForm(pgf);
    }

    //Format password profiles dependant on usage
    // - Used as PCA profile
    // - Used for generating the new password
    // - No special usage
    private void ctxPWGen_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      try
      {
        m_OnProfilesOpening(sender, e);
      }
      catch (Exception ex) { PluginDebug.AddError(ex.Message); }
    }

    private void OnProfileClick(object sender, EventArgs e)
    {
      string prof = (sender as ToolStripMenuItem).Text;

      m_Profile = null;
      if (prof == "(" + KPRes.GenPwBasedOnPrevious + ")")
        m_Profile = PwProfile.DeriveFromPassword(m_pcadata.OldPassword);
      else if (prof == "(" + KPRes.AutoGeneratedPasswordSettings + ")")
        m_Profile = Program.Config.PasswordGenerator.AutoGeneratedPasswordsProfile;
      else
        m_Profile = PwGeneratorUtil.GetAllProfiles(false).Find(x => x.Name == prof);
      if (m_Profile != null) CreateNewPassword(m_Profile);
    }

    public void UpdateProfilesContextMenu()
    {
      Image ImgProfPrevious = Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_CompFile"));
      Image ImgProfAuto = Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_FileNew"));
      Image ImgProfStandard = Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_KOrganizer"));
      ctxPWGen.Items.Clear();
      ctxPWGen.ImageScalingSize = new Size(DpiUtil.ScaleIntX(16), DpiUtil.ScaleIntY(16));

      ToolStripMenuItem miProfile = new ToolStripMenuItem("Password Generator", Config.ScaleImage((Image)Program.Resources.GetObject("B16x16_Key_New"), 16, 16), OnPWGenOpen);
      Dictionary<string, string> translation = Program.Translation.SafeGetStringTableDictionary("KeePass.Forms.PwEntryForm.m_ctxPwGen");
      string translated = string.Empty;
      if (translation.TryGetValue("m_ctxPwGenOpen", out translated))
        miProfile.Text = translated;
      ctxPWGen.Items.Add(miProfile);
      ctxPWGen.Items.Add(new ToolStripSeparator());
      miProfile = new ToolStripMenuItem("(" + KPRes.GenPwBasedOnPrevious + ")", ImgProfPrevious, OnProfileClick);
      miProfile.Tag = "(" + KPRes.GenPwBasedOnPrevious + ")";
      ctxPWGen.Items.Add(miProfile);
      miProfile = new ToolStripMenuItem("(" + KPRes.AutoGeneratedPasswordSettings + ")", ImgProfAuto, OnProfileClick);
      miProfile.Tag = "(" + KPRes.AutoGeneratedPasswordSettings + ")";
      ctxPWGen.Items.Add(miProfile);
      bool bHideBuiltIn = ((Program.Config.UI.UIFlags & (ulong)AceUIFlags.HideBuiltInPwGenPrfInEntryDlg) != 0);
      foreach (PwProfile pw in PwGeneratorUtil.GetAllProfiles(true))
      {
        if (bHideBuiltIn && PwGeneratorUtil.IsBuiltInProfile(pw.Name)) continue;
        miProfile = new ToolStripMenuItem(pw.Name, ImgProfStandard, OnProfileClick);
        miProfile.Tag = pw.Name;
        ctxPWGen.Items.Add(miProfile);
      }
    }
    #endregion

    #region password usage (toggle, copy & type)
    private void passwordTypeClick(object sender, EventArgs e)
    {
      if ((sender as Button).Name.Contains("Old"))
        PasswordChangeAssistantExt.PasswordType(m_pcadata.Entry, m_pcadata.OldPassword);
      else
        PasswordChangeAssistantExt.PasswordType(m_pcadata.Entry, tbPasswordNew.TextEx);
    }

    private void passwordCopyClick(object sender, EventArgs e)
    {
      if ((sender as Button).Name.Contains("Old"))
        PasswordChangeAssistantExt.PasswordCopy(m_pcadata.Entry, m_pcadata.OldPassword);
      else
        PasswordChangeAssistantExt.PasswordCopy(m_pcadata.Entry, tbPasswordNew.TextEx);
    }

    private void toggleOldPassword(object sender, EventArgs e)
    {
      if (AppPolicy.Try(AppPolicyId.UnhidePasswords))
        tbPasswordOld.EnableProtection(cbToggleOldPassword.Checked);
    }
    #endregion

    #region PCA sequence
    private void InitSequences()
    {
      m_Sequences = Config.DefaultPCASequences.Values.ToList();
      cbSequences.Items.AddRange(Config.DefaultPCASequences.Keys.ToArray());
      cbSequences.Enabled = cbSequences.Items.Count > 1;
    }

    private void bSequence_Click(object sender, EventArgs e)
    {
      PasswordChangeAssistantExt.SequenceType(m_pcadata.Entry, tbPasswordNew.TextEx, rtbSequence.Text);
    }

    private void bSequenceEdit_Click(object sender, EventArgs e)
    {
      EditAutoTypeItemForm dlg = new EditAutoTypeItemForm();
      AutoTypeConfig atc = new AutoTypeConfig();
      if (string.IsNullOrEmpty(rtbSequence.Text))
        atc.DefaultSequence = PasswordChangeAssistantExt.GetPCASequence(m_pcadata.Entry, PluginTranslate.DefaultSequence01);
      else atc.DefaultSequence = rtbSequence.Text;
      dlg.InitEx(atc, -1, true, rtbSequence.Text, m_pcadata.Entry.Strings);
      dlg.Text = KPRes.ConfigureKeystrokeSeq;
      Control cCustomSequence = Tools.GetControl("m_rbKeySeq", dlg);
      Control cFirst = Tools.GetControl("m_lblTargetWindow", dlg);
      if ((cCustomSequence != null) && (cFirst != null))
      {
        int y = cCustomSequence.Top - cFirst.Top;
        HideControl("m_lblTargetWindow", dlg);
        HideControl("m_rbSeqDefault", dlg);
        HideControl("m_cmbWindow", dlg);
        HideControl("m_lblOpenHint", dlg);
        HideControl("m_lnkWildcardRegexHint", dlg);
        HideControl("m_rbSeqCustom", dlg);
        MoveControlUp("m_rbKeySeq", y, dlg);
        MoveControlUp("m_lblKeySeqInsertInfo", y, dlg);
        MoveControlUp("m_rtbPlaceholders", y, dlg);
        Tools.GetControl("m_rtbPlaceholders", dlg).Height += y;
      }
      if (UIUtil.ShowDialogAndDestroy(dlg) == DialogResult.OK)
        rtbSequence.Text = atc.DefaultSequence;
    }

    private void rtbSequence_TextChanged(object sender, EventArgs e)
    {
      string sequence = rtbSequence.Text;
      int i = m_Sequences.IndexOf(sequence);
      if (i == -1) i = m_Sequences.Count - 1;
      cbSequences.SelectedIndex = Math.Min(i, cbSequences.Items.Count - 1);
      SprContext ctx = new SprContext();
      ctx.EncodeAsAutoTypeSequence = true;

      ctx.Entry = m_peCtxEntry;

      SprSyntax.Highlight(rtbSequence, ctx);
    }

    private void cbSequences_SelectedIndexChanged(object sender, EventArgs e)
    {
      string s = cbSequences.Items[cbSequences.SelectedIndex] as string;
      if (Config.DefaultPCASequences.TryGetValue(cbSequences.Items[cbSequences.SelectedIndex] as string, out s)
        && !string.IsNullOrEmpty(s)
        && (s != rtbSequence.Text))
        rtbSequence.Text = s;
    }

    private void HideControl(string ControlName, Form f)
    {
      Control c = Tools.GetControl(ControlName, f);
      if (c != null) c.Visible = false;
    }

    private void MoveControlUp(string ControlName, int pixelsUp, Form f)
    {
      Control c = Tools.GetControl(ControlName, f);
      if (c != null) c.Top -= pixelsUp;
    }
    #endregion

    private void bChangePassword_Click(object sender, EventArgs e)
    {
      if (!m_icgNewPassword.ValidateData(true))
      {
        DialogResult = DialogResult.None;
        return;
      }
    }

    private void OnMenuExpire(object sender, EventArgs e)
    {
      if (!(sender is ToolStripMenuItem)) return;
      string expiry = (sender as ToolStripMenuItem).Tag as string;
      if (string.IsNullOrEmpty(expiry)) return;
      int pos = expiry.IndexOf('Y');
      int year = 0;
      if (!int.TryParse(expiry.Substring(0, pos), out year)) return;
      expiry = expiry.Substring(pos + 1);
      pos = expiry.IndexOf('M');
      int month = 0;
      if (!int.TryParse(expiry.Substring(0, pos), out month)) return;
      expiry = expiry.Substring(pos + 1);
      pos = expiry.IndexOf('D');
      int day = 0;
      if (!int.TryParse(expiry.Substring(0, pos), out day)) return;
      SetExpireIn(year, month, day);
    }

    private void OnbExpiryClick(object sender, EventArgs e)
    {
      m_ctxDefaultTimes.ShowEx(bExpiry);
    }

    private void SetExpireIn(int nYears, int nMonths, int nDays)
    {
      DateTime dt = DateTime.Now; // Not UTC
      if ((nYears != 0) || (nMonths != 0) || (nDays != 0))
      {
        dt = dt.Date; // Remove time part
        dt = dt.AddYears(nYears);
        dt = dt.AddMonths(nMonths);
        dt = dt.AddDays(nDays);

        DateTime dtPrev = TimeUtil.ToLocal(EntryExpiry.Value, false);
        dt = dt.AddHours(dtPrev.Hour);
        dt = dt.AddMinutes(dtPrev.Minute);
        dt = dt.AddSeconds(dtPrev.Second);
      }
      // else do not change the time part of dt

      EntryExpiry.Checked = true;
      EntryExpiry.Value = dt;
    }
  }
}
