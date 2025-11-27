// Decompiled with JetBrains decompiler
// Type: WaterMark.AboutBox
// Assembly: WaterMark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7EFC8A5-D9E6-406C-B30C-DB9385FC45E9
// Assembly location: C:\Users\Administrator\Downloads\WaterMark_1.0_Single.exe

using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace WaterMark;

public class AboutBox : Form
{
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel;
  private Label labelProductName;
  private Label labelVersion;
  private Label labelCopyright;
  private Label labelCompanyName;
  private Button okButton;

  public AboutBox()
  {
    this.InitializeComponent();
    this.Text = $"关于 {this.AssemblyTitle}";
    this.labelProductName.Text = this.AssemblyProduct;
    this.labelVersion.Text = $"版本 {this.AssemblyVersion}";
    this.labelCopyright.Text = this.AssemblyCopyright;
    this.labelCompanyName.Text = this.AssemblyCompany;
  }

  public string AssemblyTitle
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
      if (customAttributes.Length > 0)
      {
        AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
        if (assemblyTitleAttribute.Title != "")
          return assemblyTitleAttribute.Title;
      }
      return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
    }
  }

  public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

  public string AssemblyDescription
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
    }
  }

  public string AssemblyProduct
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyProductAttribute) customAttributes[0]).Product;
    }
  }

  public string AssemblyCopyright
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
    }
  }

  public string AssemblyCompany
  {
    get
    {
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
      return customAttributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) customAttributes[0]).Company;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tableLayoutPanel = new TableLayoutPanel();
    this.labelProductName = new Label();
    this.labelVersion = new Label();
    this.labelCopyright = new Label();
    this.labelCompanyName = new Label();
    this.okButton = new Button();
    this.tableLayoutPanel.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel.ColumnCount = 1;
    this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel.Controls.Add((Control) this.labelProductName, 0, 0);
    this.tableLayoutPanel.Controls.Add((Control) this.labelVersion, 0, 1);
    this.tableLayoutPanel.Controls.Add((Control) this.labelCopyright, 0, 2);
    this.tableLayoutPanel.Controls.Add((Control) this.labelCompanyName, 0, 3);
    this.tableLayoutPanel.Controls.Add((Control) this.okButton, 0, 4);
    this.tableLayoutPanel.Dock = DockStyle.Fill;
    this.tableLayoutPanel.Location = new Point(10, 10);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    this.tableLayoutPanel.RowCount = 5;
    this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));
    this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 14f));
    this.tableLayoutPanel.Size = new Size(279, 115);
    this.tableLayoutPanel.TabIndex = 1;
    this.labelProductName.Dock = DockStyle.Fill;
    this.labelProductName.Location = new Point(6, 0);
    this.labelProductName.Margin = new Padding(6, 0, 3, 0);
    this.labelProductName.MaximumSize = new Size(0, 16 /*0x10*/);
    this.labelProductName.Name = "labelProductName";
    this.labelProductName.Size = new Size(270, 16 /*0x10*/);
    this.labelProductName.TabIndex = 19;
    this.labelProductName.Text = "产品名称";
    this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
    this.labelVersion.Dock = DockStyle.Fill;
    this.labelVersion.Location = new Point(6, 20);
    this.labelVersion.Margin = new Padding(6, 0, 3, 0);
    this.labelVersion.MaximumSize = new Size(0, 16 /*0x10*/);
    this.labelVersion.Name = "labelVersion";
    this.labelVersion.Size = new Size(270, 16 /*0x10*/);
    this.labelVersion.TabIndex = 0;
    this.labelVersion.Text = "版本";
    this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
    this.labelCopyright.Dock = DockStyle.Fill;
    this.labelCopyright.Location = new Point(6, 40);
    this.labelCopyright.Margin = new Padding(6, 0, 3, 0);
    this.labelCopyright.MaximumSize = new Size(0, 16 /*0x10*/);
    this.labelCopyright.Name = "labelCopyright";
    this.labelCopyright.Size = new Size(270, 16 /*0x10*/);
    this.labelCopyright.TabIndex = 21;
    this.labelCopyright.Text = "版权";
    this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
    this.labelCompanyName.Dock = DockStyle.Fill;
    this.labelCompanyName.Location = new Point(6, 60);
    this.labelCompanyName.Margin = new Padding(6, 0, 3, 0);
    this.labelCompanyName.MaximumSize = new Size(0, 16 /*0x10*/);
    this.labelCompanyName.Name = "labelCompanyName";
    this.labelCompanyName.Size = new Size(270, 16 /*0x10*/);
    this.labelCompanyName.TabIndex = 22;
    this.labelCompanyName.Text = "公司名称";
    this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
    this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.okButton.DialogResult = DialogResult.Cancel;
    this.okButton.Location = new Point(201, 91);
    this.okButton.Name = "okButton";
    this.okButton.Size = new Size(75, 21);
    this.okButton.TabIndex = 24;
    this.okButton.Text = "确定(&O)";
    this.AutoScaleDimensions = new SizeF(6f, 12f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(299, 135);
    this.Controls.Add((Control) this.tableLayoutPanel);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.Name = nameof (AboutBox);
    this.Padding = new Padding(10);
    this.ShowIcon = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "关于";
    this.tableLayoutPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
