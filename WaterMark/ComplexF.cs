using System;


namespace WaterMark;

public struct ComplexF : IComparable, ICloneable
{
  public float Re;
  public float Im;

  public ComplexF(float real, float imaginary)
  {
    this.Re = real;
    this.Im = imaginary;
  }

  public ComplexF(ComplexF c)
  {
    this.Re = c.Re;
    this.Im = c.Im;
  }

  public static ComplexF FromRealImaginary(float real, float imaginary)
  {
    ComplexF complexF;
    complexF.Re = real;
    complexF.Im = imaginary;
    return complexF;
  }

  public static ComplexF FromModulusArgument(float modulus, float argument)
  {
    ComplexF complexF;
    complexF.Re = modulus * (float) Math.Cos((double) argument);
    complexF.Im = modulus * (float) Math.Sin((double) argument);
    return complexF;
  }

  object ICloneable.Clone() => (object) new ComplexF(this);

  public ComplexF Clone() => new ComplexF(this);

  public float GetModulus()
  {
    float re = this.Re;
    float im = this.Im;
    return (float) Math.Sqrt((double) re * (double) re + (double) im * (double) im);
  }

  public float GetModulusSquared()
  {
    float re = this.Re;
    float im = this.Im;
    return (float) ((double) re * (double) re + (double) im * (double) im);
  }

  public float GetArgument() => (float) Math.Atan2((double) this.Im, (double) this.Re);

  public ComplexF GetConjugate() => ComplexF.FromRealImaginary(this.Re, -this.Im);

  public void Normalize()
  {
    double modulus = (double) this.GetModulus();
    if (modulus == 0.0)
      throw new DivideByZeroException("Can not normalize a complex number that is zero.");
    this.Re /= (float) modulus;
    this.Im /= (float) modulus;
  }

  public static explicit operator ComplexF(Complex c)
  {
    ComplexF complexF;
    complexF.Re = (float) c.Re;
    complexF.Im = (float) c.Im;
    return complexF;
  }

  public static explicit operator ComplexF(float f)
  {
    ComplexF complexF;
    complexF.Re = f;
    complexF.Im = 0.0f;
    return complexF;
  }

  public static explicit operator float(ComplexF c) => c.Re;

  public static bool operator ==(ComplexF a, ComplexF b)
  {
    return (double) a.Re == (double) b.Re && (double) a.Im == (double) b.Im;
  }

  public static bool operator !=(ComplexF a, ComplexF b)
  {
    return (double) a.Re != (double) b.Re || (double) a.Im != (double) b.Im;
  }

  public override int GetHashCode() => this.Re.GetHashCode() ^ this.Im.GetHashCode();

  public override bool Equals(object o) => o is ComplexF complexF && this == complexF;

  public int CompareTo(object o)
  {
    switch (o)
    {
      case null:
        return 1;
      case ComplexF complexF:
        return this.GetModulus().CompareTo(complexF.GetModulus());
      case float num1:
        return this.GetModulus().CompareTo(num1);
      case Complex complex:
        return this.GetModulus().CompareTo((object) complex.GetModulus());
      case double num2:
        return this.GetModulus().CompareTo((object) num2);
      default:
        throw new ArgumentException();
    }
  }

  public static ComplexF operator +(ComplexF a) => a;

  public static ComplexF operator -(ComplexF a)
  {
    a.Re = -a.Re;
    a.Im = -a.Im;
    return a;
  }

  public static ComplexF operator +(ComplexF a, float f)
  {
    a.Re += f;
    return a;
  }

  public static ComplexF operator +(float f, ComplexF a)
  {
    a.Re += f;
    return a;
  }

  public static ComplexF operator +(ComplexF a, ComplexF b)
  {
    a.Re += b.Re;
    a.Im += b.Im;
    return a;
  }

  public static ComplexF operator -(ComplexF a, float f)
  {
    a.Re -= f;
    return a;
  }

  public static ComplexF operator -(float f, ComplexF a)
  {
    a.Re = f - a.Re;
    a.Im = 0.0f - a.Im;
    return a;
  }

  public static ComplexF operator -(ComplexF a, ComplexF b)
  {
    a.Re -= b.Re;
    a.Im -= b.Im;
    return a;
  }

  public static ComplexF operator *(ComplexF a, float f)
  {
    a.Re *= f;
    a.Im *= f;
    return a;
  }

  public static ComplexF operator *(float f, ComplexF a)
  {
    a.Re *= f;
    a.Im *= f;
    return a;
  }

  public static ComplexF operator *(ComplexF a, ComplexF b)
  {
    double re1 = (double) a.Re;
    double im1 = (double) a.Im;
    double re2 = (double) b.Re;
    double im2 = (double) b.Im;
    a.Re = (float) (re1 * re2 - im1 * im2);
    a.Im = (float) (re1 * im2 + im1 * re2);
    return a;
  }

  public static ComplexF operator /(ComplexF a, float f)
  {
    if ((double) f == 0.0)
      throw new DivideByZeroException();
    a.Re /= f;
    a.Im /= f;
    return a;
  }

  public static ComplexF operator /(ComplexF a, ComplexF b)
  {
    double re1 = (double) a.Re;
    double im1 = (double) a.Im;
    double re2 = (double) b.Re;
    double im2 = (double) b.Im;
    double num = re2 * re2 + im2 * im2;
    if (num == 0.0)
      throw new DivideByZeroException();
    a.Re = (float) ((re1 * re2 + im1 * im2) / num);
    a.Im = (float) ((im1 * re2 - re1 * im2) / num);
    return a;
  }

  public static ComplexF Parse(string s)
  {
    throw new NotImplementedException("ComplexF ComplexF.Parse( string s ) is not implemented.");
  }

  public override string ToString() => $"( {this.Re}, {this.Im}i )";

  public static bool IsEqual(ComplexF a, ComplexF b, float tolerance)
  {
    return (double) Math.Abs(a.Re - b.Re) < (double) tolerance && (double) Math.Abs(a.Im - b.Im) < (double) tolerance;
  }

  public static ComplexF Zero => new ComplexF(0.0f, 0.0f);

  public static ComplexF I => new ComplexF(0.0f, 1f);

  public static ComplexF MaxValue => new ComplexF(float.MaxValue, float.MaxValue);

  public static ComplexF MinValue => new ComplexF(float.MinValue, float.MinValue);
}
