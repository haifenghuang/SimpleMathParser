using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser
{
    public class MathParserError: Exception
    {
        public MathParserError() : base() 
        { 
        }
        
        public MathParserError(string message):base(message) 
        { 
        }
    }

    public class FormulaException : MathParserError
    { 
         public FormulaException() : base() 
        { 
        }
        
        public FormulaException(string message):base(message) 
        { 
        }
    }
}
