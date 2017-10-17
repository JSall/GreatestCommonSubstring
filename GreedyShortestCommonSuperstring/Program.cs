using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyShortestCommonSuperstring
{
    public class Program
    {
        static string[] Input1 = { "all is well", "ell that en", "hat end", "t ends well", null };
        static string[] Input2 = { "the black dog h", "og howls at th", "at the mo", "moon", "" };
        static string[] Input3 = { "all is well", "ell that en",  "t ends well","end", "ell", "that", "hat", };
        static string[] Input4 = { };
        static string[] Input5 = { "","",""};

        static void Main(string[] args)
        {
            List<string> input = Input1.ToList();            
            MergeAllAndPrint(input);
            input = Input2.ToList();
            MergeAllAndPrint(input);
            input = Input3.ToList();
            MergeAllAndPrint(input);
            input = Input4.ToList();
            MergeAllAndPrint(input);
            input = Input5.ToList();
            MergeAllAndPrint(input);

        }

        public static string MergeAll(List<string> input)
        {
            while (input.Count > 1)
            {
                MergeMaxSubstrings(input);
            }

            return input.FirstOrDefault();
        }

        public static void MergeAllAndPrint(List<string> input)
        {
            while (input.Count > 1)
            {
                MergeMaxSubstrings(input);
            }

            if (input.Count == 0)
            {
                Console.WriteLine("No Data");
            }
            else
            {
                Console.WriteLine(input.First());
            }
        }

        public static void MergeMaxSubstrings(List<string> input)
        {
            string merged = "";
            string maxMerged = "";
            int maxOverlap = 0;

            //Remove garbage data.
            input.RemoveAll(s => string.IsNullOrWhiteSpace(s));

            if (input.Count <= 1)
            {
                return;
            }

            //Let this Key Value Pair represent the indexes of the inputs to be merged.
            // Key = string to replace, Value = string to remove.            
            KeyValuePair<int, int> NodesToMerge = new KeyValuePair<int, int>(0, 1);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (i != j)
                    {
                        int overLap = FindOverlapLength(input[i], input[j], out merged);
                        if (overLap >= maxOverlap)
                        {
                            maxOverlap = overLap;
                            maxMerged = merged;
                            NodesToMerge = new KeyValuePair<int, int>(i, j);
                        }
                    }
                }
            }

            input[NodesToMerge.Key] = maxMerged;
            input.Remove(input[NodesToMerge.Value]);

        }
        
        public static int FindOverlapLength(string s1, string s2, out string mergedString)
        {
            int[,] overLaps = new int[s1.Length, s2.Length];
            int longestOverlap = 0;
            mergedString = "";
            string commonString = "";

            if (s1.Contains(s2))
            {
                mergedString = s1;
                return s2.Length;
            }

            if (s2.Contains(s1))
            {
                mergedString = s2;
                return s1.Length;
            }

            for (int i = 0; i < s1.Length; i++)
            {
                int s1Index = i;
                int s2Index = 0;
                int overlap = 0;
                string substring = "";
                while (s1Index < s1.Length && s2Index < s2.Length && s1[s1Index] == s2[s2Index])
                {
                    substring += (s1[s1Index]);
                    ++s1Index;
                    ++s2Index;
                    ++overlap;
                }

                if (overlap > longestOverlap)
                {
                    longestOverlap = overlap;
                    commonString = substring;
                }
            }

            if (longestOverlap == 0)
            {
                mergedString = s1 + s2;
            }
            else
            {
                int end = s1.IndexOf(commonString);
                int start = s2.IndexOf(commonString);
                mergedString = s1.Substring(0, end) + commonString + s2.Substring(start + commonString.Length);
            }

            return longestOverlap;
        }
    }
}
