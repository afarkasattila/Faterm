using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    internal class DeterministicFiniteAutomaton
    {
        #region Data members

        /// <summary>
        /// The characters of the automaton.
        /// </summary>
        private string _inputCharacters;
        public string InputCharacters
        {
            get { return _inputCharacters;  }
        }

        /// <summary>
        /// Total number of the characters of the automaton.
        /// </summary>
        private int _inputCharacterCount;
        public int InputCharacterCount
        {
            get { return _inputCharacterCount; }
        }

        /// <summary>
        /// Dictionary mapping the characters to their indices.
        /// </summary>
        private Dictionary<char, int> _inputCharacterIndices = new Dictionary<char, int>();

        /// <summary>
        /// The number of states the automaton has.
        /// </summary>
        private int _stateCount = 0;
        public int StateCount
        {
            get { return _stateCount; }
        }
        /// <summary>
        /// Transition matrix storing the new state index for a (oldstate, character) pair.
        /// </summary>
        private int[,] transitions = null;
        //private Dictionary<int, char> transitions = null;

        // The first state is always the starting state.
        private int currentState = 0;

        #endregion


        #region Constructors
#if (DEBUG)

        /// <summary>
        /// Default constructor setting up a sample automaton. This is for testing purposes only.
        /// </summary>
        public DeterministicFiniteAutomaton()
        {
            // Set sample data
            _inputCharacters = "0123456789+-*/";
            _inputCharacterCount = InputCharacters.Count();

            for (int i = 0; i < InputCharacterCount; i++)
                _inputCharacterIndices[InputCharacters[i]] = i;

            // Set the default state count.
            _stateCount = 3;

            // Allocate the matrix with the required dimensions.
            transitions = new int[StateCount, InputCharacterCount];

            // Set each element of the matrix to -1.
            ClearMatrix(transitions, StateCount, InputCharacterCount, -1);

            // The first state is active when we have a number.
            for (int i = 0; i < 10; i++)
                SetTransition(0, i, 1);

            //transitions[0, 0] = 1;
            //transitions[0, 1] = 1;
            //transitions[0, 2] = 1;
            //transitions[0, 3] = 1;
            //transitions[0, 4] = 1;
            //transitions[0, 5] = 1;
            //transitions[0, 6] = 1;
            //transitions[0, 7] = 1;
            //transitions[0, 8] = 1;
            //transitions[0, 9] = 1;

            // The second state is active when we have an operator.
            for (int i = 11; i < 14; i++)
                SetTransition(0, i, 2);

            //transitions[0, 10] = 2;
            //transitions[0, 11] = 2;
            //transitions[0, 12] = 2;
            //transitions[0, 13] = 2;

            // If a number comes after a number stay in "number" state.
            for (int i = 0; i < 10; i++)
                SetTransition(1, i, 1);

            //transitions[1, 0] = 1;
            //transitions[1, 1] = 1;
            //transitions[1, 2] = 1;
            //transitions[1, 3] = 1;
            //transitions[1, 4] = 1;
            //transitions[1, 5] = 1;
            //transitions[1, 6] = 1;
            //transitions[1, 7] = 1;
            //transitions[1, 8] = 1;
            //transitions[1, 9] = 1;

            // If an operator comes after a number go to the third state.
            for (int i = 10; i < 14; i++)
                SetTransition(1, i, 2);

            //transitions[1, 10] = 2;
            //transitions[1, 11] = 2;
            //transitions[1, 12] = 2;
            //transitions[1, 13] = 2;

            // If a number comes after an operator go to the "number" state.
            for (int i = 0; i < 10; i++)
                SetTransition(2, i, 1);

            //transitions[2, 0] = 1;
            //transitions[2, 1] = 1;
            //transitions[2, 2] = 1;
            //transitions[2, 3] = 1;
            //transitions[2, 4] = 1;
            //transitions[2, 5] = 1;
            //transitions[2, 6] = 1;
            //transitions[2, 7] = 1;
            //transitions[2, 8] = 1;
            //transitions[2, 9] = 1;

            // If an operator comes after an other operator we have an error. There is no transition for that scenario.
        }

#endif

        public DeterministicFiniteAutomaton(int tokenCount, bool[,] transitions)
        { 
        }
        #endregion

        public bool SetTransition(int oldState, char character, int newstate)
        {
            // Parameter validation.
            ValidState(oldState);
            ValidState(newstate);
            ValidCharacter(character);

            SetTransitionWithoutValidation(oldState, _inputCharacterIndices[character], newstate);

            return true;
        }

        public bool SetTransition(int oldState, int characterIndex, int newstate)
        {
            if (oldState < 0 || oldState >= StateCount || newstate < 0 || newstate >= StateCount)
                return false;

            if (characterIndex < 0 || characterIndex >= InputCharacterCount)
                return false;

            SetTransitionWithoutValidation(oldState, characterIndex, newstate);

            return true;
        }

        private void SetTransitionWithoutValidation(int oldState, int characterIndex, int newstate)
        {
            // A -1 value means that there is no transition between states. 
            transitions[oldState, characterIndex] = newstate;
        }

        public int GetTransition(int oldState, char character)
        {
            // Parameter validation.
            ValidState(oldState);
            ValidCharacter(character);

            return transitions[oldState, _inputCharacterIndices[character]];
        }

        bool ValidState(int state)
        {
            if (state < 0 || state >= StateCount)
                throw new ArgumentOutOfRangeException("state", "Parameter is out of range.");

            return true;
        }

        bool ValidCharacter(char character)
        {
            if (InputCharacters.IndexOf(character) < 0)
                throw new ArgumentOutOfRangeException("character", "Character is not part of the automaton's alphabet.");

            return true;
        }

        bool ValidCharacter(int characterIndex)
        {
            if (characterIndex < 0 || characterIndex >= InputCharacterCount)
                throw new ArgumentOutOfRangeException("characterIndex", "Parameter is out of range.");

            return true;
        }

        /// <summary>
        /// Sets a two dimensional array to a given value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="firstDimension"></param>
        /// <param name="secondDimension"></param>
        /// <param name="defaultValue"></param>
        private void ClearMatrix(int[,] matrix, int firstDimension, int secondDimension, int defaultValue = -1)
        {
            for (int i = 0; i < firstDimension; i++)
                for (int j = 0; j < secondDimension; j++)
                    matrix[i, j] = defaultValue;
        }

        /// <summary>
        /// Reads a caracter and changes the automaton's state accordingly. It also validates the input character.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>The new state after processing the input character.</returns>
        public int ReadCharacter(char character)
        {
            // Parameter validation.
            ValidCharacter(character);

            return currentState = transitions[currentState, _inputCharacterIndices[character]];
        }


    }
}
