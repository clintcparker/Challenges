using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollatzConjecture.Library;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;

namespace CollatzConjecture.UnitTests
{
    [TestClass]
    public class FileUserInputOutputTests
    {
        private string _fileOutLocation = ConfigurationManager.AppSettings["outputFileLocation"];
        [TestMethod]
        public void ReadFromFileInAppConfigTest()
        {
            var fileReader = new FileUserInputOutput(_fileOutLocation);

            var inputs = fileReader.ReadInput();
            IEnumerable<Input> expected = new HashSet<Input>
            {
                new Input
                {
                    Start = 1,
                    End = 10
                }
            };
            
            
            CollectionAssert.AreEqual(expected.ToList(), inputs.ToList());
        }

        [TestMethod]
        public void WriteToFileTest()
        {
            var fileWriter = new FileUserInputOutput(_fileOutLocation);

            var outputs = new HashSet<Output>
            {
                new Output
                {
                    Input = new Input
                    {
                        Start = 1,
                        End = 10
                    },
                    MaxCycle = 20
                }
            };

            fileWriter.PrintOutput(outputs);

            Assert.IsTrue(File.Exists(_fileOutLocation));
            //should add additional checks that read the file and compare to expected outputs

        }
    }
}
