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

        private string _stringToTokenize = null;
        private int _currentPositionInString = 0;
        private int _oldstate = -1;

        public Tokenizer(DeterministicFiniteAutomaton automaton/*, Enum tokens*/)
        {
            Automaton = automaton;

            if (!typeof(TokensEnum).IsEnum)
                throw new ArgumentException("TokensEnum must be an enumerated type.");
        }

        public void SetStringToTokenize(string s)
        {
            _stringToTokenize = s;
            _currentPositionInString = 0;
            _oldstate = -1;
        }

        private bool GetNextToken(ref TokensEnum tokenType, ref string tokenValue)
        {
            // If we have an empty string return the default values.
            if (_stringToTokenize == null || _stringToTokenize.Equals(""))
            {
                tokenType = (TokensEnum)(object)0;
                tokenValue = "";
                return true;
            }

            int oldState = Automaton.ReadCharacter(_stringToTokenize[_currentPositionInString++]), 
                currentState = oldState;

            // If the first character wasn't processed successfully the tokenization failed.
            if (oldState == -1)
                return false;

            while (oldState == currentState)
            {
                currentState = Automaton.ReadCharacter(_stringToTokenize[_currentPositionInString++]);
            }

            tokenType = (TokensEnum)(object)oldState;
            //tokenType = Enum.ToObject(typeof(TokensEnum) , oldState));

            return true;
        }

        public List<Enum> GetTokens()
        {
            return null;
        }
    }
}
