using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIQ_3233
{
    class Program
    {
        /// <summary>
        /// 現在のパターンを保持しておく一時領域
        /// </summary>
        private static int[] patternTemp = null;
        /// <summary>
        /// 目的の合計値を保持しておく一時領域
        /// </summary>
        private static int totalDistance = 0;

        static void Main(string[] args)
        {
            //string[] input = { "3", "6" }; // 10
            //string[] input = { "4", "10" }; // 84
            //string[] input = { "5", "20" }; // 2826
            //string[] input = { "10", "30" }; // 8337880
            //string[] input = { "40", "49" }; // 1677106600
            //string[] input = { "9", "18" }; // 24301

            // 標準入力からインプットを取得
            string[] input = Console.ReadLine().Split(' ');

            int count = int.Parse(input[0]);    // m
            int distance = int.Parse(input[1]); // n

            // パターン作成一時領域を確保しておく
            patternTemp = new int[count];
            // 目的の合計値を保持しておく
            totalDistance = distance;

            long result = CalcPatternCount(count, distance);

            // 標準出力に結果出力
            Console.WriteLine(result.ToString());
        }

        /// <summary>
        /// パターン数を計算する（再帰処理）
        /// </summary>
        /// <param name="count">残りのダイヤル回転回数</param>
        /// <param name="distance">残りのダイヤル回転距離</param>
        /// <returns></returns>
        private static long CalcPatternCount(int count, int distance)
        {
            long patternCnt = 0;
            int idx = patternTemp.Length - count;

            if (count == 1)
            {
                if (distance <= 9)
                {
                    // 最後の数字
                    patternTemp[idx] = distance;
                    if (patternTemp.Sum() == totalDistance) // 作成したパターンの合計値が目的の合計値の場合
                    {
                        // その数字のパターンで取り得るすべての重複順列を計算する。
                        patternCnt = CalcPermutation(patternTemp);
                    }
                }
            }
            else
            {   
                int lowestNum = 1;    // 組み合わせ候補の最小値
                int highestNum = 9;   // 組み合わせ候補の最大値
                if (idx > 0)
                {
                    if (patternTemp[idx - 1] > lowestNum)
                    {
                        // パターン領域のひとつ前の値より大きい値を組み合わせの対象とする
                        lowestNum = patternTemp[idx - 1];
                    }
                }


                for (int num = lowestNum; num <= highestNum; num++)
                {
                    // 組み合わせパターンを作成
                    patternTemp[idx] = num;
                    if (patternTemp[idx] > distance - num)
                    {
                        // 組み合わせパターンが作成出来なくなったら途中で打ち切る
                        break;
                    }


                    // 再帰呼び出し
                    patternCnt += CalcPatternCount(count - 1, distance - num);
                }
            }

            // 最後の数字を削除（一時領域の再利用のため）
            patternTemp[idx] = 0;

            return patternCnt;
        }

        /// <summary>
        /// 重複順列の組み合わせ数の計算
        /// </summary>
        /// <param name="pattern">組み合わせパターン</param>
        /// <returns>組み合わせ数</returns>
        private static long CalcPermutation(int[] pattern)
        {
            int[] overlapCount = new int[9];
            foreach (int p in pattern)
            {
                // 各数字の重複個数を調べる
                overlapCount[p - 1]++;
            }

            // 階乗の計算時間を抑えるために、あらかじめ一番重複が多いものを選んでおく（ソートの結果の先頭が一番大きな重複数）
            List<int> overlap = new List<int>(overlapCount);
            overlap.Sort();
            overlap.Reverse();
            
            int factorialCalcCount = pattern.Count() - overlap[0]; // 階乗の計算量を抑えるための工夫（一番重複個数が多いもので減算）

            // 重複順列の計算！ （n! / r1! + r2! ... r9!）
            long permutationResult = Factorial(pattern.Count(), factorialCalcCount);
            for (int i = 1; i < overlap.Count; i++)
            {
                // 重複分をさらに除去していく
                permutationResult /= Factorial(overlap[i], overlap[i]);
            }

            // 結果
            return permutationResult;
        }

        /// <summary>
        /// 階乗の計算
        /// </summary>
        /// <param name="num">計算対象の数字</param>
        /// <param name="calcCnt">階乗の計算を途中で打ち切るためのカウンタ</param>
        /// <returns></returns>
        private static long Factorial(int num, int calcCnt)
        {
            if (num == 0)
            {
                // 0! = 1
                return 1L;
            }
            else
            {
                if (calcCnt <= 0)
                {
                    // 階乗の計算打ち切り
                    return 1L;
                }
                else
                {
                    // n * (n-1)
                    return num * Factorial(num - 1, calcCnt - 1);
                }
            }
        }
    }
}
