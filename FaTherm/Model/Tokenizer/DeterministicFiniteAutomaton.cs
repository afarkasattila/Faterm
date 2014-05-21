using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaTherm.Model.Tokenizer
{
    internal class DeterministicFiniteAutomaton
    {
        int tokenCount = 0;

        string inputCharacters;
        int inputCharacterCount;
        Dictionary<char, int> inputCharacterIndices;
        
        int stateCount = 0;
        int[,] transitions = null;

        // The first state is always the starting state.
        int currentState = 0;

        public DeterministicFiniteAutomaton()
        {
            //int sampleTokenCount = 3;
            inputCharacters = "0123456789+-*/";
            inputCharacterCount = inputCharacters.Count();

            int sampleStateCount = 3;
            int[,] sampleTransitions = new int[sampleStateCount, inputCharacterCount];

            // The first state is active when we have a number.
            sampleTransitions[0, 0] = 1;
            sampleTransitions[0, 1] = 1;
            sampleTransitions[0, 2] = 1;
            sampleTransitions[0, 3] = 1;
            sampleTransitions[0, 4] = 1;
            sampleTransitions[0, 5] = 1;
            sampleTransitions[0, 6] = 1;
            sampleTransitions[0, 7] = 1;
            sampleTransitions[0, 8] = 1;
            sampleTransitions[0, 9] = 1;

            // The second state is active when we have an operator.
            sampleTransitions[0, 10] = 2;
            sampleTransitions[0, 11] = 2;
            sampleTransitions[0, 12] = 2;
            sampleTransitions[0, 13] = 2;

            // If a number comes after a number stay in "number" state.
            sampleTransitions[1, 0] = 1;
            sampleTransitions[1, 1] = 1;
            sampleTransitions[1, 2] = 1;
            sampleTransitions[1, 3] = 1;
            sampleTransitions[1, 4] = 1;
            sampleTransitions[1, 5] = 1;
            sampleTransitions[1, 6] = 1;
            sampleTransitions[1, 7] = 1;
            sampleTransitions[1, 8] = 1;
            sampleTransitions[1, 9] = 1;

            // If a number comes after an operator go to the "number" state.
            sampleTransitions[2, 0] = 1;
            sampleTransitions[2, 1] = 1;
            sampleTransitions[2, 2] = 1;
            sampleTransitions[2, 3] = 1;
            sampleTransitions[2, 4] = 1;
            sampleTransitions[2, 5] = 1;
            sampleTransitions[2, 6] = 1;
            sampleTransitions[2, 7] = 1;
            sampleTransitions[2, 8] = 1;
            sampleTransitions[2, 9] = 1;

            //tokenCount = 3;
            transitions = sampleTransitions;
        }

        public DeterministicFiniteAutomaton(int tokenCount, bool[,] transitions)
        { 
        }
    }
}
