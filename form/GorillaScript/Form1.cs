﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using static GorillaScript.MonoInjection;

namespace GorillaScript
{
    public partial class Form1 : Form
    {
        Point lastPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(fastColoredTextBox1.Text);
                }
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                fastColoredTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Gorilla Tag");
            if (processes.Length > 0)
            {
                Thread.Sleep(10000);
                try
                {
                    byte[] dllBytes = File.ReadAllBytes("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\BepInEx\\GorillaScript.dll");
                    Injector injector = new Injector("Gorilla Tag");
                    injector.Inject(dllBytes, "GorillaScript.Main", "Loader", "FixedUpdate");

                    MessageBox.Show("Successfully attached to Gorilla Tag!", "GorillaScript", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    File.WriteAllText("injection error.txt", ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Gorilla Tag is not running", "GorillaScript", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e) { lastPoint = new Point(e.X, e.Y); }
        private void guna2Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\code.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(fastColoredTextBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}