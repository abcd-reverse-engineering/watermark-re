using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace WaterMark.Properties
{
	// Token: 0x02000013 RID: 19
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000911C File Offset: 0x0000731C
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000051 RID: 81
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
