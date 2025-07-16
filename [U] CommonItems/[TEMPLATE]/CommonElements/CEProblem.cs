using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "CommonItems"; } }

        public override void TryMyCode()
        {

            /* WRITE 4~6 DIFFERENT CASES FOR TRACE, EACH WITH
             * 1) SMALL INPUT SIZE
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT FROM THE FUNCTION
             * 4) PRINT THE CASE 
             */

            int[] output;
            int[] expected;

            int[] arr1 = { 1, 8, -1 };
            int[] arr2 = { 1, 9, -1, 15, 18, 33, 0, 4, 7 };

            output = PROBLEM_CLASS.RequiredFuntion(arr1, arr2);
            expected = new int[] { -1, 1 };
            PrintCase(arr1, arr2, output, expected);



            int[] arr3 = { 1 };
            int[] arr4 = { -1 };

            output = PROBLEM_CLASS.RequiredFuntion(arr3, arr4);
            expected = new int[] { };
            PrintCase(arr3, arr4, output, expected);



            int[] arr5 = { 6, 7, 8, 9, 10 };
            int[] arr6 = { -1, 0, 1, 2, 3, 4, 5, 6 };

            output = PROBLEM_CLASS.RequiredFuntion(arr5, arr6);
            expected = new int[] { 6 };
            PrintCase(arr5, arr6, output, expected);


            int[] arr7 = { };
            int[] arr8 = { 9, 12, 88, 5 };

            output = PROBLEM_CLASS.RequiredFuntion(arr7, arr8);
            expected = new int[] { };
            PrintCase(arr7, arr8, output, expected);



            int[] arr9 = { 1, 3, 5, 9, 11, 13, 15, 17, 19 };
            int[] arr10 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            output = PROBLEM_CLASS.RequiredFuntion(arr9, arr10);
            expected = new int[] { 1, 3, 5, 9 };
            PrintCase(arr9, arr10, output, expected);


            int[] arr11 = { 100 };
            int[] arr12 = { 100 };

            output = PROBLEM_CLASS.RequiredFuntion(arr11, arr12);
            expected = new int[] { 100 };
            PrintCase(arr9, arr10, output, expected);

        }

        Thread tstCaseThr;
        bool caseTimedOut;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            /* READ THE TEST CASES FROM THE SPECIFIED FILE, FOR EACH CASE DO:
             * 1) READ ITS INPUT & EXPECTED OUTPUT
             * 2) READ ITS EXPECTED TIMEOUT LIMIT (IF ANY)
             * 3) CALL THE FUNCTION ON THE GIVEN INPUT USING THREAD WITH THE GIVEN TIMEOUT 
             * 4) CHECK THE OUTPUT WITH THE EXPECTED ONE
             */

            int testCases;
            int N1 = 0;
            int N2 = 0;
            int[] arr1 = null;
            int[] arr2 = null;
            int[] actualResult = null;
            int[] output = null;
            int actualResultlen = 0;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
            bool readTimeFromFile = false;
            if (timeOutInMillisec == -1)
            {
                readTimeFromFile = true;
                //readTimeFromFile = false;
            }
            int i = 1;
            while (testCases-- > 0)
            {
                N1 = br.ReadInt32();
                arr1 = new int[N1];
                for (int j = 0; j < N1; j++)
                {
                    arr1[j] = br.ReadInt32();
                }

                N2 = br.ReadInt32();
                arr2 = new int[N2];
                for (int j = 0; j < N2; j++)
                {
                    arr2[j] = br.ReadInt32();
                }

                actualResultlen = br.ReadInt32();

                actualResult = new int[actualResultlen];

                for (int j = 0; j < actualResultlen; j++)
                {
                    actualResult[j] = br.ReadInt32();
                }

                //Console.WriteLine("N = {0}, Res = {1}", N, actualResult);

                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            //int sum = 0;
                            int numOfRep = 1;
                            Stopwatch sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                output = PROBLEM_CLASS.RequiredFuntion(arr1, arr2);

                            }
                            //output = sum / numOfRep;
                            sw.Stop();

                            //Console.WriteLine("N = {0}, time in ms = {1}", arr.Length, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = null;

                        }
                        caseTimedOut = false;
                    });

                    if (readTimeFromFile)
                    {
                        timeOutInMillisec = br.ReadInt32();
                    }
                    /*LARGE TIMEOUT FOR SAMPLE CASES TO ENSURE CORRECTNESS ONLY*/

                    if (level == HardniessLevel.Easy)
                    {
                        timeOutInMillisec = 1000; //Large Value 
                    }
                    /*=========================================================*/
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
                    tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }

                else if (output.Length == 0 && actualResult.Length == 0)
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    //Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    correctCases++;
                }
                else if (output.Length != 0 && actualResult.Length != 0)   //Passed
                {
                    if (output.OrderBy(x => x).SequenceEqual(actualResult.OrderBy(x => x)))
                    {
                        Console.WriteLine("Test Case {0} Passed!", i);
                        correctCases++;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Answer in Case {0}.", i);
                        wrongCases++;
                        //Console.WriteLine(output.Length);
                        //Console.WriteLine(actualResult.Length);
                    }
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0));
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        /// <summary>
        /// Generate a file of test cases according to the specified params
        /// </summary>
        /// <param name="level">Easy or Hard</param>
        /// <param name="numOfCases">Required number of cases</param>
        /// <param name="includeTimeInFile">specify whether to include the expected time for each case in the file or not</param>
        /// <param name="timeFactor">factor to be multiplied by the actual time</param>
        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int[] arr1, int[] arr2, int[] output, int[] expected)
        {
            /* PRINT THE FOLLOWING
             * 1) INPUT
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT
             * 4) WHETHER IT'S CORRECT OR WRONG
             * */
            Console.WriteLine("Array1: ");
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.Write(arr1[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Array2: ");
            for (int i = 0; i < arr2.Length; i++)
            {
                Console.Write(arr2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Expected Output: ");
            for (int i = 0; i < expected.Length; i++)
            {
                Console.Write(expected[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Returned Output: ");
            for (int i = 0; i < output.Length; i++)
            {
                Console.Write(output[i] + " ");
            }
            Console.WriteLine();
            if (output.OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x)))
            {
                Console.WriteLine("Correct!!");
            }
            else
            {
                Console.WriteLine("Wrong Answer");
            }
            Console.WriteLine("-----------------------------");

        }

        #endregion

    }
}
