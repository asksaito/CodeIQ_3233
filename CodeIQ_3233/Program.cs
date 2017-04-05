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
        {;
            //string[] input = { "3", "6" }; // 10
            //string[] input = { "4", "10" }; // 84
            //string[] input = { "5", "20" }; // 2826
            //string[] input = { "10", "30" }; // 8337880
            string[] input = { "40", "49" }; // 1677106600 ? (2:30)
            //string[] input = { "9", "18" }; // 24301
            //string[] input = Console.ReadLine().Split(' ');

            int count = int.Parse(input[0]);    // m
            int distance = int.Parse(input[1]); // n

            int patternCount = CalcLockPattern(count, distance);
            
            Console.WriteLine(patternCount.ToString());
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
            if(lowestMoveCount <= 1)
            {
                lowestMoveCount = 1;
            }

            // 最大移動距離
            int highestMoveCount = distance - remainCount + 1;
            if(highestMoveCount > 9)
            {
                highestMoveCount = 9;
            }

            if(remainCount <= 2)
            {
                return highestMoveCount - lowestMoveCount + 1;
            }

            for (int moveCount = lowestMoveCount; moveCount <= highestMoveCount; moveCount++)
            {
                // 再帰呼び出し
                patternCount += CalcLockPattern(remainCount - 1, distance - moveCount);
            }

            return patternCount;
        }
        
    }
}
