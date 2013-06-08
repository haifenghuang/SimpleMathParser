namespace ExpressionParser
{
    partial class FrmMain
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
            this.tbExpression = new System.Windows.Forms.TextBox();
            this.BtnExec = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UdBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.btnCalculateOperator = new System.Windows.Forms.Button();
            this.rbPlus = new System.Windows.Forms.RadioButton();
            this.rbMinus = new System.Windows.Forms.RadioButton();
            this.rbMulti = new System.Windows.Forms.RadioButton();
            this.rbDivide = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tbExpression
            // 
            this.tbExpression.Location = new System.Drawing.Point(12, 48);
            this.tbExpression.Name = "tbExpression";
            this.tbExpression.Size = new System.Drawing.Size(405, 20);
            this.tbExpression.TabIndex = 0;
            this.tbExpression.Text = "( 1+5) * 6+ 2* x* sqrt(9)+ sqrt(sqrt(9))+ pi+myconst+ 2* plus2(2)+ plus2(sqrt(9))" +
                "";
            // 
            // BtnExec
            // 
            this.BtnExec.Location = new System.Drawing.Point(116, 81);
            this.BtnExec.Name = "BtnExec";
            this.BtnExec.Size = new System.Drawing.Size(237, 28);
            this.BtnExec.TabIndex = 1;
            this.BtnExec.Text = "Calculate";
            this.BtnExec.UseVisualStyleBackColor = true;
            this.BtnExec.Click += new System.EventHandler(this.BtnExec_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Result:";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult.Location = new System.Drawing.Point(67, 124);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(0, 16);
            this.lbResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Math Expression: (tbExpression)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(423, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 238);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Provided Functionality";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.UdBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(21, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 208);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "5. Not Case Sensitive";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(294, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "4. User Defined Function:  (   Plus2(value) :result = value+2  )";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "x = ";
            // 
            // UdBox
            // 
            this.UdBox.Location = new System.Drawing.Point(61, 85);
            this.UdBox.Name = "UdBox";
            this.UdBox.Size = new System.Drawing.Size(74, 20);
            this.UdBox.TabIndex = 4;
            this.UdBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.UdBox.ValueChanged += new System.EventHandler(this.UdBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "3.User Defined Variable:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "2.User Defined Const (MyConst =10)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "1. Predefined Const PI = 3.1415926";
            // 
            // tbOperator
            // 
            this.tbOperator.Location = new System.Drawing.Point(12, 193);
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.Size = new System.Drawing.Size(402, 20);
            this.tbOperator.TabIndex = 6;
            this.tbOperator.Text = "(12+15)+sqrt(9)";
            // 
            // btnCalculateOperator
            // 
            this.btnCalculateOperator.Location = new System.Drawing.Point(57, 229);
            this.btnCalculateOperator.Name = "btnCalculateOperator";
            this.btnCalculateOperator.Size = new System.Drawing.Size(135, 28);
            this.btnCalculateOperator.TabIndex = 7;
            this.btnCalculateOperator.Text = "Calculate Operator";
            this.btnCalculateOperator.UseVisualStyleBackColor = true;
            this.btnCalculateOperator.Click += new System.EventHandler(this.btnCalculateOperator_Click);
            // 
            // rbPlus
            // 
            this.rbPlus.AutoSize = true;
            this.rbPlus.Checked = true;
            this.rbPlus.Location = new System.Drawing.Point(21, 156);
            this.rbPlus.Name = "rbPlus";
            this.rbPlus.Size = new System.Drawing.Size(45, 17);
            this.rbPlus.TabIndex = 8;
            this.rbPlus.TabStop = true;
            this.rbPlus.Tag = "1";
            this.rbPlus.Text = "Plus";
            this.rbPlus.UseVisualStyleBackColor = true;
            // 
            // rbMinus
            // 
            this.rbMinus.AutoSize = true;
            this.rbMinus.Location = new System.Drawing.Point(112, 156);
            this.rbMinus.Name = "rbMinus";
            this.rbMinus.Size = new System.Drawing.Size(53, 17);
            this.rbMinus.TabIndex = 9;
            this.rbMinus.Tag = "2";
            this.rbMinus.Text = "Minus";
            this.rbMinus.UseVisualStyleBackColor = true;
            // 
            // rbMulti
            // 
            this.rbMulti.AutoSize = true;
            this.rbMulti.Location = new System.Drawing.Point(223, 156);
            this.rbMulti.Name = "rbMulti";
            this.rbMulti.Size = new System.Drawing.Size(62, 17);
            this.rbMulti.TabIndex = 10;
            this.rbMulti.Tag = "3";
            this.rbMulti.Text = "MultiPle";
            this.rbMulti.UseVisualStyleBackColor = true;
            // 
            // rbDivide
            // 
            this.rbDivide.AutoSize = true;
            this.rbDivide.Location = new System.Drawing.Point(328, 156);
            this.rbDivide.Name = "rbDivide";
            this.rbDivide.Size = new System.Drawing.Size(55, 17);
            this.rbDivide.TabIndex = 11;
            this.rbDivide.Tag = "4";
            this.rbDivide.Text = "Divide";
            this.rbDivide.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Calculate Operator 2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 269);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbDivide);
            this.Controls.Add(this.rbMulti);
            this.Controls.Add(this.rbMinus);
            this.Controls.Add(this.rbPlus);
            this.Controls.Add(this.btnCalculateOperator);
            this.Controls.Add(this.tbOperator);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnExec);
            this.Controls.Add(this.tbExpression);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Expression Parser";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbExpression;
        private System.Windows.Forms.Button BtnExec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown UdBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.Button btnCalculateOperator;
        private System.Windows.Forms.RadioButton rbPlus;
        private System.Windows.Forms.RadioButton rbMinus;
        private System.Windows.Forms.RadioButton rbMulti;
        private System.Windows.Forms.RadioButton rbDivide;
        private System.Windows.Forms.Button button1;
    }
}

