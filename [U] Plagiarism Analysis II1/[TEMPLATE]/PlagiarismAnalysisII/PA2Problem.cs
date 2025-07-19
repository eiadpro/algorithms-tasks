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
        public override string ProblemName { get { return "PlagiarismAnalysisII"; } }

        public override void TryMyCode()
        {

            //Case1
            {
                Tuple<string, string, float>[] edges1 = new Tuple<string, string, float>[2];
                edges1[0] = new Tuple<string, string, float>("1", "4", 10);
                edges1[1] = new Tuple<string, string, float>("4", "5", 15);

                float expectedVal = 12.5f;
                List<string> expectedIDs = new List<string>(new string[] { "1", "4", "5" });
                float outputVal = 0;
                List<string> IDs = null;
                PROBLEM_CLASS.RequiredFunction(edges1, ref outputVal, ref IDs);
                PrintCase(edges1, outputVal, expectedVal, IDs, expectedIDs);
            }
            //Case2
            {
                Tuple<string, string, float>[] edges2 = new Tuple<string, string, float>[4];
                edges2[0] = new Tuple<string, string, float>("1", "2", 10);
                edges2[1] = new Tuple<string, string, float>("3", "4", 20);
                edges2[2] = new Tuple<string, string, float>("5", "6", 30);
                edges2[3] = new Tuple<string, string, float>("7", "8", 40);

                float expectedVal = 40;
                List<string> expectedIDs = new List<string>(new string[] { "8", "7" });
                float outputVal = 0;
                List<string> IDs = null;
                PROBLEM_CLASS.RequiredFunction(edges2, ref outputVal, ref IDs);
                PrintCase(edges2, outputVal, expectedVal, IDs, expectedIDs);
            }
            //Case3
            {
                Tuple<string, string, float>[] edges3 = new Tuple<string, string, float>[6];
                edges3[0] = new Tuple<string, string, float>("1", "2", 1);
                edges3[1] = new Tuple<string, string, float>("2", "3", 2);
                edges3[2] = new Tuple<string, string, float>("5", "4", 3);
                edges3[3] = new Tuple<string, string, float>("5", "6", 4);
                edges3[4] = new Tuple<string, string, float>("3", "5", 5);
                edges3[5] = new Tuple<string, string, float>("4", "2", 6);
                float expectedVal = 3.5f;
                List<string> expectedIDs = new List<string>(new string[] { "1", "2", "3", "4", "5", "6"});
                float outputVal = 0;
                List<string> IDs = null;
                PROBLEM_CLASS.RequiredFunction(edges3, ref outputVal, ref IDs);
                PrintCase(edges3, outputVal, expectedVal, IDs, expectedIDs);
            }
            //Case4
            {
                Tuple<string, string, float>[] edges4 = new Tuple<string, string, float>[11];
                edges4[0] = new Tuple<string, string, float>("1", "5", 10);
                edges4[1] = new Tuple<string, string, float>("1", "4", 10);
                edges4[2] = new Tuple<string, string, float>("1", "3", 10);
                edges4[3] = new Tuple<string, string, float>("1", "2", 10);
                edges4[4] = new Tuple<string, string, float>("2", "3", 15);
                edges4[5] = new Tuple<string, string, float>("3", "4", 15);
                edges4[6] = new Tuple<string, string, float>("4", "5", 15);
                edges4[7] = new Tuple<string, string, float>("5", "2", 15);
                edges4[8] = new Tuple<string, string, float>("6", "7", 30);
                edges4[9] = new Tuple<string, string, float>("6", "8", 40);
                edges4[10] = new Tuple<string, string, float>("8", "7", 30);

                float expectedVal = 33.3f;
                List<string> expectedIDs = new List<string>(new string[] { "8", "7", "6" });
                float outputVal = 0;
                List<string> IDs = null;
                PROBLEM_CLASS.RequiredFunction(edges4, ref outputVal, ref IDs);
                PrintCase(edges4, outputVal, expectedVal, IDs, expectedIDs);
            }
        }

        

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            float actualResult = float.MinValue;
            float output = float.MinValue;
            List<string> IDs = null;

            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            testCases = int.Parse(line);
   
            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
            bool readTimeFromFile = false;
            if (timeOutInMillisec == -1)
            {
                readTimeFromFile = true;
            }
            int i = 1;
            while (testCases-- > 0)
            {
                int e = int.Parse(sr.ReadLine());
               
                var edges = new Tuple<string, string, float>[e];
                for (int j = 0; j < e; j++)
                {
                    line = sr.ReadLine();
                    string[] lineParts = line.Split(',');
                    edges[j] = new Tuple<string, string, float>(lineParts[0], lineParts[1], float.Parse(lineParts[2]));
                }
                line = sr.ReadLine();
                actualResult = float.Parse(line);
                int numOfIDs = int.Parse(sr.ReadLine());
                List<string> expectedIDs = new List<string>(numOfIDs);
                for (int d = 0; d < numOfIDs; d++)
                {
                    expectedIDs.Add(sr.ReadLine());
                }
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            Stopwatch sw = Stopwatch.StartNew();
                            PROBLEM_CLASS.RequiredFunction(edges, ref output, ref IDs);
                            sw.Stop();
                            //PrintCase(vertices,edges, output, actualResult);
                            Console.WriteLine("|E| = {0}, time in ms = {1}", edges.Length, sw.ElapsedMilliseconds);
                            Console.WriteLine("{0}", Math.Round(output,1));
                        }
                        catch
                        {
                            caseException = true;
                            output = float.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    if (readTimeFromFile)
                    {
                        timeOutInMillisec = int.Parse(sr.ReadLine().Split(':')[1]);
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
                //PrintCase(edges, output, actualResult, IDs, expectedIDs);

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
                else if (Math.Round(output, 1) == Math.Round(actualResult, 1) && (IDs.OrderBy(x => x).SequenceEqual(expectedIDs.OrderBy(x => x))))    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    //Console.WriteLine(" your answer = {0}, correct answer = {1}", Math.Round(output,1), Math.Round(actualResult,1));
                    wrongCases++;
                }

                i++;
            }
            file.Close();
            sr.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();

        }

        #endregion

        #region Helper Methods
        private static void PrintCase(Tuple<string, string, float>[] edges, float outputVal, float expectedVal, List<string> IDs, List<string> expectedIDs)
        {
            
            Console.WriteLine("Edges: ");
            for (int i = 0; i < edges.Length; i++)
            {
                Console.WriteLine("{0}, {1} score = {2}", edges[i].Item1, edges[i].Item2, edges[i].Item3);
            }
            Console.WriteLine("Output Value: {0}", Math.Round(outputVal,1));
            Console.Write("Output IDs: ");
            foreach(string s in  IDs)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Expected Value: {0}", Math.Round(expectedVal,1));
            Console.Write("Expected IDs: ");
            foreach (string s in expectedIDs)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine(); 
            
            if (Math.Round(outputVal,1) == Math.Round(expectedVal,1) && (IDs.OrderBy(x => x).SequenceEqual(expectedIDs.OrderBy(x => x))))    //Passed
            {
                Console.WriteLine("CORRECT");
            }
            else                    //WrongAnswer
            {
                Console.WriteLine("WRONG");
            }
            Console.WriteLine();
        }
        
        #endregion
   
    }
}
