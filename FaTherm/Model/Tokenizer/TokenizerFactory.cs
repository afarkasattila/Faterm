using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    class TokenizerFactory
    {
        public static ITokenizer<SimpleExpressionTokensEnum> GetSimpleTokenizer()
        {
            return new Tokenizer<SimpleExpressionTokensEnum>(new DeterministicFiniteAutomaton());
        }
    }
}
