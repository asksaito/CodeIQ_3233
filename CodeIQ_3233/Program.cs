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
            //string[] input = { "40", "49" }; // ?
            string[] input = Console.ReadLine().Split(' ');

            int count = int.Parse(input[0]);    // m
            int distance = int.Parse(input[1]); // n

            int patternCount = CalcLockPattern(count, distance);
            
            Console.WriteLine(patternCount.ToString());
        }

        private static int CalcLockPattern(int remainCount, int distance)
        {
            int patternCount = 0;

            int lowestMoveCount = distance - ((remainCount - 1) * 9);
            if(lowestMoveCount <= 1)
            {
                lowestMoveCount = 1;
            }
            for (int moveCount = lowestMoveCount; moveCount <= distance - remainCount + 1; moveCount++)
            {
                if(moveCount > 9)
                {
                    break;
                }

                //if (nextLeft)
                //{
                //    // 左に回す
                //    current = turnLeft(current, moveCount);
                //}
                //else
                //{
                //    // 右に回す
                //    current = turnRight(current, moveCount);
                //}

                if (remainCount > 2)
                {
                    // 再帰
                    patternCount += CalcLockPattern(remainCount - 1, distance - moveCount);
                }
                else
                {
                    patternCount++;
                }
            }

            return patternCount;
        }

        //private static int turnLeft(int initial, int moveCount)
        //{
        //    return (initial + moveCount) % 10;
        //}

        //private static int turnRight(int initial, int moveCount)
        //{
        //    return (initial + 50 - moveCount) % 50;
        //}
    }
}
