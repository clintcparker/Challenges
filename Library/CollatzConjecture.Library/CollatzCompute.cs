using System;
using System.Collections.Generic;

namespace CollatzConjecture.Library
{
    public class CollatzCompute : IChallengeCalculator
    {
        public CollatzCompute()
        {
            InitializeLookup();
        }
        #region Static Helpers
        static Dictionary<int, int> _CollatzLookup;
        static void InitializeLookup()
        {
            if(_CollatzLookup == null)
            {
                _CollatzLookup = new Dictionary<int, int>();

                // seed with powers of 2.
                // 2^p has a cycle length of p+1.
                // 16 = 2^4: {16, 8, 4, 2, 1}

                for (int i = 1; i <= MAX_EXPONENT; i++)
                {
                    // Slightly faster than Math.Pow()
                    _CollatzLookup.Add(1 << i, i + 1);
                }
            }
        }
        const int MAX_EXPONENT = 24;
        const int MILLION = 1000000;

        static bool CheckNumber(int num)
        {
            return num > 0 && num <= MILLION;
        }

        static int f(int num)
        {
            if (num % 2 == 0)
            {
                return num / 2;
            }
            else if (num == 1)
            {
                return 1;
            }
            else
            {
                return 3 * num + 1;
            }
        }
        #endregion

        public int ComputeCycleLength(int num)
        {
            int tmpNum = num;
            var sequence = new List<int>();
            int cycleLength = 0;
            bool calculated = false;

            sequence.Add(tmpNum);

            do
            {
                tmpNum = f(tmpNum);
                sequence.Add(tmpNum);
                if (tmpNum == 1)
                {
                    calculated = true;
                }
                else if (_CollatzLookup.ContainsKey(tmpNum))
                {
                    cycleLength = sequence.Count + _CollatzLookup[tmpNum] - 1; // The -1 is because we're counting the found lookup twice.
                    calculated = true;
                }

            } while (!calculated);

            if (cycleLength == 0)
            {
                cycleLength = sequence.Count;
            }

            if (!_CollatzLookup.ContainsKey(num))
            {
                _CollatzLookup.Add(num, cycleLength);
            }

            return cycleLength;
        }


        public int CalculateMaxCycles(Input input)
        {
            var current = 0;
            for (int num = input.Start; num <= input.End; num++)
            {
                if (CheckNumber(num))
                {
                    //cycleLengths.Add(ComputeCycleLength(num));
                    var cycleLength = ComputeCycleLength(num);
                    if (cycleLength > current)
                    {
                        current = cycleLength;
                    }
                }
                //not recommended but done for simplicity
                else
                {
                    Console.WriteLine("Number is invalid");
                    break;
                }
            }
            return current;
        }
    }
}
