using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class Form1 : Form
    {
        //Объекты буфера, читателей и писатлелей
        Buffer b;
        Writer writer1, writer2;
        Reader reader1, reader2;
        public Form1(Buffer buffer)

        {
            InitializeComponent();
            b = buffer;
        }
        //Запускаем потоки, подписываемся на события
        private void Form1_Load(object sender, EventArgs e)
        {
            writer1 = new Writer(1, b);
            Thread.Sleep(2500);
            writer2 = new Writer(2, b);
            Thread.Sleep(1000);
            reader1 = new Reader(1, b);
            Thread.Sleep(1600);
            reader2 = new Reader(2, b);
            this.textBox3.Text = "0";
            this.textBox4.Text = Buffer.MaxMessages.ToString();
            writer1.Action += Writer_Action;
            writer2.Action += Writer_Action;
            reader1.Action += Reader_Action;
            reader2.Action += Reader_Action;

        }

        private void Reader_Action(string arg1, int arg2)
        {

            textBox2.Invoke(new Action(()=> { textBox2.Text += arg1 + Environment.NewLine; }));
            textBox3.Invoke(new Action(()=>textBox3.Text = arg2.ToString()));
        }

        private void Writer_Action(string arg1, int arg2)
        {
            textBox1.Invoke(new Action(() => textBox1.Text += arg1 + Environment.NewLine));
            textBox3.Invoke(new Action(() => textBox3.Text = arg2.ToString()));

        }

    }
}
