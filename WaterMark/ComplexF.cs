using System;

namespace WaterMark
{
	// Token: 0x02000005 RID: 5
	public struct ComplexF : IComparable, ICloneable
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000036CD File Offset: 0x000018CD
		public ComplexF(float real, float imaginary)
		{
			this.Re = real;
			this.Im = imaginary;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036DF File Offset: 0x000018DF
		public ComplexF(ComplexF c)
		{
			this.Re = c.Re;
			this.Im = c.Im;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036FC File Offset: 0x000018FC
		public static ComplexF FromRealImaginary(float real, float imaginary)
		{
			ComplexF complexF;
			complexF.Re = real;
			complexF.Im = imaginary;
			return complexF;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000371C File Offset: 0x0000191C
		public static ComplexF FromModulusArgument(float modulus, float argument)
		{
			ComplexF complexF;
			complexF.Re = (float)((double)modulus * Math.Cos((double)argument));
			complexF.Im = (float)((double)modulus * Math.Sin((double)argument));
			return complexF;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000374E File Offset: 0x0000194E
		object ICloneable.Clone()
		{
			return new ComplexF(this);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003760 File Offset: 0x00001960
		public ComplexF Clone()
		{
			return new ComplexF(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003770 File Offset: 0x00001970
		public float GetModulus()
		{
			float re = this.Re;
			float im = this.Im;
			return (float)Math.Sqrt((double)(re * re + im * im));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000379C File Offset: 0x0000199C
		public float GetModulusSquared()
		{
			float re = this.Re;
			float im = this.Im;
			return re * re + im * im;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037BF File Offset: 0x000019BF
		public float GetArgument()
		{
			return (float)Math.Atan2((double)this.Im, (double)this.Re);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037D5 File Offset: 0x000019D5
		public ComplexF GetConjugate()
		{
			return ComplexF.FromRealImaginary(this.Re, -this.Im);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037EC File Offset: 0x000019EC
		public void Normalize()
		{
			double num = (double)this.GetModulus();
			if (num == 0.0)
			{
				throw new DivideByZeroException("Can not normalize a complex number that is zero.");
			}
			this.Re = (float)((double)this.Re / num);
			this.Im = (float)((double)this.Im / num);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003838 File Offset: 0x00001A38
		public static explicit operator ComplexF(Complex c)
		{
			ComplexF complexF;
			complexF.Re = (float)c.Re;
			complexF.Im = (float)c.Im;
			return complexF;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003864 File Offset: 0x00001A64
		public static explicit operator ComplexF(float f)
		{
			ComplexF complexF;
			complexF.Re = f;
			complexF.Im = 0f;
			return complexF;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003887 File Offset: 0x00001A87
		public static explicit operator float(ComplexF c)
		{
			return c.Re;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003891 File Offset: 0x00001A91
		public static bool operator ==(ComplexF a, ComplexF b)
		{
			return a.Re == b.Re && a.Im == b.Im;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038B5 File Offset: 0x00001AB5
		public static bool operator !=(ComplexF a, ComplexF b)
		{
			return a.Re != b.Re || a.Im != b.Im;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038DC File Offset: 0x00001ADC
		public override int GetHashCode()
		{
			return this.Re.GetHashCode() ^ this.Im.GetHashCode();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038F8 File Offset: 0x00001AF8
		public override bool Equals(object o)
		{
			if (o is ComplexF)
			{
				ComplexF complexF = (ComplexF)o;
				return this == complexF;
			}
			return false;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003924 File Offset: 0x00001B24
		public int CompareTo(object o)
		{
			if (o == null)
			{
				return 1;
			}
			if (o is ComplexF)
			{
				return this.GetModulus().CompareTo(((ComplexF)o).GetModulus());
			}
			if (o is float)
			{
				return this.GetModulus().CompareTo((float)o);
			}
			if (o is Complex)
			{
				return this.GetModulus().CompareTo(((Complex)o).GetModulus());
			}
			if (o is double)
			{
				return this.GetModulus().CompareTo((double)o);
			}
			throw new ArgumentException();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039CB File Offset: 0x00001BCB
		public static ComplexF operator +(ComplexF a)
		{
			return a;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000039CE File Offset: 0x00001BCE
		public static ComplexF operator -(ComplexF a)
		{
			a.Re = -a.Re;
			a.Im = -a.Im;
			return a;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000039EF File Offset: 0x00001BEF
		public static ComplexF operator +(ComplexF a, float f)
		{
			a.Re += f;
			return a;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A03 File Offset: 0x00001C03
		public static ComplexF operator +(float f, ComplexF a)
		{
			a.Re += f;
			return a;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A17 File Offset: 0x00001C17
		public static ComplexF operator +(ComplexF a, ComplexF b)
		{
			a.Re += b.Re;
			a.Im += b.Im;
			return a;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A46 File Offset: 0x00001C46
		public static ComplexF operator -(ComplexF a, float f)
		{
			a.Re -= f;
			return a;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A5A File Offset: 0x00001C5A
		public static ComplexF operator -(float f, ComplexF a)
		{
			a.Re = f - a.Re;
			a.Im = 0f - a.Im;
			return a;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A83 File Offset: 0x00001C83
		public static ComplexF operator -(ComplexF a, ComplexF b)
		{
			a.Re -= b.Re;
			a.Im -= b.Im;
			return a;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public static ComplexF operator *(ComplexF a, float f)
		{
			a.Re *= f;
			a.Im *= f;
			return a;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003AD7 File Offset: 0x00001CD7
		public static ComplexF operator *(float f, ComplexF a)
		{
			a.Re *= f;
			a.Im *= f;
			return a;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003AFC File Offset: 0x00001CFC
		public static ComplexF operator *(ComplexF a, ComplexF b)
		{
			double num = (double)a.Re;
			double num2 = (double)a.Im;
			double num3 = (double)b.Re;
			double num4 = (double)b.Im;
			a.Re = (float)(num * num3 - num2 * num4);
			a.Im = (float)(num * num4 + num2 * num3);
			return a;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B4C File Offset: 0x00001D4C
		public static ComplexF operator /(ComplexF a, float f)
		{
			if (f == 0f)
			{
				throw new DivideByZeroException();
			}
			a.Re /= f;
			a.Im /= f;
			return a;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003B80 File Offset: 0x00001D80
		public static ComplexF operator /(ComplexF a, ComplexF b)
		{
			double num = (double)a.Re;
			double num2 = (double)a.Im;
			double num3 = (double)b.Re;
			double num4 = (double)b.Im;
			double num5 = num3 * num3 + num4 * num4;
			if (num5 == 0.0)
			{
				throw new DivideByZeroException();
			}
			a.Re = (float)((num * num3 + num2 * num4) / num5);
			a.Im = (float)((num2 * num3 - num * num4) / num5);
			return a;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public static ComplexF Parse(string s)
		{
			throw new NotImplementedException("ComplexF ComplexF.Parse( string s ) is not implemented.");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003BFE File Offset: 0x00001DFE
		public override string ToString()
		{
			return string.Format("( {0}, {1}i )", this.Re, this.Im);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003C20 File Offset: 0x00001E20
		public static bool IsEqual(ComplexF a, ComplexF b, float tolerance)
		{
			return Math.Abs(a.Re - b.Re) < tolerance && Math.Abs(a.Im - b.Im) < tolerance;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003C52 File Offset: 0x00001E52
		public static ComplexF Zero
		{
			get
			{
				return new ComplexF(0f, 0f);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003C63 File Offset: 0x00001E63
		public static ComplexF I
		{
			get
			{
				return new ComplexF(0f, 1f);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003C74 File Offset: 0x00001E74
		public static ComplexF MaxValue
		{
			get
			{
				return new ComplexF(float.MaxValue, float.MaxValue);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003C85 File Offset: 0x00001E85
		public static ComplexF MinValue
		{
			get
			{
				return new ComplexF(float.MinValue, float.MinValue);
			}
		}

		// Token: 0x0400000C RID: 12
		public float Re;

		// Token: 0x0400000D RID: 13
		public float Im;
	}
}
