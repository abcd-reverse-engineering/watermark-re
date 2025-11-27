// Decompiled with JetBrains decompiler
// Type: WaterMark.ComplexMath
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System;

#nullable disable
namespace WaterMark;

public class ComplexMath
{
  private static double _halfOfRoot2 = 0.5 * Math.Sqrt(2.0);

  private ComplexMath()
  {
  }

  public static void Swap(ref Complex a, ref Complex b)
  {
    Complex complex = a;
    a = b;
    b = complex;
  }

  public static void Swap(ref ComplexF a, ref ComplexF b)
  {
    ComplexF complexF = a;
    a = b;
    b = complexF;
  }

  public static ComplexF Sqrt(ComplexF c)
  {
    double re = (double) c.Re;
    double im = (double) c.Im;
    double num1 = Math.Sqrt(re * re + im * im);
    int num2 = im < 0.0 ? -1 : 1;
    c.Re = (float) (ComplexMath._halfOfRoot2 * Math.Sqrt(num1 + re));
    c.Im = (float) (ComplexMath._halfOfRoot2 * (double) num2 * Math.Sqrt(num1 - re));
    return c;
  }

  public static Complex Sqrt(Complex c)
  {
    double re = c.Re;
    double im = c.Im;
    double num1 = Math.Sqrt(re * re + im * im);
    int num2 = im < 0.0 ? -1 : 1;
    c.Re = ComplexMath._halfOfRoot2 * Math.Sqrt(num1 + re);
    c.Im = ComplexMath._halfOfRoot2 * (double) num2 * Math.Sqrt(num1 - re);
    return c;
  }

  public static ComplexF Pow(ComplexF c, double exponent)
  {
    double re = (double) c.Re;
    double im = (double) c.Im;
    double num1 = Math.Pow(re * re + im * im, exponent * 0.5);
    double num2 = Math.Atan2(im, re) * exponent;
    c.Re = (float) (num1 * Math.Cos(num2));
    c.Im = (float) (num1 * Math.Sin(num2));
    return c;
  }

  public static Complex Pow(Complex c, double exponent)
  {
    double re = c.Re;
    double im = c.Im;
    double num1 = Math.Pow(re * re + im * im, exponent * 0.5);
    double num2 = Math.Atan2(im, re) * exponent;
    c.Re = num1 * Math.Cos(num2);
    c.Im = num1 * Math.Sin(num2);
    return c;
  }
}
