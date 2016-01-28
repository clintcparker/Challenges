using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;
using System.Linq;

namespace CollatzConjecture.Library
{
    public class FileUserInputOutput : IUserInputOutput
    {
        private readonly string _outputFileLocation;
        public FileUserInputOutput(string outputFileLocation)
        {
            _outputFileLocation = outputFileLocation;
        }
        /// <summary>
        /// prints output to a file within the project 
        /// </summary>
        /// <param name="output"></param>
        public void PrintOutput(HashSet<Output> outputs)
        {
            var stringBuilder = new StringBuilder();

            using (var streamWriter = new StreamWriter(_outputFileLocation, false))
            {
                foreach (var output in outputs)
                {
                    streamWriter.WriteLine($"{output.Input.Start} {output.Input.End} {output.MaxCycle}");
                }
            }
            
        }
        /// <summary>
        /// reads the input from a static file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Input> ReadInput()
        {
            var inputList = new HashSet<Input>();

            //this is not entirely safe as the file could exhaust all memory
            //for our demonstrations we won't worry about it
            //typically this would foreach across read line by line to be safe
            //the use of Configuration Manager directly is discouraged. 
            //Wrap this in a class/interface and require it in the constructor as a dependency
            var lines = File.ReadAllLines(ConfigurationManager.AppSettings["fileLocation"]);

            foreach (var line in lines)
            {
                //space seperated. 
                var splitInput = line.Split(' ');

                //convert and put the input into a hash set
                inputList.Add(ConvertToInput(splitInput));
            }

            return inputList;
        }
        /// <summary>
        /// Converts string array of two pairs into an input
        /// </summary>
        /// <param name="splitInput"></param>
        /// <returns></returns>
        private Input ConvertToInput(string[] splitInput)
        {
            var start = 0;
            var end = 0;
            //make sure the string has what we need
            if(ValidateInput(splitInput))
            {
                int.TryParse(splitInput[0], out start);
                int.TryParse(splitInput[1], out end);
            }
            //how to construct an object and set properties in one call
            return new Input
            {
                Start = start,
                End = end
            };
        }

        private bool ValidateInput(string[] splitInput)
        {
            return splitInput.Length == 2;
        }
    }
}
