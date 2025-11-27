namespace WaterMark
{
	// Token: 0x0200000A RID: 10
	public partial class WaterMark : global::System.Windows.Forms.Form
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000077E7 File Offset: 0x000059E7
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007808 File Offset: 0x00005A08
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::WaterMark.WaterMark));
			this.pic_Picture = new global::System.Windows.Forms.PictureBox();
			this.btn_Open = new global::System.Windows.Forms.Button();
			this.lb_Text = new global::System.Windows.Forms.Label();
			this.tb_WaterMarkText = new global::System.Windows.Forms.TextBox();
			this.tb_WaterMarkPicture = new global::System.Windows.Forms.TextBox();
			this.lb_Picture = new global::System.Windows.Forms.Label();
			this.rd_Text = new global::System.Windows.Forms.RadioButton();
			this.rd_Picture = new global::System.Windows.Forms.RadioButton();
			this.groupBox_Add = new global::System.Windows.Forms.GroupBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lb_Angle = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.trackBar_Angle = new global::System.Windows.Forms.TrackBar();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.rd_General = new global::System.Windows.Forms.RadioButton();
			this.rd_Hidden = new global::System.Windows.Forms.RadioButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label3 = new global::System.Windows.Forms.Label();
			this.btn_Font = new global::System.Windows.Forms.Button();
			this.lb_AddScale = new global::System.Windows.Forms.Label();
			this.trackBar_AddScale = new global::System.Windows.Forms.TrackBar();
			this.btn_WaterMark = new global::System.Windows.Forms.Button();
			this.lb_GetScale = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.trackBar_GetScale = new global::System.Windows.Forms.TrackBar();
			this.btn_Add = new global::System.Windows.Forms.Button();
			this.btn_SaveAs = new global::System.Windows.Forms.Button();
			this.btn_Get = new global::System.Windows.Forms.Button();
			this.openFileDialog1 = new global::System.Windows.Forms.OpenFileDialog();
			this.statusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.lb_Status = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.帮助HToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.关于AToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.table_Button = new global::System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
			this.groupBox_Get = new global::System.Windows.Forms.GroupBox();
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			this.fontDialog1 = new global::System.Windows.Forms.FontDialog();
			this.saveFileDialog1 = new global::System.Windows.Forms.SaveFileDialog();
			((global::System.ComponentModel.ISupportInitialize)this.pic_Picture).BeginInit();
			this.groupBox_Add.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_Angle).BeginInit();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_AddScale).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_GetScale).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.table_Button.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox_Get.SuspendLayout();
			base.SuspendLayout();
			this.pic_Picture.BackColor = global::System.Drawing.Color.White;
			this.pic_Picture.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.pic_Picture.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pic_Picture.Location = new global::System.Drawing.Point(278, 25);
			this.pic_Picture.Name = "pic_Picture";
			this.pic_Picture.Padding = new global::System.Windows.Forms.Padding(0, 0, 10, 0);
			this.pic_Picture.Size = new global::System.Drawing.Size(517, 473);
			this.pic_Picture.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pic_Picture.TabIndex = 0;
			this.pic_Picture.TabStop = false;
			this.btn_Open.Location = new global::System.Drawing.Point(360, 9);
			this.btn_Open.Name = "btn_Open";
			this.btn_Open.Size = new global::System.Drawing.Size(100, 30);
			this.btn_Open.TabIndex = 4;
			this.btn_Open.Text = "打开图片";
			this.btn_Open.UseVisualStyleBackColor = true;
			this.btn_Open.Click += new global::System.EventHandler(this.btn_Open_Click);
			this.lb_Text.AutoSize = true;
			this.lb_Text.Location = new global::System.Drawing.Point(9, 143);
			this.lb_Text.Name = "lb_Text";
			this.lb_Text.Size = new global::System.Drawing.Size(53, 12);
			this.lb_Text.TabIndex = 5;
			this.lb_Text.Text = "水印文字";
			this.tb_WaterMarkText.Location = new global::System.Drawing.Point(11, 163);
			this.tb_WaterMarkText.Name = "tb_WaterMarkText";
			this.tb_WaterMarkText.Size = new global::System.Drawing.Size(186, 21);
			this.tb_WaterMarkText.TabIndex = 6;
			this.tb_WaterMarkPicture.Enabled = false;
			this.tb_WaterMarkPicture.Location = new global::System.Drawing.Point(8, 215);
			this.tb_WaterMarkPicture.Name = "tb_WaterMarkPicture";
			this.tb_WaterMarkPicture.ReadOnly = true;
			this.tb_WaterMarkPicture.Size = new global::System.Drawing.Size(189, 21);
			this.tb_WaterMarkPicture.TabIndex = 8;
			this.lb_Picture.AutoSize = true;
			this.lb_Picture.Enabled = false;
			this.lb_Picture.Location = new global::System.Drawing.Point(9, 196);
			this.lb_Picture.Name = "lb_Picture";
			this.lb_Picture.Size = new global::System.Drawing.Size(65, 12);
			this.lb_Picture.TabIndex = 7;
			this.lb_Picture.Text = "水印图片：";
			this.rd_Text.AutoSize = true;
			this.rd_Text.Checked = true;
			this.rd_Text.Location = new global::System.Drawing.Point(3, 3);
			this.rd_Text.Name = "rd_Text";
			this.rd_Text.Size = new global::System.Drawing.Size(71, 16);
			this.rd_Text.TabIndex = 9;
			this.rd_Text.TabStop = true;
			this.rd_Text.Text = "文字水印";
			this.rd_Text.UseVisualStyleBackColor = true;
			this.rd_Text.CheckedChanged += new global::System.EventHandler(this.rd_Text_CheckedChanged);
			this.rd_Picture.AutoSize = true;
			this.rd_Picture.Location = new global::System.Drawing.Point(80, 3);
			this.rd_Picture.Name = "rd_Picture";
			this.rd_Picture.Size = new global::System.Drawing.Size(71, 16);
			this.rd_Picture.TabIndex = 10;
			this.rd_Picture.Text = "图片水印";
			this.rd_Picture.UseVisualStyleBackColor = true;
			this.rd_Picture.CheckedChanged += new global::System.EventHandler(this.rd_Picture_CheckedChanged);
			this.groupBox_Add.Controls.Add(this.label1);
			this.groupBox_Add.Controls.Add(this.lb_Angle);
			this.groupBox_Add.Controls.Add(this.label4);
			this.groupBox_Add.Controls.Add(this.label5);
			this.groupBox_Add.Controls.Add(this.trackBar_Angle);
			this.groupBox_Add.Controls.Add(this.panel2);
			this.groupBox_Add.Controls.Add(this.panel1);
			this.groupBox_Add.Controls.Add(this.label3);
			this.groupBox_Add.Controls.Add(this.btn_Font);
			this.groupBox_Add.Controls.Add(this.lb_AddScale);
			this.groupBox_Add.Controls.Add(this.trackBar_AddScale);
			this.groupBox_Add.Controls.Add(this.btn_WaterMark);
			this.groupBox_Add.Controls.Add(this.tb_WaterMarkText);
			this.groupBox_Add.Controls.Add(this.lb_Text);
			this.groupBox_Add.Controls.Add(this.lb_Picture);
			this.groupBox_Add.Controls.Add(this.tb_WaterMarkPicture);
			this.groupBox_Add.Location = new global::System.Drawing.Point(3, 3);
			this.groupBox_Add.Name = "groupBox_Add";
			this.groupBox_Add.Size = new global::System.Drawing.Size(252, 367);
			this.groupBox_Add.TabIndex = 11;
			this.groupBox_Add.TabStop = false;
			this.groupBox_Add.Text = "添加选项";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(9, 308);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(65, 12);
			this.label1.TabIndex = 16;
			this.label1.Text = "水印强度：";
			this.lb_Angle.AutoSize = true;
			this.lb_Angle.Location = new global::System.Drawing.Point(211, 271);
			this.lb_Angle.Name = "lb_Angle";
			this.lb_Angle.Size = new global::System.Drawing.Size(11, 12);
			this.lb_Angle.TabIndex = 25;
			this.lb_Angle.Text = "0";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(9, 82);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(65, 12);
			this.label4.TabIndex = 22;
			this.label4.Text = "水印来源：";
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(7, 249);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(65, 12);
			this.label5.TabIndex = 24;
			this.label5.Text = "旋转角度：";
			this.trackBar_Angle.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.trackBar_Angle.Location = new global::System.Drawing.Point(9, 263);
			this.trackBar_Angle.Maximum = 360;
			this.trackBar_Angle.Name = "trackBar_Angle";
			this.trackBar_Angle.Size = new global::System.Drawing.Size(196, 45);
			this.trackBar_Angle.TabIndex = 23;
			this.trackBar_Angle.TickStyle = global::System.Windows.Forms.TickStyle.None;
			this.trackBar_Angle.Scroll += new global::System.EventHandler(this.trackBar_Angle_Scroll);
			this.panel2.Controls.Add(this.rd_General);
			this.panel2.Controls.Add(this.rd_Hidden);
			this.panel2.Location = new global::System.Drawing.Point(11, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(230, 30);
			this.panel2.TabIndex = 20;
			this.rd_General.AutoSize = true;
			this.rd_General.Location = new global::System.Drawing.Point(3, 3);
			this.rd_General.Name = "rd_General";
			this.rd_General.Size = new global::System.Drawing.Size(71, 16);
			this.rd_General.TabIndex = 22;
			this.rd_General.Text = "普通水印";
			this.rd_General.UseVisualStyleBackColor = true;
			this.rd_Hidden.AutoSize = true;
			this.rd_Hidden.Checked = true;
			this.rd_Hidden.Location = new global::System.Drawing.Point(80, 3);
			this.rd_Hidden.Name = "rd_Hidden";
			this.rd_Hidden.Size = new global::System.Drawing.Size(59, 16);
			this.rd_Hidden.TabIndex = 23;
			this.rd_Hidden.TabStop = true;
			this.rd_Hidden.Text = "盲水印";
			this.rd_Hidden.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.rd_Text);
			this.panel1.Controls.Add(this.rd_Picture);
			this.panel1.Location = new global::System.Drawing.Point(11, 102);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(230, 29);
			this.panel1.TabIndex = 20;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(9, 25);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(65, 12);
			this.label3.TabIndex = 21;
			this.label3.Text = "水印类型：";
			this.btn_Font.Font = new global::System.Drawing.Font("宋体", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 134);
			this.btn_Font.Location = new global::System.Drawing.Point(203, 163);
			this.btn_Font.Name = "btn_Font";
			this.btn_Font.Size = new global::System.Drawing.Size(40, 23);
			this.btn_Font.TabIndex = 20;
			this.btn_Font.Text = "字体";
			this.btn_Font.UseVisualStyleBackColor = true;
			this.btn_Font.Click += new global::System.EventHandler(this.btn_Font_Click);
			this.lb_AddScale.AutoSize = true;
			this.lb_AddScale.Location = new global::System.Drawing.Point(211, 329);
			this.lb_AddScale.Name = "lb_AddScale";
			this.lb_AddScale.Size = new global::System.Drawing.Size(17, 12);
			this.lb_AddScale.TabIndex = 18;
			this.lb_AddScale.Text = "50";
			this.trackBar_AddScale.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.trackBar_AddScale.Location = new global::System.Drawing.Point(14, 319);
			this.trackBar_AddScale.Maximum = 100;
			this.trackBar_AddScale.Minimum = 1;
			this.trackBar_AddScale.Name = "trackBar_AddScale";
			this.trackBar_AddScale.Size = new global::System.Drawing.Size(196, 45);
			this.trackBar_AddScale.TabIndex = 14;
			this.trackBar_AddScale.TickStyle = global::System.Windows.Forms.TickStyle.None;
			this.trackBar_AddScale.Value = 50;
			this.trackBar_AddScale.Scroll += new global::System.EventHandler(this.trackBar1_Scroll);
			this.btn_WaterMark.Enabled = false;
			this.btn_WaterMark.Location = new global::System.Drawing.Point(203, 213);
			this.btn_WaterMark.Name = "btn_WaterMark";
			this.btn_WaterMark.Size = new global::System.Drawing.Size(40, 23);
			this.btn_WaterMark.TabIndex = 11;
			this.btn_WaterMark.Text = "浏览";
			this.btn_WaterMark.UseVisualStyleBackColor = true;
			this.btn_WaterMark.Click += new global::System.EventHandler(this.btn_WaterMark_Click);
			this.lb_GetScale.AutoSize = true;
			this.lb_GetScale.Location = new global::System.Drawing.Point(211, 43);
			this.lb_GetScale.Name = "lb_GetScale";
			this.lb_GetScale.Size = new global::System.Drawing.Size(17, 12);
			this.lb_GetScale.TabIndex = 19;
			this.lb_GetScale.Text = "50";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(7, 26);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(65, 12);
			this.label2.TabIndex = 17;
			this.label2.Text = "提取亮度：";
			this.trackBar_GetScale.Location = new global::System.Drawing.Point(9, 41);
			this.trackBar_GetScale.Maximum = 100;
			this.trackBar_GetScale.Minimum = 1;
			this.trackBar_GetScale.Name = "trackBar_GetScale";
			this.trackBar_GetScale.Size = new global::System.Drawing.Size(196, 45);
			this.trackBar_GetScale.TabIndex = 15;
			this.trackBar_GetScale.TickStyle = global::System.Windows.Forms.TickStyle.None;
			this.trackBar_GetScale.Value = 50;
			this.trackBar_GetScale.Scroll += new global::System.EventHandler(this.trackBar_GetScale_Scroll);
			this.btn_Add.Enabled = false;
			this.btn_Add.Location = new global::System.Drawing.Point(468, 9);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new global::System.Drawing.Size(99, 30);
			this.btn_Add.TabIndex = 12;
			this.btn_Add.Text = "添加水印";
			this.btn_Add.UseVisualStyleBackColor = true;
			this.btn_Add.Click += new global::System.EventHandler(this.btn_Add_Click);
			this.btn_SaveAs.Enabled = false;
			this.btn_SaveAs.Location = new global::System.Drawing.Point(681, 9);
			this.btn_SaveAs.Name = "btn_SaveAs";
			this.btn_SaveAs.Size = new global::System.Drawing.Size(100, 30);
			this.btn_SaveAs.TabIndex = 13;
			this.btn_SaveAs.Text = "图片另存为";
			this.btn_SaveAs.UseVisualStyleBackColor = true;
			this.btn_SaveAs.Click += new global::System.EventHandler(this.btn_SaveAs_Click);
			this.btn_Get.Enabled = false;
			this.btn_Get.Location = new global::System.Drawing.Point(573, 9);
			this.btn_Get.Name = "btn_Get";
			this.btn_Get.Size = new global::System.Drawing.Size(100, 30);
			this.btn_Get.TabIndex = 14;
			this.btn_Get.Text = "提取盲水印";
			this.btn_Get.UseVisualStyleBackColor = true;
			this.btn_Get.Click += new global::System.EventHandler(this.btn_Get_Click);
			this.openFileDialog1.FileName = "openFileDialog1";
			this.statusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.lb_Status });
			this.statusStrip1.Location = new global::System.Drawing.Point(10, 547);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new global::System.Drawing.Size(785, 22);
			this.statusStrip1.TabIndex = 15;
			this.statusStrip1.Text = "statusStrip1";
			this.lb_Status.Name = "lb_Status";
			this.lb_Status.Size = new global::System.Drawing.Size(32, 17);
			this.lb_Status.Text = "就绪";
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.帮助HToolStripMenuItem });
			this.menuStrip1.Location = new global::System.Drawing.Point(10, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(785, 25);
			this.menuStrip1.TabIndex = 16;
			this.menuStrip1.Text = "menuStrip1";
			this.帮助HToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.关于AToolStripMenuItem });
			this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
			this.帮助HToolStripMenuItem.Size = new global::System.Drawing.Size(61, 21);
			this.帮助HToolStripMenuItem.Text = "帮助(&H)";
			this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
			this.关于AToolStripMenuItem.Size = new global::System.Drawing.Size(116, 22);
			this.关于AToolStripMenuItem.Text = "关于(&A)";
			this.关于AToolStripMenuItem.Click += new global::System.EventHandler(this.关于AToolStripMenuItem_Click);
			this.table_Button.ColumnCount = 5;
			this.table_Button.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.table_Button.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 108f));
			this.table_Button.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 105f));
			this.table_Button.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 108f));
			this.table_Button.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 107f));
			this.table_Button.Controls.Add(this.btn_SaveAs, 4, 0);
			this.table_Button.Controls.Add(this.btn_Get, 3, 0);
			this.table_Button.Controls.Add(this.btn_Add, 2, 0);
			this.table_Button.Controls.Add(this.btn_Open, 1, 0);
			this.table_Button.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.table_Button.Location = new global::System.Drawing.Point(10, 498);
			this.table_Button.Name = "table_Button";
			this.table_Button.Padding = new global::System.Windows.Forms.Padding(0, 6, 0, 6);
			this.table_Button.RowCount = 1;
			this.table_Button.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.table_Button.Size = new global::System.Drawing.Size(785, 49);
			this.table_Button.TabIndex = 17;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel2.Controls.Add(this.groupBox_Add, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox_Get, 0, 1);
			this.tableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.tableLayoutPanel2.Location = new global::System.Drawing.Point(10, 25);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 383f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 167f));
			this.tableLayoutPanel2.Size = new global::System.Drawing.Size(268, 473);
			this.tableLayoutPanel2.TabIndex = 18;
			this.groupBox_Get.Controls.Add(this.label2);
			this.groupBox_Get.Controls.Add(this.lb_GetScale);
			this.groupBox_Get.Controls.Add(this.trackBar_GetScale);
			this.groupBox_Get.Location = new global::System.Drawing.Point(3, 386);
			this.groupBox_Get.Name = "groupBox_Get";
			this.groupBox_Get.Size = new global::System.Drawing.Size(252, 88);
			this.groupBox_Get.TabIndex = 12;
			this.groupBox_Get.TabStop = false;
			this.groupBox_Get.Text = "提取选项";
			this.progressBar1.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.progressBar1.Location = new global::System.Drawing.Point(436, 248);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(220, 18);
			this.progressBar1.TabIndex = 19;
			this.progressBar1.Visible = false;
			this.fontDialog1.Font = new global::System.Drawing.Font("黑体", 20.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 134);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(805, 569);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.pic_Picture);
			base.Controls.Add(this.tableLayoutPanel2);
			base.Controls.Add(this.table_Button);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new global::System.Drawing.Size(821, 608);
			base.Name = "WaterMark";
			base.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WaterMark--吾爱破解论坛首发";
			((global::System.ComponentModel.ISupportInitialize)this.pic_Picture).EndInit();
			this.groupBox_Add.ResumeLayout(false);
			this.groupBox_Add.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_Angle).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_AddScale).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.trackBar_GetScale).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.table_Button.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox_Get.ResumeLayout(false);
			this.groupBox_Get.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000025 RID: 37
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.PictureBox pic_Picture;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.Button btn_Open;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.Label lb_Text;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.TextBox tb_WaterMarkText;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.TextBox tb_WaterMarkPicture;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.Label lb_Picture;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.RadioButton rd_Text;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.RadioButton rd_Picture;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.GroupBox groupBox_Add;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.Button btn_WaterMark;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Button btn_Add;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.Button btn_SaveAs;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.Button btn_Get;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.OpenFileDialog openFileDialog1;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.TrackBar trackBar_AddScale;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.StatusStrip statusStrip1;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.ToolStripStatusLabel lb_Status;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.Label lb_GetScale;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.Label lb_AddScale;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.TrackBar trackBar_GetScale;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.TableLayoutPanel table_Button;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.ProgressBar progressBar1;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.Button btn_Font;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.FontDialog fontDialog1;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.SaveFileDialog saveFileDialog1;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.GroupBox groupBox_Get;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.RadioButton rd_Hidden;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.RadioButton rd_General;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Label lb_Angle;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.TrackBar trackBar_Angle;
	}
}
