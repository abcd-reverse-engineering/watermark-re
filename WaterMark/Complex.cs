using System;


namespace WaterMark;

public struct Complex : IComparable, ICloneable
{
  public double Re;
  public double Im;

  public Complex(double real, double imaginary)
  {
    this.Re = real;
    this.Im = imaginary;
  }

  public Complex(Complex c)
  {
    this.Re = c.Re;
    this.Im = c.Im;
  }

  public static Complex FromRealImaginary(double real, double imaginary)
  {
    Complex complex;
    complex.Re = real;
    complex.Im = imaginary;
    return complex;
  }

  public static Complex FromModulusArgument(double modulus, double argument)
  {
    Complex complex;
    complex.Re = modulus * Math.Cos(argument);
    complex.Im = modulus * Math.Sin(argument);
    return complex;
  }

  object ICloneable.Clone() => (object) new Complex(this);

  public Complex Clone() => new Complex(this);

  public double GetModulus()
  {
    double re = this.Re;
    double im = this.Im;
    return Math.Sqrt(re * re + im * im);
  }

  public double GetModulusSquared()
  {
    double re = this.Re;
    double im = this.Im;
    return re * re + im * im;
  }

  public double GetArgument() => Math.Atan2(this.Im, this.Re);

  public Complex GetConjugate() => Complex.FromRealImaginary(this.Re, -this.Im);

  public void Normalize()
  {
    double modulus = this.GetModulus();
    if (modulus == 0.0)
      throw new DivideByZeroException("Can not normalize a complex number that is zero.");
    this.Re /= modulus;
    this.Im /= modulus;
  }

  public static explicit operator Complex(ComplexF cF)
  {
    Complex complex;
    complex.Re = (double) cF.Re;
    complex.Im = (double) cF.Im;
    return complex;
  }

  public static explicit operator Complex(double d)
  {
    Complex complex;
    complex.Re = d;
    complex.Im = 0.0;
    return complex;
  }

  public static explicit operator double(Complex c) => c.Re;

  public static bool operator ==(Complex a, Complex b) => a.Re == b.Re && a.Im == b.Im;

  public static bool operator !=(Complex a, Complex b) => a.Re != b.Re || a.Im != b.Im;

  public override int GetHashCode() => this.Re.GetHashCode() ^ this.Im.GetHashCode();

  public override bool Equals(object o) => o is Complex complex && this == complex;

  public int CompareTo(object o)
  {
    switch (o)
    {
      case null:
        return 1;
      case Complex complex:
        return this.GetModulus().CompareTo(complex.GetModulus());
      case double num1:
        return this.GetModulus().CompareTo(num1);
      case ComplexF complexF:
        return this.GetModulus().CompareTo((double) complexF.GetModulus());
      case float num2:
        return this.GetModulus().CompareTo((double) num2);
      default:
        throw new ArgumentException();
    }
  }

  public static Complex operator +(Complex a) => a;

  public static Complex operator -(Complex a)
  {
    a.Re = -a.Re;
    a.Im = -a.Im;
    return a;
  }

  public static Complex operator +(Complex a, double f)
  {
    a.Re += f;
    return a;
  }

  public static Complex operator +(double f, Complex a)
  {
    a.Re += f;
    return a;
  }

  public static Complex operator +(Complex a, Complex b)
  {
    a.Re += b.Re;
    a.Im += b.Im;
    return a;
  }

  public static Complex operator -(Complex a, double f)
  {
    a.Re -= f;
    return a;
  }

  public static Complex operator -(double f, Complex a)
  {
    a.Re = f - a.Re;
    a.Im = 0.0 - a.Im;
    return a;
  }

  public static Complex operator -(Complex a, Complex b)
  {
    a.Re -= b.Re;
    a.Im -= b.Im;
    return a;
  }

  public static Complex operator *(Complex a, double f)
  {
    a.Re *= f;
    a.Im *= f;
    return a;
  }

  public static Complex operator *(double f, Complex a)
  {
    a.Re *= f;
    a.Im *= f;
    return a;
  }

  public static Complex operator *(Complex a, Complex b)
  {
    double re1 = a.Re;
    double im1 = a.Im;
    double re2 = b.Re;
    double im2 = b.Im;
    a.Re = re1 * re2 - im1 * im2;
    a.Im = re1 * im2 + im1 * re2;
    return a;
  }

  public static Complex operator /(Complex a, double f)
  {
    if (f == 0.0)
      throw new DivideByZeroException();
    a.Re /= f;
    a.Im /= f;
    return a;
  }

  public static Complex operator /(Complex a, Complex b)
  {
    double re1 = a.Re;
    double im1 = a.Im;
    double re2 = b.Re;
    double im2 = b.Im;
    double num = re2 * re2 + im2 * im2;
    if (num == 0.0)
      throw new DivideByZeroException();
    a.Re = (re1 * re2 + im1 * im2) / num;
    a.Im = (im1 * re2 - re1 * im2) / num;
    return a;
  }

  public static Complex Parse(string s)
  {
    throw new NotImplementedException("Complex Complex.Parse( string s ) is not implemented.");
  }

  public override string ToString() => $"( {this.Re}, {this.Im}i )";

  public static bool IsEqual(Complex a, Complex b, double tolerance)
  {
    return Math.Abs(a.Re - b.Re) < tolerance && Math.Abs(a.Im - b.Im) < tolerance;
  }

  public static Complex Zero => new Complex(0.0, 0.0);

  public static Complex I => new Complex(0.0, 1.0);

  public static Complex MaxValue => new Complex(double.MaxValue, double.MaxValue);

  public static Complex MinValue => new Complex(double.MinValue, double.MinValue);
}
