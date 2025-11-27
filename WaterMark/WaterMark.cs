using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WaterMark
{
	// Token: 0x0200000A RID: 10
	public partial class WaterMark : Form
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005C4C File Offset: 0x00003E4C
		public WaterMark()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005C5C File Offset: 0x00003E5C
		private void rd_Text_CheckedChanged(object sender, EventArgs e)
		{
			this.lb_Text.Enabled = true;
			this.tb_WaterMarkText.Enabled = true;
			this.btn_Font.Enabled = true;
			this.lb_Picture.Enabled = false;
			this.tb_WaterMarkPicture.Enabled = false;
			this.btn_WaterMark.Enabled = false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005CB4 File Offset: 0x00003EB4
		private void rd_Picture_CheckedChanged(object sender, EventArgs e)
		{
			this.lb_Text.Enabled = false;
			this.tb_WaterMarkText.Enabled = false;
			this.btn_Font.Enabled = false;
			this.lb_Picture.Enabled = true;
			this.tb_WaterMarkPicture.Enabled = true;
			this.btn_WaterMark.Enabled = true;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005D0C File Offset: 0x00003F0C
		private void btn_WaterMark_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Title = "打开图片";
			this.openFileDialog1.Filter = "图像文件(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|所有文件(*.*)|*.*";
			this.openFileDialog1.InitialDirectory = Application.StartupPath;
			this.openFileDialog1.FileName = "";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				this.tb_WaterMarkPicture.Text = this.openFileDialog1.FileName;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005D80 File Offset: 0x00003F80
		private void btn_Open_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Title = "打开图片";
			this.openFileDialog1.Filter = "图像文件(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|所有文件(*.*)|*.*";
			this.openFileDialog1.InitialDirectory = Application.StartupPath;
			this.openFileDialog1.FileName = "";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Bitmap bitmap = new Bitmap(this.openFileDialog1.FileName);
				this.fontDialog1.Font = new Font(this.fontDialog1.Font.FontFamily, (float)(bitmap.Width / 40));
				this.OriginalImage = bitmap;
				this.EncodeImage = bitmap;
				this.pic_Picture.Image = this.OriginalImage;
				this.btn_Add.Enabled = true;
				this.btn_Get.Enabled = true;
				this.btn_SaveAs.Enabled = true;
				this.lb_Status.Text = "打开成功";
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005E6C File Offset: 0x0000406C
		private void btn_Add_Click(object sender, EventArgs e)
		{
			new Thread(new ThreadStart(this.AddWaterMark))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005E98 File Offset: 0x00004098
		private void btn_Get_Click(object sender, EventArgs e)
		{
			new Thread(new ThreadStart(this.GetWaterMark))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005EC4 File Offset: 0x000040C4
		private void btn_SaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog1.Title = "保存图片";
			this.saveFileDialog1.Filter = "BMP文件(*.BMP)|*.BMP|JPG文件(*.JPG)|*.JPG|PNG文件(*.PNG)|*.PNG";
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Image image = this.pic_Picture.Image;
				ImageFormat imageFormat = ImageFormat.Bmp;
				if (this.saveFileDialog1.FilterIndex == 1)
				{
					imageFormat = ImageFormat.Bmp;
				}
				else if (this.saveFileDialog1.FilterIndex == 2)
				{
					imageFormat = ImageFormat.Jpeg;
				}
				else if (this.saveFileDialog1.FilterIndex == 3)
				{
					imageFormat = ImageFormat.Png;
				}
				image.Save(this.saveFileDialog1.FileName, imageFormat);
				this.lb_Status.Text = "保存成功";
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005F74 File Offset: 0x00004174
		private void AddWaterMark()
		{
			try
			{
				base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { false });
				if (this.rd_Text.Checked)
				{
					if (this.tb_WaterMarkText.Text == "")
					{
						base.Invoke(new WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), new object[]
						{
							"请输入水印文字！",
							"提示",
							MessageBoxIcon.Asterisk
						});
						base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
						return;
					}
				}
				else if (this.tb_WaterMarkPicture.Text == "")
				{
					base.Invoke(new WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), new object[]
					{
						"请选择水印图片！",
						"提示",
						MessageBoxIcon.Asterisk
					});
					base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
					return;
				}
				if (this.rd_General.Checked)
				{
					this.AddWaterMarkGeneral();
				}
				else
				{
					this.AddWaterMarkHidden();
				}
			}
			catch (Exception ex)
			{
				base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 100, "添加失败" });
				base.Invoke(new WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), new object[]
				{
					ex.Message,
					"错误",
					MessageBoxIcon.Hand
				});
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000617C File Offset: 0x0000437C
		private void AddWaterMarkGeneral()
		{
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 1, "正在处理" });
			Rectangle rectangle = new Rectangle(0, 0, this.OriginalImage.Width, this.OriginalImage.Height);
			Bitmap bitmap = new Bitmap(this.OriginalImage);
			if (this.rd_Text.Checked)
			{
				Graphics graphics = Graphics.FromImage(bitmap);
				int num = (int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Angle));
				double num2 = (double)num / 180.0 * 3.1415926535897931;
				float num3 = (float)((double)(rectangle.Width / 2) - (double)(rectangle.Width / 2) * Math.Cos(num2) - (double)(rectangle.Height / 2) * Math.Sin(num2));
				float num4 = (float)((double)(rectangle.Height / 2) + (double)(rectangle.Width / 2) * Math.Sin(num2) - (double)(rectangle.Height / 2) * Math.Cos(num2));
				graphics.TranslateTransform(num3, num4);
				graphics.RotateTransform((float)(360 - num));
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 50, "正在处理 50%" });
				SizeF sizeF = graphics.MeasureString(this.tb_WaterMarkText.Text, this.fontDialog1.Font);
				float num5 = (float)((int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add)));
				SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)(num5 / 100f * 255f), 255, 255, 255));
				for (int i = -rectangle.Width / 3; i < rectangle.Width + rectangle.Width / 3; i += (int)(sizeF.Width + (float)(rectangle.Width / 5)))
				{
					for (int j = -rectangle.Height / 3; j < rectangle.Height + rectangle.Height / 3; j += (int)(sizeF.Height + (float)(rectangle.Height / 5)))
					{
						graphics.DrawString(this.tb_WaterMarkText.Text, this.fontDialog1.Font, solidBrush, (float)i, (float)j);
					}
				}
			}
			else
			{
				BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				IntPtr scan = bitmapData.Scan0;
				int num6 = rectangle.Width * rectangle.Height * 4;
				byte[] array = new byte[num6];
				Marshal.Copy(scan, array, 0, num6);
				bitmap.UnlockBits(bitmapData);
				Bitmap bitmap2 = new Bitmap(rectangle.Width, rectangle.Height);
				Bitmap bitmap3 = new Bitmap(this.tb_WaterMarkPicture.Text);
				Graphics graphics2 = Graphics.FromImage(bitmap2);
				int num7 = (int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Angle));
				double num8 = (double)num7 / 180.0 * 3.1415926535897931;
				float num9 = (float)((double)(rectangle.Width / 2) - (double)(rectangle.Width / 2) * Math.Cos(num8) - (double)(rectangle.Height / 2) * Math.Sin(num8));
				float num10 = (float)((double)(rectangle.Height / 2) + (double)(rectangle.Width / 2) * Math.Sin(num8) - (double)(rectangle.Height / 2) * Math.Cos(num8));
				graphics2.TranslateTransform(num9, num10);
				graphics2.RotateTransform((float)(360 - num7));
				for (int k = -bitmap3.Width; k < rectangle.Width + bitmap3.Width * 2; k += bitmap3.Width)
				{
					for (int l = -bitmap3.Height; l < rectangle.Height + bitmap3.Height * 2; l += bitmap3.Height)
					{
						graphics2.DrawImage(bitmap3, k, l, bitmap3.Width, bitmap3.Height);
					}
				}
				bitmap3.Dispose();
				BitmapData bitmapData2 = bitmap2.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				IntPtr scan2 = bitmapData2.Scan0;
				byte[] array2 = new byte[num6];
				Marshal.Copy(scan2, array2, 0, num6);
				bitmap2.UnlockBits(bitmapData2);
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 50, "正在处理 50%" });
				float num11 = (float)((int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add)));
				float num12 = num11 / 100f;
				for (int m = 0; m < num6; m += 4)
				{
					if (array2[m + 3] != 0)
					{
						array[m] = (byte)((float)array[m] * (1f - num12) + (float)array2[m] * num12);
						array[m + 1] = (byte)((float)array[m + 1] * (1f - num12) + (float)array2[m + 1] * num12);
						array[m + 2] = (byte)((float)array[m + 2] * (1f - num12) + (float)array2[m + 2] * num12);
					}
				}
				BitmapData bitmapData3 = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				IntPtr scan3 = bitmapData3.Scan0;
				Marshal.Copy(array, 0, scan3, num6);
				bitmap.UnlockBits(bitmapData3);
			}
			this.EncodeImage = bitmap;
			base.Invoke(new WaterMark.ShowImageDelegate(this.ShowImageCallBack), new object[] { bitmap });
			base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 100, "添加成功" });
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006744 File Offset: 0x00004944
		private void AddWaterMarkHidden()
		{
			Size size = new Size((int)Math.Pow(2.0, Math.Ceiling(Math.Log((double)this.OriginalImage.Width, 2.0))), (int)Math.Pow(2.0, Math.Ceiling(Math.Log((double)this.OriginalImage.Height, 2.0))));
			if (size != this.OriginalImage.Size)
			{
				DialogResult dialogResult = (DialogResult)base.Invoke(new WaterMark.ShowDialogDelegate(this.ShowDialogCallBack), new object[] { "添加盲水印需要调整图片大小，是否继续？", "提示" });
				if (dialogResult != DialogResult.OK)
				{
					base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
					return;
				}
				this.OriginalImage = new Bitmap(this.OriginalImage, size.Width, size.Height);
			}
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 1, "正在处理" });
			Rectangle rectangle = new Rectangle(0, 0, this.OriginalImage.Width, this.OriginalImage.Height);
			BitmapData bitmapData = this.OriginalImage.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			IntPtr scan = bitmapData.Scan0;
			int num = rectangle.Width * rectangle.Height * 4;
			byte[] array = new byte[num];
			Marshal.Copy(scan, array, 0, num);
			this.OriginalImage.UnlockBits(bitmapData);
			Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
			if (this.rd_Text.Checked)
			{
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.FillRectangle(Brushes.Black, 0, 0, rectangle.Width, rectangle.Height);
				SizeF sizeF = graphics.MeasureString(this.tb_WaterMarkText.Text, this.fontDialog1.Font);
				float num2 = (float)(rectangle.Width / 2) - sizeF.Width / 2f;
				float num3 = (float)(rectangle.Height / 4) - sizeF.Height / 2f;
				graphics.DrawString(this.tb_WaterMarkText.Text, this.fontDialog1.Font, Brushes.White, num2, num3);
			}
			else
			{
				Graphics graphics2 = Graphics.FromImage(bitmap);
				graphics2.FillRectangle(Brushes.Black, 0, 0, rectangle.Width, rectangle.Height);
				Bitmap bitmap2 = new Bitmap(this.tb_WaterMarkPicture.Text);
				int num4 = bitmap2.Width;
				int num5 = bitmap2.Height;
				if (base.Width > rectangle.Width)
				{
					num4 = rectangle.Width;
					num5 = num4 * bitmap2.Height / bitmap2.Width;
				}
				if (num5 > rectangle.Height / 2)
				{
					num5 = rectangle.Height / 2;
					num4 = num5 * bitmap2.Width / bitmap2.Height;
				}
				if (bitmap2.Width != num4 || bitmap2.Height != num5)
				{
					bitmap2 = new Bitmap(bitmap2, num4, num5);
				}
				graphics2.DrawImage(bitmap2, 0, 0);
				bitmap2.Dispose();
			}
			BitmapData bitmapData2 = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			IntPtr scan2 = bitmapData2.Scan0;
			byte[] array2 = new byte[num];
			Marshal.Copy(scan2, array2, 0, num);
			bitmap.UnlockBits(bitmapData2);
			for (int i = 0; i < num / 2; i += 4)
			{
				array2[num - i - 4] = array2[i];
				array2[num - i - 3] = array2[i + 1];
				array2[num - i - 2] = array2[i + 2];
				array2[num - i - 1] = array2[i + 3];
			}
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 10, "正在处理 10%" });
			ComplexF[] array3 = new ComplexF[num / 4];
			ComplexF[] array4 = new ComplexF[num / 4];
			ComplexF[] array5 = new ComplexF[num / 4];
			for (int j = 0; j < num; j += 4)
			{
				array5[j / 4].Re = (float)array[j];
				array4[j / 4].Re = (float)array[j + 1];
				array3[j / 4].Re = (float)array[j + 2];
			}
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 20, "正在处理 20%" });
			Fourier.FFT2(array3, rectangle.Width, rectangle.Height, FourierDirection.Forward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 30, "正在处理 30%" });
			Fourier.FFT2(array4, rectangle.Width, rectangle.Height, FourierDirection.Forward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 40, "正在处理 40%" });
			Fourier.FFT2(array5, rectangle.Width, rectangle.Height, FourierDirection.Forward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 50, "正在处理 50%" });
			int num6 = (int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add));
			for (int k = 0; k < num; k += 4)
			{
				array5[k / 4] = array5[k / 4] / (float)Math.Sqrt((double)(num / 4)) + (float)((int)array2[k] * num6 / 500);
				array4[k / 4] = array4[k / 4] / (float)Math.Sqrt((double)(num / 4)) + (float)((int)array2[k + 1] * num6 / 500);
				array3[k / 4] = array3[k / 4] / (float)Math.Sqrt((double)(num / 4)) + (float)((int)array2[k + 2] * num6 / 500);
			}
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 60, "正在处理 60%" });
			Fourier.FFT2(array3, rectangle.Width, rectangle.Height, FourierDirection.Backward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 70, "正在处理 70%" });
			Fourier.FFT2(array4, rectangle.Width, rectangle.Height, FourierDirection.Backward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 80, "正在处理 80%" });
			Fourier.FFT2(array5, rectangle.Width, rectangle.Height, FourierDirection.Backward);
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 90, "正在处理 90%" });
			for (int l = 0; l < num; l += 4)
			{
				float num7 = (float)Math.Round((double)(array5[l / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
				float num8 = (float)Math.Round((double)(array4[l / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
				float num9 = (float)Math.Round((double)(array3[l / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
				array[l] = (byte)Math.Max(0, Math.Min(255, (int)num7));
				array[l + 1] = (byte)Math.Max(0, Math.Min(255, (int)num8));
				array[l + 2] = (byte)Math.Max(0, Math.Min(255, (int)num9));
			}
			Bitmap bitmap3 = new Bitmap(rectangle.Width, rectangle.Height);
			BitmapData bitmapData3 = bitmap3.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			IntPtr scan3 = bitmapData3.Scan0;
			Marshal.Copy(array, 0, scan3, num);
			bitmap3.UnlockBits(bitmapData3);
			this.EncodeImage = bitmap3;
			base.Invoke(new WaterMark.ShowImageDelegate(this.ShowImageCallBack), new object[] { bitmap3 });
			base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
			base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 100, "添加成功" });
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007064 File Offset: 0x00005264
		private void GetWaterMark()
		{
			try
			{
				base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { false });
				Size size = new Size((int)Math.Pow(2.0, Math.Ceiling(Math.Log((double)this.EncodeImage.Width, 2.0))), (int)Math.Pow(2.0, Math.Ceiling(Math.Log((double)this.EncodeImage.Height, 2.0))));
				if (size != this.EncodeImage.Size)
				{
					DialogResult dialogResult = (DialogResult)base.Invoke(new WaterMark.ShowDialogDelegate(this.ShowDialogCallBack), new object[] { "提取盲水印需要调整图片大小，是否继续？", "提示" });
					if (dialogResult != DialogResult.OK)
					{
						base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
						return;
					}
					this.EncodeImage = new Bitmap(this.EncodeImage, size.Width, size.Height);
				}
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 1, "正在处理" });
				Rectangle rectangle = new Rectangle(0, 0, this.EncodeImage.Width, this.EncodeImage.Height);
				BitmapData bitmapData = this.EncodeImage.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				IntPtr scan = bitmapData.Scan0;
				int num = rectangle.Width * rectangle.Height * 4;
				byte[] array = new byte[num];
				Marshal.Copy(scan, array, 0, num);
				this.EncodeImage.UnlockBits(bitmapData);
				ComplexF[] array2 = new ComplexF[num / 4];
				ComplexF[] array3 = new ComplexF[num / 4];
				ComplexF[] array4 = new ComplexF[num / 4];
				for (int i = 0; i < num; i += 4)
				{
					array4[i / 4].Re = (float)array[i];
					array3[i / 4].Re = (float)array[i + 1];
					array2[i / 4].Re = (float)array[i + 2];
				}
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 10, "正在处理 10%" });
				Fourier.FFT2(array2, rectangle.Width, rectangle.Height, FourierDirection.Forward);
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 30, "正在处理 30%" });
				Fourier.FFT2(array3, rectangle.Width, rectangle.Height, FourierDirection.Forward);
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 50, "正在处理 50%" });
				Fourier.FFT2(array4, rectangle.Width, rectangle.Height, FourierDirection.Forward);
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 70, "正在处理 70%" });
				byte[] array5 = new byte[num];
				int num2 = (int)base.Invoke(new WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Get));
				for (int j = 0; j < num; j += 4)
				{
					float num3 = (float)Math.Round((double)(array4[j / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
					float num4 = (float)Math.Round((double)(array3[j / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
					float num5 = (float)Math.Round((double)(array2[j / 4].GetModulus() / (float)Math.Sqrt((double)(num / 4))));
					array5[j] = (byte)Math.Max(0, Math.Min(255, (int)(num3 * (float)num2 / 5f)));
					array5[j + 1] = (byte)Math.Max(0, Math.Min(255, (int)(num4 * (float)num2 / 5f)));
					array5[j + 2] = (byte)Math.Max(0, Math.Min(255, (int)(num5 * (float)num2 / 5f)));
					array5[j + 3] = array[j + 3];
				}
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 90, "正在处理 90%" });
				Bitmap bitmap = new Bitmap(this.EncodeImage.Width, this.EncodeImage.Height);
				BitmapData bitmapData2 = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				IntPtr scan2 = bitmapData2.Scan0;
				Marshal.Copy(array5, 0, scan2, num);
				bitmap.UnlockBits(bitmapData2);
				base.Invoke(new WaterMark.ShowImageDelegate(this.ShowImageCallBack), new object[] { bitmap });
				base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 100, "提取成功" });
			}
			catch (Exception ex)
			{
				base.Invoke(new WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), new object[] { true });
				base.Invoke(new WaterMark.SetStatusDelegate(this.SetStatusCallBack), new object[] { 100, "提取失败" });
				base.Invoke(new WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), new object[]
				{
					ex.Message,
					"错误",
					MessageBoxIcon.Hand
				});
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007688 File Offset: 0x00005888
		private void ShowImageCallBack(Bitmap bitmap)
		{
			this.pic_Picture.Image = bitmap;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007696 File Offset: 0x00005896
		private void ShowMessageCallBack(string message, string title, MessageBoxIcon icon)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000076A2 File Offset: 0x000058A2
		private DialogResult ShowDialogCallBack(string message, string title)
		{
			return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000076AE File Offset: 0x000058AE
		private void SetAllControlCallBack(bool enable)
		{
			this.groupBox_Add.Enabled = enable;
			this.groupBox_Get.Enabled = enable;
			this.table_Button.Enabled = enable;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000076D4 File Offset: 0x000058D4
		private void SetStatusCallBack(int value, string text)
		{
			if (value == 0 || value == 100)
			{
				this.progressBar1.Visible = false;
			}
			else
			{
				this.progressBar1.Visible = true;
			}
			this.progressBar1.Value = value;
			this.lb_Status.Text = text;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007710 File Offset: 0x00005910
		private int GetTrackBarValue_Add()
		{
			return this.trackBar_AddScale.Value;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000771D File Offset: 0x0000591D
		private int GetTrackBarValue_Get()
		{
			return this.trackBar_GetScale.Value;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000772A File Offset: 0x0000592A
		private int GetTrackBarValue_Angle()
		{
			return this.trackBar_Angle.Value;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007738 File Offset: 0x00005938
		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			this.lb_AddScale.Text = this.trackBar_AddScale.Value.ToString();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007764 File Offset: 0x00005964
		private void trackBar_GetScale_Scroll(object sender, EventArgs e)
		{
			this.lb_GetScale.Text = this.trackBar_GetScale.Value.ToString();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000778F File Offset: 0x0000598F
		private void btn_Font_Click(object sender, EventArgs e)
		{
			this.fontDialog1.ShowDialog();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000077A0 File Offset: 0x000059A0
		private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000077BC File Offset: 0x000059BC
		private void trackBar_Angle_Scroll(object sender, EventArgs e)
		{
			this.lb_Angle.Text = this.trackBar_Angle.Value.ToString();
		}

		// Token: 0x04000023 RID: 35
		private Bitmap OriginalImage;

		// Token: 0x04000024 RID: 36
		private Bitmap EncodeImage;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x060000E6 RID: 230
		private delegate void ShowImageDelegate(Bitmap bitmap);

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x060000EA RID: 234
		private delegate void SetAllControlDelegate(bool enable);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x060000EE RID: 238
		private delegate void SetStatusDelegate(int value, string text);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x060000F2 RID: 242
		private delegate void ShowMessageDelegate(string message, string title, MessageBoxIcon icon);

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x060000F6 RID: 246
		private delegate DialogResult ShowDialogDelegate(string message, string title);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x060000FA RID: 250
		private delegate int GetTrackBarValueDelegate();
	}
}
