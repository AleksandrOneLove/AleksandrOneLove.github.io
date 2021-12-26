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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ошибка!\n Текстовое поле не может быть пустым.");
                return;
            }
            string path = @"C:\Users\Coddy\Desktop\HMMtagger.txt";

            this.Hide();  //скрываем первое окно
            Form2 frm2 = new Form2(textBox1.Text); //где Form2 - название второй формы
            frm2.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

