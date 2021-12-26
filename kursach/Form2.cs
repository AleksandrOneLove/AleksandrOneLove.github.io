using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace kursach
{
    public partial class Form2 : Form
    {
        public struct User
        {
            public string word;
            public string PartOfSpeech;
            /*public string Edit;*/

            public User(string _word, string _PartOfSpeech/*, string _Edit*/)
            {
                word = _word;
                PartOfSpeech = _PartOfSpeech;
                /* Edit= _Edit;*/
            }
        }

        List<User> users = new List<User>();


        public Form2(string _text)
        {
            text = _text;
            InitializeComponent();

        }
        public string text;
        Dictionary<string, string> rightGrammemes = new Dictionary<string, string>();
        DataTable table = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;


            List<string> words = LayerConnection.TextToWords(text);
            List<string> grammemes = LayerConnection.TextToGrammemes(text, @"C:\Users\Coddy\Desktop\HMMtagger.txt");
            string[] input = new string[] {"существительное", "прилагательное (полное)", "прилагательное (краткое)", "компаратив", "глагол (личная форма)",
"глагол (инфинитив)",
"причастие (полное)",
"причастие (краткое)",
"деепричастие",
"числительное",
"наречие",
"местоимение-существительное",
"предикатив",
"предлог",
"союз",
"частица",
"междометие" };
            List<string> listOfGrammemes = new List<string>(input);
            for (int i = 0; i < words.Count; i++)
            {
                users.Add(new User(words[i], grammemes[i]/*, ""*/));
            }


            table.Columns.Add("Слово", typeof(string));
            table.Columns.Add("Часть речи", typeof(string));
            //table.Columns.Add("Правка", typeof(char));

            for (int i = 0; i < users.Count; i++)
            {
                table.Rows.Add(users[i].word, users[i].PartOfSpeech/*,users[i].Edit*/);
            }
            table.Columns["Слово"].ReadOnly = true;
            table.Columns["Часть речи"].ReadOnly = true;

            dataGridView1.DataSource = table;
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.HeaderText = "Правка";
            foreach (var g in listOfGrammemes)
            {
                combo.Items.Add(g);
            }
            dataGridView1.Columns.Add(combo);
            table.AcceptChanges();
            dataGridView1.CellValueChanged +=
            new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        }

        private void dataGridView1_CellValueChanged(object sender,
        DataGridViewCellEventArgs e)
        {
            string word = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
            string grammem = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (!rightGrammemes.ContainsKey(word))
                rightGrammemes.Add(word, grammem);
            else rightGrammemes[word] = grammem;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form3 fr = new Form3(dataGridView1, rightGrammemes, this);
            this.Hide();

            fr.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); //скрываем первое окно
            Form1 frm1 = new Form1(); //где Form2 - название второй формы
            frm1.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
