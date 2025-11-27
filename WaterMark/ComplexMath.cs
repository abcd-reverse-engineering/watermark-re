using System;

namespace WaterMark
{
	// Token: 0x02000006 RID: 6
	public class ComplexMath
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003C96 File Offset: 0x00001E96
		private ComplexMath()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public static void Swap(ref Complex a, ref Complex b)
		{
			Complex complex = a;
			a = b;
			b = complex;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static void Swap(ref ComplexF a, ref ComplexF b)
		{
			ComplexF complexF = a;
			a = b;
			b = complexF;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public static ComplexF Sqrt(ComplexF c)
		{
			double num = (double)c.Re;
			double num2 = (double)c.Im;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			int num4 = ((num2 < 0.0) ? (-1) : 1);
			c.Re = (float)(ComplexMath._halfOfRoot2 * Math.Sqrt(num3 + num));
			c.Im = (float)(ComplexMath._halfOfRoot2 * (double)num4 * Math.Sqrt(num3 - num));
			return c;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D60 File Offset: 0x00001F60
		public static Complex Sqrt(Complex c)
		{
			double re = c.Re;
			double im = c.Im;
			double num = Math.Sqrt(re * re + im * im);
			int num2 = ((im < 0.0) ? (-1) : 1);
			c.Re = ComplexMath._halfOfRoot2 * Math.Sqrt(num + re);
			c.Im = ComplexMath._halfOfRoot2 * (double)num2 * Math.Sqrt(num - re);
			return c;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003DCC File Offset: 0x00001FCC
		public static ComplexF Pow(ComplexF c, double exponent)
		{
			double num = (double)c.Re;
			double num2 = (double)c.Im;
			double num3 = Math.Pow(num * num + num2 * num2, exponent * 0.5);
			double num4 = Math.Atan2(num2, num) * exponent;
			c.Re = (float)(num3 * Math.Cos(num4));
			c.Im = (float)(num3 * Math.Sin(num4));
			return c;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E30 File Offset: 0x00002030
		public static Complex Pow(Complex c, double exponent)
		{
			double re = c.Re;
			double im = c.Im;
			double num = Math.Pow(re * re + im * im, exponent * 0.5);
			double num2 = Math.Atan2(im, re) * exponent;
			c.Re = num * Math.Cos(num2);
			c.Im = num * Math.Sin(num2);
			return c;
		}

		// Token: 0x0400000E RID: 14
		private static double _halfOfRoot2 = 0.5 * Math.Sqrt(2.0);
	}
}
