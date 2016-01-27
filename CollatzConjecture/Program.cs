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
        static Dictionary<int, int> _CollatzLookup;

        static void Main(string[] args)
        {
            InitializeLookup();

            int num = 22;
            int tmpNum = num;

            if (CheckNumber(num))
            {
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
                        cycleLength = sequence.Count + _CollatzLookup[tmpNum];
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

                cycleLength = _CollatzLookup[num];
                Console.WriteLine("DONE. Cycle length: {0}", cycleLength);
            }
            else
            {
                Console.WriteLine("Number is invalid");
            }

            Console.ReadLine();
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
            // 2^p has p steps to 1.

            for (int i = 1; i <= MAX_EXPONENT; i++)
            {
                // Slightly faster than Math.Pow()
                _CollatzLookup.Add(1 << i, i + 1);
            }
        }
    }
}
