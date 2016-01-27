using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollatzConjecture
{
    class Program
    {
        const int MAX_EXPONENT = 24;
        const int MILLION = 1000000;
        //const int MAX_ITERATIONS = 10000000; // Try 10 million iterations and then stop.

        static Dictionary<int, int> _CollatzLookup;

        static void Main(string[] args)
        {
            InitializeLookup();

            int i = 900, j = 1000;
            var cycleLengths = new List<int>();
            for (int num = i; num <= j; num++)
            {
                if (CheckNumber(num))
                {
                    cycleLengths.Add(ComputeCycleLength(num));
                }
                else
                {
                    Console.WriteLine("Number is invalid");
                    break;
                }
            }

            Console.WriteLine("Max cycle length between {0} and {1} is: {2}", i, j, cycleLengths.Max());
            Console.ReadLine();
        }

        static int ComputeCycleLength(int num)
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

        static bool CheckNumber(int num)
        {
            return num > 0 && num <= MILLION;
        }

        /// <summary>
        /// f(n) =  { n/2, if n is even
        ///         { 3n+1, if n is odd and > 1
        ///         { 1, otherwise
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
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

        static void InitializeLookup()
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
}
