// Decompiled with JetBrains decompiler
// Type: WaterMark.WaterMark
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace WaterMark;

public class WaterMark : Form
{
  private Bitmap OriginalImage;
  private Bitmap EncodeImage;
  private IContainer components;
  private PictureBox pic_Picture;
  private Button btn_Open;
  private Label lb_Text;
  private TextBox tb_WaterMarkText;
  private TextBox tb_WaterMarkPicture;
  private Label lb_Picture;
  private RadioButton rd_Text;
  private RadioButton rd_Picture;
  private GroupBox groupBox_Add;
  private Button btn_WaterMark;
  private Button btn_Add;
  private Button btn_SaveAs;
  private Button btn_Get;
  private OpenFileDialog openFileDialog1;
  private TrackBar trackBar_AddScale;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel lb_Status;
  private Label lb_GetScale;
  private Label lb_AddScale;
  private Label label2;
  private Label label1;
  private TrackBar trackBar_GetScale;
  private MenuStrip menuStrip1;
  private ToolStripMenuItem 帮助HToolStripMenuItem;
  private ToolStripMenuItem 关于AToolStripMenuItem;
  private TableLayoutPanel table_Button;
  private TableLayoutPanel tableLayoutPanel2;
  private ProgressBar progressBar1;
  private Button btn_Font;
  private FontDialog fontDialog1;
  private SaveFileDialog saveFileDialog1;
  private GroupBox groupBox_Get;
  private RadioButton rd_Hidden;
  private RadioButton rd_General;
  private Label label3;
  private Panel panel1;
  private Panel panel2;
  private Label label4;
  private Label lb_Angle;
  private Label label5;
  private TrackBar trackBar_Angle;

  public WaterMark() => this.InitializeComponent();

  private void rd_Text_CheckedChanged(object sender, EventArgs e)
  {
    this.lb_Text.Enabled = true;
    this.tb_WaterMarkText.Enabled = true;
    this.btn_Font.Enabled = true;
    this.lb_Picture.Enabled = false;
    this.tb_WaterMarkPicture.Enabled = false;
    this.btn_WaterMark.Enabled = false;
  }

  private void rd_Picture_CheckedChanged(object sender, EventArgs e)
  {
    this.lb_Text.Enabled = false;
    this.tb_WaterMarkText.Enabled = false;
    this.btn_Font.Enabled = false;
    this.lb_Picture.Enabled = true;
    this.tb_WaterMarkPicture.Enabled = true;
    this.btn_WaterMark.Enabled = true;
  }

  private void btn_WaterMark_Click(object sender, EventArgs e)
  {
    this.openFileDialog1.Title = "打开图片";
    this.openFileDialog1.Filter = "图像文件(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|所有文件(*.*)|*.*";
    this.openFileDialog1.InitialDirectory = Application.StartupPath;
    this.openFileDialog1.FileName = "";
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    this.tb_WaterMarkPicture.Text = this.openFileDialog1.FileName;
  }

  private void btn_Open_Click(object sender, EventArgs e)
  {
    this.openFileDialog1.Title = "打开图片";
    this.openFileDialog1.Filter = "图像文件(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|所有文件(*.*)|*.*";
    this.openFileDialog1.InitialDirectory = Application.StartupPath;
    this.openFileDialog1.FileName = "";
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    Bitmap bitmap = new Bitmap(this.openFileDialog1.FileName);
    this.fontDialog1.Font = new Font(this.fontDialog1.Font.FontFamily, (float) (bitmap.Width / 40));
    this.OriginalImage = bitmap;
    this.EncodeImage = bitmap;
    this.pic_Picture.Image = (Image) this.OriginalImage;
    this.btn_Add.Enabled = true;
    this.btn_Get.Enabled = true;
    this.btn_SaveAs.Enabled = true;
    this.lb_Status.Text = "打开成功";
  }

  private void btn_Add_Click(object sender, EventArgs e)
  {
    new Thread(new ThreadStart(this.AddWaterMark))
    {
      IsBackground = true
    }.Start();
  }

  private void btn_Get_Click(object sender, EventArgs e)
  {
    new Thread(new ThreadStart(this.GetWaterMark))
    {
      IsBackground = true
    }.Start();
  }

  private void btn_SaveAs_Click(object sender, EventArgs e)
  {
    this.saveFileDialog1.Title = "保存图片";
    this.saveFileDialog1.Filter = "BMP文件(*.BMP)|*.BMP|JPG文件(*.JPG)|*.JPG|PNG文件(*.PNG)|*.PNG";
    if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    Image image = this.pic_Picture.Image;
    ImageFormat format = ImageFormat.Bmp;
    if (this.saveFileDialog1.FilterIndex == 1)
      format = ImageFormat.Bmp;
    else if (this.saveFileDialog1.FilterIndex == 2)
      format = ImageFormat.Jpeg;
    else if (this.saveFileDialog1.FilterIndex == 3)
      format = ImageFormat.Png;
    image.Save(this.saveFileDialog1.FileName, format);
    this.lb_Status.Text = "保存成功";
  }

