using System;

namespace WaterMark
{
	// Token: 0x02000008 RID: 8
	public class Fourier
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004260 File Offset: 0x00002460
		private Fourier()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004268 File Offset: 0x00002468
		private static void Swap(ref float a, ref float b)
		{
			float num = a;
			a = b;
			b = num;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004280 File Offset: 0x00002480
		private static void Swap(ref double a, ref double b)
		{
			double num = a;
			a = b;
			b = num;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004298 File Offset: 0x00002498
		private static void Swap(ref ComplexF a, ref ComplexF b)
		{
			ComplexF complexF = a;
			a = b;
			b = complexF;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000042C0 File Offset: 0x000024C0
		private static void Swap(ref Complex a, ref Complex b)
		{
			Complex complex = a;
			a = b;
			b = complex;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000042E7 File Offset: 0x000024E7
		private static bool IsPowerOf2(int x)
		{
			return (x & (x - 1)) == 0;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042F1 File Offset: 0x000024F1
		private static int Pow2(int exponent)
		{
			if (exponent >= 0 && exponent < 31)
			{
				return 1 << exponent;
			}
			return 0;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004304 File Offset: 0x00002504
		private static int Log2(int x)
		{
			if (x <= 65536)
			{
				if (x <= 256)
				{
					if (x <= 16)
					{
						if (x <= 4)
						{
							if (x > 2)
							{
								return 2;
							}
							if (x <= 1)
							{
								return 0;
							}
							return 1;
						}
						else
						{
							if (x <= 8)
							{
								return 3;
							}
							return 4;
						}
					}
					else if (x <= 64)
					{
						if (x <= 32)
						{
							return 5;
						}
						return 6;
					}
					else
					{
						if (x <= 128)
						{
							return 7;
						}
						return 8;
					}
				}
				else if (x <= 4096)
				{
					if (x <= 1024)
					{
						if (x <= 512)
						{
							return 9;
						}
						return 10;
					}
					else
					{
						if (x <= 2048)
						{
							return 11;
						}
						return 12;
					}
				}
				else if (x <= 16384)
				{
					if (x <= 8192)
					{
						return 13;
					}
					return 14;
				}
				else
				{
					if (x <= 32768)
					{
						return 15;
					}
					return 16;
				}
			}
			else if (x <= 16777216)
			{
				if (x <= 1048576)
				{
					if (x <= 262144)
					{
						if (x <= 131072)
						{
							return 17;
						}
						return 18;
					}
					else
					{
						if (x <= 524288)
						{
							return 19;
						}
						return 20;
					}
				}
				else if (x <= 4194304)
				{
					if (x <= 2097152)
					{
						return 21;
					}
					return 22;
				}
				else
				{
					if (x <= 8388608)
					{
						return 23;
					}
					return 24;
				}
			}
			else if (x <= 268435456)
			{
				if (x <= 67108864)
				{
					if (x <= 33554432)
					{
						return 25;
					}
					return 26;
				}
				else
				{
					if (x <= 134217728)
					{
						return 27;
					}
					return 28;
				}
			}
			else
			{
				if (x > 1073741824)
				{
					return 31;
				}
				if (x <= 536870912)
				{
					return 29;
				}
				return 30;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000444C File Offset: 0x0000264C
		private static int ReverseBits(int index, int numberOfBits)
		{
			int num = 0;
			for (int i = 0; i < numberOfBits; i++)
			{
				num = (num << 1) | (index & 1);
				index >>= 1;
			}
			return num;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004478 File Offset: 0x00002678
		private static int[] GetReversedBits(int numberOfBits)
		{
			if (Fourier._reversedBits[numberOfBits - 1] == null)
			{
				int num = Fourier.Pow2(numberOfBits);
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = 0;
					for (int j = 0; j < numberOfBits; j++)
					{
						num3 = (num3 << 1) | (num2 & 1);
						num2 >>= 1;
					}
					array[i] = num3;
				}
				Fourier._reversedBits[numberOfBits - 1] = array;
			}
			return Fourier._reversedBits[numberOfBits - 1];
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000044E8 File Offset: 0x000026E8
		private static void ReorderArray(float[] data)
		{
			int num = data.Length / 2;
			int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(num));
			for (int i = 0; i < num; i++)
			{
				int num2 = reversedBits[i];
				if (num2 > i)
				{
					Fourier.Swap(ref data[i << 1], ref data[num2 << 1]);
					Fourier.Swap(ref data[(i << 1) + 1], ref data[(num2 << 1) + 1]);
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004550 File Offset: 0x00002750
		private static void ReorderArray(double[] data)
		{
			int num = data.Length / 2;
			int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(num));
			for (int i = 0; i < num; i++)
			{
				int num2 = reversedBits[i];
				if (num2 > i)
				{
					Fourier.Swap(ref data[i << 1], ref data[num2 << 1]);
					Fourier.Swap(ref data[i << 2], ref data[num2 << 2]);
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045B4 File Offset: 0x000027B4
		private static void ReorderArray(Complex[] data)
		{
			int num = data.Length;
			int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(num));
			for (int i = 0; i < num; i++)
			{
				int num2 = reversedBits[i];
				if (num2 > i)
				{
					Complex complex = data[i];
					data[i] = data[num2];
					data[num2] = complex;
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000461C File Offset: 0x0000281C
		private static void ReorderArray(ComplexF[] data)
		{
			int num = data.Length;
			int[] reversedBits = Fourier.GetReversedBits(Fourier.Log2(num));
			for (int i = 0; i < num; i++)
			{
				int num2 = reversedBits[i];
				if (num2 > i)
				{
					ComplexF complexF = data[i];
					data[i] = data[num2];
					data[num2] = complexF;
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004684 File Offset: 0x00002884
		private static int _ReverseBits(int bits, int n)
		{
			int num = 0;
			for (int i = 0; i < n; i++)
			{
				num = (num << 1) | (bits & 1);
				bits >>= 1;
			}
			return num;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000046B0 File Offset: 0x000028B0
		private static void InitializeReverseBits(int levels)
		{
			Fourier._reverseBits = new int[levels + 1][];
			for (int i = 0; i < levels + 1; i++)
			{
				int num = (int)Math.Pow(2.0, (double)i);
				Fourier._reverseBits[i] = new int[num];
				for (int j = 0; j < num; j++)
				{
					Fourier._reverseBits[i][j] = Fourier._ReverseBits(j, i);
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004714 File Offset: 0x00002914
		private static void SyncLookupTableLength(int length)
		{
			if (length > Fourier._lookupTabletLength)
			{
				int num = (int)Math.Ceiling(Math.Log((double)length, 2.0));
				Fourier.InitializeReverseBits(num);
				Fourier.InitializeComplexRotations(num);
				Fourier._lookupTabletLength = length;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004752 File Offset: 0x00002952
		private static int GetLookupTableLength()
		{
			return Fourier._lookupTabletLength;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004759 File Offset: 0x00002959
		private static void ClearLookupTables()
		{
			Fourier._uRLookup = null;
			Fourier._uILookup = null;
			Fourier._uRLookupF = null;
			Fourier._uILookupF = null;
			Fourier._lookupTabletLength = -1;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000477C File Offset: 0x0000297C
		private static void InitializeComplexRotations(int levels)
		{
			Fourier._uRLookup = new double[levels + 1, 2][];
			Fourier._uILookup = new double[levels + 1, 2][];
			Fourier._uRLookupF = new float[levels + 1, 2][];
			Fourier._uILookupF = new float[levels + 1, 2][];
			int num = 1;
			for (int i = 1; i <= levels; i++)
			{
				int num2 = num;
				num <<= 1;
				double num3 = 1.0;
				double num4 = 0.0;
				double num5 = 3.1415926535897931 / (double)num2 * 1.0;
				double num6 = Math.Cos(num5);
				double num7 = Math.Sin(num5);
				Fourier._uRLookup[i, 0] = new double[num2];
				Fourier._uILookup[i, 0] = new double[num2];
				Fourier._uRLookupF[i, 0] = new float[num2];
				Fourier._uILookupF[i, 0] = new float[num2];
				for (int j = 0; j < num2; j++)
				{
					Fourier._uRLookupF[i, 0][j] = (float)(Fourier._uRLookup[i, 0][j] = num3);
					Fourier._uILookupF[i, 0][j] = (float)(Fourier._uILookup[i, 0][j] = num4);
					double num8 = num3 * num7 + num4 * num6;
					num3 = num3 * num6 - num4 * num7;
					num4 = num8;
				}
				double num9 = 1.0;
				double num10 = 0.0;
				double num11 = 3.1415926535897931 / (double)num2 * -1.0;
				double num12 = Math.Cos(num11);
				double num13 = Math.Sin(num11);
				Fourier._uRLookup[i, 1] = new double[num2];
				Fourier._uILookup[i, 1] = new double[num2];
				Fourier._uRLookupF[i, 1] = new float[num2];
				Fourier._uILookupF[i, 1] = new float[num2];
				for (int k = 0; k < num2; k++)
				{
					Fourier._uRLookupF[i, 1][k] = (float)(Fourier._uRLookup[i, 1][k] = num9);
					Fourier._uILookupF[i, 1][k] = (float)(Fourier._uILookup[i, 1][k] = num10);
					double num14 = num9 * num13 + num10 * num12;
					num9 = num9 * num12 - num10 * num13;
					num10 = num14;
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000049E5 File Offset: 0x00002BE5
		private static void LockBufferF(int length, ref float[] buffer)
		{
			Fourier._bufferFLocked = true;
			if (length >= Fourier._bufferF.Length)
			{
				Fourier._bufferF = new float[length];
			}
			buffer = Fourier._bufferF;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004A09 File Offset: 0x00002C09
		private static void UnlockBufferF(ref float[] buffer)
		{
			Fourier._bufferFLocked = false;
			buffer = null;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004A14 File Offset: 0x00002C14
		private static void LinearFFT(float[] data, int start, int inc, int length, FourierDirection direction)
		{
			float[] array = null;
			Fourier.LockBufferF(length * 2, ref array);
			int num = start;
			for (int i = 0; i < length * 2; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferF(ref array);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A70 File Offset: 0x00002C70
		private static void LinearFFT_Quick(float[] data, int start, int inc, int length, FourierDirection direction)
		{
			float[] array = null;
			Fourier.LockBufferF(length * 2, ref array);
			int num = start;
			for (int i = 0; i < length * 2; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT_Quick(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferF(ref array);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004ACB File Offset: 0x00002CCB
		private static void LockBufferCF(int length, ref ComplexF[] buffer)
		{
			Fourier._bufferCFLocked = true;
			if (length != Fourier._bufferCF.Length)
			{
				Fourier._bufferCF = new ComplexF[length];
			}
			buffer = Fourier._bufferCF;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004AEF File Offset: 0x00002CEF
		private static void UnlockBufferCF(ref ComplexF[] buffer)
		{
			Fourier._bufferCFLocked = false;
			buffer = null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004AFC File Offset: 0x00002CFC
		private static void LinearFFT(ComplexF[] data, int start, int inc, int length, FourierDirection direction)
		{
			ComplexF[] array = null;
			Fourier.LockBufferCF(length, ref array);
			int num = start;
			for (int i = 0; i < length; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferCF(ref array);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B78 File Offset: 0x00002D78
		private static void LinearFFT_Quick(ComplexF[] data, int start, int inc, int length, FourierDirection direction)
		{
			ComplexF[] array = null;
			Fourier.LockBufferCF(length, ref array);
			int num = start;
			for (int i = 0; i < length; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferCF(ref array);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004BF3 File Offset: 0x00002DF3
		private static void LockBufferC(int length, ref Complex[] buffer)
		{
			Fourier._bufferCLocked = true;
			if (length >= Fourier._bufferC.Length)
			{
				Fourier._bufferC = new Complex[length];
			}
			buffer = Fourier._bufferC;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004C17 File Offset: 0x00002E17
		private static void UnlockBufferC(ref Complex[] buffer)
		{
			Fourier._bufferCLocked = false;
			buffer = null;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004C24 File Offset: 0x00002E24
		private static void LinearFFT(Complex[] data, int start, int inc, int length, FourierDirection direction)
		{
			Complex[] array = null;
			Fourier.LockBufferC(length, ref array);
			int num = start;
			for (int i = 0; i < length; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferC(ref array);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004CA0 File Offset: 0x00002EA0
		private static void LinearFFT_Quick(Complex[] data, int start, int inc, int length, FourierDirection direction)
		{
			Complex[] array = null;
			Fourier.LockBufferC(length, ref array);
			int num = start;
			for (int i = 0; i < length; i++)
			{
				array[i] = data[num];
				num += inc;
			}
			Fourier.FFT_Quick(array, length, direction);
			num = start;
			for (int j = 0; j < length; j++)
			{
				data[num] = array[j];
				num += inc;
			}
			Fourier.UnlockBufferC(ref array);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static void FFT(float[] data, int length, FourierDirection direction)
		{
			Fourier.SyncLookupTableLength(length);
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				float[] array = Fourier._uRLookupF[i, num3];
				float[] array2 = Fourier._uILookupF[i, num3];
				for (int j = 0; j < num4; j++)
				{
					float num5 = array[j];
					float num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k << 1;
						int num8 = k + num4 << 1;
						float num9 = data[num8];
						float num10 = data[num8 + 1];
						float num11 = num9 * num5 - num10 * num6;
						float num12 = num9 * num6 + num10 * num5;
						num9 = data[num7];
						num10 = data[num7 + 1];
						data[num7] = num9 + num11;
						data[num7 + 1] = num10 + num12;
						data[num8] = num9 - num11;
						data[num8 + 1] = num10 - num12;
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004E20 File Offset: 0x00003020
		public static void FFT_Quick(float[] data, int length, FourierDirection direction)
		{
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				float[] array = Fourier._uRLookupF[i, num3];
				float[] array2 = Fourier._uILookupF[i, num3];
				for (int j = 0; j < num4; j++)
				{
					float num5 = array[j];
					float num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k << 1;
						int num8 = k + num4 << 1;
						float num9 = data[num8];
						float num10 = data[num8 + 1];
						float num11 = num9 * num5 - num10 * num6;
						float num12 = num9 * num6 + num10 * num5;
						num9 = data[num7];
						num10 = data[num7 + 1];
						data[num7] = num9 + num11;
						data[num7 + 1] = num10 + num12;
						data[num8] = num9 - num11;
						data[num8 + 1] = num10 - num12;
					}
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F20 File Offset: 0x00003120
		public static void FFT(ComplexF[] data, int length, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < length)
			{
				throw new ArgumentOutOfRangeException("length", length, "must be at least as large as 'data.Length' parameter");
			}
			if (!Fourier.IsPowerOf2(length))
			{
				throw new ArgumentOutOfRangeException("length", length, "must be a power of 2");
			}
			Fourier.SyncLookupTableLength(length);
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				float[] array = Fourier._uRLookupF[i, num3];
				float[] array2 = Fourier._uILookupF[i, num3];
				for (int j = 0; j < num4; j++)
				{
					float num5 = array[j];
					float num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k + num4;
						float num8 = data[num7].Re;
						float num9 = data[num7].Im;
						float num10 = num8 * num5 - num9 * num6;
						float num11 = num8 * num6 + num9 * num5;
						num8 = data[k].Re;
						num9 = data[k].Im;
						data[k].Re = num8 + num10;
						data[k].Im = num9 + num11;
						data[num7].Re = num8 - num10;
						data[num7].Im = num9 - num11;
					}
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000050AC File Offset: 0x000032AC
		public static void FFT_Quick(ComplexF[] data, int length, FourierDirection direction)
		{
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				float[] array = Fourier._uRLookupF[i, num3];
				float[] array2 = Fourier._uILookupF[i, num3];
				for (int j = 0; j < num4; j++)
				{
					float num5 = array[j];
					float num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k + num4;
						float num8 = data[num7].Re;
						float num9 = data[num7].Im;
						float num10 = num8 * num5 - num9 * num6;
						float num11 = num8 * num6 + num9 * num5;
						num8 = data[k].Re;
						num9 = data[k].Im;
						data[k].Re = num8 + num10;
						data[k].Im = num9 + num11;
						data[num7].Re = num8 - num10;
						data[num7].Im = num9 - num11;
					}
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000051E7 File Offset: 0x000033E7
		public static void FFT(ComplexF[] data, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Fourier.FFT(data, data.Length, direction);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005204 File Offset: 0x00003404
		public static void FFT(Complex[] data, int length, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < length)
			{
				throw new ArgumentOutOfRangeException("length", length, "must be at least as large as 'data.Length' parameter");
			}
			if (!Fourier.IsPowerOf2(length))
			{
				throw new ArgumentOutOfRangeException("length", length, "must be a power of 2");
			}
			Fourier.SyncLookupTableLength(length);
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				double[] array = Fourier._uRLookup[i, num3];
				double[] array2 = Fourier._uILookup[i, num3];
				for (int j = 0; j < num4; j++)
				{
					double num5 = array[j];
					double num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k + num4;
						double num8 = data[num7].Re;
						double num9 = data[num7].Im;
						double num10 = num8 * num5 - num9 * num6;
						double num11 = num8 * num6 + num9 * num5;
						num8 = data[k].Re;
						num9 = data[k].Im;
						data[k].Re = num8 + num10;
						data[k].Im = num9 + num11;
						data[num7].Re = num8 - num10;
						data[num7].Im = num9 - num11;
					}
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005390 File Offset: 0x00003590
		public static void FFT_Quick(Complex[] data, int length, FourierDirection direction)
		{
			int num = Fourier.Log2(length);
			Fourier.ReorderArray(data);
			int num2 = 1;
			int num3 = ((direction == FourierDirection.Forward) ? 0 : 1);
			for (int i = 1; i <= num; i++)
			{
				int num4 = num2;
				num2 <<= 1;
				double[] array = Fourier._uRLookup[i, num3];
				double[] array2 = Fourier._uILookup[i, num3];
				for (int j = 0; j < num4; j++)
				{
					double num5 = array[j];
					double num6 = array2[j];
					for (int k = j; k < length; k += num2)
					{
						int num7 = k + num4;
						double num8 = data[num7].Re;
						double num9 = data[num7].Im;
						double num10 = num8 * num5 - num9 * num6;
						double num11 = num8 * num6 + num9 * num5;
						num8 = data[k].Re;
						num9 = data[k].Im;
						data[k].Re = num8 + num10;
						data[k].Im = num9 + num11;
						data[num7].Re = num8 - num10;
						data[num7].Im = num9 - num11;
					}
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000054CB File Offset: 0x000036CB
		public static void RFFT(float[] data, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Fourier.RFFT(data, data.Length, direction);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000054E8 File Offset: 0x000036E8
		public static void RFFT(float[] data, int length, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < length)
			{
				throw new ArgumentOutOfRangeException("length", length, "must be at least as large as 'data.Length' parameter");
			}
			if (!Fourier.IsPowerOf2(length))
			{
				throw new ArgumentOutOfRangeException("length", length, "must be a power of 2");
			}
			float num = 0.5f;
			float num2 = 3.14159274f / (float)(length / 2);
			float num3;
			if (direction == FourierDirection.Forward)
			{
				num3 = -0.5f;
				Fourier.FFT(data, length / 2, direction);
			}
			else
			{
				num3 = 0.5f;
				num2 = -num2;
			}
			float num4 = (float)Math.Sin(0.5 * (double)num2);
			float num5 = -2f * num4 * num4;
			float num6 = (float)Math.Sin((double)num2);
			float num7 = 1f + num5;
			float num8 = num6;
			for (int i = 1; i < length / 4; i++)
			{
				int num9 = 2 * i;
				int num10 = length - 2 * i;
				float num11 = num * (data[num9] + data[num10]);
				float num12 = num * (data[num9 + 1] - data[num10 + 1]);
				float num13 = -num3 * (data[num9 + 1] + data[num10 + 1]);
				float num14 = num3 * (data[num9] - data[num10]);
				data[num9] = num11 + num7 * num13 - num8 * num14;
				data[num9 + 1] = num12 + num7 * num14 + num8 * num13;
				data[num10] = num11 - num7 * num13 + num8 * num14;
				data[num10 + 1] = -num12 + num7 * num14 + num8 * num13;
				num7 = (num4 = num7) * num5 - num8 * num6 + num7;
				num8 = num8 * num5 + num4 * num6 + num8;
			}
			if (direction == FourierDirection.Forward)
			{
				float num15 = data[0];
				data[0] = num15 + data[1];
				data[1] = num15 - data[1];
				return;
			}
			float num16 = data[0];
			data[0] = num * (num16 + data[1]);
			data[1] = num * (num16 - data[1]);
			Fourier.FFT(data, length / 2, direction);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000056B8 File Offset: 0x000038B8
		public static void FFT2(float[] data, int xLength, int yLength, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < xLength * yLength * 2)
			{
				throw new ArgumentOutOfRangeException("data.Length", data.Length, "must be at least as large as 'xLength * yLength * 2' parameter");
			}
			if (!Fourier.IsPowerOf2(xLength))
			{
				throw new ArgumentOutOfRangeException("xLength", xLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(yLength))
			{
				throw new ArgumentOutOfRangeException("yLength", yLength, "must be a power of 2");
			}
			int num = 1;
			if (xLength > 1)
			{
				Fourier.SyncLookupTableLength(xLength);
				for (int i = 0; i < yLength; i++)
				{
					int num2 = i * xLength;
					Fourier.LinearFFT_Quick(data, num2, num, xLength, direction);
				}
			}
			if (yLength > 1)
			{
				Fourier.SyncLookupTableLength(yLength);
				for (int j = 0; j < xLength; j++)
				{
					int num3 = j * num;
					Fourier.LinearFFT_Quick(data, num3, xLength, yLength, direction);
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005784 File Offset: 0x00003984
		public static void FFT2(ComplexF[] data, int xLength, int yLength, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < xLength * yLength)
			{
				throw new ArgumentOutOfRangeException("data.Length", data.Length, "must be at least as large as 'xLength * yLength' parameter");
			}
			if (!Fourier.IsPowerOf2(xLength))
			{
				throw new ArgumentOutOfRangeException("xLength", xLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(yLength))
			{
				throw new ArgumentOutOfRangeException("yLength", yLength, "must be a power of 2");
			}
			int num = 1;
			if (xLength > 1)
			{
				Fourier.SyncLookupTableLength(xLength);
				for (int i = 0; i < yLength; i++)
				{
					int num2 = i * xLength;
					Fourier.LinearFFT_Quick(data, num2, num, xLength, direction);
				}
			}
			if (yLength > 1)
			{
				Fourier.SyncLookupTableLength(yLength);
				for (int j = 0; j < xLength; j++)
				{
					int num3 = j * num;
					Fourier.LinearFFT_Quick(data, num3, xLength, yLength, direction);
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005850 File Offset: 0x00003A50
		public static void FFT2(Complex[] data, int xLength, int yLength, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < xLength * yLength)
			{
				throw new ArgumentOutOfRangeException("data.Length", data.Length, "must be at least as large as 'xLength * yLength' parameter");
			}
			if (!Fourier.IsPowerOf2(xLength))
			{
				throw new ArgumentOutOfRangeException("xLength", xLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(yLength))
			{
				throw new ArgumentOutOfRangeException("yLength", yLength, "must be a power of 2");
			}
			int num = 1;
			if (xLength > 1)
			{
				Fourier.SyncLookupTableLength(xLength);
				for (int i = 0; i < yLength; i++)
				{
					int num2 = i * xLength;
					Fourier.LinearFFT_Quick(data, num2, num, xLength, direction);
				}
			}
			if (yLength > 1)
			{
				Fourier.SyncLookupTableLength(yLength);
				for (int j = 0; j < xLength; j++)
				{
					int num3 = j * num;
					Fourier.LinearFFT_Quick(data, num3, xLength, yLength, direction);
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000591C File Offset: 0x00003B1C
		public static void FFT3(ComplexF[] data, int xLength, int yLength, int zLength, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < xLength * yLength * zLength)
			{
				throw new ArgumentOutOfRangeException("data.Length", data.Length, "must be at least as large as 'xLength * yLength * zLength' parameter");
			}
			if (!Fourier.IsPowerOf2(xLength))
			{
				throw new ArgumentOutOfRangeException("xLength", xLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(yLength))
			{
				throw new ArgumentOutOfRangeException("yLength", yLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(zLength))
			{
				throw new ArgumentOutOfRangeException("zLength", zLength, "must be a power of 2");
			}
			int num = 1;
			int num2 = xLength * yLength;
			if (xLength > 1)
			{
				Fourier.SyncLookupTableLength(xLength);
				for (int i = 0; i < zLength; i++)
				{
					for (int j = 0; j < yLength; j++)
					{
						int num3 = j * xLength + i * num2;
						Fourier.LinearFFT_Quick(data, num3, num, xLength, direction);
					}
				}
			}
			if (yLength > 1)
			{
				Fourier.SyncLookupTableLength(yLength);
				for (int k = 0; k < zLength; k++)
				{
					for (int l = 0; l < xLength; l++)
					{
						int num4 = k * num2 + l * num;
						Fourier.LinearFFT_Quick(data, num4, xLength, yLength, direction);
					}
				}
			}
			if (zLength > 1)
			{
				Fourier.SyncLookupTableLength(zLength);
				for (int m = 0; m < yLength; m++)
				{
					for (int n = 0; n < xLength; n++)
					{
						int num5 = m * xLength + n * num;
						Fourier.LinearFFT_Quick(data, num5, num2, zLength, direction);
					}
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005A7C File Offset: 0x00003C7C
		public static void FFT3(Complex[] data, int xLength, int yLength, int zLength, FourierDirection direction)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < xLength * yLength * zLength)
			{
				throw new ArgumentOutOfRangeException("data.Length", data.Length, "must be at least as large as 'xLength * yLength * zLength' parameter");
			}
			if (!Fourier.IsPowerOf2(xLength))
			{
				throw new ArgumentOutOfRangeException("xLength", xLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(yLength))
			{
				throw new ArgumentOutOfRangeException("yLength", yLength, "must be a power of 2");
			}
			if (!Fourier.IsPowerOf2(zLength))
			{
				throw new ArgumentOutOfRangeException("zLength", zLength, "must be a power of 2");
			}
			int num = 1;
			int num2 = xLength * yLength;
			if (xLength > 1)
			{
				Fourier.SyncLookupTableLength(xLength);
				for (int i = 0; i < zLength; i++)
				{
					for (int j = 0; j < yLength; j++)
					{
						int num3 = j * xLength + i * num2;
						Fourier.LinearFFT_Quick(data, num3, num, xLength, direction);
					}
				}
			}
			if (yLength > 1)
			{
				Fourier.SyncLookupTableLength(yLength);
				for (int k = 0; k < zLength; k++)
				{
					for (int l = 0; l < xLength; l++)
					{
						int num4 = k * num2 + l * num;
						Fourier.LinearFFT_Quick(data, num4, xLength, yLength, direction);
					}
				}
			}
			if (zLength > 1)
			{
				Fourier.SyncLookupTableLength(zLength);
				for (int m = 0; m < yLength; m++)
				{
					for (int n = 0; n < xLength; n++)
					{
						int num5 = m * xLength + n * num;
						Fourier.LinearFFT_Quick(data, num5, num2, zLength, direction);
					}
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private const int cMaxLength = 4096;

		// Token: 0x04000010 RID: 16
		private const int cMinLength = 1;

		// Token: 0x04000011 RID: 17
		private const int cMaxBits = 12;

		// Token: 0x04000012 RID: 18
		private const int cMinBits = 0;

		// Token: 0x04000013 RID: 19
		private static int[][] _reversedBits = new int[12][];

		// Token: 0x04000014 RID: 20
		private static int[][] _reverseBits = null;

		// Token: 0x04000015 RID: 21
		private static int _lookupTabletLength = -1;

		// Token: 0x04000016 RID: 22
		private static double[,][] _uRLookup = null;

		// Token: 0x04000017 RID: 23
		private static double[,][] _uILookup = null;

		// Token: 0x04000018 RID: 24
		private static float[,][] _uRLookupF = null;

		// Token: 0x04000019 RID: 25
		private static float[,][] _uILookupF = null;

		// Token: 0x0400001A RID: 26
		private static bool _bufferFLocked = false;

		// Token: 0x0400001B RID: 27
		private static float[] _bufferF = new float[0];

		// Token: 0x0400001C RID: 28
		private static bool _bufferCFLocked = false;

		// Token: 0x0400001D RID: 29
		private static ComplexF[] _bufferCF = new ComplexF[0];

		// Token: 0x0400001E RID: 30
		private static bool _bufferCLocked = false;

		// Token: 0x0400001F RID: 31
		private static Complex[] _bufferC = new Complex[0];
	}
}
