using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinProcess
{
    public partial class Form1 : Form
    {

        System.Threading.Timer timer;
        System.Threading.Timer timerStartProg;
        TimerCallback callback;
        TimerCallback callbackStartProg;
        bool x;
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("ProcessName");
            listView1.Columns.Add("ProcessId");
            listView1.GridLines = true;
            callback = new TimerCallback(TimerMetod);
            timer = new System.Threading.Timer(callback);
            //timer.Change(2000, 10000);
           

            //dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            callbackStartProg = new TimerCallback(TimerMetodStartProg);
         
            x = false; //контроль запуска по времени, если не запущено;

            button2.Enabled = false;

        }

        public void TimerMetodStartProg(object obj)
        {
            TimerProg();
        }

        public void TimerProg()
        {
            try
            {
                if (DateTime.Now.ToShortTimeString() == dateTimePicker1.Text&&x==false)
                { 
                Process.Start(textBox1.Text);
                    x = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void TimerMetod(object obj)
        {
            Count();
        }
        public void Count()
        {
            Process[] proc = Process.GetProcesses();
            listView1.Items.Clear();
            for (int i = 0; i < proc.Count(); i++)
            listView1.Items.Add(new ListViewItem(new[] { proc[i].ProcessName, proc[i].Id.ToString()}));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer.Change(1000, 10000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerStartProg = new System.Threading.Timer(callbackStartProg);
            timerStartProg.Change(100, 200);
        }

        private void button3_Click(object sender, EventArgs e) //задание пути к файлу
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text= openFileDialog1.FileName;

                button2.Enabled = true;
                button3.Enabled = false;
                //ExtractIcon(path);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)  //сброс
        {
            button3.Enabled = true;
            button2.Enabled = false;
            x = false;

            timerStartProg.Dispose();
        }
    }
}
