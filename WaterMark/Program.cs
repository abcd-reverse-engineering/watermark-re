using System;
using System.Windows.Forms;

namespace WaterMark
{
	// Token: 0x02000011 RID: 17
	internal static class Program
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000090B4 File Offset: 0x000072B4
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WaterMark());
		}
	}
}
