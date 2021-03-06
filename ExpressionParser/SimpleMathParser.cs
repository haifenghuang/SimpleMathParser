using System;
using System.Collections.Generic;
using System.Text;

/*
{ BNF(巴克诺范式):

  Expression::=<Term> [<AddOp> <Term>]*
  <Term>    ::=<Factor> [<MulOp> <Factor> ]*
  <Factor>  ::=(<Expression>) | <Numeric> | <Constant> | UserFunc | PreDefinedFunc
}
{*------------------------------------------------------------------------------
  一个简单的数学表达式控件(由原来的Delphi语言更改成C#语言)
  @author  huanghaifeng
  @version 2006/03/03 1.0 Initial revision.
  @todo
  @comment 这个类是从RxLib单元的Parsing中更改而来的，感谢他们所做出的贡献，同时我要感谢Let's Build a Compiler的作者Jack W. Crenshaw
-------------------------------------------------------------------------------}
 */
namespace ExpressionParser
{
    public sealed class SimpleMathParser
    {
        #region === Constructor
        public SimpleMathParser()
        {
            AddUserConstant("PI", Math.PI);
            AddUserConstant("E", Math.E);
            FuncNames.Add(TParserFunc.pfArcTan, "ARCTAN");
            FuncNames.Add(TParserFunc.pfCos, "COS");
            FuncNames.Add(TParserFunc.pfSin, "SIN");
            FuncNames.Add(TParserFunc.pfTan, "TAN");
            FuncNames.Add(TParserFunc.pfAbs, "ABS");
            FuncNames.Add(TParserFunc.pfExp, "EXP");
            FuncNames.Add(TParserFunc.pfLn, "LN");
            FuncNames.Add(TParserFunc.pfLog, "LOG");
            FuncNames.Add(TParserFunc.pfSqrt, "SQRT");
            FuncNames.Add(TParserFunc.pfSqr, "SQR");
            FuncNames.Add(TParserFunc.pfInt, "INT");
            FuncNames.Add(TParserFunc.pfFrac, "FRAC");
            FuncNames.Add(TParserFunc.pfTrunc, "TRUNC");
            FuncNames.Add(TParserFunc.pfRound, "ROUND");
            FuncNames.Add(TParserFunc.pfFloor, "FLOOR");
            FuncNames.Add(TParserFunc.pfCeiling, "CEILING");
            FuncNames.Add(TParserFunc.pfArcSin, "ARCSIN");
            FuncNames.Add(TParserFunc.pfArcCos, "ARCCOS");
            FuncNames.Add(TParserFunc.pfSign, "SIGN");
            FuncNames.Add(TParserFunc.pfNot, "NOT");
            FuncNames.Add(TParserFunc.pfFact, "FACT");
        }
        #endregion

        #region === Fields
        private int FCurPos;
        private string FParseText;
        private string FFormula;
        //private bool FCaseSensitive;
        private double FExecuteResult;
        private enum TParserFunc
        {
            pfArcTan, pfCos, pfSin, pfTan, pfAbs, pfExp, pfLn, pfLog,
            pfSqrt, pfSqr, pfInt, pfFrac, pfTrunc, pfRound, pfFloor, pfCeiling, 
            pfArcSin, pfArcCos, pfSign, pfNot, pfFact
        }
        private Dictionary<TParserFunc, string> FuncNames = new Dictionary<TParserFunc, string>();
        private Dictionary<string, double> ConstTable = new Dictionary<string, double>();
        private Dictionary<string, double> VarTable = new Dictionary<string, double>();
        private Dictionary<string, UserFunction> UserFuncList = new Dictionary<string, UserFunction>();

        #endregion

        #region === Property
        public double ExecuteResult
        {
            get
            {
                return FExecuteResult;
            }
        }
        //property Formula:String read FFormula write FFormula; 
        public string Formula
        {
            get { return FFormula; }
            set
            {
                /*不应该有这个判断，因为：
                 * 例如：FFormula = (1+5)*6+x
                 * x为用户定义的变量,如果程序中通过setValue方法改变了变量的值，
                 * 但是这时候虽然FFormula没有变，但是x却已经变了,这时候就应该执行
                 * Exec()方法
                 */
                //if (value != FFormula) 
                //{
                FFormula = value;
                FExecuteResult = Exec();
                //}
            }
        }

