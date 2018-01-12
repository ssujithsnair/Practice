using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(JourneyToTheMoon());
            //DiagonalSum();
            //MinMax();
            //MaxProfit();
            //Flip();
            //AlternateChar();
            //AppendCharWithCount();
            //FindRepeated();
            //IceCreamTest();
            Misc();
        }
        public static void Swap(ref int x, ref int y)
        {
            if (x == y) // MUST since x/y points to address
                return;
            x = x + y;
            y = x - y;
            x = x - y;
        }
        static bool GetTwoIntsFromLine(out int a, out int b)
        {
            a = b = -1;
            var firstLine = Console.ReadLine();
            var args = firstLine.Split(' ');
            if (args.Length != 2)
                return false;
            if (!int.TryParse(args[0], out a))
                return false;
            if (!int.TryParse(args[1], out b))
                return false;
            return true;
        }
        static string JourneyToTheMoon()
        {
            //https://www.hackerrank.com/challenges/journey-to-the-moon/problem
            int N, I;
            if (!GetTwoIntsFromLine(out N, out I))
                return "Invalid Input";
            List<List<int>> pairs = new List<List<int>>(N);
            for (int i = 0; i < N; i++)
            {
                pairs.Add(new List<int>());
            }
            for (int i = 0; i < I; i++)
            {
                int a, b;
                if (!GetTwoIntsFromLine(out a, out b))
                    return "Invalid Input";
                pairs[a].Add(b);
                pairs[b].Add(a);
            }
            List<int> countrySizes = new List<int>();
            bool[] countries = new bool[N];
            int totalMatched = 0;
            for (int i = 0; i < N; i++)
            {
                int countrySize = 0;
                Queue<int> q = new Queue<int>();
                q.Enqueue(i);
                while (q.Count > 0)
                {
                    var entry = q.Dequeue();
                    if (countries[entry])
                        continue;
                    countrySize++;
                    countries[entry] = true;
                    var list = pairs[entry];
                    if (list.Count == 0)
                        continue;
                    foreach (int person in list)
                    {
                        if (!countries[person])
                            q.Enqueue(person);
                    }
                }
                if (countrySize > 1)
                {
                    countrySizes.Add(countrySize);
                    totalMatched += countrySize;
                }
            }
            long remaining = N;
            long totalPairs = 0;
            foreach (var size in countrySizes)
            {
                remaining -= size;
                totalPairs += size * remaining;
            }
            totalPairs += (remaining * (remaining - 1)) / 2;
            return totalPairs.ToString();
        }

        static void DiagonalSum()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[][] a = new int[n][];
            for (int a_i = 0; a_i < n; a_i++)
            {
                string[] a_temp = Console.ReadLine().Split(' ');
                a[a_i] = Array.ConvertAll(a_temp, Int32.Parse);
            }
            int i = 0, j = 0;
            int firstSum = 0;
            while (i < n && j < n)
            {
                firstSum += a[i++][j++];
            }
            int secondSum = 0;
            i = n - 1; j = 0;
            while (i >= 0 && j >= 0)
                secondSum += a[i--][j++];
            Console.WriteLine(Math.Abs(firstSum - secondSum));
        }
        static void MinMax()
        {
            string[] arr_temp = Console.ReadLine().Split(' ');
            int[] arr = Array.ConvertAll(arr_temp, Int32.Parse);
            Array.Sort(arr);
            long minSum = 0, maxSum = 0;
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                minSum += arr[i];
            for (int i = n - 1; i > 0; i--)
                maxSum += arr[i];
            Console.Write(string.Format("{0} {1}", minSum, maxSum));
        }
        static void MaxProfit()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < t; a0++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                string[] arr_temp = Console.ReadLine().Split(' ');
                int[] arr = Array.ConvertAll(arr_temp, Int32.Parse);
                Console.WriteLine(getMaxProfit(arr));
            }
        }
        static long getMaxProfit(int[] prices)
        {
            long profit = 0L;
            long maxSoFar = 0;
            for (int i = prices.Length - 1; i > -1; i--)
            {
                if (prices[i] >= maxSoFar)
                {
                    maxSoFar = prices[i];
                }
                profit += maxSoFar - prices[i];
            }
            return profit;
        }
        static void Flip()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[t];
            for (int i = 0; i < t; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(~arr[i]);
        }
        static void AlternateChar()
        {
            int len = Convert.ToInt32(Console.ReadLine());
            string s = Console.ReadLine();
            if (len == 0)
            {
                Console.WriteLine(0);
                return;
            }
            Console.WriteLine(MaxValidCount(s));
        }
        static int MaxValidCount(string s)
        {
            int maxCount = 0;
            char[] dist = s.Distinct().ToArray();
            if (dist.Length <= 1)
                return 0;
            for (int i = 0; i < dist.Length - 1; i++)
            {
                for (int j = i + 1; j < dist.Length; j++)
                {
                    var newS = new string(s.Where(a => a == dist[i] || a == s[j]).ToArray());
                    if (isValid(newS))
                        maxCount = Math.Max(maxCount, newS.Length);
                }
            }
            return maxCount == 1 ? 0 : maxCount;
        }

        static bool isValid(string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
                if (s[i] == s[i + 1])
                    return false;
            return true;
        }
        static void AppendCharWithCount()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string s = Console.ReadLine();
            int k = Convert.ToInt32(Console.ReadLine());
            StringBuilder builder = new StringBuilder(n);
            foreach (char c in s)
            {
                if (Char.IsLetter(c))
                {
                    char a = Char.IsUpper(c) ? 'A' : 'a';
                    builder.Append((char)(a + ((c - a + k) % 26)));
                }
                else
                {
                    builder.Append(c);
                }
            }
            Console.WriteLine(builder.ToString());
        }
        static void FindRepeated()
        {
            string s = Console.ReadLine();
            string result = super_reduced_string(s);
        }
        static string super_reduced_string(string s)
        {
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    s = s.Substring(0, i - 1) + s.Substring(i + 1);
                    i = 0;
                }
            }
            return s.Length > 0 ? s : "Empty string";
        }
        static void IceCreamTest()
        {
            string str = "01010";
            str = str.Replace("010", "");
            string[] arr1 = new string[3] { "abcdffff", "bcdeff", "cdeffsfsfd" };
            int gemCount = 0;
            string s1 = arr1[0];
            foreach (char c in s1.Distinct())
            {
            }
            foreach (char c in s1)
            {
                bool gem = true;
                for (int i = 1; i < arr1.Length; i++)
                {
                    if (!arr1[i].Contains(c.ToString()))
                    {
                        gem = false;
                        break;
                    }
                }
                if (gem)
                    gemCount++;
            }
            return;
            var f = Enumerable.Range(0, int.Parse(Console.ReadLine()));
            var s = f.Select(_ => Console.ReadLine().AsEnumerable()).ToArray();
            var third = s.Aggregate(Enumerable.Intersect).ToArray();
            Console.WriteLine(third.Count());
            return;
            //double cost = Convert.ToDouble(Console.ReadLine());
            //int tip = Convert.ToInt32(Console.ReadLine());
            //int tax = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("The total meal cost is {0} dollars.", Math.Round(cost + cost * (double)(tip + tax) / 100, MidpointRounding.AwayFromZero));
            //return;
            int m = Convert.ToInt32(Console.ReadLine());
            int n = Convert.ToInt32(Console.ReadLine());
            string[] arr_temp = Console.ReadLine().Split(' ');
            int[] arr = Array.ConvertAll(arr_temp, Int32.Parse);
            //int i = 0;
            //bool done = false;
            //while (!done && i < n - 1)
            //{
            //    int j = 1;
            //    while (!done && j < n)
            //    {
            //        if ((arr[i] + arr[j]) == m)
            //        {
            //            done = true;
            //            Console.WriteLine("{0} {1}", (i + 1).ToString(), (j + 1).ToString());
            //        }
            //    }
            //}
            bool[] hash = new bool[10000];
            int first = 0, second = 0;
            foreach (int item in arr)
            {
                int diff = m - item;
                if (diff >= 0 && hash[diff])
                {
                    first = diff;
                    second = item;
                    break;
                }
                hash[item] = true;
            }
            int firstI = 0, secondI = 0;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] == first)
                    firstI = i;
                else if (arr[i] == second)
                    secondI = i;

            }
            Console.WriteLine("{0} {1}", firstI.ToString(), secondI.ToString());
        }
        public int[] TwoSum(int[] nums, int target, int start, int end)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = start; i <= end; i++)
            {
                int diff = target - nums[i];
                if (map.ContainsKey(diff))
                    return new int[] { map[diff], i };
                else if (!map.ContainsKey(nums[i]))
                    map.Add(nums[i], i);
            }
            return null;
        }
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                Dictionary<int, int> map = new Dictionary<int, int>();
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int sum = nums[i] + nums[j];
                    if (map.ContainsKey(-sum))
                    {
                        bool found = false;
                        foreach (var entry in result)
                        {
                            int count = 0;
                            foreach (var item in entry)
                            {
                                if (item == nums[i])
                                    count++;
                                else if (item == nums[j])
                                    count++;
                                else if (item == -sum)
                                    count++;
                            }
                            if (count >= 3)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                            result.Add(new List<int>() { nums[i], -sum, nums[j] });
                    }
                    if (!map.ContainsKey(nums[j]))
                        map.Add(nums[j], i);
                }
            }
            return result;
        }
        static List<string> letterCombinations(string digits)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(digits))
                return new List<string>();
            //List<string> v = new List<string>{"abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"};
            List<string> v = new List<string> { "ab", "cd", "ef" };
            result.Add("");   // add a seed for the initial case
            foreach (char c in digits)
            {
                int num = c - '0';
                if (num < 0 || num > 9) break;
                string candidate = v[num];
                if (string.IsNullOrEmpty(candidate))
                    continue;
                List<string> tmp = new List<string>();
                foreach (char d in candidate)
                {
                    foreach (string val in result)
                    {
                        tmp.Add(val + d);
                    }
                }
                //result.CopyTo(tmp);
                result.AddRange(tmp);
            }
            return result;
        }

        static string[] MapArrayInput(Dictionary<string, string[]> mappings, string input)
        {
            if (input.Length == 1)
            {
                return mappings[input[0].ToString()];
            }
            string[] result = new string[] { };
            string[] val = mappings[input[0].ToString()];
            string[] remaining = MapArrayInput(mappings, input.Substring(1));
            result = MapArrays(val, remaining);
            return result;
        }
        static string[] MapArrays(string[] array1, string[] array2)
        {
            int m = array1.Length;
            int n = array2.Length;
            string[] concat = new string[m * n];
            int current = 0;
            
            foreach (string s1 in array1)
            {
                foreach (string s2 in array2)
                {
                    concat[current++] = s1 + s2;
                }
            }
            return concat;

        }
        public static int Search(int[] nums, int target)
        {
            return nums.Length > 0 ? Search(nums, target, 0, nums.Length - 1) : -1;
        }

        private static int Search(int[] nums, int target, int left, int right)
        {
            int mid = (left + right) / 2;
            if (nums[mid] == target)
                return mid;
            if (right < left)
                return -1;
            if (nums[left] < nums[mid])
            {
                if (nums[left] <= target && nums[mid] > target)
                    return Search(nums, target, left, mid - 1);
                else
                    return Search(nums, target, mid + 1, right);
            }
            else if (nums[left] > nums[mid])
            {
                if (target > nums[mid] && target <= nums[right])
                    return Search(nums, target, mid + 1, right);
                else
                    return Search(nums, target, left, mid - 1);
            }
            else if (nums[left] == nums[mid])
            {
                if (nums[right] != nums[mid])
                    return Search(nums, target, mid + 1, right);
                int result = Search(nums, target, left, mid - 1);
                if (result == -1)
                    return Search(nums, target, mid + 1, right);
            }
            return -1;
        }
        public static List<string> GenerateParanthesis(int n)
        {
            List<string> p = new List<string>();
            GenerateParanthesis(p, new char[n * 2], 0, n, 0, 0);
            return p;
        }
        private static void GenerateParanthesis(List<string> result, char[] current, int start, int total, int open, int close)
        {
            if (start == total * 2)
            {
                if (open == close)
                {
                    result.Add(new string(current));
                }
                return;
            }
            if (open < total)
            {
                current[start] = '(';
                GenerateParanthesis(result, current, start + 1, total, open + 1, close);
            }
            if (close < open)
            {
                current[start] = ')';
                GenerateParanthesis(result, current, start + 1, total, open, close + 1);
            }
        }

        public static bool IsValidExpression(string s)
        {
            Stack<char> data = new Stack<char>();
            if (string.IsNullOrEmpty(s))
                return false;
            data.Push(s[0]);
            for (int i = 1; i < s.Length; i++)
            {
                if (IsOpen(s[i]))
                {
                    data.Push(s[i]);
                    continue;
                }
                if (data.Count == 0)
                    return false;
                char c = data.Peek();
                if (IsOpposite(c, s[i]))
                {
                    data.Pop();
                }
                else
                return false;
            }
            return data.Count == 0;
        }
        const string pattern = "({[";
        private static bool IsOpen(char c)
        {
            return pattern.Contains(c);
        }

        private static bool IsOpposite(char open, char close)
        {
            switch (open)
            {
                case '(':
                    return close == ')';
                case '{':
                    return close == '}';
                case '[':
                    return close == ']';
            }
            return false;
        }

        public static int longestValidParentheses(String s)
        {
            int maxans = 0;
            Stack<int> stack = new Stack<int>();
            stack.Push(-1);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(i);
                }
                else
                {
                    stack.Pop();
                    if (stack.Count == 0)
                    {
                        stack.Push(i);
                    }
                    else
                    {
                        maxans = Math.Max(maxans, i - stack.Peek());
                    }
                }
            }
            return maxans;
        }

        private static int GetMaxProfit(int[] prices)
        {
            if (prices.Length < 1)
                return int.MinValue;
            int min = prices[0];
            int maxprofit = prices[1] - prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                maxprofit = Math.Max(maxprofit, prices[i] - min);
                min = Math.Min(min, prices[i]);
            }
            return maxprofit;
        }
        private static int[] NextGreaterElement()
        {
            /*
             * Question: There is an array A[N] of N numbers. You have to compose an array Output[N] such that each element in
             * Output[i] will tell the next greater element to the right in the original array. If there is no greater number to
             * the right, then the output array should contain -1 in that position.
                Array 1: {4, 6, 5, 4, 3, 4, 9, 8, 1}
                Output: {6, 9, 9, 9, 4, 9, -1, -1, -1}
             */
            int[] arr = new int[9] { 4, 6, 5, 4, 3, 4, 9, 8, 1 };
            if (arr == null)
                throw new ArgumentNullException();
            if (arr.Length == 0)
                return new int[0];
            if (arr.Length == 1)
                return new int[1] { -1 };
            int[] output = new int[arr.Length];
            //int index = 0;
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            for (int i = 1; i < arr.Length; i++)
            {
                while (stack.Count > 0 && arr[stack.Peek()] < arr[i])
                {
                    int index = stack.Pop();
                    output[index] = arr[i];
                }
                stack.Push(i);
            }
            while(stack.Count > 0)
                output[stack.Pop()] = -1;

            return output;
        }

        private static int MaxSpan()
        {
            // Consider the leftmost and righmost appearances of some value in an array. 
            // We'll say that the "span" is the number of elements between the two inclusive.
            int result = 0;
            //int[] arr = new int[] { 1, 2, 1, 1, 3 };
            //int[] arr = new int[] { 1, 4, 2, 1, 4, 1, 4 };
            int[] arr = new int[] { 1, 4, 2, 1, 4, 4, 4 };
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (!map.ContainsKey(arr[i]))
                    map.Add(arr[i], i);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                result = Math.Max(map[arr[i]] - i, result);
            }
            return result+1;
        }

        private static void Fix34()
        {
            /*
             * Return an array that contains exactly the same numbers as the given array, but rearranged so that every 4
             * is immediately followed by a 5. Do not move the 4's, but every other number may move. The array contains the 
             * same number of 4's and 5's, and every 4 has a number after it that is not a 4. In this version, 5's may 
             * appear anywhere in the original array.
             */
            //int[] arr = new int[] {1, 3, 1, 4};
            //int[] arr = new int[] { 1, 3, 1, 4, 4, 3, 1 };
            //int[] arr = new int[] { 3, 2, 2, 4 };
            //int[] arr = new int[] { 3, 2, 3, 1, 4, 4 };
            int[] arr = new int[] { 5, 4, 9, 4, 9, 5 };
            Queue<int> fours = new Queue<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 5)
                    fours.Enqueue(i);
            }
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] == 4 && j < arr.Length-1 && fours.Count > 0)
                {
                    Swap(ref arr[j + 1], ref arr[fours.Dequeue()]);
                }
            }
        }
        private static bool IsBalancedArray()
        {
            //Given a non-empty array, return true if there is a place to split the array so that the sum of the numbers 
            // on one side is equal to the sum of the numbers on the other side.

            //int[] arr = new int[] { 1, 1, 1, 2, 1 };
            //int[] arr = new int[] { 2, 1, 1, 2, 1 };
            int[] arr = new int[] { 10,1,1,1,1,1,1,1,1,1,1 };
            int left = 0, right = 0;
            bool? l = null;
            int i = 0, j = arr.Length - 1;
            int total = 0;
            while (i < j)
            {
                if (l != false)
                {
                    left += arr[i];
                    total++;
                }
                if (l != true)
                {
                    right += arr[j];
                    total++;
                }
                if (left < right)
                {
                    i++;
                    l = true;
                }
                else if (right < left)
                {
                    j--;
                    l = false;
                }
                else
                {
                    i++; j--;
                    l = null;
                }
            }
            return left == right && total == arr.Length;
        }

        private static int CountClumps()
        {
            int count = 0;
            //int[] arr = new int[] { 1, 2, 2, 3, 4, 4 };
            //int[] arr = new int[] { 1, 1, 2, 1, 1 };
            int[] arr = new int[] { 1, 1, 1, 1, 1 };
            int n = 0;
            int current = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == current)
                {
                    n++;
                }
                else
                {
                    if (n >= 2)
                        count++;
                    n = 1;
                    current = arr[i];
                }
            }
            if (n >= 2)
                count++;
            return count;
        }
        private static bool MakeBricks()
        {
            int small = 3, big = 2, goal = 10;
            while (big > 0 && goal > 0)
            {
                if (goal >= 5)
                    goal -= 5;
                else
                    break;
                big--;
            }
            while (small > 0 && goal > 0)
            {
                goal -= 1;
                small--;
            }
            return goal == 0;
        }
        private static bool IsWordSquare(string[] sarray)
        {
            for (int i = 0; i < sarray.Length; i++)
            {
                for (int j = 0; j < sarray.Length; j++)
                {
                    if (sarray[i][j] != sarray[j][i])
                        return false;
                }
            }
            return true;
        }
        
        private static double ExcelConversion(string s)
        {
            double result = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int j = s.Length - 1- i;
                result += (char.ToUpper(s[j]) - 'A'+ 1) * Math.Pow(26, i);
            }
            return result;
        }

        private static Dictionary<int, string> digitToWord = new Dictionary<int, string>() { 
            {1,"one"},
            {2,"two"},
            {3,"three"},
            {4,"four"}
        };
        public static void Misc()
        {
            if (false)
            {
                ExcelConversion("AAZ");
                int v = longestValidParentheses("())()");
                bool b = IsWordSquare(new string[] { "BALL", "AREA", "LEAD", "LADY" });
                var e = new FlateningEnumerator<int>(FlateningEnumerator<int>.GetTestData());
                foreach (var i in e)
                {
                    Console.Write(i);
                }
                MakeBricks();
                Hangman.Play();
                var r = "xxx".Replace("xx", "");
                CountClumps();
                IsBalancedArray();
                Fix34();
                MaxSpan();
                NextGreaterElement();
                int[] stockPricesYesterday = new int[] { 10, 7, 5, 5 };
                var profit = GetMaxProfit(stockPricesYesterday);
                bool isvalid = IsValidExpression("(({[[]]))");
                GenerateParanthesis(3);
                var result2 = Search(new int[] { 1, 3 }, 3);
                var mappings = new Dictionary<string, string[]>();
                mappings.Add("1", new string[] { "A", "B", "X" });
                mappings.Add("2", new string[] { "C", "D", "Y" });
                mappings.Add("3", new string[] { "E", "F", "Z" });
                mappings.Add("4", new string[] { "P", "Q", "R" });
                var result1 = MapArrayInput(mappings, "1234");

                var list = new List<int> { 1, 2, 3, 4, 5, 6 };
                var filter = new FilteringEnumerator(list.GetEnumerator(), new EvenNumberTest());
                foreach (var item in filter)
                {
                    Console.WriteLine("Item from filter " + item);
                }
                ThreadTest testObj = new ThreadTest();
                var items = testObj.GetItems();
                foreach (var item in items)
                {
                    Console.WriteLine("Item in main" + item);
                }
                Console.ReadLine();
                Thread thread1 = new Thread(testObj.ThreadFunc1);
                Thread thread2 = new Thread(testObj.ThreadFunc2);
                thread1.Start();
                thread2.Start();
                Console.ReadLine();
                return;
                int val = int.MaxValue % 10;
                int x = -123;
                int output = 0;
                bool negative = x > 0;
                if (negative)
                    x = -x;
                int prev = 0;
                while (x > 0)
                {
                    output = output * 10 + x % 10;
                    x = x / 10;
                    if (output / 10 != prev)
                    {
                        // overflow
                    }
                    prev = output;
                }

                //return output;
                var result = ThreeSum(new int[] { -5, 0, -2, 3, -2, 1, 1, 3, 0, -5, 3, 3, 0, -1 });
                int x1 = 5; string s2 = "hello";
                //Console.WriteLine($"{x1} {s2}");
                //string myString = "{foo} is {bar} and {yadi} is {yada}".Inject(o);
                var linked = new LinkedList<int>(Enumerable.Range(0, 5));
                Linq.Test();
                List<int> arr = new List<int> { 1, 2, 3, 4, 5 };
                foreach (var i in arr)
                {
                    arr.Remove(i);
                    arr.Add(i);
                }
            }
            MaxProduct();
            var narr = new string[] { "5", "9", "34", "3", "30"};
            //Array.Sort(narr, CompareNumberStrings);
            //Array.Sort(narr, (s1, s2) => s2.CompareTo(s1));
            int w = FindWordLadder("hit", "cog", new HashSet<string> { "hot", "hip", "dot", "dog", "lot", "log", "cog" });
            DivideWithoutOperator();
            FindDuplicateNumber();
            DiceProblem();
        }

        private static bool SearchWord()
        {
            //https://leetcode.com/problems/word-search/description/
            //Given a 2D board and a word, find if the word exists in the grid.
            // The word can be constructed from letters of sequentially adjacent cell, where "adjacent" cells are those horizontally
            // or vertically neighboring. The same letter cell may not be used more than once.
            char[,] matrix = new char[5, 5];
            string s = "";
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (SearchWord(matrix, s.ToCharArray(), i, j, 0, m, n))
                        return true;
            return false;
        }

        private static bool SearchWord(char[,] matrix, char[] chars, int i, int j, int index, int m, int n)
        {
            if (index == chars.Length)
                return true;
            if (i < 0 || j < 0 || i == m || j == n || matrix[i, j] != chars[index])
                return false;
            matrix[i, j] = 0;
            bool bfound = SearchWord(matrix, chars, i, j + 1, index + 1, m, n) ||
                SearchWord(matrix, chars, i, j - 1, index + 1, m, n) ||
                SearchWord(matrix, chars, i + 1, j, index + 1, m, n) ||
                SearchWord(matrix, chars, i - 1, j, index + 1, m, n);
            matrix[i, j] = chars[index];
            return bfound;
        }

        private static void MaxProduct()
        {
            //https://leetcode.com/problems/maximum-product-subarray/description/
            // Find the contiguous subarray within an array (containing at least one number) which has the largest product.
            // For example, given the array [2,3,-2,4], the contiguous subarray [2,3] has the largest product = 6.
            int [] arr = new int[]{0,2,-3,2,4,1};
            int max = arr[0], min = arr[0], result = arr[0];
            int maxstart = 0, minstart = 0, end = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                int oldmax = max;
                max = Math.Max(Math.Max(max * arr[i], min * arr[i]), arr[i]);
                if (max == arr[i])
                    maxstart = i;
                else if (max == min * arr[i])
                    maxstart = minstart;
                min= Math.Min(Math.Min(oldmax * arr[i], min * arr[i]), arr[i]);
                if (min == oldmax * arr[i])
                    minstart = maxstart;
                else if (min == arr[i])
                    minstart = i;
                if (max > result)
                {
                    result = max;
                    end = i;
                }
            }            
        }
        private static int CompareNumberStrings(string x, string y)
        {
            // ascending x, y  descending y,x
            // this will give "30" higher than "3". to make 3 higher, use x+y, y+x format ( ie, 330  higher than 303)
            return string.Compare(y+x, x+y);
        }

        private static void SurroundRegionMatrix(int[,] matrix)
        {
            //https://leetcode.com/problems/surrounded-regions/description/
            // Given a 2D board containing 'X' and 'O' (the letter O), capture all regions surrounded by 'X'
            // Idea is to start from borders and if its an 'O' mark that and neighbor 'O' to 1, so that it can be kept as 'O' later
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                FindO(matrix, i, 0, row, col);
                if (col > 1)
                    FindO(matrix, i, col - 1, row, col);
            }
            for (int j = 1; j < col-1; j++)
            {
                FindO(matrix, 0, j, row, col);
                if (row > 1)
                    FindO(matrix, row -1, j, row, col);
            }
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    matrix[i, j] = matrix[i, j] == 1 ? 'O' : 'X';
        }

        private static void FindO(int[,] matrix, int i, int j, int row, int col)
        {
            if (matrix[i,j] == 'O')
            {
                matrix[i, j] = 1;
                if (j < col -1)
                    FindO(matrix, i, j + 1, row, col);                
                if (j > 1)
                    FindO(matrix, i, j - 1, row, col);
                if ( i < row-1)
                    FindO(matrix, i+1, j, row, col);
                if ( i > 1)
                    FindO(matrix, i-1, j, row, col);
            }
        }
        private static int FindWordLadder(string start, string end, HashSet<string> dict)
        {
            Queue<string> ladder = new Queue<string>();
            int distance = 1;
            ladder.Enqueue(start);
            while (ladder.Count > 0)
            {
                int count = ladder.Count;
                while (count-- > 0)
                {
                    string word = ladder.Dequeue();
                    if (word == end)
                        return distance;
                    foreach (string s in GetNextWords(word, dict))
                        ladder.Enqueue(s);
                }
                distance++;
            }
            return 0;
        }
        private static List<string> GetNextWords(string s, HashSet<string> dict)
        {
            List<string> next = new List<string>();
            char[] chars = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                for (char c = 'a'; c < 'z'; c++)
                {
                    char old = chars[i];
                    chars[i] = c;
                    string word = new string(chars);
                    if (dict.Remove(word))
                        next.Add(word);
                    chars[i] = old;
                }
            }
            return next;
        }

        private static int DivideWithoutOperator()
        {
            int n = 99, d = 5;
            int result = 0;
            int n1 = Math.Abs(n), d1 = Math.Abs(d);
            while (n1 >= d1)
            {
                int a = d1;
                int p = 1;
                while ((a << 1) < n1)
                {
                    a <<= 1;
                    p <<= 1;
                }
                result += p;
                n1 -= a;
            }
            return ((n > 0 && d < 0) || (n < 0 && d > 0)) ? -result : result;
        }

        private static int FindDuplicateNumber()
        {
            //https://leetcode.com/problems/find-the-duplicate-number/solution/
            // Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive), prove that at least
            // one duplicate number must exist. Assume that there is only one duplicate number, find the duplicate one.
            // Hashset will have O(n) spaice complexity, so use a fast/slow runner technique used for linkedlist cycle detection
            // Since elements are in range 1-n and there are n+1 elements, values will point to an index resulting in cycle
            int[] nums = new int[7]{1,4,6,6,6,2,3};
            int fast = nums[0];
            int slow = nums[0];
            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (fast != slow);
            slow = nums[0];
            while (slow != fast)
            {
                slow = nums[slow];
                fast = nums[fast];
            }
            return slow;
        }

        private static void DiceProblem()
        {
            //https://code.google.com/codejam/contest/6314486/dashboard#s=p0
            int tests = Convert.ToInt32(Console.ReadLine());
            for (int t = 0; t < tests; t++)
            {
                int N = Convert.ToInt32(Console.ReadLine());
                List<HashSet<int>> diceMap = new List<HashSet<int>>();
                for (int i = 0; i < N; i++)
                {
                    string[] arr = Console.ReadLine().Split(' ');
                    var dices = new HashSet<int>();
                    foreach (string s in arr)
                        dices.Add(Convert.ToInt32(s));
                    diceMap.Add(dices);
                }
                int totalOptions = 0;
                for (var j=0; j< diceMap.Count; j++)
                {
                    var set = diceMap[j];
                    foreach (var k in set)
                    {
                        bool []visited = new bool [N];
                        visited[j] = true;
                        totalOptions = Math.Max(1+FindTotalCombination(diceMap, visited, k), totalOptions);
                    }
                }
                Console.WriteLine("Case #{0} = {1}", t, totalOptions);
            }
        }
        private static int FindTotalCombination(List<HashSet<int>> diceMap, bool[] visited, int prev)
        {
            int i = 0;
            for (; i < diceMap.Count; i++)
            {
                if (visited[i])
                    continue;
                if (diceMap[i].Contains(prev + 1))
                {
                    visited[i] = true;
                    return 1 + FindTotalCombination(diceMap, visited, prev + 1);
                }
            }
            return 0;
        }
    }

    class ThreadTest
    {
        private readonly object SyncObject1 = new object();
        private readonly object SyncObject2 = new object();
        IList<int> mylist = new List<int> { 1, 2, 3, 4, 5 };
        public IEnumerable<int> GetItems()
        {
            lock (SyncObject1)
            {
                var items = mylist.Where(delegate(int item)
                {
                    Console.WriteLine("item inside getitems = " + item);
                    return (item > 2);
                });
                Console.WriteLine("GetItems returning releasing lock");
                return items;
            }
        }
        public void ThreadFunc1()
        {
            lock (SyncObject1)
            {
                Console.WriteLine("ThreadFunc1 - Acquired SyncObject1");
                lock (SyncObject2)
                {
                    // Shared resource
                    System.Threading.Thread.Sleep(2000);
                }
            }
            Console.WriteLine("Completed ThreadFunc1");
        }
        public void ThreadFunc2()
        {
            lock (SyncObject2)
            {
                Console.WriteLine("ThreadFunc2 - Acquired SyncObject2");
                lock (SyncObject1)
                {
                    // Shared resource
                    System.Threading.Thread.Sleep(2000);
                }
            }
            Console.WriteLine("Completed ThreadFunc2");
        }
    }



    interface IObjectTest
    {
        bool Test(object o);
    }

    class EvenNumberTest : IObjectTest
    {
        public bool Test(object o)
        {
            int val = 0;
            if (int.TryParse(o.ToString(), out val))
                return val % 2 == 0;
            return true;
        }
    }

    class FilteringEnumerator : IEnumerable, IEnumerator
    {
        IEnumerator myEnumerator;
        IObjectTest myTest;
        public FilteringEnumerator(IEnumerator myEnumerator, IObjectTest myTest)
        {
            this.myEnumerator = myEnumerator;
            this.myTest = myTest;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public void Reset()
        {
            myEnumerator.Reset();
        }

        public object Current
        {
            get
            {
                return myEnumerator.Current;
            }
        }

        public bool MoveNext()
        {
            bool bContinue = true;
            while (bContinue)
            {
                bContinue = myEnumerator.MoveNext();
                if (bContinue && myTest.Test(myEnumerator.Current))
                    break;
            }
            return bContinue;
        }
    }

    class FlateningEnumerator<T> : IEnumerable, IEnumerator
    {
        Queue<IEnumerator> q = new Queue<IEnumerator>();
        public FlateningEnumerator(IEnumerable<IEnumerator<T>> enumerators)
        {
            foreach (IEnumerator e in enumerators)
                if (e.MoveNext())
                    q.Enqueue(e);
        }

        public static IEnumerable<IEnumerator<int>> GetTestData()
        {
            int[] arr1 = new int[]{1, 2, 3};
            int[] arr2 = new int[]{4, 5};
            int[] arr3 = new int[]{6, 7, 8, 9};
            //IEnumerator<int> a = ((IEnumerable<int>)arr1).GetEnumerator();
            //IEnumerator<int> b = ((IEnumerable<int>)arr2).GetEnumerator();
            //IEnumerator<int> c = ((IEnumerable<int>)arr3).GetEnumerator();
            IEnumerator<int> a = arr1.Cast<int>().GetEnumerator();
            IEnumerator<int> b = arr2.Cast<int>().GetEnumerator();
            IEnumerator<int> c = arr3.Cast<int>().GetEnumerator();
            IEnumerator<int>[] iterlist = new IEnumerator<int>[]{a, b, c};
            return iterlist;
        }
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public void Reset()
        {
            //myEnumerator.Reset();
        }

        public object Current
        {
            get
            {
                if (q.Count == 0)
                    throw new InvalidOperationException();
                var e = q.Dequeue();
                var result = e.Current;
                if (e.MoveNext())
                    q.Enqueue(e);
                return result;
            }
        }

        public bool MoveNext()
        {
            return q.Count > 0;
        }
    }
}

