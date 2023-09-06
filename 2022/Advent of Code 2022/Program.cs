using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2022
{
    internal class Program
    {
        public static string filepath = @"C:\Users\fairc\Documents\LCO.1_Com CAH 22-23\fairclough_nathan\Advent of Code 2022\Advent of Code 2022\PuzzleInputs\";

        public static int LimitX(int n, int upper, int lower)
        {
            if (n > upper)
                return upper;
            if (n < lower)
                return lower;
            return n;
        }
        public static int Day15P1(List<(int, int)> position, List<int> distanceq, int row)
        {
            int displY, displX;
            HashSet<int> non = new HashSet<int>();
            for (int i = 0; i < position.Count; i++)
            {
                if ((displY = Math.Abs(position[i].Item2 - row)) < distanceq[i])
                {
                    displX = distanceq[i] - displY;
                    non.UnionWith(Funcs.NateRange(position[i].Item1 - displX, position[i].Item1 + displX));
                }
            }
            return non.Distinct().Count();
        }
        static HashSet<(int, int)> DiamondHash((int, int) center, int rad)
        {
            HashSet<(int, int)> hash = new HashSet<(int, int)>();
            for (int r = 0; r < rad; r++)
            {
                hash.Add((center.Item1 + r, center.Item2 + rad - r)); // NE
                hash.Add((center.Item1 - r, center.Item2 - rad + r)); // SW
                hash.Add((center.Item1 + rad - r, center.Item2 - r)); // SE
                hash.Add((center.Item1 - rad + r, center.Item2 + r)); // NW
            }
            return hash;
        }
        static void Main(string[] args)
        {
            List<(int, int)> sensors = new List<(int, int)>();
            List<(int, int)> beacons = new List<(int, int)>();
            List<int> distanceq = new List<int>();
            using (var reader = new StreamReader(filepath + "Day15.txt"))
            {
                string line;
                string[] parts, sense, beak;
                while ((line = reader.ReadLine()) != null)
                {
                    parts = line.Split(':');
                    sense = parts[0].Split(',');
                    sensors.Add((int.Parse(sense[0].Substring(12)), int.Parse(sense[1].Substring(3))));
                    beak = parts[1].Split(',');
                    beacons.Add((int.Parse(beak[0].Substring(24)), int.Parse(beak[1].Substring(3))));
                    distanceq.Add(Math.Abs(sensors.Last().Item1 - int.Parse(beak[0].Substring(24))) + Math.Abs(sensors.Last().Item2 - int.Parse(beak[1].Substring(3))));
                }
            }

            List<(int, int)> scaled = new List<(int, int)>();
            List<int> distc = new List<int>();
            HashSet<(int, int)> mines = new HashSet<(int, int)>();

            int scalefact = 40000;
            for (int i = 0; i < sensors.Count; i++)
            {
                (int, int) p = sensors[i], m = beacons[i];
                int d = distanceq[i];
                scaled.Add((p.Item1 / scalefact, p.Item2 / scalefact));
                distc.Add(d / scalefact);
                mines.Add((m.Item1 / scalefact, m.Item2 / scalefact));
            }

            HashSet<(int, int)> diamonds = new HashSet<(int, int)>();
            for (int i = 0; i < sensors.Count; i++)
            {
                diamonds.UnionWith(DiamondHash(scaled[i], distc[i]));
            }

            for (int y = 0; y <= 100; y++)
            {
                for (int x = 0; x <= 100; x++)
                {
                    if (scaled.Contains((x, y)))
                        Console.Write('S');
                    else if (mines.Contains((x, y)))
                        Console.Write('B');
                    else if (diamonds.Contains((x, y)))
                        Console.Write('#');
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }
    }
}