        #endregion

        #region === Private Method
        private bool IsAddOp(char c)
        {
            return (c == '+' || c == '-');
        }

        private bool IsMulOp(char c)
        {
            return (c == '*' || c == '/' || c == '^');
        }

        private bool IsNumeric(char c)
        {
            return (char.IsDigit(c) || c == '.');
        }
        private bool IsLetter(char c)
        {
            return (char.IsLetter(c) || c == '_');
        }

        private double Expression()
        {
            double Value;
            //double Result;

            Value = Term();
            while (IsAddOp(FParseText[FCurPos]))
            {
                FCurPos++;
                switch (FParseText[FCurPos - 1])
                {
                    case '+': Value += Term(); break;
                    case '-': Value -= Term(); break;
                }// end switch
            }// end while
            if (!((FParseText[FCurPos] == '\0') ||
                   FParseText[FCurPos] == ')' ||
                   FParseText[FCurPos] == '='))
                InvalidExpression("表达式语法错误！");

            //Result = Value;
            return Value;
        }

        private double Term()
        {
            double Value;

            Value = Factor();
            while (IsMulOp(FParseText[FCurPos]))
            {
                FCurPos++;
                switch (FParseText[FCurPos - 1])
                {
                    case '*': Value = Value * Factor(); break;
                    case '^': Value = Math.Pow(Value, Factor()); break;
                    case '/':
                        {
                            try
                            {
                                Value = Value / Factor();
                            }
                            catch (DivideByZeroException)
                            {
                                InvalidExpression("被零除！");
                            }
                        } break;
                }// end switch
            }// end while
            return Value;
        }

