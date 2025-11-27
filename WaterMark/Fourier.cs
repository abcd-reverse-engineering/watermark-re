// Decompiled with JetBrains decompiler
// Type: WaterMark.Fourier
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System;

#nullable disable
namespace WaterMark;

public class Fourier
{
  private const int cMaxLength = 4096 /*0x1000*/;
  private const int cMinLength = 1;
  private const int cMaxBits = 12;
  private const int cMinBits = 0;
  private static int[][] _reversedBits = new int[12][];
  private static int[][] _reverseBits = (int[][]) null;
  private static int _lookupTabletLength = -1;
  private static double[,][] _uRLookup = (double[,][]) null;
  private static double[,][] _uILookup = (double[,][]) null;
  private static float[,][] _uRLookupF = (float[,][]) null;
  private static float[,][] _uILookupF = (float[,][]) null;
  private static bool _bufferFLocked = false;
  private static float[] _bufferF = new float[0];
  private static bool _bufferCFLocked = false;
  private static ComplexF[] _bufferCF = new ComplexF[0];
  private static bool _bufferCLocked = false;
  private static Complex[] _bufferC = new Complex[0];

  private Fourier()
  {
  }

  private static void Swap(ref float a, ref float b)
  {
    float num = a;
    a = b;
    b = num;
  }

  private static void Swap(ref double a, ref double b)
  {
    double num = a;
    a = b;
    b = num;
  }

  private static void Swap(ref ComplexF a, ref ComplexF b)
  {
    ComplexF complexF = a;
    a = b;
    b = complexF;
  }

  private static void Swap(ref Complex a, ref Complex b)
  {
    Complex complex = a;
    a = b;
    b = complex;
  }

  private static bool IsPowerOf2(int x) => (x & x - 1) == 0;

  private static int Pow2(int exponent)
  {
    return exponent >= 0 && exponent < 31 /*0x1F*/ ? 1 << exponent : 0;
  }

  private static int Log2(int x)
  {
    if (x <= 65536 /*0x010000*/)
    {
      if (x <= 256 /*0x0100*/)
      {
        if (x <= 16 /*0x10*/)
        {
          if (x <= 4)
          {
            if (x > 2)
              return 2;
            return x <= 1 ? 0 : 1;
          }
          return x <= 8 ? 3 : 4;
        }
        return x <= 64 /*0x40*/ ? (x <= 32 /*0x20*/ ? 5 : 6) : (x <= 128 /*0x80*/ ? 7 : 8);
      }
      return x <= 4096 /*0x1000*/ ? (x <= 1024 /*0x0400*/ ? (x <= 512 /*0x0200*/ ? 9 : 10) : (x <= 2048 /*0x0800*/ ? 11 : 12)) : (x <= 16384 /*0x4000*/ ? (x <= 8192 /*0x2000*/ ? 13 : 14) : (x <= 32768 /*0x8000*/ ? 15 : 16 /*0x10*/));
    }
    if (x <= 16777216 /*0x01000000*/)
      return x <= 1048576 /*0x100000*/ ? (x <= 262144 /*0x040000*/ ? (x <= 131072 /*0x020000*/ ? 17 : 18) : (x <= 524288 /*0x080000*/ ? 19 : 20)) : (x <= 4194304 /*0x400000*/ ? (x <= 2097152 /*0x200000*/ ? 21 : 22) : (x <= 8388608 /*0x800000*/ ? 23 : 24));
    if (x <= 268435456 /*0x10000000*/)
      return x <= 67108864 /*0x04000000*/ ? (x <= 33554432 /*0x02000000*/ ? 25 : 26) : (x <= 134217728 /*0x08000000*/ ? 27 : 28);
    if (x > 1073741824 /*0x40000000*/)
      return 31 /*0x1F*/;
    return x <= 536870912 /*0x20000000*/ ? 29 : 30;
  }

  private static int ReverseBits(int index, int numberOfBits)
  {
    int num = 0;
    for (int index1 = 0; index1 < numberOfBits; ++index1)
    {
      num = num << 1 | index & 1;
      index >>= 1;
    }
    return num;
  }

