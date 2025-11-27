using System;

namespace WaterMark
{
	// Token: 0x02000003 RID: 3
	public struct Complex : IComparable, ICloneable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000279A File Offset: 0x0000099A
		public Complex(double real, double imaginary)
		{
			this.Re = real;
			this.Im = imaginary;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000027AC File Offset: 0x000009AC
		public Complex(Complex c)
		{
			this.Re = c.Re;
			this.Im = c.Im;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000027C8 File Offset: 0x000009C8
		public static Complex FromRealImaginary(double real, double imaginary)
		{
			Complex complex;
			complex.Re = real;
			complex.Im = imaginary;
			return complex;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027E8 File Offset: 0x000009E8
		public static Complex FromModulusArgument(double modulus, double argument)
		{
			Complex complex;
			complex.Re = modulus * Math.Cos(argument);
			complex.Im = modulus * Math.Sin(argument);
			return complex;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002816 File Offset: 0x00000A16
		object ICloneable.Clone()
		{
			return new Complex(this);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002828 File Offset: 0x00000A28
		public Complex Clone()
		{
			return new Complex(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002838 File Offset: 0x00000A38
		public double GetModulus()
		{
			double re = this.Re;
			double im = this.Im;
			return Math.Sqrt(re * re + im * im);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002860 File Offset: 0x00000A60
		public double GetModulusSquared()
		{
			double re = this.Re;
			double im = this.Im;
			return re * re + im * im;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002883 File Offset: 0x00000A83
		public double GetArgument()
		{
			return Math.Atan2(this.Im, this.Re);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002897 File Offset: 0x00000A97
		public Complex GetConjugate()
		{
			return Complex.FromRealImaginary(this.Re, -this.Im);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028AC File Offset: 0x00000AAC
		public void Normalize()
		{
			double modulus = this.GetModulus();
			if (modulus == 0.0)
			{
				throw new DivideByZeroException("Can not normalize a complex number that is zero.");
			}
			this.Re /= modulus;
			this.Im /= modulus;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028F8 File Offset: 0x00000AF8
		public static explicit operator Complex(ComplexF cF)
		{
			Complex complex;
			complex.Re = (double)cF.Re;
			complex.Im = (double)cF.Im;
			return complex;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002924 File Offset: 0x00000B24
		public static explicit operator Complex(double d)
		{
			Complex complex;
			complex.Re = d;
			complex.Im = 0.0;
			return complex;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000294B File Offset: 0x00000B4B
		public static explicit operator double(Complex c)
		{
			return c.Re;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002955 File Offset: 0x00000B55
		public static bool operator ==(Complex a, Complex b)
		{
			return a.Re == b.Re && a.Im == b.Im;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002979 File Offset: 0x00000B79
		public static bool operator !=(Complex a, Complex b)
		{
			return a.Re != b.Re || a.Im != b.Im;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029A0 File Offset: 0x00000BA0
		public override int GetHashCode()
		{
			return this.Re.GetHashCode() ^ this.Im.GetHashCode();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029BC File Offset: 0x00000BBC
		public override bool Equals(object o)
		{
			if (o is Complex)
			{
				Complex complex = (Complex)o;
				return this == complex;
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029E8 File Offset: 0x00000BE8
		public int CompareTo(object o)
		{
			if (o == null)
			{
				return 1;
			}
			if (o is Complex)
			{
				return this.GetModulus().CompareTo(((Complex)o).GetModulus());
			}
			if (o is double)
			{
				return this.GetModulus().CompareTo((double)o);
			}
			if (o is ComplexF)
			{
				return this.GetModulus().CompareTo((double)((ComplexF)o).GetModulus());
			}
			if (o is float)
			{
				return this.GetModulus().CompareTo((double)((float)o));
			}
			throw new ArgumentException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A87 File Offset: 0x00000C87
		public static Complex operator +(Complex a)
		{
			return a;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A8A File Offset: 0x00000C8A
		public static Complex operator -(Complex a)
		{
			a.Re = -a.Re;
			a.Im = -a.Im;
			return a;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002AAB File Offset: 0x00000CAB
		public static Complex operator +(Complex a, double f)
		{
			a.Re += f;
			return a;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002ABF File Offset: 0x00000CBF
		public static Complex operator +(double f, Complex a)
		{
			a.Re += f;
			return a;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public static Complex operator +(Complex a, Complex b)
		{
			a.Re += b.Re;
			a.Im += b.Im;
			return a;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B02 File Offset: 0x00000D02
		public static Complex operator -(Complex a, double f)
		{
			a.Re -= f;
			return a;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B16 File Offset: 0x00000D16
		public static Complex operator -(double f, Complex a)
		{
			a.Re = (double)((float)(f - a.Re));
			a.Im = (double)((float)(0.0 - a.Im));
			return a;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B45 File Offset: 0x00000D45
		public static Complex operator -(Complex a, Complex b)
		{
			a.Re -= b.Re;
			a.Im -= b.Im;
			return a;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B74 File Offset: 0x00000D74
		public static Complex operator *(Complex a, double f)
		{
			a.Re *= f;
			a.Im *= f;
			return a;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B99 File Offset: 0x00000D99
		public static Complex operator *(double f, Complex a)
		{
			a.Re *= f;
			a.Im *= f;
			return a;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static Complex operator *(Complex a, Complex b)
		{
			double re = a.Re;
			double im = a.Im;
			double re2 = b.Re;
			double im2 = b.Im;
			a.Re = re * re2 - im * im2;
			a.Im = re * im2 + im * re2;
			return a;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static Complex operator /(Complex a, double f)
		{
			if (f == 0.0)
			{
				throw new DivideByZeroException();
			}
			a.Re /= f;
			a.Im /= f;
			return a;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C44 File Offset: 0x00000E44
		public static Complex operator /(Complex a, Complex b)
		{
			double re = a.Re;
			double im = a.Im;
			double re2 = b.Re;
			double im2 = b.Im;
			double num = re2 * re2 + im2 * im2;
			if (num == 0.0)
			{
				throw new DivideByZeroException();
			}
			a.Re = (re * re2 + im * im2) / num;
			a.Im = (im * re2 - re * im2) / num;
			return a;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public static Complex Parse(string s)
		{
			throw new NotImplementedException("Complex Complex.Parse( string s ) is not implemented.");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CBE File Offset: 0x00000EBE
		public override string ToString()
		{
			return string.Format("( {0}, {1}i )", this.Re, this.Im);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static bool IsEqual(Complex a, Complex b, double tolerance)
		{
			return Math.Abs(a.Re - b.Re) < tolerance && Math.Abs(a.Im - b.Im) < tolerance;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002D12 File Offset: 0x00000F12
		public static Complex Zero
		{
			get
			{
				return new Complex(0.0, 0.0);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002D2B File Offset: 0x00000F2B
		public static Complex I
		{
			get
			{
				return new Complex(0.0, 1.0);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002D44 File Offset: 0x00000F44
		public static Complex MaxValue
		{
			get
			{
				return new Complex(double.MaxValue, double.MaxValue);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D5D File Offset: 0x00000F5D
		public static Complex MinValue
		{
			get
			{
				return new Complex(double.MinValue, double.MinValue);
			}
		}

		// Token: 0x04000008 RID: 8
		public double Re;

		// Token: 0x04000009 RID: 9
		public double Im;
	}
}