        private double Factor()
        {
            double Value;
            string TmpStr = "";
            //int Code;
            TParserFunc NoFunc;
            string UserFuncName = "";
            //object Func;
            //double Result;

            Value = 0;
            if (FParseText[FCurPos] == '(')
            {
                FCurPos++;
                Value = Expression();
                if (FParseText[FCurPos] != ')')
                    InvalidExpression("括号不匹配！");
                FCurPos++;
            }
            else
            {
                if (IsNumeric(FParseText[FCurPos]))
                {
                    while (IsNumeric(FParseText[FCurPos]))
                    {
                        TmpStr += FParseText[FCurPos];
                        FCurPos++;
                    }// end while
                    Value = Convert.ToDouble(TmpStr);
                    //Val(TmpStr,Value,Code);
                    //if( Code!=0 )
                    //InvalidExpression("表达式错误！");
                }
                else
                {
                    if (!GetConst(ref Value)) //如果不是常量
                        if (!GetVariable(ref Value)) //如果不是变量
                            if (GetUserFunction(ref UserFuncName)) //如果是用户定义的方法
                            {
                                FCurPos++;
                                if (UserFuncList[UserFuncName] != null)
                                    Value = UserFuncList[UserFuncName](Expression());
                                if (FParseText[FCurPos] != ')')
                                    InvalidExpression("扩号不匹配！");
                                FCurPos++;
                            }
                            else
                                if (GetFunction(out NoFunc)) //如果是系统预定义的方法
                                {
                                    FCurPos++;
                                    Value = Expression();
                                    try
                                    {
                                        switch (NoFunc)
                                        {
                                            case TParserFunc.pfArcTan: Value = Math.Atan(Value); break;
                                            case TParserFunc.pfCos: Value = Math.Cos(Value); break;
                                            case TParserFunc.pfSin: Value = Math.Sin(Value); break;
                                            case TParserFunc.pfTan:
                                                if (Math.Cos(Value) == 0)
                                                    InvalidExpression("除数为零！");
                                                else
                                                    Value = Math.Sin(Value) / Math.Cos(Value); break;
                                            case TParserFunc.pfAbs: Value = Math.Abs(Value); break;
                                            case TParserFunc.pfExp: Value = Math.Exp(Value); break;
                                            case TParserFunc.pfLn:
                                                if (Value <= 0)
                                                    InvalidExpression("解析Ln函数错误！");
                                                else
                                                    Value = Math.Log(Value) / Math.Log(10);
                                                break;
                                            case TParserFunc.pfLog:
                                                if (Value <= 0)
                                                    InvalidExpression("解析Log函数错误！");
                                                else
                                                    Value = Math.Log(Value);
                                                break;
                                            case TParserFunc.pfSqrt:
                                                if (Value < 0)
                                                    InvalidExpression("解析Sqrt函数错误！");
                                                else
                                                    Value = Math.Sqrt(Value); break;
                                            case TParserFunc.pfFact:// 阶乘
                                                if (Value < 0)
                                                    InvalidExpression("解析Factorial函数错误！");
                                                else
                                                    Value = Factorial(Convert.ToInt32(Value));
                                                break;
                                            case TParserFunc.pfSqr: Value = Math.Pow(Value, 2); break;
                                            case TParserFunc.pfInt: Value = Math.Round(Value); break;
                                            //case pfFrac: Value = Math.f(Value); break;
                                            case TParserFunc.pfTrunc: Value = Math.Truncate(Value); break;
                                            case TParserFunc.pfRound: Value = System.Math.Round(Value); break;
                                            case TParserFunc.pfFloor: Value = System.Math.Floor(Value); break;
                                            case TParserFunc.pfCeiling: Value = System.Math.Ceiling(Value); break;
                                            case TParserFunc.pfArcSin:
                                                if (Value == 1)
                                                    Value = Math.PI / 2;
                                                else
                                                    //Value = Math.Atan(Value / Math.Sqrt(1 - Math.Pow(Value, 2))); break;
                                                    Value = Math.Asin(Value); break;
                                            case TParserFunc.pfArcCos:
                                                if (Value == 1)
                                                    Value = 0;
                                                else
                                                    //Value = Math.PI / 2 - Math.Atan(Value / Math.Sqrt(1 - Math.Pow(Value, 2))); break;
                                                    Value = Math.Acos(Value); break;
                                            case TParserFunc.pfSign:
                                                if (Value > 0)
                                                    Value = 1;
                                                else if (Value < 0)
                                                    Value = -1;
                                                break;
                                            //case TParserFunc.pfNot: Value =  !( Math.Truncate(Value)); break;
                                        }// end switch

                                    }//end try
                                    catch (MathParserError e)
                                    {
                                        InvalidExpression(e.Message);
                                    }
                                    catch
                                    {
                                        InvalidExpression("无效的浮点数运算！");
                                    }

                                    if (FParseText[FCurPos] != ')')
                                        InvalidExpression("扩号不匹配！");
                                    FCurPos++;
                                }
                                else
                                    InvalidExpression("表达式语法错误！");
                }// end else
            }// end else
            return Value;
        }

        private bool GetUserFunction(ref string fname)
        {
            string TmpStr;
            bool Result = false;

            if (IsLetter(FParseText[FCurPos]) && UserFuncList.Count > 0)
            {
                foreach (string funcName in UserFuncList.Keys)
                {
                    TmpStr = FParseText.Substring(FCurPos, funcName.Length);
                    if (TmpStr.CompareTo(funcName) == 0 && UserFuncList[funcName] != null)
                    {
                        if (FParseText[FCurPos + TmpStr.Length] == '(')
                        {
                            Result = true;
                            FCurPos += TmpStr.Length;
                            fname = funcName;
                            return Result;
                        }
                    }
                }//end foreach
            }
            return Result;
        }

        private bool GetFunction(out TParserFunc AValue)
        {
            string TmpStr;
            bool Result = false;

            AValue = TParserFunc.pfArcTan;
            if (IsLetter(FParseText[FCurPos]))
            {
                foreach (TParserFunc func in FuncNames.Keys)
                {
                    TmpStr = FParseText.Substring(FCurPos, FuncNames[func].Length);
                    if (TmpStr.CompareTo(FuncNames[func]) == 0)
                    {
                        AValue = func;
                        if (FParseText[FCurPos + TmpStr.Length] == '(')
                        {
                            Result = true;
                            FCurPos += TmpStr.Length;
                            break;
                        }
                    }
                }//end foreach
            }//end if
            return Result;
        }

