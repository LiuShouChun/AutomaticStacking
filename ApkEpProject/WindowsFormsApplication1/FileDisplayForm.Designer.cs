namespace WindowsFormsApplication1
{
    partial class FileDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tv_display = new System.Windows.Forms.TreeView();
            this.ll_smali = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_bundle = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_display
            // 
            this.tv_display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_display.Location = new System.Drawing.Point(3, 59);
            this.tv_display.Name = "tv_display";
            this.tv_display.Size = new System.Drawing.Size(311, 609);
            this.tv_display.TabIndex = 0;
            this.tv_display.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_display_BeforeExpand);
            this.tv_display.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_display_AfterExpand);
            this.tv_display.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_display_MouseDoubleClick);
            // 
            // ll_smali
            // 
            this.ll_smali.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ll_smali.Location = new System.Drawing.Point(320, 59);
            this.ll_smali.Multiline = true;
            this.ll_smali.Name = "ll_smali";
            this.ll_smali.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ll_smali.Size = new System.Drawing.Size(1041, 609);
            this.ll_smali.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.2816F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.71841F));
            this.tableLayoutPanel1.Controls.Add(this.ll_smali, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tv_display, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 615F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1364, 665);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 50);
            this.label1.TabIndex = 2;
            this.label1.Text = "目录";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.5562F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.4438F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bt_bundle, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(320, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1041, 50);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(843, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件内容展示";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_bundle
            // 
            this.bt_bundle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_bundle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_bundle.Location = new System.Drawing.Point(852, 3);
            this.bt_bundle.Name = "bt_bundle";
            this.bt_bundle.Size = new System.Drawing.Size(186, 44);
            this.bt_bundle.TabIndex = 4;
            this.bt_bundle.Text = "加固";
            this.bt_bundle.UseVisualStyleBackColor = true;
            this.bt_bundle.Click += new System.EventHandler(this.bt_bundle_Click);
            // 
            // FileDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 665);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileDisplayForm";
            this.Text = "FileDisplayForm";
            this.Load += new System.EventHandler(this.FileDisplayForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_display;
        private System.Windows.Forms.TextBox ll_smali;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_bundle;

    }
}