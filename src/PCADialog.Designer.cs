﻿
using System;
using KeePass.UI;
namespace PasswordChangeAssistant
{
	partial class PCADialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button bChangePassword;
		private System.Windows.Forms.Button bCancel;
		private SecureTextBoxEx tbPasswordOld;
		private SecureTextBoxEx tbPasswordNew;
		private System.Windows.Forms.Label lOldPassword;
		private System.Windows.Forms.Label lNewPassword;
		private System.Windows.Forms.Label m_lblPasswordRepeat;
		private KeePass.UI.SecureTextBoxEx tbPasswordNewRepeat;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox cbToggleOldPassword;
		private System.Windows.Forms.CheckBox cbToggleNewPassword;
		private KeePass.UI.QualityProgressBar pbNewPasswordQuality;
		private System.Windows.Forms.Button bOldPasswordType;
		private System.Windows.Forms.Button bNewPasswordType;
		private System.Windows.Forms.Label lNewPasswordQualityInfo;
		private System.Windows.Forms.Button bOldPasswordCopy;
		private System.Windows.Forms.Button bNewPasswordCopy;
		private System.Windows.Forms.Button GeneratePW;
		private System.Windows.Forms.Label lEntry;
		private System.Windows.Forms.ContextMenuStrip ctxPWGen;
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pEntryInfo = new System.Windows.Forms.Panel();
			this.tbURL2 = new PromptedTextBox();// new System.Windows.Forms.TextBox();
			this.ctxOpenWith = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.lURL2 = new System.Windows.Forms.LinkLabel();
			this.lURL = new System.Windows.Forms.LinkLabel();
			this.lEntry = new System.Windows.Forms.Label();
			this.pButtons = new System.Windows.Forms.Panel();
			this.bCancel = new System.Windows.Forms.Button();
			this.bChangePassword = new System.Windows.Forms.Button();
			this.lOldPassword = new System.Windows.Forms.Label();
			this.lNewPassword = new System.Windows.Forms.Label();
			this.m_lblPasswordRepeat = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cbToggleOldPassword = new System.Windows.Forms.CheckBox();
			this.cbToggleNewPassword = new System.Windows.Forms.CheckBox();
			this.lNewPasswordQualityInfo = new System.Windows.Forms.Label();
			this.bOldPasswordType = new System.Windows.Forms.Button();
			this.bNewPasswordType = new System.Windows.Forms.Button();
			this.bOldPasswordCopy = new System.Windows.Forms.Button();
			this.bNewPasswordCopy = new System.Windows.Forms.Button();
			this.GeneratePW = new System.Windows.Forms.Button();
			this.ctxPWGen = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.m_lblQuality = new System.Windows.Forms.Label();
			this.gPasswords = new System.Windows.Forms.GroupBox();
			this.bExpiry = new System.Windows.Forms.Button();
			this.dtExpire = new System.Windows.Forms.DateTimePicker();
			this.m_cbExpires = new System.Windows.Forms.CheckBox();
			this.pbNewPasswordQuality = new KeePass.UI.QualityProgressBar();
			this.tbPasswordNewRepeat = new KeePass.UI.SecureTextBoxEx();
			this.tbPasswordNew = new KeePass.UI.SecureTextBoxEx();
			this.tbPasswordOld = new KeePass.UI.SecureTextBoxEx();
			this.gSequence = new System.Windows.Forms.GroupBox();
			this.bSequenceEdit = new System.Windows.Forms.Button();
			this.bSequence = new System.Windows.Forms.Button();
			this.rtbSequence = new KeePass.UI.CustomRichTextBoxEx();
			this.cbSequences = new System.Windows.Forms.ComboBox();
			this.m_ctxDefaultTimes = new KeePass.UI.CustomContextMenuStripEx(this.components);
			this.m_menuExpireNow = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpireSep0 = new System.Windows.Forms.ToolStripSeparator();
			this.m_menuExpire1Week = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpire2Weeks = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpireSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_menuExpire1Month = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpire3Months = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpire6Months = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menuExpireSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.m_menuExpire1Year = new System.Windows.Forms.ToolStripMenuItem();
			this.pEntryInfo.SuspendLayout();
			this.pButtons.SuspendLayout();
			this.gPasswords.SuspendLayout();
			this.gSequence.SuspendLayout();
			this.m_ctxDefaultTimes.SuspendLayout();
			this.SuspendLayout();
			// 
			// pEntryInfo
			// 
			this.pEntryInfo.Controls.Add(this.tbURL2);
			this.pEntryInfo.Controls.Add(this.lURL2);
			this.pEntryInfo.Controls.Add(this.lURL);
			this.pEntryInfo.Controls.Add(this.lEntry);
			this.pEntryInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pEntryInfo.Location = new System.Drawing.Point(0, 0);
			this.pEntryInfo.Name = "pEntryInfo";
			this.pEntryInfo.Size = new System.Drawing.Size(629, 90);
			this.pEntryInfo.TabIndex = 100;
			// 
			// tbURL2
			// 
			this.tbURL2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbURL2.Location = new System.Drawing.Point(108, 64);
			this.tbURL2.Margin = new System.Windows.Forms.Padding(0);
			this.tbURL2.Name = "tbURL2";
			this.tbURL2.Size = new System.Drawing.Size(519, 19);
			this.tbURL2.TabIndex = 103;
			this.tbURL2.WordWrap = false;
			this.tbURL2.TextChanged += new System.EventHandler(this.tbURL2_TextChanged);
			// 
			// ctxOpenWith
			// 
			this.ctxOpenWith.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ctxOpenWith.Name = "ctxOpenWith";
			this.ctxOpenWith.Size = new System.Drawing.Size(61, 4);
			// 
			// lURL2
			// 
			this.lURL2.AutoSize = true;
			this.lURL2.ContextMenuStrip = this.ctxOpenWith;
			this.lURL2.Location = new System.Drawing.Point(8, 64);
			this.lURL2.Name = "lURL2";
			this.lURL2.Size = new System.Drawing.Size(42, 20);
			this.lURL2.TabIndex = 102;
			this.lURL2.TabStop = true;
			this.lURL2.Text = "URL";
			this.lURL2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EntryURLClicked);
			// 
			// lURL
			// 
			this.lURL.AutoSize = true;
			this.lURL.ContextMenuStrip = this.ctxOpenWith;
			this.lURL.Location = new System.Drawing.Point(8, 37);
			this.lURL.Name = "lURL";
			this.lURL.Size = new System.Drawing.Size(42, 20);
			this.lURL.TabIndex = 101;
			this.lURL.TabStop = true;
			this.lURL.Text = "URL";
			this.lURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EntryURLClicked);
			// 
			// lEntry
			// 
			this.lEntry.AutoSize = true;
			this.lEntry.Location = new System.Drawing.Point(8, 10);
			this.lEntry.Name = "lEntry";
			this.lEntry.Size = new System.Drawing.Size(50, 20);
			this.lEntry.TabIndex = 199;
			this.lEntry.Text = "Entry:";
			// 
			// pButtons
			// 
			this.pButtons.Controls.Add(this.bCancel);
			this.pButtons.Controls.Add(this.bChangePassword);
			this.pButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pButtons.Location = new System.Drawing.Point(0, 495);
			this.pButtons.Name = "pButtons";
			this.pButtons.Size = new System.Drawing.Size(629, 60);
			this.pButtons.TabIndex = 900;
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(514, 10);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(100, 35);
			this.bCancel.TabIndex = 999;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			// 
			// bChangePassword
			// 
			this.bChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bChangePassword.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bChangePassword.Location = new System.Drawing.Point(328, 10);
			this.bChangePassword.Name = "bChangePassword";
			this.bChangePassword.Size = new System.Drawing.Size(180, 35);
			this.bChangePassword.TabIndex = 998;
			this.bChangePassword.Text = "Change password";
			this.bChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.bChangePassword.UseVisualStyleBackColor = true;
			this.bChangePassword.Click += new System.EventHandler(this.bChangePassword_Click);
			// 
			// lOldPassword
			// 
			this.lOldPassword.AutoSize = true;
			this.lOldPassword.Location = new System.Drawing.Point(8, 29);
			this.lOldPassword.Name = "lOldPassword";
			this.lOldPassword.Size = new System.Drawing.Size(110, 20);
			this.lOldPassword.TabIndex = 4;
			this.lOldPassword.Text = "Old Password:";
			// 
			// lNewPassword
			// 
			this.lNewPassword.AutoSize = true;
			this.lNewPassword.Location = new System.Drawing.Point(8, 111);
			this.lNewPassword.Name = "lNewPassword";
			this.lNewPassword.Size = new System.Drawing.Size(117, 20);
			this.lNewPassword.TabIndex = 5;
			this.lNewPassword.Text = "New Password:";
			// 
			// lNewPasswordRepeat
			// 
			this.m_lblPasswordRepeat.AutoSize = true;
			this.m_lblPasswordRepeat.Location = new System.Drawing.Point(8, 145);
			this.m_lblPasswordRepeat.Name = "m_lblPasswordRepeat";
			this.m_lblPasswordRepeat.Size = new System.Drawing.Size(66, 20);
			this.m_lblPasswordRepeat.TabIndex = 6;
			this.m_lblPasswordRepeat.Text = "Repeat:";
			// 
			// cbToggleOldPassword
			// 
			this.cbToggleOldPassword.Appearance = System.Windows.Forms.Appearance.Button;
			this.cbToggleOldPassword.Checked = true;
			this.cbToggleOldPassword.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbToggleOldPassword.Location = new System.Drawing.Point(533, 24);
			this.cbToggleOldPassword.Name = "cbToggleOldPassword";
			this.cbToggleOldPassword.Size = new System.Drawing.Size(40, 35);
			this.cbToggleOldPassword.TabIndex = 202;
			this.cbToggleOldPassword.Text = "***";
			this.cbToggleOldPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cbToggleOldPassword.UseVisualStyleBackColor = true;
			this.cbToggleOldPassword.CheckedChanged += new System.EventHandler(this.toggleOldPassword);
			// 
			// cbToggleNewPassword
			// 
			this.cbToggleNewPassword.Appearance = System.Windows.Forms.Appearance.Button;
			this.cbToggleNewPassword.Location = new System.Drawing.Point(533, 107);
			this.cbToggleNewPassword.Name = "cbToggleNewPassword";
			this.cbToggleNewPassword.Size = new System.Drawing.Size(40, 35);
			this.cbToggleNewPassword.TabIndex = 207;
			this.cbToggleNewPassword.Text = "***";
			this.cbToggleNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cbToggleNewPassword.UseVisualStyleBackColor = true;
			// 
			// lNewPasswordQualityInfo
			// 
			this.lNewPasswordQualityInfo.Location = new System.Drawing.Point(529, 171);
			this.lNewPasswordQualityInfo.Name = "lNewPasswordQualityInfo";
			this.lNewPasswordQualityInfo.Size = new System.Drawing.Size(100, 23);
			this.lNewPasswordQualityInfo.TabIndex = 11;
			this.lNewPasswordQualityInfo.Text = "0 ch.";
			// 
			// bOldPasswordType
			// 
			this.bOldPasswordType.AutoSize = true;
			this.bOldPasswordType.Location = new System.Drawing.Point(285, 55);
			this.bOldPasswordType.Name = "bOldPasswordType";
			this.bOldPasswordType.Size = new System.Drawing.Size(100, 35);
			this.bOldPasswordType.TabIndex = 204;
			this.bOldPasswordType.Text = "Type";
			this.bOldPasswordType.UseVisualStyleBackColor = true;
			this.bOldPasswordType.Click += new System.EventHandler(this.passwordTypeClick);
			// 
			// bNewPasswordType
			// 
			this.bNewPasswordType.AutoSize = true;
			this.bNewPasswordType.Location = new System.Drawing.Point(285, 194);
			this.bNewPasswordType.Name = "bNewPasswordType";
			this.bNewPasswordType.Size = new System.Drawing.Size(100, 35);
			this.bNewPasswordType.TabIndex = 210;
			this.bNewPasswordType.Text = "Type";
			this.bNewPasswordType.UseVisualStyleBackColor = true;
			this.bNewPasswordType.Click += new System.EventHandler(this.passwordTypeClick);
			// 
			// bOldPasswordCopy
			// 
			this.bOldPasswordCopy.AutoSize = true;
			this.bOldPasswordCopy.Location = new System.Drawing.Point(156, 55);
			this.bOldPasswordCopy.Name = "bOldPasswordCopy";
			this.bOldPasswordCopy.Size = new System.Drawing.Size(100, 35);
			this.bOldPasswordCopy.TabIndex = 203;
			this.bOldPasswordCopy.Text = "Copy";
			this.bOldPasswordCopy.UseVisualStyleBackColor = true;
			this.bOldPasswordCopy.Click += new System.EventHandler(this.passwordCopyClick);
			// 
			// bNewPasswordCopy
			// 
			this.bNewPasswordCopy.AutoSize = true;
			this.bNewPasswordCopy.Location = new System.Drawing.Point(156, 194);
			this.bNewPasswordCopy.Name = "bNewPasswordCopy";
			this.bNewPasswordCopy.Size = new System.Drawing.Size(100, 35);
			this.bNewPasswordCopy.TabIndex = 209;
			this.bNewPasswordCopy.Text = "Copy";
			this.bNewPasswordCopy.UseVisualStyleBackColor = true;
			this.bNewPasswordCopy.Click += new System.EventHandler(this.passwordCopyClick);
			// 
			// GeneratePW
			// 
			this.GeneratePW.ContextMenuStrip = this.ctxPWGen;
			this.GeneratePW.Location = new System.Drawing.Point(579, 107);
			this.GeneratePW.Name = "GeneratePW";
			this.GeneratePW.Size = new System.Drawing.Size(35, 35);
			this.GeneratePW.TabIndex = 208;
			this.GeneratePW.UseVisualStyleBackColor = true;
			this.GeneratePW.Click += new System.EventHandler(this.GeneratePWClick);
			// 
			// ctxPWGen
			// 
			this.ctxPWGen.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.ctxPWGen.Name = "ctxPWGen";
			this.ctxPWGen.Size = new System.Drawing.Size(61, 4);
			this.ctxPWGen.Opening += new System.ComponentModel.CancelEventHandler(this.ctxPWGen_Opening);
			// 
			// lNewPasswordQuality
			// 
			this.m_lblQuality.AutoSize = true;
			this.m_lblQuality.Location = new System.Drawing.Point(8, 174);
			this.m_lblQuality.Name = "m_lblQuality";
			this.m_lblQuality.Size = new System.Drawing.Size(61, 20);
			this.m_lblQuality.TabIndex = 15;
			this.m_lblQuality.Text = "Quality:";
			// 
			// gPasswords
			// 
			this.gPasswords.Controls.Add(this.bExpiry);
			this.gPasswords.Controls.Add(this.dtExpire);
			this.gPasswords.Controls.Add(this.m_cbExpires);
			this.gPasswords.Controls.Add(this.m_lblQuality);
			this.gPasswords.Controls.Add(this.GeneratePW);
			this.gPasswords.Controls.Add(this.bNewPasswordCopy);
			this.gPasswords.Controls.Add(this.bOldPasswordCopy);
			this.gPasswords.Controls.Add(this.bNewPasswordType);
			this.gPasswords.Controls.Add(this.bOldPasswordType);
			this.gPasswords.Controls.Add(this.lNewPasswordQualityInfo);
			this.gPasswords.Controls.Add(this.cbToggleNewPassword);
			this.gPasswords.Controls.Add(this.cbToggleOldPassword);
			this.gPasswords.Controls.Add(this.pbNewPasswordQuality);
			this.gPasswords.Controls.Add(this.tbPasswordNewRepeat);
			this.gPasswords.Controls.Add(this.m_lblPasswordRepeat);
			this.gPasswords.Controls.Add(this.lNewPassword);
			this.gPasswords.Controls.Add(this.lOldPassword);
			this.gPasswords.Controls.Add(this.tbPasswordNew);
			this.gPasswords.Controls.Add(this.tbPasswordOld);
			this.gPasswords.Dock = System.Windows.Forms.DockStyle.Top;
			this.gPasswords.Location = new System.Drawing.Point(0, 90);
			this.gPasswords.Name = "gPasswords";
			this.gPasswords.Padding = new System.Windows.Forms.Padding(10, 6, 10, 3);
			this.gPasswords.Size = new System.Drawing.Size(629, 275);
			this.gPasswords.TabIndex = 200;
			this.gPasswords.TabStop = false;
			// 
			// bExpiry
			// 
			this.bExpiry.ContextMenuStrip = this.ctxPWGen;
			this.bExpiry.Location = new System.Drawing.Point(533, 235);
			this.bExpiry.Name = "bExpiry";
			this.bExpiry.Size = new System.Drawing.Size(35, 35);
			this.bExpiry.TabIndex = 213;
			this.bExpiry.UseVisualStyleBackColor = true;
			this.bExpiry.Click += new System.EventHandler(this.OnbExpiryClick);
			// 
			// dtExpire
			// 
			this.dtExpire.Location = new System.Drawing.Point(156, 236);
			this.dtExpire.Name = "dtExpire";
			this.dtExpire.Size = new System.Drawing.Size(371, 26);
			this.dtExpire.TabIndex = 212;
			// 
			// cbExpires
			// 
			this.m_cbExpires.AutoSize = true;
			this.m_cbExpires.Location = new System.Drawing.Point(8, 236);
			this.m_cbExpires.Name = "m_cbExpires";
			this.m_cbExpires.Size = new System.Drawing.Size(91, 24);
			this.m_cbExpires.TabIndex = 211;
			this.m_cbExpires.Text = "Expires in:";
			// 
			// pbNewPasswordQuality
			// 
			this.pbNewPasswordQuality.Location = new System.Drawing.Point(156, 174);
			this.pbNewPasswordQuality.Name = "pbNewPasswordQuality";
			this.pbNewPasswordQuality.Size = new System.Drawing.Size(371, 16);
			this.pbNewPasswordQuality.TabIndex = 8;
			this.pbNewPasswordQuality.TabStop = false;
			// 
			// tbPasswordNewRepeat
			// 
			this.tbPasswordNewRepeat.Location = new System.Drawing.Point(156, 142);
			this.tbPasswordNewRepeat.Name = "tbPasswordNewRepeat";
			this.tbPasswordNewRepeat.Size = new System.Drawing.Size(371, 26);
			this.tbPasswordNewRepeat.TabIndex = 206;
			// 
			// tbPasswordNew
			// 
			this.tbPasswordNew.Location = new System.Drawing.Point(156, 108);
			this.tbPasswordNew.Name = "tbPasswordNew";
			this.tbPasswordNew.Size = new System.Drawing.Size(371, 26);
			this.tbPasswordNew.TabIndex = 205;
			// 
			// tbPasswordOld
			// 
			this.tbPasswordOld.Location = new System.Drawing.Point(156, 25);
			this.tbPasswordOld.Name = "tbPasswordOld";
			this.tbPasswordOld.ReadOnly = true;
			this.tbPasswordOld.Size = new System.Drawing.Size(371, 26);
			this.tbPasswordOld.TabIndex = 201;
			// 
			// gSequence
			// 
			this.gSequence.Controls.Add(this.bSequenceEdit);
			this.gSequence.Controls.Add(this.bSequence);
			this.gSequence.Controls.Add(this.rtbSequence);
			this.gSequence.Controls.Add(this.cbSequences);
			this.gSequence.Dock = System.Windows.Forms.DockStyle.Top;
			this.gSequence.Location = new System.Drawing.Point(0, 365);
			this.gSequence.Name = "gSequence";
			this.gSequence.Padding = new System.Windows.Forms.Padding(10, 6, 10, 3);
			this.gSequence.Size = new System.Drawing.Size(629, 130);
			this.gSequence.TabIndex = 300;
			this.gSequence.TabStop = false;
			this.gSequence.Text = "gSequence";
			// 
			// bSequenceEdit
			// 
			this.bSequenceEdit.AutoSize = true;
			this.bSequenceEdit.Location = new System.Drawing.Point(141, 83);
			this.bSequenceEdit.Name = "bSequenceEdit";
			this.bSequenceEdit.Size = new System.Drawing.Size(100, 35);
			this.bSequenceEdit.TabIndex = 903;
			this.bSequenceEdit.Text = "Edit";
			this.bSequenceEdit.UseVisualStyleBackColor = true;
			this.bSequenceEdit.Click += new System.EventHandler(this.bSequenceEdit_Click);
			// 
			// bSequence
			// 
			this.bSequence.AutoSize = true;
			this.bSequence.Location = new System.Drawing.Point(270, 83);
			this.bSequence.Name = "bSequence";
			this.bSequence.Size = new System.Drawing.Size(100, 35);
			this.bSequence.TabIndex = 904;
			this.bSequence.Text = "Type";
			this.bSequence.UseVisualStyleBackColor = true;
			this.bSequence.Click += new System.EventHandler(this.bSequence_Click);
			// 
			// rtbSequence
			// 
			this.rtbSequence.Dock = System.Windows.Forms.DockStyle.Top;
			this.rtbSequence.Location = new System.Drawing.Point(10, 53);
			this.rtbSequence.Margin = new System.Windows.Forms.Padding(12, 12, 3, 3);
			this.rtbSequence.Multiline = false;
			this.rtbSequence.Name = "rtbSequence";
			this.rtbSequence.Size = new System.Drawing.Size(609, 26);
			this.rtbSequence.TabIndex = 902;
			this.rtbSequence.Text = "";
			this.rtbSequence.TextChanged += new System.EventHandler(this.rtbSequence_TextChanged);
			// 
			// cbSequences
			// 
			this.cbSequences.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSequences.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSequences.FormattingEnabled = true;
			this.cbSequences.ItemHeight = 20;
			this.cbSequences.Location = new System.Drawing.Point(10, 25);
			this.cbSequences.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
			this.cbSequences.Name = "cbSequences";
			this.cbSequences.Size = new System.Drawing.Size(609, 28);
			this.cbSequences.TabIndex = 901;
			this.cbSequences.SelectedIndexChanged += new System.EventHandler(this.cbSequences_SelectedIndexChanged);
			// 
			// m_ctxDefaultTimes
			// 
			this.m_ctxDefaultTimes.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.m_ctxDefaultTimes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuExpireNow,
            this.m_menuExpireSep0,
            this.m_menuExpire1Week,
            this.m_menuExpire2Weeks,
            this.m_menuExpireSep1,
            this.m_menuExpire1Month,
            this.m_menuExpire3Months,
            this.m_menuExpire6Months,
            this.m_menuExpireSep2,
            this.m_menuExpire1Year});
			this.m_ctxDefaultTimes.Name = "m_ctxDefaultTimes";
			this.m_ctxDefaultTimes.Size = new System.Drawing.Size(161, 232);
			// 
			// m_menuExpireNow
			// 
			this.m_menuExpireNow.Name = "m_menuExpireNow";
			this.m_menuExpireNow.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpireNow.Tag = "0Y0M0D";
			this.m_menuExpireNow.Text = "&Now";
			this.m_menuExpireNow.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpireSep0
			// 
			this.m_menuExpireSep0.Name = "m_menuExpireSep0";
			this.m_menuExpireSep0.Size = new System.Drawing.Size(157, 6);
			// 
			// m_menuExpire1Week
			// 
			this.m_menuExpire1Week.Name = "m_menuExpire1Week";
			this.m_menuExpire1Week.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire1Week.Tag = "0Y0M7D";
			this.m_menuExpire1Week.Text = "&1 Week";
			this.m_menuExpire1Week.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpire2Weeks
			// 
			this.m_menuExpire2Weeks.Name = "m_menuExpire2Weeks";
			this.m_menuExpire2Weeks.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire2Weeks.Tag = "0Y0M14D";
			this.m_menuExpire2Weeks.Text = "&2 Weeks";
			this.m_menuExpire2Weeks.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpireSep1
			// 
			this.m_menuExpireSep1.Name = "m_menuExpireSep1";
			this.m_menuExpireSep1.Size = new System.Drawing.Size(157, 6);
			// 
			// m_menuExpire1Month
			// 
			this.m_menuExpire1Month.Name = "m_menuExpire1Month";
			this.m_menuExpire1Month.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire1Month.Tag = "0Y1M0D";
			this.m_menuExpire1Month.Text = "1 &Month";
			this.m_menuExpire1Month.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpire3Months
			// 
			this.m_menuExpire3Months.Name = "m_menuExpire3Months";
			this.m_menuExpire3Months.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire3Months.Tag = "0Y3M0D";
			this.m_menuExpire3Months.Text = "&3 Months";
			this.m_menuExpire3Months.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpire6Months
			// 
			this.m_menuExpire6Months.Name = "m_menuExpire6Months";
			this.m_menuExpire6Months.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire6Months.Tag = "0Y6M0D";
			this.m_menuExpire6Months.Text = "&6 Months";
			this.m_menuExpire6Months.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// m_menuExpireSep2
			// 
			this.m_menuExpireSep2.Name = "m_menuExpireSep2";
			this.m_menuExpireSep2.Size = new System.Drawing.Size(157, 6);
			// 
			// m_menuExpire1Year
			// 
			this.m_menuExpire1Year.Name = "m_menuExpire1Year";
			this.m_menuExpire1Year.Size = new System.Drawing.Size(160, 30);
			this.m_menuExpire1Year.Tag = "1Y0M0D";
			this.m_menuExpire1Year.Text = "1 &Year";
			this.m_menuExpire1Year.Click += new System.EventHandler(this.OnMenuExpire);
			// 
			// PCADialog
			// 
			this.AcceptButton = this.bChangePassword;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(629, 558);
			this.Controls.Add(this.pButtons);
			this.Controls.Add(this.gSequence);
			this.Controls.Add(this.gPasswords);
			this.Controls.Add(this.pEntryInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PCADialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = " ";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PCADialog_FormClosed);
			this.Load += new System.EventHandler(this.PCADialog_Load);
			this.Shown += new System.EventHandler(this.PCADialog_Shown);
			this.pEntryInfo.ResumeLayout(false);
			this.pEntryInfo.PerformLayout();
			this.pButtons.ResumeLayout(false);
			this.gPasswords.ResumeLayout(false);
			this.gPasswords.PerformLayout();
			this.gSequence.ResumeLayout(false);
			this.gSequence.PerformLayout();
			this.m_ctxDefaultTimes.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.LinkLabel lURL;
		private System.Windows.Forms.ContextMenuStrip ctxOpenWith;
		private System.Windows.Forms.Label m_lblQuality;
		private System.Windows.Forms.GroupBox gSequence;
		private System.Windows.Forms.GroupBox gPasswords;
		private System.Windows.Forms.Button bSequenceEdit;
		private System.Windows.Forms.Button bSequence;
		private CustomRichTextBoxEx rtbSequence;
		private System.Windows.Forms.ComboBox cbSequences;
		private System.Windows.Forms.Panel pEntryInfo;
		private System.Windows.Forms.Panel pButtons;
		private System.Windows.Forms.CheckBox m_cbExpires;
		private System.Windows.Forms.DateTimePicker dtExpire;
		private System.Windows.Forms.Button bExpiry;
		private KeePass.UI.CustomContextMenuStripEx m_ctxDefaultTimes;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpireNow;
		private System.Windows.Forms.ToolStripSeparator m_menuExpireSep0;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire1Week;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire2Weeks;
		private System.Windows.Forms.ToolStripSeparator m_menuExpireSep1;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire1Month;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire3Months;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire6Months;
		private System.Windows.Forms.ToolStripSeparator m_menuExpireSep2;
		private System.Windows.Forms.ToolStripMenuItem m_menuExpire1Year;
		private System.Windows.Forms.LinkLabel lURL2;
		//private System.Windows.Forms.TextBox tbURL2;
		private PromptedTextBox tbURL2;
	}
}
