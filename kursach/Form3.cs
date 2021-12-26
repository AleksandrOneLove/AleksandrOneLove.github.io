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
    public partial class Form3 : Form
    {
        DataGridView dataForm2;
        Dictionary<string, string> rightGrammemes;
        Form2 form2;
        public Form3(DataGridView _dataForm2, Dictionary<string, string> _rightGrammemes, Form2 _form2)
        {
            dataForm2 = _dataForm2;
            InitializeComponent();
            rightGrammemes = _rightGrammemes;
            form2 = _form2;

        }



        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            DataTable table = new DataTable();
            table.Columns.Add("Слово", typeof(string));
            table.Columns.Add("Правильная часть речи", typeof(string));
            foreach (var rg in rightGrammemes)
            {
                table.Rows.Add(rg.Key, rg.Value);
            }
            dataGridView1.DataSource = table;
        }




        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); //скрываем первое окно
            
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Спасибо! Изменения сохранены :)");
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}


