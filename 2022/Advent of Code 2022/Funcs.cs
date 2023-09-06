using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2022
{
    internal class Funcs
    {
        public static string filepath = @"C:\Users\fairc\Documents\LCO.1_Com CAH 22-23\fairclough_nathan\Advent of Code 2022\Advent of Code 2022\PuzzleInputs\";
        public static List<List<int>> DeSpreadsheet(string filename)
        {
            return File.ReadAllLines(filename).Select(line => line.Split('\t').Select(n => int.Parse(n)).ToList()).ToList();
        }
        public static void RotateStacks()
        {
            string[] input = File.ReadAllLines(filepath + "Day5 Stacks.txt");
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                    Console.Write(input[j][i]);
                Console.WriteLine();
            }
        }
        public static (int, int) MoveHead(char direct, (int, int) coords)
        {
            switch (direct)
            {
                case ('U'):
                    return (coords.Item1, coords.Item2 + 1);
                case ('D'):
                    return (coords.Item1, coords.Item2 - 1);
                case ('L'):
                    return (coords.Item1 - 1, coords.Item2);
                case ('R'):
                    return (coords.Item1 + 1, coords.Item2);
            }
            throw new NotImplementedException();
        }
        public static bool Close((int, int) head, (int, int) tail)
        {
            int xdif = tail.Item1 - head.Item1;
            int ydif = tail.Item2 - head.Item2;
            return (xdif * xdif) + (ydif * ydif) <= 2;
        }
        public static List<(int, int)> BuildSnake(int size)
        {
            List<(int, int)> segments = new List<(int, int)>();
            for (int i = 0; i < size; i++)
                segments.Add((0, 0));
            return segments;
        }
        public static char DrawPixel(int CRT, int X)
        {
            int middle = (CRT - 1) % 40;
            if (middle == X || middle == X - 1 || middle == X + 1)
                return '#';
            return '.';
        }
        public static int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19 };
        public static int[] GetQuotients(int n)
        {
            int[] quotients = new int[primes.Length];
            for (int i = 0; i < primes.Length; i++)
                quotients[i] = n % primes[i];
            return quotients;
        }
        public static int[] QuotientFix(int[] qs)
        {
            for (int i = 0; i < qs.Length; i++)
                qs[i] = qs[i] % primes[i];
            return qs;
        }
        public static int[] QuotientMath(int[] qs, string opp)
        {
            if (opp == "* old\r")
            {
                for (int i = 0; i < qs.Length; i++)
                    qs[i] = qs[i] * qs[i];
                return QuotientFix(qs);
            }
            int n = int.Parse(opp.Substring(2));
            if (opp[0] == '*')
            {
                for (int i = 0; i < qs.Length; i++)
                    qs[i] = qs[i] * n;
            }
            else if (opp[0] == '+')
            {
                for (int i = 0; i < qs.Length; i++)
                    qs[i] = qs[i] + n;
            }
            return QuotientFix(qs);
        }
        public static List<List<char>> map = File.ReadAllLines(filepath + "Day12.txt").Select(str => str.ToList()).ToList();
        public static void NextEpoch(Queue<(int, int)> epoch, List<(int, int)> visited)
        {
            int nodes = epoch.Count();
            (int, int) observe;
            while (nodes > 0)
            {
                observe = epoch.Dequeue();
                if (map[observe.Item1][observe.Item2] == 'E')
                {
                    epoch.Clear();
                    return;
                }
                if (TestNear(observe, 1, 0, visited))
                    epoch.Enqueue((observe.Item1 + 1, observe.Item2));
                if (TestNear(observe, -1, 0, visited))
                    epoch.Enqueue((observe.Item1 - 1, observe.Item2));
                if (TestNear(observe, 0, 1, visited))
                    epoch.Enqueue((observe.Item1, observe.Item2 + 1));
                if (TestNear(observe, 0, -1, visited))
                    epoch.Enqueue((observe.Item1, observe.Item2 - 1));
                nodes--;
            }
        }
        public static bool TestNear((int, int) me, int xdif, int ydif, List<(int, int)> visited)
        {
            if (visited.Contains((me.Item1 + xdif, me.Item2 + ydif)))
                return false;
            if (me.Item1 + xdif > map.Count - 1 || me.Item1 + xdif < 0)
                return false;
            if (me.Item2 + ydif > map[0].Count - 1 || me.Item2 + ydif < 0)
                return false;
            if (map[me.Item1 + xdif][me.Item2 + ydif] > map[me.Item1][me.Item2] + 1)
                return false;
            visited.Add((me.Item1 + xdif, me.Item2 + ydif));
            return true;
        }

        public static IEnumerable<int> NateRange(int start, int end)
        {
            if (start < end)
                return Enumerable.Range(start, end - start + 1);
            if (start > end)
                return Enumerable.Range(end, start - end + 1);
            else
                return Enumerable.Range(start, 1);
        }
    }
}