  private void AddWaterMark()
  {
    try
    {
      this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) false);
      if (this.rd_Text.Checked)
      {
        if (this.tb_WaterMarkText.Text == "")
        {
          this.Invoke((Delegate) new WaterMark.WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), (object) "请输入水印文字！", (object) "提示", (object) MessageBoxIcon.Asterisk);
          this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
          return;
        }
      }
      else if (this.tb_WaterMarkPicture.Text == "")
      {
        this.Invoke((Delegate) new WaterMark.WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), (object) "请选择水印图片！", (object) "提示", (object) MessageBoxIcon.Asterisk);
        this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
        return;
      }
      if (this.rd_General.Checked)
        this.AddWaterMarkGeneral();
      else
        this.AddWaterMarkHidden();
    }
    catch (Exception ex)
    {
      this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 100, (object) "添加失败");
      this.Invoke((Delegate) new WaterMark.WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), (object) ex.Message, (object) "错误", (object) MessageBoxIcon.Hand);
    }
  }

  private void AddWaterMarkGeneral()
  {
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 1, (object) "正在处理");
    Rectangle rect = new Rectangle(0, 0, this.OriginalImage.Width, this.OriginalImage.Height);
    Bitmap bitmap1 = new Bitmap((Image) this.OriginalImage);
    if (this.rd_Text.Checked)
    {
      Graphics graphics = Graphics.FromImage((Image) bitmap1);
      int num1 = (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Angle));
      double num2 = (double) num1 / 180.0 * Math.PI;
      float dx = (float) ((double) (rect.Width / 2) - (double) (rect.Width / 2) * Math.Cos(num2) - (double) (rect.Height / 2) * Math.Sin(num2));
      float dy = (float) ((double) (rect.Height / 2) + (double) (rect.Width / 2) * Math.Sin(num2) - (double) (rect.Height / 2) * Math.Cos(num2));
      graphics.TranslateTransform(dx, dy);
      graphics.RotateTransform((float) (360 - num1));
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 50, (object) "正在处理 50%");
      SizeF sizeF = graphics.MeasureString(this.tb_WaterMarkText.Text, this.fontDialog1.Font);
      SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int) ((double) (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add)) / 100.0 * (double) byte.MaxValue), (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      for (int x = -rect.Width / 3; x < rect.Width + rect.Width / 3; x += (int) ((double) sizeF.Width + (double) (rect.Width / 5)))
      {
        for (int y = -rect.Height / 3; y < rect.Height + rect.Height / 3; y += (int) ((double) sizeF.Height + (double) (rect.Height / 5)))
          graphics.DrawString(this.tb_WaterMarkText.Text, this.fontDialog1.Font, (Brush) solidBrush, (float) x, (float) y);
      }
    }
    else
    {
      BitmapData bitmapdata1 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0_1 = bitmapdata1.Scan0;
      int length = rect.Width * rect.Height * 4;
      byte[] numArray = new byte[length];
      Marshal.Copy(scan0_1, numArray, 0, length);
      bitmap1.UnlockBits(bitmapdata1);
      Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height);
      Bitmap bitmap3 = new Bitmap(this.tb_WaterMarkPicture.Text);
      Graphics graphics = Graphics.FromImage((Image) bitmap2);
      int num3 = (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Angle));
      double num4 = (double) num3 / 180.0 * Math.PI;
      float dx = (float) ((double) (rect.Width / 2) - (double) (rect.Width / 2) * Math.Cos(num4) - (double) (rect.Height / 2) * Math.Sin(num4));
      float dy = (float) ((double) (rect.Height / 2) + (double) (rect.Width / 2) * Math.Sin(num4) - (double) (rect.Height / 2) * Math.Cos(num4));
      graphics.TranslateTransform(dx, dy);
      graphics.RotateTransform((float) (360 - num3));
      for (int x = -bitmap3.Width; x < rect.Width + bitmap3.Width * 2; x += bitmap3.Width)
      {
        for (int y = -bitmap3.Height; y < rect.Height + bitmap3.Height * 2; y += bitmap3.Height)
          graphics.DrawImage((Image) bitmap3, x, y, bitmap3.Width, bitmap3.Height);
      }
      bitmap3.Dispose();
      BitmapData bitmapdata2 = bitmap2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0_2 = bitmapdata2.Scan0;
      byte[] destination = new byte[length];
      Marshal.Copy(scan0_2, destination, 0, length);
      bitmap2.UnlockBits(bitmapdata2);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 50, (object) "正在处理 50%");
      float num5 = (float) (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add)) / 100f;
      for (int index = 0; index < length; index += 4)
      {
        if (destination[index + 3] != (byte) 0)
        {
          numArray[index] = (byte) ((double) numArray[index] * (1.0 - (double) num5) + (double) destination[index] * (double) num5);
          numArray[index + 1] = (byte) ((double) numArray[index + 1] * (1.0 - (double) num5) + (double) destination[index + 1] * (double) num5);
          numArray[index + 2] = (byte) ((double) numArray[index + 2] * (1.0 - (double) num5) + (double) destination[index + 2] * (double) num5);
        }
      }
      BitmapData bitmapdata3 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0_3 = bitmapdata3.Scan0;
      Marshal.Copy(numArray, 0, scan0_3, length);
      bitmap1.UnlockBits(bitmapdata3);
    }
    this.EncodeImage = bitmap1;
    this.Invoke((Delegate) new WaterMark.WaterMark.ShowImageDelegate(this.ShowImageCallBack), (object) bitmap1);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 100, (object) "添加成功");
  }

  private void AddWaterMarkHidden()
  {
    Size size = new Size((int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) this.OriginalImage.Width, 2.0))), (int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) this.OriginalImage.Height, 2.0))));
    if (size != this.OriginalImage.Size)
    {
      if ((DialogResult) this.Invoke((Delegate) new WaterMark.WaterMark.ShowDialogDelegate(this.ShowDialogCallBack), (object) "添加盲水印需要调整图片大小，是否继续？", (object) "提示") == DialogResult.OK)
      {
        this.OriginalImage = new Bitmap((Image) this.OriginalImage, size.Width, size.Height);
      }
      else
      {
        this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
        return;
      }
    }
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 1, (object) "正在处理");
    Rectangle rect = new Rectangle(0, 0, this.OriginalImage.Width, this.OriginalImage.Height);
    BitmapData bitmapdata1 = this.OriginalImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    IntPtr scan0_1 = bitmapdata1.Scan0;
    int length = rect.Width * rect.Height * 4;
    byte[] numArray = new byte[length];
    Marshal.Copy(scan0_1, numArray, 0, length);
    this.OriginalImage.UnlockBits(bitmapdata1);
    Bitmap bitmap1 = new Bitmap(rect.Width, rect.Height);
    if (this.rd_Text.Checked)
    {
      Graphics graphics = Graphics.FromImage((Image) bitmap1);
      graphics.FillRectangle(Brushes.Black, 0, 0, rect.Width, rect.Height);
      SizeF sizeF = graphics.MeasureString(this.tb_WaterMarkText.Text, this.fontDialog1.Font);
      float x = (float) (rect.Width / 2) - sizeF.Width / 2f;
      float y = (float) (rect.Height / 4) - sizeF.Height / 2f;
      graphics.DrawString(this.tb_WaterMarkText.Text, this.fontDialog1.Font, Brushes.White, x, y);
    }
    else
    {
      Graphics graphics = Graphics.FromImage((Image) bitmap1);
      graphics.FillRectangle(Brushes.Black, 0, 0, rect.Width, rect.Height);
      Bitmap original = new Bitmap(this.tb_WaterMarkPicture.Text);
      int width = original.Width;
      int height = original.Height;
      if (this.Width > rect.Width)
      {
        width = rect.Width;
        height = width * original.Height / original.Width;
      }
      if (height > rect.Height / 2)
      {
        height = rect.Height / 2;
        width = height * original.Width / original.Height;
      }
      if (original.Width != width || original.Height != height)
        original = new Bitmap((Image) original, width, height);
      graphics.DrawImage((Image) original, 0, 0);
      original.Dispose();
    }
    BitmapData bitmapdata2 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    IntPtr scan0_2 = bitmapdata2.Scan0;
    byte[] destination = new byte[length];
    Marshal.Copy(scan0_2, destination, 0, length);
    bitmap1.UnlockBits(bitmapdata2);
    for (int index = 0; index < length / 2; index += 4)
    {
      destination[length - index - 4] = destination[index];
      destination[length - index - 3] = destination[index + 1];
      destination[length - index - 2] = destination[index + 2];
      destination[length - index - 1] = destination[index + 3];
    }
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 10, (object) "正在处理 10%");
    ComplexF[] data1 = new ComplexF[length / 4];
    ComplexF[] data2 = new ComplexF[length / 4];
    ComplexF[] data3 = new ComplexF[length / 4];
    for (int index = 0; index < length; index += 4)
    {
      data3[index / 4].Re = (float) numArray[index];
      data2[index / 4].Re = (float) numArray[index + 1];
      data1[index / 4].Re = (float) numArray[index + 2];
    }
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 20, (object) "正在处理 20%");
    Fourier.FFT2(data1, rect.Width, rect.Height, FourierDirection.Forward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 30, (object) "正在处理 30%");
    Fourier.FFT2(data2, rect.Width, rect.Height, FourierDirection.Forward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 40, (object) "正在处理 40%");
    Fourier.FFT2(data3, rect.Width, rect.Height, FourierDirection.Forward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 50, (object) "正在处理 50%");
    int num = (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Add));
    for (int index = 0; index < length; index += 4)
    {
      data3[index / 4] = data3[index / 4] / (float) Math.Sqrt((double) (length / 4)) + (float) ((int) destination[index] * num / 500);
      data2[index / 4] = data2[index / 4] / (float) Math.Sqrt((double) (length / 4)) + (float) ((int) destination[index + 1] * num / 500);
      data1[index / 4] = data1[index / 4] / (float) Math.Sqrt((double) (length / 4)) + (float) ((int) destination[index + 2] * num / 500);
    }
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 60, (object) "正在处理 60%");
    Fourier.FFT2(data1, rect.Width, rect.Height, FourierDirection.Backward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 70, (object) "正在处理 70%");
    Fourier.FFT2(data2, rect.Width, rect.Height, FourierDirection.Backward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 80 /*0x50*/, (object) "正在处理 80%");
    Fourier.FFT2(data3, rect.Width, rect.Height, FourierDirection.Backward);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 90, (object) "正在处理 90%");
    for (int index = 0; index < length; index += 4)
    {
      float val2_1 = (float) Math.Round((double) data3[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
      float val2_2 = (float) Math.Round((double) data2[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
      float val2_3 = (float) Math.Round((double) data1[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
      numArray[index] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) val2_1));
      numArray[index + 1] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) val2_2));
      numArray[index + 2] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) val2_3));
    }
    Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height);
    BitmapData bitmapdata3 = bitmap2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    IntPtr scan0_3 = bitmapdata3.Scan0;
    Marshal.Copy(numArray, 0, scan0_3, length);
    bitmap2.UnlockBits(bitmapdata3);
    this.EncodeImage = bitmap2;
    this.Invoke((Delegate) new WaterMark.WaterMark.ShowImageDelegate(this.ShowImageCallBack), (object) bitmap2);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
    this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 100, (object) "添加成功");
  }

  private void GetWaterMark()
  {
    try
    {
      this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) false);
      Size size = new Size((int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) this.EncodeImage.Width, 2.0))), (int) Math.Pow(2.0, Math.Ceiling(Math.Log((double) this.EncodeImage.Height, 2.0))));
      if (size != this.EncodeImage.Size)
      {
        if ((DialogResult) this.Invoke((Delegate) new WaterMark.WaterMark.ShowDialogDelegate(this.ShowDialogCallBack), (object) "提取盲水印需要调整图片大小，是否继续？", (object) "提示") == DialogResult.OK)
        {
          this.EncodeImage = new Bitmap((Image) this.EncodeImage, size.Width, size.Height);
        }
        else
        {
          this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
          return;
        }
      }
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 1, (object) "正在处理");
      Rectangle rect = new Rectangle(0, 0, this.EncodeImage.Width, this.EncodeImage.Height);
      BitmapData bitmapdata1 = this.EncodeImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0_1 = bitmapdata1.Scan0;
      int length = rect.Width * rect.Height * 4;
      byte[] destination = new byte[length];
      Marshal.Copy(scan0_1, destination, 0, length);
      this.EncodeImage.UnlockBits(bitmapdata1);
      ComplexF[] data1 = new ComplexF[length / 4];
      ComplexF[] data2 = new ComplexF[length / 4];
      ComplexF[] data3 = new ComplexF[length / 4];
      for (int index = 0; index < length; index += 4)
      {
        data3[index / 4].Re = (float) destination[index];
        data2[index / 4].Re = (float) destination[index + 1];
        data1[index / 4].Re = (float) destination[index + 2];
      }
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 10, (object) "正在处理 10%");
      Fourier.FFT2(data1, rect.Width, rect.Height, FourierDirection.Forward);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 30, (object) "正在处理 30%");
      Fourier.FFT2(data2, rect.Width, rect.Height, FourierDirection.Forward);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 50, (object) "正在处理 50%");
      Fourier.FFT2(data3, rect.Width, rect.Height, FourierDirection.Forward);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 70, (object) "正在处理 70%");
      byte[] source = new byte[length];
      int num1 = (int) this.Invoke((Delegate) new WaterMark.WaterMark.GetTrackBarValueDelegate(this.GetTrackBarValue_Get));
      for (int index = 0; index < length; index += 4)
      {
        float num2 = (float) Math.Round((double) data3[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
        float num3 = (float) Math.Round((double) data2[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
        float num4 = (float) Math.Round((double) data1[index / 4].GetModulus() / Math.Sqrt((double) (length / 4)));
        source[index] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) num2 * (double) num1 / 5.0)));
        source[index + 1] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) num3 * (double) num1 / 5.0)));
        source[index + 2] = (byte) Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) num4 * (double) num1 / 5.0)));
        source[index + 3] = destination[index + 3];
      }
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 90, (object) "正在处理 90%");
      Bitmap bitmap = new Bitmap(this.EncodeImage.Width, this.EncodeImage.Height);
      BitmapData bitmapdata2 = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0_2 = bitmapdata2.Scan0;
      Marshal.Copy(source, 0, scan0_2, length);
      bitmap.UnlockBits(bitmapdata2);
      this.Invoke((Delegate) new WaterMark.WaterMark.ShowImageDelegate(this.ShowImageCallBack), (object) bitmap);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 100, (object) "提取成功");
    }
    catch (Exception ex)
    {
      this.Invoke((Delegate) new WaterMark.WaterMark.SetAllControlDelegate(this.SetAllControlCallBack), (object) true);
      this.Invoke((Delegate) new WaterMark.WaterMark.SetStatusDelegate(this.SetStatusCallBack), (object) 100, (object) "提取失败");
      this.Invoke((Delegate) new WaterMark.WaterMark.ShowMessageDelegate(this.ShowMessageCallBack), (object) ex.Message, (object) "错误", (object) MessageBoxIcon.Hand);
    }
  }

  private void ShowImageCallBack(Bitmap bitmap) => this.pic_Picture.Image = (Image) bitmap;

  private void ShowMessageCallBack(string message, string title, MessageBoxIcon icon)
  {
    int num = (int) MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
  }

  private DialogResult ShowDialogCallBack(string message, string title)
  {
    return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
  }

  private void SetAllControlCallBack(bool enable)
  {
    this.groupBox_Add.Enabled = enable;
    this.groupBox_Get.Enabled = enable;
    this.table_Button.Enabled = enable;
  }

  private void SetStatusCallBack(int value, string text)
  {
    if (value == 0 || value == 100)
      this.progressBar1.Visible = false;
    else
      this.progressBar1.Visible = true;
    this.progressBar1.Value = value;
    this.lb_Status.Text = text;
  }

  private int GetTrackBarValue_Add() => this.trackBar_AddScale.Value;

  private int GetTrackBarValue_Get() => this.trackBar_GetScale.Value;

  private int GetTrackBarValue_Angle() => this.trackBar_Angle.Value;

  private void trackBar1_Scroll(object sender, EventArgs e)
  {
    this.lb_AddScale.Text = this.trackBar_AddScale.Value.ToString();
  }

  private void trackBar_GetScale_Scroll(object sender, EventArgs e)
  {
    this.lb_GetScale.Text = this.trackBar_GetScale.Value.ToString();
  }

  private void btn_Font_Click(object sender, EventArgs e)
  {
    int num = (int) this.fontDialog1.ShowDialog();
  }

  private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
  {
    int num = (int) new AboutBox().ShowDialog();
  }

  private void trackBar_Angle_Scroll(object sender, EventArgs e)
  {
    this.lb_Angle.Text = this.trackBar_Angle.Value.ToString();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WaterMark.WaterMark));
    this.pic_Picture = new PictureBox();
    this.btn_Open = new Button();
    this.lb_Text = new Label();
    this.tb_WaterMarkText = new TextBox();
    this.tb_WaterMarkPicture = new TextBox();
    this.lb_Picture = new Label();
    this.rd_Text = new RadioButton();
    this.rd_Picture = new RadioButton();
    this.groupBox_Add = new GroupBox();
    this.label1 = new Label();
    this.lb_Angle = new Label();
    this.label4 = new Label();
    this.label5 = new Label();
    this.trackBar_Angle = new TrackBar();
    this.panel2 = new Panel();
    this.rd_General = new RadioButton();
    this.rd_Hidden = new RadioButton();
    this.panel1 = new Panel();
    this.label3 = new Label();
    this.btn_Font = new Button();
    this.lb_AddScale = new Label();
    this.trackBar_AddScale = new TrackBar();
    this.btn_WaterMark = new Button();
    this.lb_GetScale = new Label();
    this.label2 = new Label();
    this.trackBar_GetScale = new TrackBar();
    this.btn_Add = new Button();
    this.btn_SaveAs = new Button();
    this.btn_Get = new Button();
    this.openFileDialog1 = new OpenFileDialog();
    this.statusStrip1 = new StatusStrip();
    this.lb_Status = new ToolStripStatusLabel();
    this.menuStrip1 = new MenuStrip();
    this.帮助HToolStripMenuItem = new ToolStripMenuItem();
    this.关于AToolStripMenuItem = new ToolStripMenuItem();
    this.table_Button = new TableLayoutPanel();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.groupBox_Get = new GroupBox();
    this.progressBar1 = new ProgressBar();
    this.fontDialog1 = new FontDialog();
    this.saveFileDialog1 = new SaveFileDialog();
    ((ISupportInitialize) this.pic_Picture).BeginInit();
    this.groupBox_Add.SuspendLayout();
    this.trackBar_Angle.BeginInit();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.trackBar_AddScale.BeginInit();
    this.trackBar_GetScale.BeginInit();
    this.statusStrip1.SuspendLayout();
    this.menuStrip1.SuspendLayout();
    this.table_Button.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.groupBox_Get.SuspendLayout();
    this.SuspendLayout();
    this.pic_Picture.BackColor = Color.White;
    this.pic_Picture.BorderStyle = BorderStyle.FixedSingle;
    this.pic_Picture.Dock = DockStyle.Fill;
    this.pic_Picture.Location = new Point(278, 25);
    this.pic_Picture.Name = "pic_Picture";
    this.pic_Picture.Padding = new Padding(0, 0, 10, 0);
    this.pic_Picture.Size = new Size(517, 473);
    this.pic_Picture.SizeMode = PictureBoxSizeMode.Zoom;
    this.pic_Picture.TabIndex = 0;
    this.pic_Picture.TabStop = false;
    this.btn_Open.Location = new Point(360, 9);
    this.btn_Open.Name = "btn_Open";
    this.btn_Open.Size = new Size(100, 30);
    this.btn_Open.TabIndex = 4;
    this.btn_Open.Text = "打开图片";
    this.btn_Open.UseVisualStyleBackColor = true;
    this.btn_Open.Click += new EventHandler(this.btn_Open_Click);
    this.lb_Text.AutoSize = true;
    this.lb_Text.Location = new Point(9, 143);
    this.lb_Text.Name = "lb_Text";
    this.lb_Text.Size = new Size(53, 12);
    this.lb_Text.TabIndex = 5;
    this.lb_Text.Text = "水印文字";
    this.tb_WaterMarkText.Location = new Point(11, 163);
    this.tb_WaterMarkText.Name = "tb_WaterMarkText";
    this.tb_WaterMarkText.Size = new Size(186, 21);
    this.tb_WaterMarkText.TabIndex = 6;
    this.tb_WaterMarkPicture.Enabled = false;
    this.tb_WaterMarkPicture.Location = new Point(8, 215);
    this.tb_WaterMarkPicture.Name = "tb_WaterMarkPicture";
    this.tb_WaterMarkPicture.ReadOnly = true;
    this.tb_WaterMarkPicture.Size = new Size(189, 21);
    this.tb_WaterMarkPicture.TabIndex = 8;
    this.lb_Picture.AutoSize = true;
    this.lb_Picture.Enabled = false;
    this.lb_Picture.Location = new Point(9, 196);
    this.lb_Picture.Name = "lb_Picture";
    this.lb_Picture.Size = new Size(65, 12);
    this.lb_Picture.TabIndex = 7;
    this.lb_Picture.Text = "水印图片：";
    this.rd_Text.AutoSize = true;
    this.rd_Text.Checked = true;
    this.rd_Text.Location = new Point(3, 3);
    this.rd_Text.Name = "rd_Text";
    this.rd_Text.Size = new Size(71, 16 /*0x10*/);
    this.rd_Text.TabIndex = 9;
    this.rd_Text.TabStop = true;
    this.rd_Text.Text = "文字水印";
    this.rd_Text.UseVisualStyleBackColor = true;
    this.rd_Text.CheckedChanged += new EventHandler(this.rd_Text_CheckedChanged);
    this.rd_Picture.AutoSize = true;
    this.rd_Picture.Location = new Point(80 /*0x50*/, 3);
    this.rd_Picture.Name = "rd_Picture";
    this.rd_Picture.Size = new Size(71, 16 /*0x10*/);
    this.rd_Picture.TabIndex = 10;
    this.rd_Picture.Text = "图片水印";
    this.rd_Picture.UseVisualStyleBackColor = true;
    this.rd_Picture.CheckedChanged += new EventHandler(this.rd_Picture_CheckedChanged);
    this.groupBox_Add.Controls.Add((Control) this.label1);
    this.groupBox_Add.Controls.Add((Control) this.lb_Angle);
    this.groupBox_Add.Controls.Add((Control) this.label4);
    this.groupBox_Add.Controls.Add((Control) this.label5);
    this.groupBox_Add.Controls.Add((Control) this.trackBar_Angle);
    this.groupBox_Add.Controls.Add((Control) this.panel2);
    this.groupBox_Add.Controls.Add((Control) this.panel1);
    this.groupBox_Add.Controls.Add((Control) this.label3);
    this.groupBox_Add.Controls.Add((Control) this.btn_Font);
    this.groupBox_Add.Controls.Add((Control) this.lb_AddScale);
    this.groupBox_Add.Controls.Add((Control) this.trackBar_AddScale);
    this.groupBox_Add.Controls.Add((Control) this.btn_WaterMark);
    this.groupBox_Add.Controls.Add((Control) this.tb_WaterMarkText);
    this.groupBox_Add.Controls.Add((Control) this.lb_Text);
    this.groupBox_Add.Controls.Add((Control) this.lb_Picture);
    this.groupBox_Add.Controls.Add((Control) this.tb_WaterMarkPicture);
    this.groupBox_Add.Location = new Point(3, 3);
    this.groupBox_Add.Name = "groupBox_Add";
    this.groupBox_Add.Size = new Size(252, 367);
    this.groupBox_Add.TabIndex = 11;
    this.groupBox_Add.TabStop = false;
    this.groupBox_Add.Text = "添加选项";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 308);
    this.label1.Name = "label1";
    this.label1.Size = new Size(65, 12);
    this.label1.TabIndex = 16 /*0x10*/;
    this.label1.Text = "水印强度：";
    this.lb_Angle.AutoSize = true;
    this.lb_Angle.Location = new Point(211, 271);
    this.lb_Angle.Name = "lb_Angle";
    this.lb_Angle.Size = new Size(11, 12);
    this.lb_Angle.TabIndex = 25;
    this.lb_Angle.Text = "0";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(9, 82);
    this.label4.Name = "label4";
    this.label4.Size = new Size(65, 12);
    this.label4.TabIndex = 22;
    this.label4.Text = "水印来源：";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(7, 249);
    this.label5.Name = "label5";
    this.label5.Size = new Size(65, 12);
    this.label5.TabIndex = 24;
    this.label5.Text = "旋转角度：";
    this.trackBar_Angle.Anchor = AnchorStyles.None;
    this.trackBar_Angle.Location = new Point(9, 263);
    this.trackBar_Angle.Maximum = 360;
    this.trackBar_Angle.Name = "trackBar_Angle";
    this.trackBar_Angle.Size = new Size(196, 45);
    this.trackBar_Angle.TabIndex = 23;
    this.trackBar_Angle.TickStyle = TickStyle.None;
    this.trackBar_Angle.Scroll += new EventHandler(this.trackBar_Angle_Scroll);
    this.panel2.Controls.Add((Control) this.rd_General);
    this.panel2.Controls.Add((Control) this.rd_Hidden);
    this.panel2.Location = new Point(11, 40);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(230, 30);
    this.panel2.TabIndex = 20;
    this.rd_General.AutoSize = true;
    this.rd_General.Location = new Point(3, 3);
    this.rd_General.Name = "rd_General";
    this.rd_General.Size = new Size(71, 16 /*0x10*/);
    this.rd_General.TabIndex = 22;
    this.rd_General.Text = "普通水印";
    this.rd_General.UseVisualStyleBackColor = true;
    this.rd_Hidden.AutoSize = true;
    this.rd_Hidden.Checked = true;
    this.rd_Hidden.Location = new Point(80 /*0x50*/, 3);
    this.rd_Hidden.Name = "rd_Hidden";
    this.rd_Hidden.Size = new Size(59, 16 /*0x10*/);
    this.rd_Hidden.TabIndex = 23;
    this.rd_Hidden.TabStop = true;
    this.rd_Hidden.Text = "盲水印";
    this.rd_Hidden.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.rd_Text);
    this.panel1.Controls.Add((Control) this.rd_Picture);
    this.panel1.Location = new Point(11, 102);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(230, 29);
    this.panel1.TabIndex = 20;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(9, 25);
    this.label3.Name = "label3";
    this.label3.Size = new Size(65, 12);
    this.label3.TabIndex = 21;
    this.label3.Text = "水印类型：";
    this.btn_Font.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
    this.btn_Font.Location = new Point(203, 163);
    this.btn_Font.Name = "btn_Font";
    this.btn_Font.Size = new Size(40, 23);
    this.btn_Font.TabIndex = 20;
    this.btn_Font.Text = "字体";
    this.btn_Font.UseVisualStyleBackColor = true;
    this.btn_Font.Click += new EventHandler(this.btn_Font_Click);
    this.lb_AddScale.AutoSize = true;
    this.lb_AddScale.Location = new Point(211, 329);
    this.lb_AddScale.Name = "lb_AddScale";
    this.lb_AddScale.Size = new Size(17, 12);
    this.lb_AddScale.TabIndex = 18;
    this.lb_AddScale.Text = "50";
    this.trackBar_AddScale.Anchor = AnchorStyles.None;
    this.trackBar_AddScale.Location = new Point(14, 319);
    this.trackBar_AddScale.Maximum = 100;
    this.trackBar_AddScale.Minimum = 1;
    this.trackBar_AddScale.Name = "trackBar_AddScale";
    this.trackBar_AddScale.Size = new Size(196, 45);
    this.trackBar_AddScale.TabIndex = 14;
    this.trackBar_AddScale.TickStyle = TickStyle.None;
    this.trackBar_AddScale.Value = 50;
    this.trackBar_AddScale.Scroll += new EventHandler(this.trackBar1_Scroll);
    this.btn_WaterMark.Enabled = false;
    this.btn_WaterMark.Location = new Point(203, 213);
    this.btn_WaterMark.Name = "btn_WaterMark";
    this.btn_WaterMark.Size = new Size(40, 23);
    this.btn_WaterMark.TabIndex = 11;
    this.btn_WaterMark.Text = "浏览";
    this.btn_WaterMark.UseVisualStyleBackColor = true;
    this.btn_WaterMark.Click += new EventHandler(this.btn_WaterMark_Click);
    this.lb_GetScale.AutoSize = true;
    this.lb_GetScale.Location = new Point(211, 43);
    this.lb_GetScale.Name = "lb_GetScale";
    this.lb_GetScale.Size = new Size(17, 12);
    this.lb_GetScale.TabIndex = 19;
    this.lb_GetScale.Text = "50";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(7, 26);
    this.label2.Name = "label2";
    this.label2.Size = new Size(65, 12);
    this.label2.TabIndex = 17;
    this.label2.Text = "提取亮度：";
    this.trackBar_GetScale.Location = new Point(9, 41);
    this.trackBar_GetScale.Maximum = 100;
    this.trackBar_GetScale.Minimum = 1;
    this.trackBar_GetScale.Name = "trackBar_GetScale";
    this.trackBar_GetScale.Size = new Size(196, 45);
    this.trackBar_GetScale.TabIndex = 15;
    this.trackBar_GetScale.TickStyle = TickStyle.None;
    this.trackBar_GetScale.Value = 50;
    this.trackBar_GetScale.Scroll += new EventHandler(this.trackBar_GetScale_Scroll);
    this.btn_Add.Enabled = false;
    this.btn_Add.Location = new Point(468, 9);
    this.btn_Add.Name = "btn_Add";
    this.btn_Add.Size = new Size(99, 30);
    this.btn_Add.TabIndex = 12;
    this.btn_Add.Text = "添加水印";
    this.btn_Add.UseVisualStyleBackColor = true;
    this.btn_Add.Click += new EventHandler(this.btn_Add_Click);
    this.btn_SaveAs.Enabled = false;
    this.btn_SaveAs.Location = new Point(681, 9);
    this.btn_SaveAs.Name = "btn_SaveAs";
    this.btn_SaveAs.Size = new Size(100, 30);
    this.btn_SaveAs.TabIndex = 13;
    this.btn_SaveAs.Text = "图片另存为";
    this.btn_SaveAs.UseVisualStyleBackColor = true;
    this.btn_SaveAs.Click += new EventHandler(this.btn_SaveAs_Click);
    this.btn_Get.Enabled = false;
    this.btn_Get.Location = new Point(573, 9);
    this.btn_Get.Name = "btn_Get";
    this.btn_Get.Size = new Size(100, 30);
    this.btn_Get.TabIndex = 14;
    this.btn_Get.Text = "提取盲水印";
    this.btn_Get.UseVisualStyleBackColor = true;
    this.btn_Get.Click += new EventHandler(this.btn_Get_Click);
    this.openFileDialog1.FileName = "openFileDialog1";
    this.statusStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.lb_Status
    });
    this.statusStrip1.Location = new Point(10, 547);
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.Size = new Size(785, 22);
    this.statusStrip1.TabIndex = 15;
    this.statusStrip1.Text = "statusStrip1";
    this.lb_Status.Name = "lb_Status";
    this.lb_Status.Size = new Size(32 /*0x20*/, 17);
    this.lb_Status.Text = "就绪";
    this.menuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.帮助HToolStripMenuItem
    });
    this.menuStrip1.Location = new Point(10, 0);
    this.menuStrip1.Name = "menuStrip1";
    this.menuStrip1.Size = new Size(785, 25);
    this.menuStrip1.TabIndex = 16 /*0x10*/;
    this.menuStrip1.Text = "menuStrip1";
    this.帮助HToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.关于AToolStripMenuItem
    });
    this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
    this.帮助HToolStripMenuItem.Size = new Size(61, 21);
    this.帮助HToolStripMenuItem.Text = "帮助(&H)";
    this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
    this.关于AToolStripMenuItem.Size = new Size(116, 22);
    this.关于AToolStripMenuItem.Text = "关于(&A)";
    this.关于AToolStripMenuItem.Click += new EventHandler(this.关于AToolStripMenuItem_Click);
    this.table_Button.ColumnCount = 5;
    this.table_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.table_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108f));
    this.table_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105f));
    this.table_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108f));
    this.table_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 107f));
    this.table_Button.Controls.Add((Control) this.btn_SaveAs, 4, 0);
    this.table_Button.Controls.Add((Control) this.btn_Get, 3, 0);
    this.table_Button.Controls.Add((Control) this.btn_Add, 2, 0);
    this.table_Button.Controls.Add((Control) this.btn_Open, 1, 0);
    this.table_Button.Dock = DockStyle.Bottom;
    this.table_Button.Location = new Point(10, 498);
    this.table_Button.Name = "table_Button";
    this.table_Button.Padding = new Padding(0, 6, 0, 6);
    this.table_Button.RowCount = 1;
    this.table_Button.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.table_Button.Size = new Size(785, 49);
    this.table_Button.TabIndex = 17;
    this.tableLayoutPanel2.ColumnCount = 1;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Controls.Add((Control) this.groupBox_Add, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this.groupBox_Get, 0, 1);
    this.tableLayoutPanel2.Dock = DockStyle.Left;
    this.tableLayoutPanel2.Location = new Point(10, 25);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 2;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 383f));
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 167f));
    this.tableLayoutPanel2.Size = new Size(268, 473);
    this.tableLayoutPanel2.TabIndex = 18;
    this.groupBox_Get.Controls.Add((Control) this.label2);
    this.groupBox_Get.Controls.Add((Control) this.lb_GetScale);
    this.groupBox_Get.Controls.Add((Control) this.trackBar_GetScale);
    this.groupBox_Get.Location = new Point(3, 386);
    this.groupBox_Get.Name = "groupBox_Get";
    this.groupBox_Get.Size = new Size(252, 88);
    this.groupBox_Get.TabIndex = 12;
    this.groupBox_Get.TabStop = false;
    this.groupBox_Get.Text = "提取选项";
    this.progressBar1.Anchor = AnchorStyles.None;
    this.progressBar1.Location = new Point(436, 248);
    this.progressBar1.Name = "progressBar1";
    this.progressBar1.Size = new Size(220, 18);
    this.progressBar1.TabIndex = 19;
    this.progressBar1.Visible = false;
    this.fontDialog1.Font = new Font("黑体", 20.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
    this.AutoScaleDimensions = new SizeF(6f, 12f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(805, 569);
    this.Controls.Add((Control) this.progressBar1);
    this.Controls.Add((Control) this.pic_Picture);
    this.Controls.Add((Control) this.tableLayoutPanel2);
    this.Controls.Add((Control) this.table_Button);
    this.Controls.Add((Control) this.statusStrip1);
    this.Controls.Add((Control) this.menuStrip1);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MainMenuStrip = this.menuStrip1;
    this.MinimumSize = new Size(821, 608);
    this.Name = nameof (WaterMark);
    this.Padding = new Padding(10, 0, 10, 0);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "WaterMark--吾爱破解论坛首发";
    ((ISupportInitialize) this.pic_Picture).EndInit();
    this.groupBox_Add.ResumeLayout(false);
    this.groupBox_Add.PerformLayout();
    this.trackBar_Angle.EndInit();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.trackBar_AddScale.EndInit();
    this.trackBar_GetScale.EndInit();
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.menuStrip1.ResumeLayout(false);
    this.menuStrip1.PerformLayout();
    this.table_Button.ResumeLayout(false);
    this.tableLayoutPanel2.ResumeLayout(false);
    this.groupBox_Get.ResumeLayout(false);
    this.groupBox_Get.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private delegate void ShowImageDelegate(Bitmap bitmap);

  private delegate void SetAllControlDelegate(bool enable);

  private delegate void SetStatusDelegate(int value, string text);

  private delegate void ShowMessageDelegate(string message, string title, MessageBoxIcon icon);

  private delegate DialogResult ShowDialogDelegate(string message, string title);

  private delegate int GetTrackBarValueDelegate();
}
