using System.Collections.Generic;

namespace CollatzConjecture.Library
{
    public interface IUserInputOutput
    {
        /// <summary>
        /// implement how and where you read input from
        /// </summary>
        /// <returns></returns>
        IEnumerable<Input> ReadInput();

        /// <summary>
        /// implement how you print output 
        /// </summary>
        /// <param name="output"></param>
        void PrintOutput(HashSet<Output> outputs);
    }
}
