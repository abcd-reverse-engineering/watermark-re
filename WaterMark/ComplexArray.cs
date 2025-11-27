using System;

namespace WaterMark
{
	// Token: 0x02000004 RID: 4
	public class ComplexArray
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002D76 File Offset: 0x00000F76
		private ComplexArray()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D80 File Offset: 0x00000F80
		public static void ClampLength(Complex[] array, double fMinimum, double fMaximum)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Complex.FromModulusArgument(Math.Max(fMinimum, Math.Min(fMaximum, array[i].GetModulus())), array[i].GetArgument());
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static void Clamp(Complex[] array, Complex minimum, Complex maximum)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Re = Math.Min(Math.Max(array[i].Re, minimum.Re), maximum.Re);
				array[i].Im = Math.Min(Math.Max(array[i].Re, minimum.Im), maximum.Im);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E4C File Offset: 0x0000104C
		public static void ClampToRealUnit(Complex[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Re = Math.Min(Math.Max(array[i].Re, 0.0), 1.0);
				array[i].Im = 0.0;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002EB0 File Offset: 0x000010B0
		private static void LockWorkspaceF(int length, ref ComplexF[] workspace)
		{
			ComplexArray._workspaceFLocked = true;
			if (length >= ComplexArray._workspaceF.Length)
			{
				ComplexArray._workspaceF = new ComplexF[length];
			}
			workspace = ComplexArray._workspaceF;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002ED4 File Offset: 0x000010D4
		private static void UnlockWorkspaceF(ref ComplexF[] workspace)
		{
			ComplexArray._workspaceFLocked = false;
			workspace = null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002EE0 File Offset: 0x000010E0
		public static void Shift(Complex[] array, int offset)
		{
			if (offset == 0)
			{
				return;
			}
			int num = array.Length;
			Complex[] array2 = new Complex[num];
			for (int i = 0; i < num; i++)
			{
				array2[(i + offset) % num] = array[i];
			}
			for (int j = 0; j < num; j++)
			{
				array[j] = array2[j];
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F48 File Offset: 0x00001148
		public static void Shift(ComplexF[] array, int offset)
		{
			if (offset == 0)
			{
				return;
			}
			int num = array.Length;
			ComplexF[] array2 = null;
			ComplexArray.LockWorkspaceF(num, ref array2);
			for (int i = 0; i < num; i++)
			{
				array2[(i + offset) % num] = array[i];
			}
			for (int j = 0; j < num; j++)
			{
				array[j] = array2[j];
			}
			ComplexArray.UnlockWorkspaceF(ref array2);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FBC File Offset: 0x000011BC
		public static void GetLengthRange(Complex[] array, ref double minimum, ref double maximum)
		{
			minimum = double.MaxValue;
			maximum = double.MinValue;
			for (int i = 0; i < array.Length; i++)
			{
				double modulus = array[i].GetModulus();
				minimum = Math.Min(modulus, minimum);
				maximum = Math.Max(modulus, maximum);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003010 File Offset: 0x00001210
		public static void GetLengthRange(ComplexF[] array, ref float minimum, ref float maximum)
		{
			minimum = float.MaxValue;
			maximum = float.MinValue;
			for (int i = 0; i < array.Length; i++)
			{
				float modulus = array[i].GetModulus();
				minimum = Math.Min(modulus, minimum);
				maximum = Math.Max(modulus, maximum);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000305C File Offset: 0x0000125C
		public static bool IsEqual(Complex[] array1, Complex[] array2, double tolerance)
		{
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!Complex.IsEqual(array1[i], array2[i], tolerance))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000030A4 File Offset: 0x000012A4
		public static bool IsEqual(ComplexF[] array1, ComplexF[] array2, float tolerance)
		{
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!ComplexF.IsEqual(array1[i], array2[i], tolerance))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000030EC File Offset: 0x000012EC
		public static void Offset(Complex[] array, double offset)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				array[num2].Re = array[num2].Re + offset;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003120 File Offset: 0x00001320
		public static void Offset(Complex[] array, Complex offset)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] += offset;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003158 File Offset: 0x00001358
		public static void Offset(ComplexF[] array, float offset)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				array[num2].Re = array[num2].Re + offset;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000318C File Offset: 0x0000138C
		public static void Offset(ComplexF[] array, ComplexF offset)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] += offset;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000031C4 File Offset: 0x000013C4
		public static void Scale(Complex[] array, double scale)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] *= scale;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031FC File Offset: 0x000013FC
		public static void Scale(Complex[] array, double scale, int start, int length)
		{
			for (int i = 0; i < length; i++)
			{
				array[i + start] *= scale;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003230 File Offset: 0x00001430
		public static void Scale(Complex[] array, Complex scale)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] *= scale;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003268 File Offset: 0x00001468
		public static void Scale(Complex[] array, Complex scale, int start, int length)
		{
			for (int i = 0; i < length; i++)
			{
				array[i + start] *= scale;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000329C File Offset: 0x0000149C
		public static void Scale(ComplexF[] array, float scale)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] *= scale;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000032D4 File Offset: 0x000014D4
		public static void Scale(ComplexF[] array, float scale, int start, int length)
		{
			for (int i = 0; i < length; i++)
			{
				array[i + start] *= scale;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003308 File Offset: 0x00001508
		public static void Scale(ComplexF[] array, ComplexF scale)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] *= scale;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003340 File Offset: 0x00001540
		public static void Scale(ComplexF[] array, ComplexF scale, int start, int length)
		{
			for (int i = 0; i < length; i++)
			{
				array[i + start] *= scale;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003373 File Offset: 0x00001573
		public static void Multiply(Complex[] target, Complex[] rhs)
		{
			ComplexArray.Multiply(target, rhs, target);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003380 File Offset: 0x00001580
		public static void Multiply(Complex[] lhs, Complex[] rhs, Complex[] result)
		{
			int num = lhs.Length;
			for (int i = 0; i < num; i++)
			{
				result[i] = lhs[i] * rhs[i];
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000033C6 File Offset: 0x000015C6
		public static void Multiply(ComplexF[] target, ComplexF[] rhs)
		{
			ComplexArray.Multiply(target, rhs, target);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000033D0 File Offset: 0x000015D0
		public static void Multiply(ComplexF[] lhs, ComplexF[] rhs, ComplexF[] result)
		{
			int num = lhs.Length;
			for (int i = 0; i < num; i++)
			{
				result[i] = lhs[i] * rhs[i];
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003416 File Offset: 0x00001616
		public static void Divide(Complex[] target, Complex[] rhs)
		{
			ComplexArray.Divide(target, rhs, target);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003420 File Offset: 0x00001620
		public static void Divide(Complex[] lhs, Complex[] rhs, Complex[] result)
		{
			int num = lhs.Length;
			for (int i = 0; i < num; i++)
			{
				result[i] = lhs[i] / rhs[i];
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003466 File Offset: 0x00001666
		public static void Divide(ComplexF[] target, ComplexF[] rhs)
		{
			ComplexArray.Divide(target, rhs, target);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003470 File Offset: 0x00001670
		public static void Divide(ComplexF[] lhs, ComplexF[] rhs, ComplexF[] result)
		{
			ComplexF zero = ComplexF.Zero;
			int num = lhs.Length;
			for (int i = 0; i < num; i++)
			{
				if (rhs[i] != zero)
				{
					result[i] = lhs[i] / rhs[i];
				}
				else
				{
					result[i] = zero;
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034E0 File Offset: 0x000016E0
		public static void Copy(Complex[] dest, Complex[] source)
		{
			for (int i = 0; i < dest.Length; i++)
			{
				dest[i] = source[i];
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003514 File Offset: 0x00001714
		public static void Copy(ComplexF[] dest, ComplexF[] source)
		{
			for (int i = 0; i < dest.Length; i++)
			{
				dest[i] = source[i];
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003548 File Offset: 0x00001748
		public static void Reverse(Complex[] array)
		{
			int num = array.Length;
			for (int i = 0; i < num / 2; i++)
			{
				Complex complex = array[i];
				array[i] = array[num - 1 - i];
				array[num - 1 - i] = complex;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000035A4 File Offset: 0x000017A4
		public static void Normalize(Complex[] array)
		{
			double num = 0.0;
			double num2 = 0.0;
			ComplexArray.GetLengthRange(array, ref num, ref num2);
			ComplexArray.Scale(array, 1.0 / (num2 - num));
			ComplexArray.Offset(array, -num / (num2 - num));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000035F0 File Offset: 0x000017F0
		public static void Normalize(ComplexF[] array)
		{
			float num = 0f;
			float num2 = 0f;
			ComplexArray.GetLengthRange(array, ref num, ref num2);
			ComplexArray.Scale(array, 1f / (num2 - num));
			ComplexArray.Offset(array, -num / (num2 - num));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003630 File Offset: 0x00001830
		public static void Invert(Complex[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Complex)1.0 / array[i];
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003678 File Offset: 0x00001878
		public static void Invert(ComplexF[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (ComplexF)1f / array[i];
			}
		}

		// Token: 0x0400000A RID: 10
		private static bool _workspaceFLocked = false;

		// Token: 0x0400000B RID: 11
		private static ComplexF[] _workspaceF = new ComplexF[0];
	}
}
