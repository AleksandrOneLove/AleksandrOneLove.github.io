using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{
    static class HMMTagger
    {
        public const int nOfGrammemes = 17;
        public static Dictionary<string, int> grammemes = new Dictionary<string, int>
        {
            {"NOUN", 0},
            {"ADJF", 1},
            {"ADJS", 2},
            {"COMP", 3},
            {"VERB", 4},
            {"INFN", 5},
            {"PRTF", 6},
            {"PRTS", 7},
            {"GRND", 8},
            {"NUMR", 9},
            {"ADVB", 10},
            {"NPRO", 11},
            {"PRED", 12},
            {"PREP", 13},
            {"CONJ", 14},
            {"PRCL", 15},
            {"INTJ", 16}
        };
        public static decimal[,] A = new decimal[nOfGrammemes, nOfGrammemes]; // матрица переходов
        public static decimal[] p = new decimal[nOfGrammemes]; // матрица начального распределения
        public static Dictionary<string, Dictionary<string, decimal>> B = new Dictionary<string, Dictionary<string, decimal>>();
        static HMMTagger()
        {
            for (int i = 0; i <nOfGrammemes; i++)
            {
                for (int j = 0; j < nOfGrammemes; j++)
                    A[i, j] = 0;
            }
            for (int i = 0; i < nOfGrammemes; i++)
            {
                p[i] = 0;
            }
            foreach (var g in grammemes.Keys)
            {
                B.Add(g, new Dictionary<string, decimal>());
            }
        }
        public static void AddWord(string grammem, string word)
        {
            word = word.ToLower();
            if (word.Length > 3)
                word = word.Substring(word.Length - 3);
            if (!HMMTagger.B[grammem].ContainsKey(word))
                HMMTagger.B[grammem].Add(word, 0);
            HMMTagger.B[grammem][word]++;
        }
        public static void AddWord(string grammem, string word, int count) 
        {
            word = word.ToLower();
            if (word.Length > 3)
                word = word.Substring(word.Length - 3);
            if (!HMMTagger.B[grammem].ContainsKey(word))
                HMMTagger.B[grammem].Add(word, 0);
            HMMTagger.B[grammem][word] = count;
        }
        public static void Normalizing()
        {
            decimal sum = 0;
            for (int i = 0; i < nOfGrammemes; i++)
            {
                sum = 0;
                for (int j = 0; j < nOfGrammemes; j++)
                    sum += (decimal)A[i, j];
                for (int j = 0; j < nOfGrammemes; j++)
                    A[i, j] /= sum;
            }
            sum = 0;
            for (int i = 0; i < nOfGrammemes; i++)
                sum += (decimal)p[i];
            for (int i = 0; i < nOfGrammemes; i++)
                p[i] /= sum;
            Dictionary<string, Dictionary<string, decimal>> tempB = new Dictionary<string, Dictionary<string, decimal>>();
            foreach (var g in grammemes.Keys)
            {
                tempB.Add(g, new Dictionary<string, decimal>());
            }
            foreach (var b in B)
            {
                sum = 0;
                foreach (var word in B[b.Key])
                {
                    sum += word.Value;
                }
                foreach (var word in B[b.Key])
                {
                    tempB[b.Key].Add(word.Key, word.Value/sum);
                }
            }
            B = tempB;
        }
        public static void SaveModel(StreamWriter sw)
        {
            for (int i = 0; i < nOfGrammemes; i++)
                sw.WriteLine(p[i]);
            for (int i = 0; i < nOfGrammemes; i++)
            {
                for (int j = 0; j < nOfGrammemes; j++)
                {
                    sw.WriteLine(A[i, j]);
                }
            }
            foreach(var b in B)
            {
                sw.WriteLine(b.Key);
                foreach (var word in B[b.Key])
                {
                    sw.WriteLine("{0} {1}", word.Key, word.Value);
                }
            }
            sw.Close();
        }
        public static void ReadModel(StreamReader sr)
        {
            for (int i = 0; i < nOfGrammemes; i++)
                p[i] = Convert.ToDecimal(sr.ReadLine());
            for (int i = 0; i < nOfGrammemes; i++)
            {
                for (int j = 0; j < nOfGrammemes; j++)
                {
                    A[i, j] = Convert.ToDecimal(sr.ReadLine());
                }
            }
            string s = "";
            string grammem = "";
            string[] subs;
            while (sr.Peek()!= -1)
            {
                s = sr.ReadLine();
                if (HMMTagger.B.ContainsKey(s))
                {
                    grammem = s;
                    continue;
                }
                subs = s.Split(' ');
                AddWord(grammem, subs[0], Convert.ToInt32(subs[1]));
            }
            sr.Close();
        }
        public static void PrintA()
        {
            for (int i = 0; i < nOfGrammemes; i++)
            {
                decimal sum = 0;
                for (int j = 0; j < nOfGrammemes; j++)
                {
                    Console.Write("{0} ", A[i, j]);
                    sum += A[i, j];
                }
                Console.WriteLine(sum);
            }
        }
        public static void PrintP()
        {
            decimal sum = 0;
            for (int i = 0; i < nOfGrammemes; i++)
            {
                Console.WriteLine(p[i]);
                sum += p[i];
            }
            Console.WriteLine(sum);
        }
        public static void PrintB()
        {
            decimal sum = 0;
            foreach (var w in B["INTJ"])
            {
/*                  if (w.Value > 10)
*/                      Console.WriteLine("{0} {1}", w.Key, w.Value);
                sum += w.Value;
            }
             Console.WriteLine(sum);
        }
    }
}
