using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class Hangman
    {
        private static List<string> words = new List<string>() {"Hello", "Come", "Welcome" };
        private static Dictionary<char, List<int>> mapping = new Dictionary<char, List<int>>();
        public static void Play()
        {
            bool bContinue = true;
            int entry = 0;
            while (bContinue)
            {
                Console.WriteLine("Welcome to Hangman");
                if (entry >= words.Count)
                    entry = 0;
                string word = words[entry];
                int guesscount = 8;
                char[] carr = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    carr[i] = '-';
                    List<int> items = null;
                    char c = char.ToUpper(word[i]);
                    if (!mapping.ContainsKey(c))
                        mapping.Add(c, items = new List<int>());
                    else
                        items = mapping[c];
                    items.Add(i);
                }
                int total = 0;
                while (guesscount > 0 && total < word.Length)
                {
                    Console.WriteLine("The word now looks like this:" + new string(carr));
                    Console.WriteLine("You have {0} guesses left", guesscount);
                    Console.Write("Your guess:");
                    char c = Console.ReadLine().ToUpper()[0];
                    //Console.WriteLine();
                    if (mapping.ContainsKey(c))
                    {
                        Console.WriteLine("That guess is correct");
                        foreach (int j in mapping[c])
                        {
                            if (carr[j] != c)
                            {
                                carr[j] = c;
                                total++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is an incorrect guess");
                    }
                    guesscount--;
                }
                if (total == word.Length)
                    Console.WriteLine("You win, the word is " + new string(carr));
                else
                    Console.WriteLine("You are hung");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();
            }
        }
    }
}
