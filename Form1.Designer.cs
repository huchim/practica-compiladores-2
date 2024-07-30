namespace Compiladores
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.cmdParse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.astCode = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.astCode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.39655F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.60345F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(687, 464);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel4.Controls.Add(this.txtCode, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdParse, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(337, 437);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // txtCode
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.txtCode, 2);
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCode.Size = new System.Drawing.Size(331, 331);
            this.txtCode.TabIndex = 13;
            this.txtCode.Text = "3+      5*10.5/3*(    \r\n\r\n15.5689+154.2-498) +\r\n\r\n 52 - 1 * ((5+1\r\n\r\n        )*2)" +
    "";
            // 
            // cmdParse
            // 
            this.cmdParse.Location = new System.Drawing.Point(232, 347);
            this.cmdParse.Margin = new System.Windows.Forms.Padding(10);
            this.cmdParse.Name = "cmdParse";
            this.cmdParse.Size = new System.Drawing.Size(75, 23);
            this.cmdParse.TabIndex = 14;
            this.cmdParse.Text = "Analizar";
            this.cmdParse.UseVisualStyleBackColor = true;
            this.cmdParse.Click += new System.EventHandler(this.cmdParse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 100);
            this.label3.TabIndex = 15;
            this.label3.Text = "Números, +, -, Paréntesis, Espacios.\r\n\r\n1+2\r\n1+(4+2)\r\n1+((3+1)+2)+5";
            // 
            // astCode
            // 
            this.astCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astCode.Location = new System.Drawing.Point(346, 200);
            this.astCode.Multiline = true;
            this.astCode.Name = "astCode";
            this.tableLayoutPanel1.SetRowSpan(this.astCode, 2);
            this.astCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.astCode.Size = new System.Drawing.Size(338, 261);
            this.astCode.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 464);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Universidad del Sur : Compiladores (Karen, Adirlan y Carlos)";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button cmdParse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox astCode;
    }
}

