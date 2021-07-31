﻿using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;
using PluginTranslation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PasswordChangeAssistant
{
	public class PCAInitData
	{
		private PwEntry m_pe = null;
		public ProtectedStringDictionary Strings;
		private string m_Title = string.Empty;
		public string Title
		{
			get { return m_pe != null ? m_pe.Strings.ReadSafe(PwDefs.TitleField) : m_Title; }
		}
		private string m_User = string.Empty;
		public string User
		{
			get { return m_pe != null ? m_pe.Strings.ReadSafe(PwDefs.UserNameField) : m_User; }
		}
		public ProtectedString OldPassword;
		public string MainURL;
		public string PCAURL;
		public DateTime Expiry;
		public bool Expires;
		public bool SetExpiry;
		public string PCASequence;
		public PCAInitData(PwEntry pe)
		{
			if (pe == null) return;
			m_pe = pe;
			m_Title = pe.Strings.ReadSafe(PwDefs.TitleField);
			m_User = pe.Strings.ReadSafe(PwDefs.UserNameField);
			Expires = pe.Expires;
			Expiry = pe.ExpiryTime;
			OldPassword = pe.Strings.GetSafe(PwDefs.PasswordField);
			PCASequence = PasswordChangeAssistantExt.GetPCASequence(pe, Config.DefaultPCASequences[PluginTranslate.DefaultSequence01]);
			SetExpiry = false;
			MainURL = pe.Strings.ReadSafe(PwDefs.UrlField);
			PCAURL = pe.Strings.ReadSafe(Config.PCAURLField);
			Strings = pe.Strings;
		}
	}

	public static class Config
	{
		public const string PCAURLField = "PCAURL";
		public const string PCAPluginField = "PCAPluginField";
		public const string PCAPluginFieldRef = "{S:" + PCAPluginField + "}";
		public const string PCASequence = "PCASequence";
		public const string PlaceholderOldPW = "{PCA_OldPW}";
		public const string PlaceholderNewPW = "{PCA_NewPW}";
		public static Dictionary<string, string> DefaultPCASequences = new Dictionary<string, string>();
		public const string ProfileDBOnly = " (DB)";
		public const string ProfileCopied = "(*)";
		public const string ProfileLastUsedProfile = "PPS.Profile";
		public const string ProfileAutoGenerated = "PPS.Auto";
		public const string ProfileConfig = "PPS.";

		internal static Image ScaleImage(Image img)
		{
			return ScaleImage(img, 16, 16);
		}

		internal static Image ScaleImage(Image img, int w, int h)
		{
			return KeePassLib.Utility.GfxUtil.ScaleImage(img, (int)(w * KeePass.UI.DpiUtil.FactorX), (int)(h * KeePass.UI.DpiUtil.FactorY));
		}
	}
}
