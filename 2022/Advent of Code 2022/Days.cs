using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2022
{
    internal class Archive
    {
        public static string filepath = @"C:\Users\fairc\Documents\LCO.1_Com CAH 22-23\fairclough_nathan\Advent of Code 2022\Advent of Code 2022\PuzzleInputs\";
        public static void Day1()
        {
            var elves = File.ReadAllText(filepath + "Day1.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None).Select(str => (str.Split('\n').Select(n => int.Parse(n)).Sum())).OrderByDescending(x => x);
            Console.WriteLine("Part 1: {0}\nPart 2: {1}", elves.First(), elves.Take(3).Sum());
        }
        public static void Day2P1()
        {
            int[][] score = new int[][]
            { //            A  B  C
                new int[] { 3, 0, 6}, // X
                new int[] { 6, 3, 0}, // Y
                new int[] { 0, 6, 3}  // Z
            };
            int[] vals = new int[] { 1, 2, 3 };

            var totals = File.ReadAllLines(filepath + "Day2.txt").Select(game => vals[game[2] - 88] + score[game[2] - 88][game[0] - 65]);
            Console.WriteLine(totals.Sum());
        }
        public static void Day2P2()
        {
            int[][] score2 = new int[][]
            { //            A  B  C 
                new int[] { 3, 1, 2}, // X - Lose
                new int[] { 1, 2, 3}, // Y - Draw
                new int[] { 2, 3, 1}  // Z - Win
            };
            int[] win = { 0, 3, 6 };

            var totals = File.ReadAllLines(filepath + "Day2.txt").Select(game => win[game[2] - 88] + score2[game[2] - 88][game[0] - 65]);
            Console.WriteLine(totals.Sum());
        }
        public static void Day3P1()
        {
            int priority = 0;

            foreach (string elf in File.ReadAllLines(filepath + "Day3.txt"))
            {
                char repeated = elf.Substring(0, elf.Length / 2).Intersect(elf.Substring(elf.Length / 2)).First();
                if (repeated > 90)
                    priority += repeated - 96;
                else
                    priority += repeated - 38;
            }
            Console.WriteLine(priority);
        }
        public static void Day3P2()
        {
            int priority = 0;

            using (var reader = new StreamReader(filepath + "Day3.txt"))
            {
                for (int i = 0; i < 100; i++)
                {
                    char badge = reader.ReadLine().Intersect(reader.ReadLine()).Intersect(reader.ReadLine()).First();
                    if (badge > 90)
                        priority += badge - 96;
                    else
                        priority += badge - 38;
                }
            }
            Console.WriteLine(priority);
        }
        public static void Day4()
        {
            int contains = 0, overlaps = 0;

            using (var reader = new StreamReader(filepath + "Day4.txt"))
            {
                string line;
                char[] delimiters = new char[] { ',', '-' };
                int[] nums;

                while ((line = reader.ReadLine()) != null)
                {
                    nums = line.Split(delimiters).Select(x => int.Parse(x)).ToArray();
                    if ((nums[0] >= nums[2] && nums[1] <= nums[3]) || (nums[2] >= nums[0] && nums[3] <= nums[1]))
                        contains++;
                    if ((nums[1] >= nums[2] && nums[0] <= nums[2]) || (nums[3] >= nums[0] && nums[2] <= nums[0]))
                        overlaps++;
                }
            }
            Console.WriteLine("Part 1: {0}\nPart 2: {1}", contains, overlaps);
        }
        public static void Day5P1()
        {
            var stacks = new List<Stack<char>>();
            using (var reader = new StreamReader(filepath + "Day5 Stacks.txt"))
            {
                char[] now;
                for (int i = 0; i < 9; i++)
                {
                    now = reader.ReadLine().ToCharArray();
                    Array.Reverse(now);
                    stacks.Add(new Stack<char>(now));
                }
            }
            using (var reader = new StreamReader(filepath + "Day5.txt"))
            {
                string instructions;
                while ((instructions = reader.ReadLine()) != null)
                {
                    int[] parts = instructions.Split().Where(x => !Char.IsLetter(x[0])).Select(n => int.Parse(n)).ToArray();
                    int moving = parts[0], start = parts[1] - 1, end = parts[2] - 1;

                    while (moving > 0)
                    {
                        stacks[end].Push(stacks[start].Pop());
                        moving--;
                    }
                }
            }
            foreach (var stack in stacks)
                Console.Write(stack.Pop());
        }
        public static void Day5P2()
        {
            var stacks = new List<Stack<char>>();
            using (var reader = new StreamReader(filepath + "Day5 Stacks.txt"))
            {
                char[] now;
                for (int i = 0; i < 10; i++)
                {
                    now = reader.ReadLine().ToCharArray();
                    Array.Reverse(now);
                    stacks.Add(new Stack<char>(now));
                }
            }
            using (var reader = new StreamReader(filepath + "Day5.txt"))
            {
                string instructions;
                while ((instructions = reader.ReadLine()) != null)
                {
                    int[] parts = instructions.Split().Where(x => !Char.IsLetter(x[0])).Select(n => int.Parse(n)).ToArray();
                    int moving = parts[0], start = parts[1] - 1, end = parts[2] - 1;

                    while (moving > 0)
                    {
                        stacks[9].Push(stacks[start].Pop());
                        moving--;
                    }
                    while (moving < parts[0])
                    {
                        stacks[end].Push(stacks[9].Pop());
                        moving++;
                    }
                }
            }
            foreach (var stack in stacks.Take(9))
                Console.Write(stack.Pop());
        }
        public static void Day6()
        {
            string input = File.ReadAllText(filepath + "Day6.txt");
            bool unique = false;
            int count = 4;
            string splice;
            while (!unique)
            {
                splice = input.Substring(count - 4, 4);
                if (splice.Distinct().Count() == splice.Length)
                    unique = true;
                count++;
            }
            Console.WriteLine(count - 1); // part 1
            count = 14;
            while (unique)
            {
                splice = input.Substring(count - 14, 14);
                if (splice.Distinct().Count() == splice.Length)
                    unique = false;
                count++;
            }
            Console.WriteLine(count - 1); // part 2
        }
        public static void Day7P1()
        {
            long sum = 0;
            using (var reader = new StreamReader(filepath + "Day7.txt"))
            {
                string instruction, seeker;
                long filesize;
                int line = 0, depth;
                while ((instruction = reader.ReadLine()) != null)
                {
                    line++;
                    if (instruction.Substring(0, 4) == "$ cd" && instruction != "$ cd ..")
                    {
                        filesize = 0;
                        depth = 0;
                        while (depth > -1 && (seeker = reader.ReadLine()) != null)
                        {
                            if (seeker == "$ cd ..")
                                depth--;
                            else if (seeker.Substring(0, 4) == "$ cd")
                                depth++;
                            else if (char.IsNumber(seeker[0]))
                            {
                                filesize += long.Parse(seeker.Split()[0]);
                                if (filesize > 100000)
                                {
                                    filesize = 0;
                                    break;
                                }
                            }
                        }
                        sum += filesize;
                        reader.DiscardBufferedData();
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        for (int i = 0; i < line; i++)
                            reader.ReadLine();
                    }
                }
            }
            Console.WriteLine(sum);
        }
        public static void Day7P2()
        {
            List<long> filesizes = new List<long>();
            using (var reader = new StreamReader(filepath + "Day7.txt"))
            {
                string instruction, seeker;
                long filesize;
                int line = 0, depth;
                while ((instruction = reader.ReadLine()) != null)
                {
                    line++;
                    if (instruction.Substring(0, 4) == "$ cd" && instruction != "$ cd ..")
                    {
                        filesize = 0;
                        depth = 0;
                        while (depth > -1 && (seeker = reader.ReadLine()) != null)
                        {
                            if (seeker == "$ cd ..")
                                depth--;
                            else if (seeker.Substring(0, 4) == "$ cd")
                                depth++;
                            else if (char.IsNumber(seeker[0]))
                            {
                                filesize += long.Parse(seeker.Split()[0]);
                            }
                        }
                        filesizes.Add(filesize);
                        reader.DiscardBufferedData();
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        for (int i = 0; i < line; i++)
                            reader.ReadLine();
                    }
                }
            }
            long free = filesizes.First() - 40000000, best = long.MaxValue;
            foreach (long n in filesizes)
                if (n > free && n < best)
                    best = n;
            Console.WriteLine(best);
        }
        public static void Day8P1()
        {
            List<List<char>> trees = File.ReadAllLines(filepath + "Day8.txt").Select(line => line.Select(c => c).ToList()).ToList();
            int visible = 2 * trees.Count + 2 * (trees[0].Count - 2);
            for (int row = 1; row < trees.Count - 1; row++)
                for (int col = 1; col < trees[row].Count - 1; col++)
                    if ((trees[row].Take(col).Where(c => c >= trees[row][col]).Count() < 1) || (trees[row].Skip(col + 1).Where(c => c >= trees[row][col]).Count() < 1) || (trees.Take(row).Where(c => c[col] >= trees[row][col]).Count() < 1) || (trees.Skip(row + 1).Where(c => c[col] >= trees[row][col]).Count() < 1))
                        visible++;
            Console.WriteLine(visible);
        }
        public static void Day8P2()
        {
            List<List<char>> trees = File.ReadAllLines(filepath + "Day8.txt").Select(line => line.Select(c => c).ToList()).ToList();
            int best = 0;
            int[] ways = new int[4];
            for (int row = 1; row < trees.Count - 1; row++)
            {
                for (int col = 1; col < trees[row].Count - 1; col++)
                {
                    ways = new int[] { 1, 1, 1, 1 };
                    while (col - ways[0] > 0 && trees[row][col - ways[0]] < trees[row][col])
                        ways[0]++;
                    while (col + ways[1] < trees[0].Count - 1 && trees[row][col + ways[1]] < trees[row][col])
                        ways[1]++;
                    while (row - ways[2] > 0 && trees[row - ways[2]][col] < trees[row][col])
                        ways[2]++;
                    while (row + ways[3] < trees.Count - 1 && trees[row + ways[3]][col] < trees[row][col])
                        ways[3]++;

                    int view = ways[0] * ways[1] * ways[2] * ways[3];
                    if (view > best)
                        best = view;
                }
            }
            Console.WriteLine(best);
        }
        public static void Day9()
        {
            List<(int, int)> visited = new List<(int, int)>();
            List<(int, int)> tailpath = new List<(int, int)>();
            using (var reader = new StreamReader(filepath + "Day9.txt"))
            {
                string instruction;
                char direction;
                int steps;
                List<(int x, int y)> snakecoords = Funcs.BuildSnake(10);

                while ((instruction = reader.ReadLine()) != null)
                {
                    direction = instruction[0];
                    steps = int.Parse(instruction.Substring(2));
                    while (steps > 0)
                    {
                        snakecoords[0] = Funcs.MoveHead(direction, snakecoords[0]);
                        for (int i = 1; i < 10; i++)
                        {
                            if (!Funcs.Close(snakecoords[i - 1], snakecoords[i]))
                            {
                                if (snakecoords[i - 1].x > snakecoords[i].x)
                                    snakecoords[i] = (snakecoords[i].x + 1, snakecoords[i].y);
                                else if (snakecoords[i - 1].x < snakecoords[i].x)
                                    snakecoords[i] = (snakecoords[i].x - 1, snakecoords[i].y);
                                if (snakecoords[i - 1].y > snakecoords[i].y)
                                    snakecoords[i] = (snakecoords[i].x, snakecoords[i].y + 1);
                                else if (snakecoords[i - 1].y < snakecoords[i].y)
                                    snakecoords[i] = (snakecoords[i].x, snakecoords[i].y - 1);
                            }
                        }
                        visited.Add(snakecoords[1]);
                        tailpath.Add(snakecoords[9]);
                        steps--;
                    }
                }
            }
            Console.WriteLine(visited.Distinct().Count()); // part 1
            Console.WriteLine(tailpath.Distinct().Count()); // part 2
        }
        public static void Day10()
        {
            int sum = 0;
            using (var reader = new StreamReader(filepath + "Day10.txt"))
            {
                string instruction;
                int cycle = 0;
                int X = 1;
                int[] checkpoints = new int[] { 20, 60, 100, 140, 180, 220 };
                while ((instruction = reader.ReadLine()) != null)
                {
                    cycle++;
                    Console.Write(Funcs.DrawPixel(cycle, X));
                    if (cycle % 40 == 0) Console.WriteLine();
                    if (checkpoints.Contains(cycle))
                        sum += X * cycle;
                    if (instruction.Length > 4)
                    {
                        cycle++;
                        Console.Write(Funcs.DrawPixel(cycle, X));
                        if (cycle % 40 == 0) Console.WriteLine();
                        if (checkpoints.Contains(cycle))
                            sum += X * cycle;
                        X += int.Parse(instruction.Substring(4));
                    }

                }
            }
            Console.WriteLine(sum); // part 1
        }
        public static void Day10P1()
        {
            List<Queue<int>> monkeys = new List<Queue<int>>();
            List<(int left, int right)> friends = new List<(int left, int right)>();
            List<(int oppnum, char opp, bool self)> operation = new List<(int oppnum, char opp, bool self)>();
            List<int> divisors = new List<int>();
            List<int> inspections = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };

            var init = File.ReadAllText(filepath + "Day11.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None).Select(l => l.Split('\n')).ToList();

            int a = 0; char b; bool c;
            for (int i = 0; i < 8; i++)
            {
                monkeys.Add(new Queue<int>());
                foreach (int item in init[i][1].Substring(17).Split(',').Select(n => int.Parse(n)))
                    monkeys[i].Enqueue(item);
                friends.Add((int.Parse(init[i][4].Substring(29)), int.Parse(init[i][5].Substring(30))));
                if (!(c = !Int32.TryParse(init[i][2].Substring(25), out int num)))
                    a = num;
                b = init[i][2][23];
                operation.Add((a, b, c));
                divisors.Add(int.Parse(init[i][3].Substring(21)));
            }

            int X;
            for (int round = 0; round < 20; round++)
            {
                for (int turn = 0; turn < 8; turn++)
                {
                    while (monkeys[turn].Count > 0)
                    {
                        X = monkeys[turn].Dequeue();
                        inspections[turn]++;
                        if (operation[turn].self)
                            X = X * X / 3;
                        else if (operation[turn].opp == '*')
                            X = X * operation[turn].oppnum / 3;
                        else if (operation[turn].opp == '+')
                            X = (X + operation[turn].oppnum) / 3;
                        monkeys[X % divisors[turn] == 0 ? friends[turn].left : friends[turn].right].Enqueue(X);
                    }
                }
            }
            var sorted = inspections.OrderByDescending(x => x).ToList();
            Console.WriteLine(sorted[0] * sorted[1]);
        }
        public static void Day11P2()
        {
            List<Queue<int[]>> monkeys = new List<Queue<int[]>>();
            List<(int left, int right)> friends = new List<(int left, int right)>();
            List<int> divisors = new List<int>();
            List<long> inspections = new List<long>() { 0, 0, 0, 0, 0, 0, 0, 0 };

            var init = File.ReadAllText(filepath + "Day11.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None).Select(l => l.Split('\n')).ToList();

            for (int i = 0; i < 8; i++)
            {
                monkeys.Add(new Queue<int[]>());
                foreach (int item in init[i][1].Substring(17).Split(',').Select(n => int.Parse(n)))
                    monkeys[i].Enqueue(Funcs.GetQuotients(item));
                friends.Add((int.Parse(init[i][4].Substring(29)), int.Parse(init[i][5].Substring(30))));
                divisors.Add(int.Parse(init[i][3].Substring(21)));
            }
            int[] X;
            for (int round = 0; round < 10000; round++)
            {
                for (int turn = 0; turn < 8; turn++)
                {
                    while (monkeys[turn].Count > 0)
                    {
                        X = Funcs.QuotientMath(monkeys[turn].Dequeue(), init[turn][2].Substring(23));
                        monkeys[X[Array.IndexOf(Funcs.primes, divisors[turn])] == 0 ? friends[turn].left : friends[turn].right].Enqueue(X);
                        inspections[turn]++;
                    }
                }
            }
            inspections = inspections.OrderByDescending(x => x).ToList();
            Console.WriteLine(inspections[0] * inspections[1]);
        }

        public static void Day12()
        {
            List<(int, int)> visited = new List<(int, int)>();
            Queue<(int, int)> visiting = new Queue<(int, int)>();

            visiting.Enqueue((20, 0));
            int steps = 0;
            while (visiting.Count > 0)
            {
                Funcs.NextEpoch(visiting, visited);
                steps++;
            }
            Console.WriteLine(steps - 1); // part 1

            visited.Clear();
            visiting.Clear();

            visiting.Enqueue((27, 0));
            steps = 0;
            while (visiting.Count > 0)
            {
                Funcs.NextEpoch(visiting, visited);
                steps++;
            }
            Console.WriteLine(steps - 1); // part 2
        }

        static void DrawCave(HashSet<(int, int)> hashrock, HashSet<(int, int)> sand)
        {
            for (int i = 485; i <= 499; i++)
                Console.Write(' ');
            Console.WriteLine('+');

            for (int y = 0; y <= 180; y++) // set y back to 157
            {
                for (int x = 485; x <= 600; x++)
                {
                    if (hashrock.Contains((x, y)))
                        Console.Write('#');
                    else if (sand.Contains((x, y)))
                        Console.Write('o');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }
        static bool DropSand(HashSet<(int, int)> hashrock, HashSet<(int, int)> sand)
        {
            (int, int) position = (500, 0);
            bool falling = true;
            while (falling)
            {

                if (!hashrock.Contains((position.Item1, position.Item2 + 1)) && !sand.Contains((position.Item1, position.Item2 + 1)))
                    position = (position.Item1, position.Item2 + 1);
                else if (!hashrock.Contains((position.Item1 - 1, position.Item2 + 1)) && !sand.Contains((position.Item1 - 1, position.Item2 + 1)))
                    position = (position.Item1 - 1, position.Item2 + 1);
                else if (!hashrock.Contains((position.Item1 + 1, position.Item2 + 1)) && !sand.Contains((position.Item1 + 1, position.Item2 + 1)))
                    position = (position.Item1 + 1, position.Item2 + 1);
                else
                    falling = false;
            }
            if (position.Item2 < 1)
                return false;
            sand.Add(position);
            return true;
        }
        public static void Day14()
        {
            List<(int, int)> rocks = new List<(int, int)>();
            using (var stream = new StreamReader(filepath + "Day14.txt"))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var rockmap = line.Split(new string[] { " -> " }, StringSplitOptions.None).Select(str => str.Split(',').Select(n => int.Parse(n)).ToArray()).ToArray();
                    for (int i = 0; i < rockmap.Count() - 1; i++)
                    {
                        foreach (int y in Funcs.NateRange(rockmap[i][1], rockmap[i + 1][1]))
                        {
                            foreach (int x in Funcs.NateRange(rockmap[i][0], rockmap[i + 1][0]))
                            {
                                rocks.Add((x, y));
                            }
                        }
                    }

                }
            }
            for (int x = -1000; x <= 2000; x++)
                rocks.Add((x, 173));
            HashSet<(int, int)> hashrock = new HashSet<(int, int)>(rocks);
            HashSet<(int, int)> sand = new HashSet<(int, int)>();


            DrawCave(hashrock, sand);
            while (DropSand(hashrock, sand)) ;
            DrawCave(hashrock, sand);
            Console.WriteLine(sand.Count());
        }
    }
}
