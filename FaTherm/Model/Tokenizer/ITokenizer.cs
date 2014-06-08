using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    interface ITokenizer<TokensEnum>
    {
        void SetStringToTokenize(string stringToTokenize);
        List<Tuple<string, TokensEnum>> GetTokens();
    }
}
