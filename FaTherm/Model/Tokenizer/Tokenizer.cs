using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    class Tokenizer<TokensEnum> where TokensEnum : IConvertible
    {
        private DeterministicFiniteAutomaton Automaton = null;
        //private Enum Tokens = null;

        private string _stringToTokenize = null;
        private int _currentPositionInString = 0;

        public Tokenizer(DeterministicFiniteAutomaton automaton/*, Enum tokens*/)
        {
            Automaton = automaton;
            //Tokens = tokens;
        }

        private int GetNextToken(ref Enum tokenType, ref string tokenValue)
        {
            if (_stringToTokenize == null || _stringToTokenize.Equals(""))
                return 0;

            int oldState = Automaton.ReadCharacter(_stringToTokenize[_currentPositionInString++]), currentState = oldState;

            while (oldState == currentState)
            {
            }

            return 0;
        }

        public List<Enum> GetTokens()
        {
            return null;
        }
    }
}
