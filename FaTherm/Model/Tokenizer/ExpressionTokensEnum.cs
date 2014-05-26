using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    enum ExpressionTokensEnum
    {
        None = 0,
        Constant = 1,
        Variable = 2,
        Operator = 3,
        Function = 4
    }
}
