using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WaterMark.Properties
{
	// Token: 0x02000012 RID: 18
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000090CB File Offset: 0x000072CB
		internal Resources()
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000090D4 File Offset: 0x000072D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("WaterMark.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000910D File Offset: 0x0000730D
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00009114 File Offset: 0x00007314
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400004F RID: 79
		private static ResourceManager resourceMan;

		// Token: 0x04000050 RID: 80
		private static CultureInfo resourceCulture;
	}
}
