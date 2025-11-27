using System;


namespace WaterMark;

public class ComplexStats
{
  private ComplexStats()
  {
  }

  public static ComplexF Sum(ComplexF[] data) => ComplexStats.SumRecursion(data, 0, data.Length);

  private static ComplexF SumRecursion(ComplexF[] data, int start, int end)
  {
    if (end - start <= 1000)
    {
      ComplexF zero = ComplexF.Zero;
      for (int index = start; index < end; ++index)
        zero += data[index];
      return zero;
    }
    int num = start + end >> 1;
    return ComplexStats.SumRecursion(data, start, num) + ComplexStats.SumRecursion(data, num, end);
  }

  public static Complex Sum(Complex[] data) => ComplexStats.SumRecursion(data, 0, data.Length);

  private static Complex SumRecursion(Complex[] data, int start, int end)
  {
    if (end - start <= 1000)
    {
      Complex zero = Complex.Zero;
      for (int index = start; index < end; ++index)
        zero += data[index];
      return zero;
    }
    int num = start + end >> 1;
    return ComplexStats.SumRecursion(data, start, num) + ComplexStats.SumRecursion(data, num, end);
  }

  public static ComplexF SumOfSquares(ComplexF[] data)
  {
    return ComplexStats.SumOfSquaresRecursion(data, 0, data.Length);
  }

  private static ComplexF SumOfSquaresRecursion(ComplexF[] data, int start, int end)
  {
    if (end - start <= 1000)
    {
      ComplexF zero = ComplexF.Zero;
      for (int index = start; index < end; ++index)
        zero += data[index] * data[index];
      return zero;
    }
    int num = start + end >> 1;
    return ComplexStats.SumOfSquaresRecursion(data, start, num) + ComplexStats.SumOfSquaresRecursion(data, num, end);
  }

  public static Complex SumOfSquares(Complex[] data)
  {
    return ComplexStats.SumOfSquaresRecursion(data, 0, data.Length);
  }

  private static Complex SumOfSquaresRecursion(Complex[] data, int start, int end)
  {
    if (end - start <= 1000)
    {
      Complex zero = Complex.Zero;
      for (int index = start; index < end; ++index)
        zero += data[index] * data[index];
      return zero;
    }
    int num = start + end >> 1;
    return ComplexStats.SumOfSquaresRecursion(data, start, num) + ComplexStats.SumOfSquaresRecursion(data, num, end);
  }

  public static ComplexF Mean(ComplexF[] data) => ComplexStats.Sum(data) / (float) data.Length;

  public static Complex Mean(Complex[] data) => ComplexStats.Sum(data) / (double) data.Length;

  public static ComplexF Variance(ComplexF[] data)
  {
    if (data.Length == 0)
      throw new DivideByZeroException("length of data is zero");
    return ComplexStats.SumOfSquares(data) / (float) data.Length - ComplexStats.Sum(data);
  }

  public static Complex Variance(Complex[] data)
  {
    if (data.Length == 0)
      throw new DivideByZeroException("length of data is zero");
    return ComplexStats.SumOfSquares(data) / (double) data.Length - ComplexStats.Sum(data);
  }

  public static ComplexF StdDev(ComplexF[] data)
  {
    return data.Length != 0 ? ComplexMath.Sqrt(ComplexStats.Variance(data)) : throw new DivideByZeroException("length of data is zero");
  }

  public static Complex StdDev(Complex[] data)
  {
    return data.Length != 0 ? ComplexMath.Sqrt(ComplexStats.Variance(data)) : throw new DivideByZeroException("length of data is zero");
  }

  public static float RMSError(ComplexF[] alpha, ComplexF[] beta)
  {
    return (float) Math.Sqrt((double) ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, 0, alpha.Length));
  }

  private static float SumOfSquaredErrorRecursion(
    ComplexF[] alpha,
    ComplexF[] beta,
    int start,
    int end)
  {
    if (end - start <= 1000)
    {
      float num = 0.0f;
      for (int index = start; index < end; ++index)
      {
        ComplexF complexF = beta[index] - alpha[index];
        num += (float) ((double) complexF.Re * (double) complexF.Re + (double) complexF.Im * (double) complexF.Im);
      }
      return num;
    }
    int num1 = start + end >> 1;
    return ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, start, num1) + ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, num1, end);
  }

  public static double RMSError(Complex[] alpha, Complex[] beta)
  {
    return Math.Sqrt(ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, 0, alpha.Length));
  }

  private static double SumOfSquaredErrorRecursion(
    Complex[] alpha,
    Complex[] beta,
    int start,
    int end)
  {
    if (end - start <= 1000)
    {
      double num = 0.0;
      for (int index = start; index < end; ++index)
      {
        Complex complex = beta[index] - alpha[index];
        num += complex.Re * complex.Re + complex.Im * complex.Im;
      }
      return num;
    }
    int num1 = start + end >> 1;
    return ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, start, num1) + ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, num1, end);
  }
}
