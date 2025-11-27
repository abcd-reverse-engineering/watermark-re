using System;

namespace WaterMark
{
	// Token: 0x02000007 RID: 7
	public class ComplexStats
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003EAF File Offset: 0x000020AF
		private ComplexStats()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003EB7 File Offset: 0x000020B7
		public static ComplexF Sum(ComplexF[] data)
		{
			return ComplexStats.SumRecursion(data, 0, data.Length);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003EC4 File Offset: 0x000020C4
		private static ComplexF SumRecursion(ComplexF[] data, int start, int end)
		{
			if (end - start <= 1000)
			{
				ComplexF complexF = ComplexF.Zero;
				for (int i = start; i < end; i++)
				{
					complexF += data[i];
				}
				return complexF;
			}
			int num = start + end >> 1;
			return ComplexStats.SumRecursion(data, start, num) + ComplexStats.SumRecursion(data, num, end);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F1D File Offset: 0x0000211D
		public static Complex Sum(Complex[] data)
		{
			return ComplexStats.SumRecursion(data, 0, data.Length);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003F2C File Offset: 0x0000212C
		private static Complex SumRecursion(Complex[] data, int start, int end)
		{
			if (end - start <= 1000)
			{
				Complex complex = Complex.Zero;
				for (int i = start; i < end; i++)
				{
					complex += data[i];
				}
				return complex;
			}
			int num = start + end >> 1;
			return ComplexStats.SumRecursion(data, start, num) + ComplexStats.SumRecursion(data, num, end);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003F85 File Offset: 0x00002185
		public static ComplexF SumOfSquares(ComplexF[] data)
		{
			return ComplexStats.SumOfSquaresRecursion(data, 0, data.Length);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F94 File Offset: 0x00002194
		private static ComplexF SumOfSquaresRecursion(ComplexF[] data, int start, int end)
		{
			if (end - start <= 1000)
			{
				ComplexF complexF = ComplexF.Zero;
				for (int i = start; i < end; i++)
				{
					complexF += data[i] * data[i];
				}
				return complexF;
			}
			int num = start + end >> 1;
			return ComplexStats.SumOfSquaresRecursion(data, start, num) + ComplexStats.SumOfSquaresRecursion(data, num, end);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FFE File Offset: 0x000021FE
		public static Complex SumOfSquares(Complex[] data)
		{
			return ComplexStats.SumOfSquaresRecursion(data, 0, data.Length);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000400C File Offset: 0x0000220C
		private static Complex SumOfSquaresRecursion(Complex[] data, int start, int end)
		{
			if (end - start <= 1000)
			{
				Complex complex = Complex.Zero;
				for (int i = start; i < end; i++)
				{
					complex += data[i] * data[i];
				}
				return complex;
			}
			int num = start + end >> 1;
			return ComplexStats.SumOfSquaresRecursion(data, start, num) + ComplexStats.SumOfSquaresRecursion(data, num, end);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004076 File Offset: 0x00002276
		public static ComplexF Mean(ComplexF[] data)
		{
			return ComplexStats.Sum(data) / (float)data.Length;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004087 File Offset: 0x00002287
		public static Complex Mean(Complex[] data)
		{
			return ComplexStats.Sum(data) / (double)data.Length;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004098 File Offset: 0x00002298
		public static ComplexF Variance(ComplexF[] data)
		{
			if (data.Length == 0)
			{
				throw new DivideByZeroException("length of data is zero");
			}
			return ComplexStats.SumOfSquares(data) / (float)data.Length - ComplexStats.Sum(data);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000040C4 File Offset: 0x000022C4
		public static Complex Variance(Complex[] data)
		{
			if (data.Length == 0)
			{
				throw new DivideByZeroException("length of data is zero");
			}
			return ComplexStats.SumOfSquares(data) / (double)data.Length - ComplexStats.Sum(data);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000040F0 File Offset: 0x000022F0
		public static ComplexF StdDev(ComplexF[] data)
		{
			if (data.Length == 0)
			{
				throw new DivideByZeroException("length of data is zero");
			}
			return ComplexMath.Sqrt(ComplexStats.Variance(data));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000410D File Offset: 0x0000230D
		public static Complex StdDev(Complex[] data)
		{
			if (data.Length == 0)
			{
				throw new DivideByZeroException("length of data is zero");
			}
			return ComplexMath.Sqrt(ComplexStats.Variance(data));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000412A File Offset: 0x0000232A
		public static float RMSError(ComplexF[] alpha, ComplexF[] beta)
		{
			return (float)Math.Sqrt((double)ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, 0, alpha.Length));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004140 File Offset: 0x00002340
		private static float SumOfSquaredErrorRecursion(ComplexF[] alpha, ComplexF[] beta, int start, int end)
		{
			if (end - start <= 1000)
			{
				float num = 0f;
				for (int i = start; i < end; i++)
				{
					ComplexF complexF = beta[i] - alpha[i];
					num += complexF.Re * complexF.Re + complexF.Im * complexF.Im;
				}
				return num;
			}
			int num2 = start + end >> 1;
			return ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, start, num2) + ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, num2, end);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000041C4 File Offset: 0x000023C4
		public static double RMSError(Complex[] alpha, Complex[] beta)
		{
			return Math.Sqrt(ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, 0, alpha.Length));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000041D8 File Offset: 0x000023D8
		private static double SumOfSquaredErrorRecursion(Complex[] alpha, Complex[] beta, int start, int end)
		{
			if (end - start <= 1000)
			{
				double num = 0.0;
				for (int i = start; i < end; i++)
				{
					Complex complex = beta[i] - alpha[i];
					num += complex.Re * complex.Re + complex.Im * complex.Im;
				}
				return num;
			}
			int num2 = start + end >> 1;
			return ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, start, num2) + ComplexStats.SumOfSquaredErrorRecursion(alpha, beta, num2, end);
		}
	}
}
