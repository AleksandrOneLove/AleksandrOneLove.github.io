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
            public char Edit;

            public User(string _word, string _PartOfSpeech, char _Edit)
            {
                word= _word;
                PartOfSpeech= _PartOfSpeech;
                Edit= _Edit;
            }
        }

        List<User> users = new List<User>();

       
        public Form2(string _text)
        {
           text = _text;
            InitializeComponent();
            
        }
        public string text;
       

       

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            List<string> words = LayerConnection.TextToWords(text);
            List<string> grammemes = LayerConnection.TextToGrammemes(text, @"C:\Users\Coddy\Desktop\HMMtagger.txt");
            for(int i = 0; i < words.Count; i++)
            {
                users.Add(new User(words[i], grammemes[i], '+'));
            }
            

            

            DataTable table = new DataTable();
            table.Columns.Add("Слово", typeof(string));
            table.Columns.Add("Часть речи", typeof(string));
            table.Columns.Add("Правка", typeof(char));

            for(int i =0; i<users.Count;i++)
            {
                table.Rows.Add(users[i].word,users[i].PartOfSpeech,users[i].Edit);
            }
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            //тут дальше циклами пробегаемся по гриду
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                   // fr.dataGridView1.Rows[i].Cells[j].Value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
               //fr.dataGridView1.Rows.Add();
            }
            fr.ShowDialog();

            this.Hide();  //скрываем первое окно
           
            fr.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();  //скрываем первое окно
            Form1 frm1 = 
            new Form1(); //где Form2 - название второй формы
            frm1.Show();
        }
    }
    }

