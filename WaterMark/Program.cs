// Decompiled with JetBrains decompiler
// Type: WaterMark.Program
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System;
using System.Windows.Forms;

#nullable disable
namespace WaterMark;

internal static class Program
{
  [STAThread]
  private static void Main()
  {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run((Form) new WaterMark.WaterMark());
  }
}