// 568. Maximum Vacation Days
// https://leetcode.com/problems/maximum-vacation-days/

/*
LeetCode wants to give one of its best employees the option to travel among N cities to collect algorithm problems. 
 * But all work and no play makes Jack a dull boy, you could take vacations in some particular cities and weeks. Your job is to 
 * schedule the traveling to maximize the number of vacation days you could take, but there are certain rules and restrictions you
 * need to follow.
Rules and restrictions:
You can only travel among N cities, represented by indexes from 0 to N-1. Initially, you are in the city indexed 0 on Monday.
The cities are connected by flights. The flights are represented as a N*N matrix (not necessary symmetrical), called flights 
 * representing the airline status from the city i to the city j. If there is no flight from the city i to the city j, 
 * flights[i][j] = 0; Otherwise, flights[i][j] = 1. Also, flights[i][i] = 0 for all i.
You totally have K weeks (each week has 7 days) to travel. You can only take flights at most once per day and can only take flights
 * on each week's Monday morning. Since flight time is so short, we don't consider the impact of flight time.
For each city, you can only have restricted vacation days in different weeks, given an N*K matrix called days representing this
 * relationship. For the value of days[i][j], it represents the maximum days you could take vacation in the city i in the week j.
You're given the flights matrix and days matrix, and you need to output the maximum vacation days you could take during K weeks.
Example 1:
Input:flights = [[0,1,1],[1,0,1],[1,1,0]], days = [[1,3,1],[6,0,3],[3,3,3]]
Output: 12
Explanation: 
Ans = 6 + 3 + 3 = 12. 
One of the best strategies is:
1st week : fly from city 0 to city 1 on Monday, and play 6 days and work 1 day. 
(Although you start at city 0, we could also fly to and start at other cities since it is Monday.) 
2nd week : fly from city 1 to city 2 on Monday, and play 3 days and work 4 days.
3rd week : stay at city 2, and play 3 days and work 4 days.
Example 2:
Input:flights = [[0,0,0],[0,0,0],[0,0,0]], days = [[1,1,1],[7,7,7],[7,7,7]]
Output: 3
Explanation: 
Ans = 1 + 1 + 1 = 3. 
Since there is no flights enable you to move to another city, you have to stay at city 0 for the whole 3 weeks. 
For each week, you only have one day to play and six days to work. 
So the maximum number of vacation days is 3.
Example 3:
Input:flights = [[0,1,1],[1,0,1],[1,1,0]], days = [[7,0,0],[0,7,0],[0,0,7]]
Output: 21
Explanation:
Ans = 7 + 7 + 7 = 21
One of the best strategies is:
1st week : stay at city 0, and play 7 days. 
2nd week : fly from city 0 to city 1 on Monday, and play 7 days.
3rd week : fly from city 1 to city 2 on Monday, and play 7 days.
Note:
N and K are positive integers, which are in the range of [1, 100].
In the matrix flights, all the values are integers in the range of [0, 1].
In the matrix days, all the values are integers in the range [0, 7].
You could stay at a city beyond the number of vacation days, but you should work on the extra days, which won't be counted as vacation
 * days. If you fly from the city A to the city B and take the vacation on that day, the deduction towards vacation days will count 
 * towards the vacation days of city B in that week.
We don't consider the impact of flight hours towards the calculation of vacation days.
 * https://discuss.leetcode.com/topic/87869/c-java-10-lines-clean-code-graphic-explanation
 * class Solution {
    public int maxVacationDays(int[][] flights, int[][] days) {
        int maxplay = 0, n = days.length, k = days[0].length; // n city , k weeks
        int[][] dp = new int[n][k]; // dp[i][j] - max days play if you spent week j in city i;
        for (int j = k - 1; j >= 0; j--)
            for (int i = 0; i < n; i++) {
                dp[i][j] = days[i][j];
                for (int i1 = 0; i1 < n && j < k - 1; i1++)
                    if (flights[i][i1] > 0 || i == i1)
                        dp[i][j] = Math.max(dp[i][j], days[i][j] + dp[i1][j + 1]);
                if (j == 0 && (i == 0 || flights[0][i] > 0)) maxplay = Math.max(maxplay, dp[i][0]);
            }
        return maxplay;        
    }
}
*/
