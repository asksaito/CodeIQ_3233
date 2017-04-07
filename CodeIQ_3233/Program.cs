using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIQ_3233
{
    class Program
    {
        static void Main(string[] args)
        {
            ;
            //string[] input = { "3", "6" }; // 10
            //string[] input = { "4", "10" }; // 84
            //string[] input = { "5", "20" }; // 2826
            //string[] input = { "10", "30" }; // 8337880
            string[] input = { "40", "49" }; // 1677106600 ? (2:30)
            //string[] input = { "9", "18" }; // 24301
            //string[] input = Console.ReadLine().Split(' ');

            int count = int.Parse(input[0]);    // m
            int distance = int.Parse(input[1]); // n

            currentPattern = new int[count];
            targetDistance = distance;

            //int patternCount = CalcLockPattern(count, distance);
            //CalcLockPatternList(count, distance, "{");
            long patternCount = CalcPattern(count, distance);

            Console.WriteLine(patternCount.ToString());
            //foreach (string pat in patternList)
            //{
            //    Console.WriteLine(pat);
            //}
        }

        private static int CalcLockPattern(int remainCount, int distance)
        {
            int patternCount = 0;

            //System.Diagnostics.Debug.Assert(remainCount <= distance);

            if (remainCount == distance)
            {
                // ダイヤル回転回数と総移動距離が同じ場合、必ず1パターンに定まる
                return 1;
            }

            // 最小移動距離
            int lowestMoveCount = distance - ((remainCount - 1) * 9);
            if (lowestMoveCount <= 1)
            {
                lowestMoveCount = 1;
            }

            // 最大移動距離
            int highestMoveCount = distance - remainCount + 1;
            if (highestMoveCount > 9)
            {
                highestMoveCount = 9;
            }

            if (remainCount <= 2)
            {
                return highestMoveCount - lowestMoveCount + 1;
            }

            if (distance <= 10)
            {
                //int patternSum = 0;
                int pattern = distance - remainCount + 1;
                for (int i = pattern; i > 0; i--)
                {
                    patternCount += i;
                    for (int j = i - 1; j > 0; j--)
                    {
                        patternCount += j;
                    }
                }

                return patternCount;
            }

            for (int moveCount = lowestMoveCount; moveCount <= highestMoveCount; moveCount++)
            {
                // 再帰呼び出し
                patternCount += CalcLockPattern(remainCount - 1, distance - moveCount);
            }

            return patternCount;
        }

        //private static void CalcLockPatternList(int remainCount, int distance, string pattern)
        //{

        //    if (remainCount == 1)
        //    {
        //        patternList.Add(pattern + " " + distance.ToString() + " }");
        //    }

        //    // 最小移動距離
        //    int lowestMoveCount = distance - ((remainCount - 1) * 9);
        //    if (lowestMoveCount <= 1)
        //    {
        //        lowestMoveCount = 1;
        //    }

        //    // 最大移動距離
        //    int highestMoveCount = distance - remainCount + 1;
        //    if (highestMoveCount > 9)
        //    {
        //        highestMoveCount = 9;
        //    }


        //    for (int moveCount = lowestMoveCount; moveCount <= highestMoveCount; moveCount++)
        //    {
        //        // 再帰呼び出し
        //        CalcLockPatternList(remainCount - 1, distance - moveCount, pattern + " " + moveCount.ToString());

        //    }

        //}

        private static long CalcPattern(int count, int distance)
        {
            long patternCnt = 0;
            int idx = currentPattern.Length - count;

            if (count == 1)
            {
                currentPattern[idx] = distance;
                if (currentPattern.Sum() == targetDistance)
                {
                    patternCnt = CalcPermutation(currentPattern);
                }
                currentPattern[idx] = 0;
            }
            else
            {
                // 最小移動距離
                int lowestMoveCount = distance - ((count - 1) * 9);
                if (lowestMoveCount <= 1)
                {
                    lowestMoveCount = 1;
                }

                if (idx > 0)
                {
                    if (currentPattern[idx - 1] > lowestMoveCount)
                    {
                        lowestMoveCount = currentPattern[idx - 1];
                    }
                }

                // 最大移動距離
                int highestMoveCount = distance - count + 1;
                if (highestMoveCount > 9)
                {
                    highestMoveCount = 9;
                }


                for (int moveCount = lowestMoveCount; moveCount <= highestMoveCount; moveCount++)
                {
                    currentPattern[idx] = moveCount;
                    if (currentPattern[idx] > distance - moveCount)
                    {
                        currentPattern[idx] = 0;
                        break;
                    }


                    // 再帰呼び出し
                    patternCnt += CalcPattern(count - 1, distance - moveCount);
                }
            }

            return patternCnt;
        }

        private static long CalcPermutation(int[] pattern)
        {
            //foreach (int p in pattern) {
            //    Console.Write(p + " ");
            //}
            //Console.WriteLine("");

            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;

            foreach (int p in pattern)
            {
                switch (p)
                {
                    case 1:
                        num1++;
                        break;
                    case 2:
                        num2++;
                        break;
                    case 3:
                        num3++;
                        break;
                    case 4:
                        num4++;
                        break;
                    case 5:
                        num5++;
                        break;
                    case 6:
                        num6++;
                        break;
                    case 7:
                        num7++;
                        break;
                    case 8:
                        num8++;
                        break;
                    case 9:
                        num9++;
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false);
                        break;
                }
            }
            //int maxNum = Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(num1, num2),num3),num4),num5),num6),num7),num8),num9);
            List<int> sort = new List<int>() { num1, num2, num3, num4, num5, num6, num7, num8, num9 };
            sort.Sort();
            sort.Reverse();

            // n! / r1! + r2!...
            int nCount = pattern.Count() - sort[0];

            long permutationResult = Factorial(pattern.Count(), nCount);
            for (int i = 1; i < sort.Count; i++)
            {
                permutationResult /= Factorial(sort[i], sort[i]);
            }

            return permutationResult;
        }

        private static long Factorial(int num, int cnt)
        {
            if (cnt == 0)
            {
                return 1L;
            }

            if (num == 0)
            {
                return 1L;
            }
            else
            {
                return num * Factorial(num - 1, cnt - 1);
            }
        }

        //private static List<string> patternList = new List<string>();
        private static int[] currentPattern = null;
        //private static int idx = 0;
        private static int targetDistance = 0;
    }
}