        private bool GetConst(ref double AValue)
        {
            string TmpStr = "";
            bool Result;

            Result = false;
            AValue = 0;
            if (IsLetter(FParseText[FCurPos]))
            {
                foreach (string key in ConstTable.Keys)
                {
                    /* * 注意：下面的语句必须用try,catch来处理，因为如果Formula=(1+5)*6+PI+x
                     * 其中x为一个变量，这下面的Substring就会出现索引越界的问题，这时候不用管,
                     * 只需捕获异常即可，不需要处理，因为下面的CompareTo方法会处理是否为常量.
                     */
                    try
                    {
                        TmpStr = FParseText.Substring(FCurPos, key.Length);
                    }
                    catch
                    {
                        //do nothing
                    }
                    if (TmpStr.CompareTo(key) == 0)
                    {
                        AValue = ConstTable[key];
                        FCurPos += key.Length;
                        Result = true;
                        break;
                    }
                }//end foreach
            }// end if
            return Result;
        }

        private bool GetVariable(ref double AValue)
        {
            string TmpStr = "";
            bool Result;

            Result = false;
            AValue = 0;
            if (char.IsLetter(FParseText[FCurPos]))
            {
                foreach (string key in VarTable.Keys)
                {
                    try
                    {
                        TmpStr = FParseText.Substring(FCurPos, key.Length);
                    }
                    catch
                    {
                        //do nothing
                    }
                    if (TmpStr.CompareTo(key) == 0)
                    {
                        AValue = VarTable[key];
                        FCurPos += key.Length;
                        Result = true;
                    }
                }//end foreach
            }// end if
            return Result;

        }
        #endregion

        #region === Public method
        public delegate double UserFunction(double value);
        //计算表达式
        public double Exec()
        {
            int J;
            double Result;

            if (FFormula.Trim().Length == 0)
                InvalidExpression("表达式为空！");
            J = 0;
            Result = 0;
            FParseText = "";
            // 首先查找括号是否匹配
            for (int I = 0; I < FFormula.Length; I++)
            {
                switch (FFormula[I])
                {
                    case '(': J++; break;
                    case ')': J--; break;
                }// end case
                if (FFormula[I] > ' ')
                {
                    //是否转换为大写字母
                    //if (!CaseSensitive)
                    //{
                    FParseText = FParseText + char.ToUpper(FFormula[I]);
                    //}
                    //else
                    //{
                    //    FParseText = FParseText + FFormula[I];
                    //}
                }
            }// end for
            if (J == 0) // 括号匹配
            {
                FCurPos = 0;
                FParseText += '\0';// 最后加入一个#0代表结束符
                // 如果第一个字符为+,-，则字符串变为 "0+x"
                if (FParseText[FCurPos] == '+' || FParseText[FCurPos] == '-') FParseText = "0" + FParseText;
                Result = Expression();
            }
            else
                InvalidExpression("括号不匹配！");
            return Result;
        }

        //增加一个自定义变量
        public void AddUserVar(string name, double value)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (VarTable.ContainsKey(tmp))
                VarTable[tmp] = value;//更改这个变量
            else
                VarTable.Add(tmp, value);
        }

