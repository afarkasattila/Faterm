using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    /// <summary>
    /// Templated class which tokenizes a string.
    /// TokensEnmum should have a "None" type having 0 as int value.
    /// </summary>
    /// <typeparam name="TokensEnum"></typeparam>
    class Tokenizer<TokensEnum> : ITokenizer<TokensEnum>
        where TokensEnum : IConvertible
    {
        private DeterministicFiniteAutomaton Automaton = null;

        private string _stringToTokenize = null;
        private int _currentPositionInString = -1;
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
            ResetTokenizer();
        }

        private bool GetNextToken(out string tokenValue, out TokensEnum tokenType)
        {
            #region Verification of starting conditions.
            // If we have an empty string return the default values.
            if (_stringToTokenize == null || _stringToTokenize.Equals(""))
            {
                tokenType = (TokensEnum)(object)0;
                tokenValue = "";
                return true;
            }

            // If there are no more characters to read no token can be returned.
            if (_currentPositionInString > _stringToTokenize.Length)
            {
                tokenValue = "";
                tokenType = (TokensEnum)(object)0;

                return false;
            }
            #endregion

            // Variable storing the beginning of the token in the string. 
            // Unless we are determining the first token we already read the first caracter of the current token
            // (in order to discover that the previous token finished). Decrease the pointer by 1 for this reason.
            int oldPositionInString = _currentPositionInString - 1;

            int currentState;

            // If the tokenization was not yet started red the first character and calculate the current state.
            if (_currentPositionInString == -1)
            {
                try
                {
                    currentState = Automaton.ReadCharacter(_stringToTokenize[++_currentPositionInString]);
                }
                // If the first character is invalid return the untyped token and the whole string.
                // Set the pointer within the string beyond the string length in order to finish the tokenization.
                catch (ArgumentOutOfRangeException) 
                {
                    tokenType = (TokensEnum)(object)0;
                    tokenValue = _stringToTokenize;
                    _currentPositionInString = _stringToTokenize.Length + 1;

                    return true;
                }
                    
                // First run. Set the token's string start position to 0.
                oldPositionInString = 0;
            }
            // Else use the last stored state as current state.
            else
                currentState = _oldstate;

            // If the first character wasn't processed successfully the tokenization failed.
            if (currentState == -1)
            {
                tokenValue = _stringToTokenize.Substring(_currentPositionInString,
                    _stringToTokenize.Length - _currentPositionInString);
                tokenType = (TokensEnum)(object)0;

                return false;
            }

            // Old state will be used to monitor when the state changes meaning we found the end of the token
            // and a new token is starting.
            int oldState = currentState;

            // Read character until state (meaning token) changes and there are characters left.
            while (oldState == currentState && _currentPositionInString < _stringToTokenize.Length)
            {
                try
                {
                    currentState = Automaton.ReadCharacter(_stringToTokenize[_currentPositionInString++]);
                }
                // If an invalid character has been encountered return the remaining string 
                // as the last untyped token.
                // Set the pointer within the string beyond the string length in order to finish the tokenization.
                catch (ArgumentOutOfRangeException)
                {
                    tokenType = (TokensEnum)(object)0;
                    tokenValue = _stringToTokenize.Substring(oldPositionInString, _stringToTokenize.Length - oldPositionInString);
                    _currentPositionInString = _stringToTokenize.Length + 1;

                    return true;
                }
            }

            // If the old state differs from the current state one character was read ahead.
            // Otherwise the loop stopped since the end of the string was reached: no characters read ahead. 
            int charactersReadAhead = oldState != currentState ? 1 : 0;

            tokenType = (TokensEnum)(object)oldState;
            tokenValue = _stringToTokenize.Substring(oldPositionInString, _currentPositionInString - oldPositionInString - charactersReadAhead);

            // If the state changed store the new state for later when the next token is determined.
            if (oldState != currentState)
                _oldstate = currentState;
            // Otherwise the end of the string is reached. Increase the pointer beyond string length.
            else
                _currentPositionInString++;

            return true;
        }

        private void ResetTokenizer()
        {
            _currentPositionInString = -1;
            _oldstate = -1;
        }

        public List<Tuple<string, TokensEnum>> GetTokens()
        {
            #region Verification of starting conditions.
            // If we have an empty string return the default values.
            if (_stringToTokenize == null || _stringToTokenize.Equals(""))
            {
                return new List<Tuple<string,TokensEnum>>();
            }
            #endregion

            string currentTokenStr;
            TokensEnum currentTokenType;
            List<Tuple<string, TokensEnum>> retVal = new List<Tuple<string, TokensEnum>>();

            while (GetNextToken(out currentTokenStr, out currentTokenType))
            {
                retVal.Add(new Tuple<string, TokensEnum>(currentTokenStr, currentTokenType));
            }

            // After getting all the tokens reset the tokenizer for any subsequent calls.
            ResetTokenizer();

            return retVal;
        }
    }
}