  private static int[] GetReversedBits(int numberOfBits)
  {
    if (Fourier._reversedBits[numberOfBits - 1] == null)
    {
      int length = Fourier.Pow2(numberOfBits);
      int[] numArray = new int[length];
      for (int index1 = 0; index1 < length; ++index1)
      {
        int num1 = index1;
        int num2 = 0;
        for (int index2 = 0; index2 < numberOfBits; ++index2)
        {
          num2 = num2 << 1 | num1 & 1;
          num1 >>= 1;
        }
        numArray[index1] = num2;
      }
      Fourier._reversedBits[numberOfBits - 1] = numArray;
    }
    return Fourier._reversedBits[numberOfBits - 1];
  }

  private static void ReorderArray(float[] data)
  {
    int x = data.Length / 2;
    int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(x));
    for (int index = 0; index < x; ++index)
    {
      int num = reversedBits[index];
      if (num > index)
      {
        Fourier.Swap(ref data[index << 1], ref data[num << 1]);
        Fourier.Swap(ref data[(index << 1) + 1], ref data[(num << 1) + 1]);
      }
    }
  }

  private static void ReorderArray(double[] data)
  {
    int x = data.Length / 2;
    int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(x));
    for (int index = 0; index < x; ++index)
    {
      int num = reversedBits[index];
      if (num > index)
      {
        Fourier.Swap(ref data[index << 1], ref data[num << 1]);
        Fourier.Swap(ref data[index << 2], ref data[num << 2]);
      }
    }
  }

  private static void ReorderArray(Complex[] data)
  {
    int length = data.Length;
    int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(length));
    for (int index1 = 0; index1 < length; ++index1)
    {
      int index2 = reversedBits[index1];
      if (index2 > index1)
      {
        Complex complex = data[index1];
        data[index1] = data[index2];
        data[index2] = complex;
      }
    }
  }

  private static void ReorderArray(ComplexF[] data)
  {
    int length = data.Length;
    int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(length));
    for (int index1 = 0; index1 < length; ++index1)
    {
      int index2 = reversedBits[index1];
      if (index2 > index1)
      {
        ComplexF complexF = data[index1];
        data[index1] = data[index2];
        data[index2] = complexF;
      }
    }
  }

  private static int _ReverseBits(int bits, int n)
  {
    int num = 0;
    for (int index = 0; index < n; ++index)
    {
      num = num << 1 | bits & 1;
      bits >>= 1;
    }
    return num;
  }

  private static void InitializeReverseBits(int levels)
  {
    Fourier._reverseBits = new int[levels + 1][];
    for (int index = 0; index < levels + 1; ++index)
    {
      int length = (int) Math.Pow(2.0, (double) index);
      Fourier._reverseBits[index] = new int[length];
      for (int bits = 0; bits < length; ++bits)
        Fourier._reverseBits[index][bits] = Fourier._ReverseBits(bits, index);
    }
  }

  private static void SyncLookupTableLength(int length)
  {
    if (length <= Fourier._lookupTabletLength)
      return;
    int levels = (int) Math.Ceiling(Math.Log((double) length, 2.0));
    Fourier.InitializeReverseBits(levels);
    Fourier.InitializeComplexRotations(levels);
    Fourier._lookupTabletLength = length;
  }

  private static int GetLookupTableLength() => Fourier._lookupTabletLength;

  private static void ClearLookupTables()
  {
    Fourier._uRLookup = (double[,][]) null;
    Fourier._uILookup = (double[,][]) null;
    Fourier._uRLookupF = (float[,][]) null;
    Fourier._uILookupF = (float[,][]) null;
    Fourier._lookupTabletLength = -1;
  }

  private static void InitializeComplexRotations(int levels)
  {
    int num1 = levels;
    Fourier._uRLookup = new double[levels + 1, 2][];
    Fourier._uILookup = new double[levels + 1, 2][];
    Fourier._uRLookupF = new float[levels + 1, 2][];
    Fourier._uILookupF = new float[levels + 1, 2][];
    int num2 = 1;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      int length = num2;
      num2 <<= 1;
      double num3 = 1.0;
      double num4 = 0.0;
      double num5 = Math.PI / (double) length * 1.0;
      double num6 = Math.Cos(num5);
      double num7 = Math.Sin(num5);
      Fourier._uRLookup[index1, 0] = new double[length];
      Fourier._uILookup[index1, 0] = new double[length];
      Fourier._uRLookupF[index1, 0] = new float[length];
      Fourier._uILookupF[index1, 0] = new float[length];
      for (int index2 = 0; index2 < length; ++index2)
      {
        Fourier._uRLookupF[index1, 0][index2] = (float) (Fourier._uRLookup[index1, 0][index2] = num3);
        Fourier._uILookupF[index1, 0][index2] = (float) (Fourier._uILookup[index1, 0][index2] = num4);
        double num8 = num3 * num7 + num4 * num6;
        num3 = num3 * num6 - num4 * num7;
        num4 = num8;
      }
      double num9 = 1.0;
      double num10 = 0.0;
      double num11 = Math.PI / (double) length * -1.0;
      double num12 = Math.Cos(num11);
      double num13 = Math.Sin(num11);
      Fourier._uRLookup[index1, 1] = new double[length];
      Fourier._uILookup[index1, 1] = new double[length];
      Fourier._uRLookupF[index1, 1] = new float[length];
      Fourier._uILookupF[index1, 1] = new float[length];
      for (int index3 = 0; index3 < length; ++index3)
      {
        Fourier._uRLookupF[index1, 1][index3] = (float) (Fourier._uRLookup[index1, 1][index3] = num9);
        Fourier._uILookupF[index1, 1][index3] = (float) (Fourier._uILookup[index1, 1][index3] = num10);
        double num14 = num9 * num13 + num10 * num12;
        num9 = num9 * num12 - num10 * num13;
        num10 = num14;
      }
    }
  }

  private static void LockBufferF(int length, ref float[] buffer)
  {
    Fourier._bufferFLocked = true;
    if (length >= Fourier._bufferF.Length)
      Fourier._bufferF = new float[length];
    buffer = Fourier._bufferF;
  }

  private static void UnlockBufferF(ref float[] buffer)
  {
    Fourier._bufferFLocked = false;
    buffer = (float[]) null;
  }

  private static void LinearFFT(
    float[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    float[] buffer = (float[]) null;
    Fourier.LockBufferF(length * 2, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length * 2; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferF(ref buffer);
  }

  private static void LinearFFT_Quick(
    float[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    float[] buffer = (float[]) null;
    Fourier.LockBufferF(length * 2, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length * 2; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT_Quick(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferF(ref buffer);
  }

  private static void LockBufferCF(int length, ref ComplexF[] buffer)
  {
    Fourier._bufferCFLocked = true;
    if (length != Fourier._bufferCF.Length)
      Fourier._bufferCF = new ComplexF[length];
    buffer = Fourier._bufferCF;
  }

  private static void UnlockBufferCF(ref ComplexF[] buffer)
  {
    Fourier._bufferCFLocked = false;
    buffer = (ComplexF[]) null;
  }

  private static void LinearFFT(
    ComplexF[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    ComplexF[] buffer = (ComplexF[]) null;
    Fourier.LockBufferCF(length, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferCF(ref buffer);
  }

  private static void LinearFFT_Quick(
    ComplexF[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    ComplexF[] buffer = (ComplexF[]) null;
    Fourier.LockBufferCF(length, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferCF(ref buffer);
  }

  private static void LockBufferC(int length, ref Complex[] buffer)
  {
    Fourier._bufferCLocked = true;
    if (length >= Fourier._bufferC.Length)
      Fourier._bufferC = new Complex[length];
    buffer = Fourier._bufferC;
  }

  private static void UnlockBufferC(ref Complex[] buffer)
  {
    Fourier._bufferCLocked = false;
    buffer = (Complex[]) null;
  }

  private static void LinearFFT(
    Complex[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    Complex[] buffer = (Complex[]) null;
    Fourier.LockBufferC(length, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferC(ref buffer);
  }

  private static void LinearFFT_Quick(
    Complex[] data,
    int start,
    int inc,
    int length,
    FourierDirection direction)
  {
    Complex[] buffer = (Complex[]) null;
    Fourier.LockBufferC(length, ref buffer);
    int index1 = start;
    for (int index2 = 0; index2 < length; ++index2)
    {
      buffer[index2] = data[index1];
      index1 += inc;
    }
    Fourier.FFT_Quick(buffer, length, direction);
    int index3 = start;
    for (int index4 = 0; index4 < length; ++index4)
    {
      data[index3] = buffer[index4];
      index3 += inc;
    }
    Fourier.UnlockBufferC(ref buffer);
  }

  public static void FFT(float[] data, int length, FourierDirection direction)
  {
    Fourier.SyncLookupTableLength(length);
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      float[] numArray1 = Fourier._uRLookupF[index2, index1];
      float[] numArray2 = Fourier._uILookupF[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        float num4 = numArray1[index3];
        float num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 << 1;
          int index6 = index4 + num3 << 1;
          float num6 = data[index6];
          float num7 = data[index6 + 1];
          float num8 = (float) ((double) num6 * (double) num4 - (double) num7 * (double) num5);
          float num9 = (float) ((double) num6 * (double) num5 + (double) num7 * (double) num4);
          float num10 = data[index5];
          float num11 = data[index5 + 1];
          data[index5] = num10 + num8;
          data[index5 + 1] = num11 + num9;
          data[index6] = num10 - num8;
          data[index6 + 1] = num11 - num9;
        }
      }
    }
  }

  public static void FFT_Quick(float[] data, int length, FourierDirection direction)
  {
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      float[] numArray1 = Fourier._uRLookupF[index2, index1];
      float[] numArray2 = Fourier._uILookupF[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        float num4 = numArray1[index3];
        float num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 << 1;
          int index6 = index4 + num3 << 1;
          float num6 = data[index6];
          float num7 = data[index6 + 1];
          float num8 = (float) ((double) num6 * (double) num4 - (double) num7 * (double) num5);
          float num9 = (float) ((double) num6 * (double) num5 + (double) num7 * (double) num4);
          float num10 = data[index5];
          float num11 = data[index5 + 1];
          data[index5] = num10 + num8;
          data[index5 + 1] = num11 + num9;
          data[index6] = num10 - num8;
          data[index6 + 1] = num11 - num9;
        }
      }
    }
  }

  public static void FFT(ComplexF[] data, int length, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < length)
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be at least as large as 'data.Length' parameter");
    if (!Fourier.IsPowerOf2(length))
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be a power of 2");
    Fourier.SyncLookupTableLength(length);
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      float[] numArray1 = Fourier._uRLookupF[index2, index1];
      float[] numArray2 = Fourier._uILookupF[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        float num4 = numArray1[index3];
        float num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 + num3;
          float re1 = data[index5].Re;
          float im1 = data[index5].Im;
          float num6 = (float) ((double) re1 * (double) num4 - (double) im1 * (double) num5);
          float num7 = (float) ((double) re1 * (double) num5 + (double) im1 * (double) num4);
          float re2 = data[index4].Re;
          float im2 = data[index4].Im;
          data[index4].Re = re2 + num6;
          data[index4].Im = im2 + num7;
          data[index5].Re = re2 - num6;
          data[index5].Im = im2 - num7;
        }
      }
    }
  }

  public static void FFT_Quick(ComplexF[] data, int length, FourierDirection direction)
  {
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      float[] numArray1 = Fourier._uRLookupF[index2, index1];
      float[] numArray2 = Fourier._uILookupF[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        float num4 = numArray1[index3];
        float num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 + num3;
          float re1 = data[index5].Re;
          float im1 = data[index5].Im;
          float num6 = (float) ((double) re1 * (double) num4 - (double) im1 * (double) num5);
          float num7 = (float) ((double) re1 * (double) num5 + (double) im1 * (double) num4);
          float re2 = data[index4].Re;
          float im2 = data[index4].Im;
          data[index4].Re = re2 + num6;
          data[index4].Im = im2 + num7;
          data[index5].Re = re2 - num6;
          data[index5].Im = im2 - num7;
        }
      }
    }
  }

  public static void FFT(ComplexF[] data, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    Fourier.FFT(data, data.Length, direction);
  }

  public static void FFT(Complex[] data, int length, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < length)
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be at least as large as 'data.Length' parameter");
    if (!Fourier.IsPowerOf2(length))
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be a power of 2");
    Fourier.SyncLookupTableLength(length);
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      double[] numArray1 = Fourier._uRLookup[index2, index1];
      double[] numArray2 = Fourier._uILookup[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        double num4 = numArray1[index3];
        double num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 + num3;
          double re1 = data[index5].Re;
          double im1 = data[index5].Im;
          double num6 = re1 * num4 - im1 * num5;
          double num7 = re1 * num5 + im1 * num4;
          double re2 = data[index4].Re;
          double im2 = data[index4].Im;
          data[index4].Re = re2 + num6;
          data[index4].Im = im2 + num7;
          data[index5].Re = re2 - num6;
          data[index5].Im = im2 - num7;
        }
      }
    }
  }

  public static void FFT_Quick(Complex[] data, int length, FourierDirection direction)
  {
    int num1 = Fourier.Log2(length);
    Fourier.ReorderArray(data);
    int num2 = 1;
    int index1 = direction == FourierDirection.Forward ? 0 : 1;
    for (int index2 = 1; index2 <= num1; ++index2)
    {
      int num3 = num2;
      num2 <<= 1;
      double[] numArray1 = Fourier._uRLookup[index2, index1];
      double[] numArray2 = Fourier._uILookup[index2, index1];
      for (int index3 = 0; index3 < num3; ++index3)
      {
        double num4 = numArray1[index3];
        double num5 = numArray2[index3];
        for (int index4 = index3; index4 < length; index4 += num2)
        {
          int index5 = index4 + num3;
          double re1 = data[index5].Re;
          double im1 = data[index5].Im;
          double num6 = re1 * num4 - im1 * num5;
          double num7 = re1 * num5 + im1 * num4;
          double re2 = data[index4].Re;
          double im2 = data[index4].Im;
          data[index4].Re = re2 + num6;
          data[index4].Im = im2 + num7;
          data[index5].Re = re2 - num6;
          data[index5].Im = im2 - num7;
        }
      }
    }
  }

  public static void RFFT(float[] data, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    Fourier.RFFT(data, data.Length, direction);
  }

  public static void RFFT(float[] data, int length, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < length)
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be at least as large as 'data.Length' parameter");
    if (!Fourier.IsPowerOf2(length))
      throw new ArgumentOutOfRangeException(nameof (length), (object) length, "must be a power of 2");
    float num1 = 0.5f;
    float a = 3.14159274f / (float) (length / 2);
    float num2;
    if (direction == FourierDirection.Forward)
    {
      num2 = -0.5f;
      Fourier.FFT(data, length / 2, direction);
    }
    else
    {
      num2 = 0.5f;
      a = -a;
    }
    float num3 = (float) Math.Sin(0.5 * (double) a);
    float num4 = -2f * num3 * num3;
    float num5 = (float) Math.Sin((double) a);
    float num6 = 1f + num4;
    float num7 = num5;
    for (int index1 = 1; index1 < length / 4; ++index1)
    {
      int index2 = 2 * index1;
      int index3 = length - 2 * index1;
      float num8 = num1 * (data[index2] + data[index3]);
      float num9 = num1 * (data[index2 + 1] - data[index3 + 1]);
      float num10 = (float) (-(double) num2 * ((double) data[index2 + 1] + (double) data[index3 + 1]));
      float num11 = num2 * (data[index2] - data[index3]);
      data[index2] = (float) ((double) num8 + (double) num6 * (double) num10 - (double) num7 * (double) num11);
      data[index2 + 1] = (float) ((double) num9 + (double) num6 * (double) num11 + (double) num7 * (double) num10);
      data[index3] = (float) ((double) num8 - (double) num6 * (double) num10 + (double) num7 * (double) num11);
      data[index3 + 1] = (float) (-(double) num9 + (double) num6 * (double) num11 + (double) num7 * (double) num10);
      float num12;
      num6 = (float) ((double) (num12 = num6) * (double) num4 - (double) num7 * (double) num5) + num6;
      num7 = (float) ((double) num7 * (double) num4 + (double) num12 * (double) num5) + num7;
    }
    if (direction == FourierDirection.Forward)
    {
      float num13 = data[0];
      data[0] = num13 + data[1];
      data[1] = num13 - data[1];
    }
    else
    {
      float num14 = data[0];
      data[0] = num1 * (num14 + data[1]);
      data[1] = num1 * (num14 - data[1]);
      Fourier.FFT(data, length / 2, direction);
    }
  }

  public static void FFT2(float[] data, int xLength, int yLength, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < xLength * yLength * 2)
      throw new ArgumentOutOfRangeException("data.Length", (object) data.Length, "must be at least as large as 'xLength * yLength * 2' parameter");
    if (!Fourier.IsPowerOf2(xLength))
      throw new ArgumentOutOfRangeException(nameof (xLength), (object) xLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(yLength))
      throw new ArgumentOutOfRangeException(nameof (yLength), (object) yLength, "must be a power of 2");
    int inc1 = 1;
    int inc2 = xLength;
    if (xLength > 1)
    {
      Fourier.SyncLookupTableLength(xLength);
      for (int index = 0; index < yLength; ++index)
      {
        int start = index * inc2;
        Fourier.LinearFFT_Quick(data, start, inc1, xLength, direction);
      }
    }
    if (yLength <= 1)
      return;
    Fourier.SyncLookupTableLength(yLength);
    for (int index = 0; index < xLength; ++index)
    {
      int start = index * inc1;
      Fourier.LinearFFT_Quick(data, start, inc2, yLength, direction);
    }
  }

  public static void FFT2(ComplexF[] data, int xLength, int yLength, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < xLength * yLength)
      throw new ArgumentOutOfRangeException("data.Length", (object) data.Length, "must be at least as large as 'xLength * yLength' parameter");
    if (!Fourier.IsPowerOf2(xLength))
      throw new ArgumentOutOfRangeException(nameof (xLength), (object) xLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(yLength))
      throw new ArgumentOutOfRangeException(nameof (yLength), (object) yLength, "must be a power of 2");
    int inc1 = 1;
    int inc2 = xLength;
    if (xLength > 1)
    {
      Fourier.SyncLookupTableLength(xLength);
      for (int index = 0; index < yLength; ++index)
      {
        int start = index * inc2;
        Fourier.LinearFFT_Quick(data, start, inc1, xLength, direction);
      }
    }
    if (yLength <= 1)
      return;
    Fourier.SyncLookupTableLength(yLength);
    for (int index = 0; index < xLength; ++index)
    {
      int start = index * inc1;
      Fourier.LinearFFT_Quick(data, start, inc2, yLength, direction);
    }
  }

  public static void FFT2(Complex[] data, int xLength, int yLength, FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < xLength * yLength)
      throw new ArgumentOutOfRangeException("data.Length", (object) data.Length, "must be at least as large as 'xLength * yLength' parameter");
    if (!Fourier.IsPowerOf2(xLength))
      throw new ArgumentOutOfRangeException(nameof (xLength), (object) xLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(yLength))
      throw new ArgumentOutOfRangeException(nameof (yLength), (object) yLength, "must be a power of 2");
    int inc1 = 1;
    int inc2 = xLength;
    if (xLength > 1)
    {
      Fourier.SyncLookupTableLength(xLength);
      for (int index = 0; index < yLength; ++index)
      {
        int start = index * inc2;
        Fourier.LinearFFT_Quick(data, start, inc1, xLength, direction);
      }
    }
    if (yLength <= 1)
      return;
    Fourier.SyncLookupTableLength(yLength);
    for (int index = 0; index < xLength; ++index)
    {
      int start = index * inc1;
      Fourier.LinearFFT_Quick(data, start, inc2, yLength, direction);
    }
  }

  public static void FFT3(
    ComplexF[] data,
    int xLength,
    int yLength,
    int zLength,
    FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < xLength * yLength * zLength)
      throw new ArgumentOutOfRangeException("data.Length", (object) data.Length, "must be at least as large as 'xLength * yLength * zLength' parameter");
    if (!Fourier.IsPowerOf2(xLength))
      throw new ArgumentOutOfRangeException(nameof (xLength), (object) xLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(yLength))
      throw new ArgumentOutOfRangeException(nameof (yLength), (object) yLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(zLength))
      throw new ArgumentOutOfRangeException(nameof (zLength), (object) zLength, "must be a power of 2");
    int inc1 = 1;
    int inc2 = xLength;
    int inc3 = xLength * yLength;
    if (xLength > 1)
    {
      Fourier.SyncLookupTableLength(xLength);
      for (int index1 = 0; index1 < zLength; ++index1)
      {
        for (int index2 = 0; index2 < yLength; ++index2)
        {
          int start = index2 * inc2 + index1 * inc3;
          Fourier.LinearFFT_Quick(data, start, inc1, xLength, direction);
        }
      }
    }
    if (yLength > 1)
    {
      Fourier.SyncLookupTableLength(yLength);
      for (int index3 = 0; index3 < zLength; ++index3)
      {
        for (int index4 = 0; index4 < xLength; ++index4)
        {
          int start = index3 * inc3 + index4 * inc1;
          Fourier.LinearFFT_Quick(data, start, inc2, yLength, direction);
        }
      }
    }
    if (zLength <= 1)
      return;
    Fourier.SyncLookupTableLength(zLength);
    for (int index5 = 0; index5 < yLength; ++index5)
    {
      for (int index6 = 0; index6 < xLength; ++index6)
      {
        int start = index5 * inc2 + index6 * inc1;
        Fourier.LinearFFT_Quick(data, start, inc3, zLength, direction);
      }
    }
  }

  public static void FFT3(
    Complex[] data,
    int xLength,
    int yLength,
    int zLength,
    FourierDirection direction)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length < xLength * yLength * zLength)
      throw new ArgumentOutOfRangeException("data.Length", (object) data.Length, "must be at least as large as 'xLength * yLength * zLength' parameter");
    if (!Fourier.IsPowerOf2(xLength))
      throw new ArgumentOutOfRangeException(nameof (xLength), (object) xLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(yLength))
      throw new ArgumentOutOfRangeException(nameof (yLength), (object) yLength, "must be a power of 2");
    if (!Fourier.IsPowerOf2(zLength))
      throw new ArgumentOutOfRangeException(nameof (zLength), (object) zLength, "must be a power of 2");
    int inc1 = 1;
    int inc2 = xLength;
    int inc3 = xLength * yLength;
    if (xLength > 1)
    {
      Fourier.SyncLookupTableLength(xLength);
      for (int index1 = 0; index1 < zLength; ++index1)
      {
        for (int index2 = 0; index2 < yLength; ++index2)
        {
          int start = index2 * inc2 + index1 * inc3;
          Fourier.LinearFFT_Quick(data, start, inc1, xLength, direction);
        }
      }
    }
    if (yLength > 1)
    {
      Fourier.SyncLookupTableLength(yLength);
      for (int index3 = 0; index3 < zLength; ++index3)
      {
        for (int index4 = 0; index4 < xLength; ++index4)
        {
          int start = index3 * inc3 + index4 * inc1;
          Fourier.LinearFFT_Quick(data, start, inc2, yLength, direction);
        }
      }
    }
    if (zLength <= 1)
      return;
    Fourier.SyncLookupTableLength(zLength);
    for (int index5 = 0; index5 < yLength; ++index5)
    {
      for (int index6 = 0; index6 < xLength; ++index6)
      {
        int start = index5 * inc2 + index6 * inc1;
        Fourier.LinearFFT_Quick(data, start, inc3, zLength, direction);
      }
    }
  }
}
