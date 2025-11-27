// Decompiled with JetBrains decompiler
// Type: WaterMark.ComplexArray
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System;

#nullable disable
namespace WaterMark;

public class ComplexArray
{
  private static bool _workspaceFLocked = false;
  private static ComplexF[] _workspaceF = new ComplexF[0];

  private ComplexArray()
  {
  }

  public static void ClampLength(Complex[] array, double fMinimum, double fMaximum)
  {
    for (int index = 0; index < array.Length; ++index)
      array[index] = Complex.FromModulusArgument(Math.Max(fMinimum, Math.Min(fMaximum, array[index].GetModulus())), array[index].GetArgument());
  }

  public static void Clamp(Complex[] array, Complex minimum, Complex maximum)
  {
    for (int index = 0; index < array.Length; ++index)
    {
      array[index].Re = Math.Min(Math.Max(array[index].Re, minimum.Re), maximum.Re);
      array[index].Im = Math.Min(Math.Max(array[index].Re, minimum.Im), maximum.Im);
    }
  }

  public static void ClampToRealUnit(Complex[] array)
  {
    for (int index = 0; index < array.Length; ++index)
    {
      array[index].Re = Math.Min(Math.Max(array[index].Re, 0.0), 1.0);
      array[index].Im = 0.0;
    }
  }

  private static void LockWorkspaceF(int length, ref ComplexF[] workspace)
  {
    ComplexArray._workspaceFLocked = true;
    if (length >= ComplexArray._workspaceF.Length)
      ComplexArray._workspaceF = new ComplexF[length];
    workspace = ComplexArray._workspaceF;
  }

  private static void UnlockWorkspaceF(ref ComplexF[] workspace)
  {
    ComplexArray._workspaceFLocked = false;
    workspace = (ComplexF[]) null;
  }

  public static void Shift(Complex[] array, int offset)
  {
    if (offset == 0)
      return;
    int length = array.Length;
    Complex[] complexArray = new Complex[length];
    for (int index = 0; index < length; ++index)
      complexArray[(index + offset) % length] = array[index];
    for (int index = 0; index < length; ++index)
      array[index] = complexArray[index];
  }

  public static void Shift(ComplexF[] array, int offset)
  {
    if (offset == 0)
      return;
    int length = array.Length;
    ComplexF[] workspace = (ComplexF[]) null;
    ComplexArray.LockWorkspaceF(length, ref workspace);
    for (int index = 0; index < length; ++index)
      workspace[(index + offset) % length] = array[index];
    for (int index = 0; index < length; ++index)
      array[index] = workspace[index];
    ComplexArray.UnlockWorkspaceF(ref workspace);
  }

  public static void GetLengthRange(Complex[] array, ref double minimum, ref double maximum)
  {
    minimum = double.MaxValue;
    maximum = double.MinValue;
    for (int index = 0; index < array.Length; ++index)
    {
      double modulus = array[index].GetModulus();
      minimum = Math.Min(modulus, minimum);
      maximum = Math.Max(modulus, maximum);
    }
  }

  public static void GetLengthRange(ComplexF[] array, ref float minimum, ref float maximum)
  {
    minimum = float.MaxValue;
    maximum = float.MinValue;
    for (int index = 0; index < array.Length; ++index)
    {
      float modulus = array[index].GetModulus();
      minimum = Math.Min(modulus, minimum);
      maximum = Math.Max(modulus, maximum);
    }
  }

  public static bool IsEqual(Complex[] array1, Complex[] array2, double tolerance)
  {
    if (array1.Length != array2.Length)
      return false;
    for (int index = 0; index < array1.Length; ++index)
    {
      if (!Complex.IsEqual(array1[index], array2[index], tolerance))
        return false;
    }
    return true;
  }

  public static bool IsEqual(ComplexF[] array1, ComplexF[] array2, float tolerance)
  {
    if (array1.Length != array2.Length)
      return false;
    for (int index = 0; index < array1.Length; ++index)
    {
      if (!ComplexF.IsEqual(array1[index], array2[index], tolerance))
        return false;
    }
    return true;
  }