        //增加用户自定义常量
        public void AddUserConstant(string name, double value)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (ConstTable.ContainsKey(tmp))
                ConstTable[tmp] = value;
            else
                ConstTable.Add(tmp, value);
        }

        //设置某个变量的值
        public void SetValue(string name, double value)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (!VarTable.ContainsKey(tmp))
                throw new FormulaException("变量\"" + name + "\" 不存在！");
            VarTable[tmp] = value;
        }

        //得到某个变量的值
        public double GetValue(string name)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (!VarTable.ContainsKey(tmp))
                throw new FormulaException("变量\"" + name + "\" 不存在！");
            return VarTable[tmp];
        }

        //将某个变量的值加上某个固定的值
        public void IncValue(string name, double value)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (!VarTable.ContainsKey(tmp))
                throw new FormulaException("变量\"" + name + " 不存在！");

            VarTable[tmp] += value;
        }

        //将某个变量的值减去某个固定的值
        public void DecValue(string name, double value)
        {
            string tmp = name.ToUpper();
            //if (!CaseSensitive)
            //    tmp = name.ToUpper();
            if (!VarTable.ContainsKey(tmp))
                throw new FormulaException("变量\"" + name + " 不存在！");

            VarTable[tmp] -= value;
        }

        public override string ToString()
        {
            return Convert.ToString(FExecuteResult);
        }
        #endregion

        #region === InternalMethod
        internal double Factorial(int value)
        {

            double result;

            if (value <= 1)
                result = value;
            else
                result = value * Factorial(value - 1);
            return result;
        }

        internal void InvalidExpression(string str)
        {
            throw new MathParserError(str);
        }
        #endregion

        #region === Static Class Methods
        public void RegisterUserFunction(string funcName, UserFunction proc)
        {
            string tmp = funcName.ToUpper();
            //if (!CaseSensitive)
            //    tmp = funcName.ToUpper();
            if ((funcName.Length > 0) &&
                (funcName[0] >= 'a' && funcName[0] <= 'z') ||
                (funcName[0] >= 'A' && funcName[0] <= 'Z') ||
                (funcName[0] == '_'))
            {
                if (!UserFuncList.ContainsKey(tmp))
                {
                    UserFunction ufunc = new UserFunction(proc);
                    UserFuncList.Add(tmp, ufunc);
                }
                else
                    throw new MathParserError("语法解析错误！");
            }
        }
        #endregion

        #region === Operator overloading

        /* overloading '+' sign 
         * For Example:
         *    exp2 = exp + 10;
         */
        public static double operator+(SimpleMathParser parser, object exp)
        {
            double ret = 0.0;
            if (exp is int) 
            {
                ret = parser.ExecuteResult + (double)exp;
            }
            else if (exp is double)
            {
                ret = parser.ExecuteResult + (double)exp;
            }
            else if (exp is string)
            {
                SimpleMathParser newParser = new SimpleMathParser();
                newParser.Formula = exp.ToString();
                ret = parser.ExecuteResult + newParser.ExecuteResult;
            }
            else if (exp is SimpleMathParser)
            {
                ret = parser.ExecuteResult + (exp as SimpleMathParser).ExecuteResult;
            }
            return ret;
        }

        /* overloading '-' sign */
        public static double operator -(SimpleMathParser parser, object exp)
        {
            double ret = 0.0;
            if (exp is int)
            {
                ret = parser.ExecuteResult - (double)exp;
            }
            else if (exp is double)
            {
                ret = parser.ExecuteResult - (double)exp;
            }
            else if (exp is string)
            {
                SimpleMathParser newParser = new SimpleMathParser();
                newParser.Formula = exp.ToString();
                ret = parser.ExecuteResult - newParser.ExecuteResult;
            }
            else if (exp is SimpleMathParser)
            {
                ret = parser.ExecuteResult + (exp as SimpleMathParser).ExecuteResult;
            }
            return ret;

        }

        /* overloading '*' sign */
        public static double operator *(SimpleMathParser parser, object exp)
        {
            double ret = 0.0;
            if (exp is int)
            {
                ret = parser.ExecuteResult * (double)exp;
            }
            else if (exp is double)
            {
                ret = parser.ExecuteResult * (double)exp;
            }
            else if (exp is string)
            {
                SimpleMathParser newParser = new SimpleMathParser();
                newParser.Formula = exp.ToString();
                ret = parser.ExecuteResult * newParser.ExecuteResult;
            }
            else if (exp is SimpleMathParser)
            {
                ret = parser.ExecuteResult + (exp as SimpleMathParser).ExecuteResult;
            }
            return ret;
        }

        /* overloading '/' sign */
        public static double operator /(SimpleMathParser parser, object exp)
        {
            double ret = 0.0;
            if (exp is int)
            {
                ret = parser.ExecuteResult / (double)exp;
            }
            else if (exp is double)
            {
                ret = parser.ExecuteResult / (double)exp;
            }
            else if (exp is string)
            {
                SimpleMathParser newParser = new SimpleMathParser();
                newParser.Formula = exp.ToString();
                ret = parser.ExecuteResult / newParser.ExecuteResult;
            }
            else if (exp is SimpleMathParser)
            {
                ret = parser.ExecuteResult + (exp as SimpleMathParser).ExecuteResult;
            }
            return ret;
        }



        #endregion
    }
}
