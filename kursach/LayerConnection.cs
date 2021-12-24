using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kursach
{
    static class LayerConnection
    {
        static Dictionary<int, string> grammemes = new Dictionary<int, string>
        {
            {0, "существительное"},
            {1, "прилагательное (полное)"},
            {2, "прилагательное (краткое)"},
            {3, "компаратив"},
            {4, "глагол (личная форма)"},
            {5, "глагол (инфинитив)"},
            {6, "причастие (полное)"},
            {7, "причастие (краткое)"},
            {8, "деепричастие"},
            {9, "числительное"},
            {10, "наречие"},
            {11, "местоимение-существительное"},
            {12, "предикатив"},
            {13, "предлог"},
            {14, "союз"},
            {15, "частица"},
            {16, "междометие"}
        };
        public static List<string> TextToGrammemes(string text, string path) // кнопка ввода текста
        {
            List<string> words = TextToWords(text);
            StreamReader sr = new StreamReader(path);
            HMMTagger.ReadModel(sr);
            HMMTagger.Normalizing();
            ViterbiAlgorithm vi = new ViterbiAlgorithm(words);
            return SequenceToGrammemes(vi.Sequence);
        }
        public static void ChangeModel(List<String> words, int[] sequence, string path) // 2 экран
        {
            StreamReader sr = new StreamReader(path);
            HMMTagger.ReadModel(sr);
            sr.Close();
            for (int i = 1; i < sequence.Length; i++)
            {
                HMMTagger.A[sequence[i - 1], sequence[i]]++;
            }
            for (int i = 0; i < words.Count; i++)
            {
                HMMTagger.AddWord(ViterbiAlgorithm.grammemes[sequence[i]], words[i]);
            }
            StreamWriter sw = new StreamWriter(path);
            HMMTagger.SaveModel(sw);
            sw.Close();
        }
        public static List<String> TextToWords(string text)
        {
            List<string> words = new List<string>();
            string[] subs = text.Split(' ');
            for (int i = 0; i < subs.Length; i++)
            {
                words.Add(subs[i]);
            }
            return words;
        }
        static List<string> SequenceToGrammemes(int[] sequence)
        {
            List<string> userGrammemes = new List<string>();
            for (int i = 0; i < sequence.Length; i++)
                userGrammemes.Add(grammemes[sequence[i]]);
            return userGrammemes;
        }
    }
}
