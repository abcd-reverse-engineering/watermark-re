// Decompiled with JetBrains decompiler
// Type: WaterMark.Properties.Resources
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace WaterMark.Properties;

[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (WaterMark.Properties.Resources.resourceMan == null)
        WaterMark.Properties.Resources.resourceMan = new ResourceManager("WaterMark.Properties.Resources", typeof (WaterMark.Properties.Resources).Assembly);
      return WaterMark.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => WaterMark.Properties.Resources.resourceCulture;
    set => WaterMark.Properties.Resources.resourceCulture = value;
  }
}
