using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Expression
{
    public class Expression
    {
        private IExpressionNode _root = null;
        public IExpressionNode Root
        {
            get
            {
                return _root;
            }
        }

        public Expression(string expressionString)
        {
            _root = CreateExpressionTree(expressionString);
        }

        private IExpressionNode CreateExpressionTree(string expressionString)
        {
            return null;
        }


    }
}