  public static void Offset(Complex[] array, double offset)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index].Re += offset;
  }

  public static void Offset(Complex[] array, Complex offset)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] += offset;
  }

  public static void Offset(ComplexF[] array, float offset)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index].Re += offset;
  }

  public static void Offset(ComplexF[] array, ComplexF offset)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] += offset;
  }

  public static void Scale(Complex[] array, double scale)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] *= scale;
  }

  public static void Scale(Complex[] array, double scale, int start, int length)
  {
    for (int index = 0; index < length; ++index)
      array[index + start] *= scale;
  }

  public static void Scale(Complex[] array, Complex scale)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] *= scale;
  }

  public static void Scale(Complex[] array, Complex scale, int start, int length)
  {
    for (int index = 0; index < length; ++index)
      array[index + start] *= scale;
  }

  public static void Scale(ComplexF[] array, float scale)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] *= scale;
  }

  public static void Scale(ComplexF[] array, float scale, int start, int length)
  {
    for (int index = 0; index < length; ++index)
      array[index + start] *= scale;
  }

  public static void Scale(ComplexF[] array, ComplexF scale)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
      array[index] *= scale;
  }

  public static void Scale(ComplexF[] array, ComplexF scale, int start, int length)
  {
    for (int index = 0; index < length; ++index)
      array[index + start] *= scale;
  }

  public static void Multiply(Complex[] target, Complex[] rhs)
  {
    ComplexArray.Multiply(target, rhs, target);
  }

  public static void Multiply(Complex[] lhs, Complex[] rhs, Complex[] result)
  {
    int length = lhs.Length;
    for (int index = 0; index < length; ++index)
      result[index] = lhs[index] * rhs[index];
  }

  public static void Multiply(ComplexF[] target, ComplexF[] rhs)
  {
    ComplexArray.Multiply(target, rhs, target);
  }

  public static void Multiply(ComplexF[] lhs, ComplexF[] rhs, ComplexF[] result)
  {
    int length = lhs.Length;
    for (int index = 0; index < length; ++index)
      result[index] = lhs[index] * rhs[index];
  }

  public static void Divide(Complex[] target, Complex[] rhs)
  {
    ComplexArray.Divide(target, rhs, target);
  }

  public static void Divide(Complex[] lhs, Complex[] rhs, Complex[] result)
  {
    int length = lhs.Length;
    for (int index = 0; index < length; ++index)
      result[index] = lhs[index] / rhs[index];
  }

  public static void Divide(ComplexF[] target, ComplexF[] rhs)
  {
    ComplexArray.Divide(target, rhs, target);
  }

  public static void Divide(ComplexF[] lhs, ComplexF[] rhs, ComplexF[] result)
  {
    ComplexF zero = ComplexF.Zero;
    int length = lhs.Length;
    for (int index = 0; index < length; ++index)
      result[index] = !(rhs[index] != zero) ? zero : lhs[index] / rhs[index];
  }

  public static void Copy(Complex[] dest, Complex[] source)
  {
    for (int index = 0; index < dest.Length; ++index)
      dest[index] = source[index];
  }

  public static void Copy(ComplexF[] dest, ComplexF[] source)
  {
    for (int index = 0; index < dest.Length; ++index)
      dest[index] = source[index];
  }

  public static void Reverse(Complex[] array)
  {
    int length = array.Length;
    for (int index = 0; index < length / 2; ++index)
    {
      Complex complex = array[index];
      array[index] = array[length - 1 - index];
      array[length - 1 - index] = complex;
    }
  }

  public static void Normalize(Complex[] array)
  {
    double minimum = 0.0;
    double maximum = 0.0;
    ComplexArray.GetLengthRange(array, ref minimum, ref maximum);
    ComplexArray.Scale(array, 1.0 / (maximum - minimum));
    ComplexArray.Offset(array, -minimum / (maximum - minimum));
  }

  public static void Normalize(ComplexF[] array)
  {
    float minimum = 0.0f;
    float maximum = 0.0f;
    ComplexArray.GetLengthRange(array, ref minimum, ref maximum);
    ComplexArray.Scale(array, (float) (1.0 / ((double) maximum - (double) minimum)));
    ComplexArray.Offset(array, (float) (-(double) minimum / ((double) maximum - (double) minimum)));
  }

  public static void Invert(Complex[] array)
  {
    for (int index = 0; index < array.Length; ++index)
      array[index] = (Complex) 1.0 / array[index];
  }

  public static void Invert(ComplexF[] array)
  {
    for (int index = 0; index < array.Length; ++index)
      array[index] = (ComplexF) 1f / array[index];
  }
}
