using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteganographyA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "Слова и фразы 1. Слова и фразы 2. Слова и фразы 3! Слова и фразы 4? Слова и фразы 5. Слова и фразы 6. Слова и фразы 7. Слова и фразы 8! Слова и фразы 9? Слова и фразы 10.";
            label1.Text = (richTextBox1.Text.Split('.', '!', '?').Length - 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text + "#";
            label1.Text = (str.Split('.','!', '?').Length-1).ToString();
            char[] chars = str.ToCharArray();
            
            string msg = "";

            for (int i = 0; i < chars.Length; i++)
            {
                
                if (chars[i] == '.' || chars[i] == '!' || chars[i] == '?')
                {
                   
                    if (chars[i] == '#') break;
                    if(chars[i + 1]==' ' && chars[i + 2]==' ')
                    {
                        msg += "0";
                    }
                    if (chars[i + 1] == ' ' && chars[i + 2] != ' ')
                    {
                        msg += "1";
                    }                   
                }
            }
            MessageBox.Show(msg + " " + (char)Convert.ToInt32(msg, 2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;
            label1.Text = (str.Split('.', '!', '?').Length - 1).ToString();
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textBox1.Text);           
            char[] msgChars = (Convert.ToString(asciiBytes[0], 2)+"#").ToCharArray();
            string msgD ="";
            for (int i=0; i< msgChars.Length;i++)
            {  msgD += msgChars[i]; }          
            MessageBox.Show(msgD);

            
            str = str.Replace("  ", " ");
           // str = str.Replace("", "@");
            string text = "";

            char[] chars = str.ToCharArray();           
            List<Char> indexs = new List<Char>();
            List<string> subStrs = new List<string>();           
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '.' || chars[i] == '!' || chars[i] == '?')
                {                   
                    indexs.Add(chars[i]);
                }
            }
            subStrs.AddRange(str.Split('.', '!', '?'));
            for(int i = 1; i < subStrs.Count; i++)
            {
                subStrs[i] = subStrs[i].TrimStart(' ');
            }

            for (int i = 0; i < indexs.Count; i++)
            {
                subStrs[i] = subStrs[i] + indexs[i];                
            }
            int last = 0;
            for (int i = 0; i < msgChars.Length; i++)
            {
                if (msgChars[i] == '#')
                    richTextBox3.Text += subStrs[i] + '#';
                if (msgChars[i] == '1')
                    richTextBox3.Text += subStrs[i] + " ";
                if (msgChars[i] == '0')
                    richTextBox3.Text += subStrs[i] + "  ";
                //
                subStrs[i] = subStrs[i] + msgChars[i];
                text += subStrs[i];
                last = i;

                
            }
            for (int i = last+1; i < indexs.Count; i++)
            {
               // subStrs[i] = subStrs[i];
                text += subStrs[i];
                richTextBox3.Text += subStrs[i];
            }

            richTextBox2.Text = text ;


        }
    }
}
