using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExpressionParser
{
    public partial class FrmMain : Form
    {
        private SimpleMathParser parser = null;
        public FrmMain()
        {
            InitializeComponent();

        }

        private void BtnExec_Click(object sender, EventArgs e)
        {
            try
            {
                //parser.CaseSensitive = cbCase.Checked;
                parser.Formula = tbExpression.Text;

                //lbResult.Text = Convert.ToString(parser.ExecuteResult);
                lbResult.Text = parser.ToString();
            }
            catch (MathParserError exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unknown Error: " + exc.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            parser = new SimpleMathParser();
            parser.AddUserConstant("MyConst", 10);
            parser.AddUserVar("x", 5);
            parser.RegisterUserFunction("plus2", plus2);
        }

        private double plus2(double value)
        {
            return value + 2;
        }

        private void UdBox_ValueChanged(object sender, EventArgs e)
        {
            parser.SetValue("x", Convert.ToDouble(UdBox.Value));
        }

        private void btnCalculateOperator_Click(object sender, EventArgs e)
        {
            //double result = parser + int.Parse(tbOperator.Text);==>OK
            double result=0.0; //= parser + tbOperator.Text;
            if (rbPlus.Checked)
            {
                result = parser + tbOperator.Text;
            }
            if (rbMinus.Checked)
            {
                result = parser - tbOperator.Text;
            }

            if (rbMulti.Checked)
            {
                result = parser * tbOperator.Text;
            }
            if (rbDivide.Checked)
            {
                result = parser / tbOperator.Text;
            }
            MessageBox.Show(result.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double result = 0.0;
            SimpleMathParser newParser = new SimpleMathParser();
            newParser.Formula = "(1+2)*3";
            result = parser + newParser;
            MessageBox.Show(result.ToString());
        }

    }
}