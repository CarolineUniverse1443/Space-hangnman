using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form2 : Form
    {
        public int ly;
        public static int mode = 1;

        public Form2()
        {
            Program.f2 = this;          
            InitializeComponent();
            ly = label1.Location.Y;
            label1.Location = new Point(Form2.ActiveForm.Width/7, ly);
            radioButton1.Checked = true;
            radioButton1.Location = new Point(ClientSize.Width / 2 - radioButton1.Width / 2, 100);
            ly = radioButton1.Location.X;
            radioButton2.Location = new Point(ly, 150);
            radioButton3.Location = new Point(ly, 200);
            ly = button1.Location.Y;
            button1.Location = new Point(ClientSize.Width / 2 - button1.Width / 2, ly);
            textBox1.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            textBox1.Text = Environment.NewLine + "    Hello, our new sysadmin. Our radars have detected an unidentified flying object approaching the Earth. " +
                            "All workers have taken refuge in a bunker in fear, so now all hope for you! Enter the password to activate " +
                            "the Destroying Laser Ray 3000 and save all humanity!" + Environment.NewLine + Environment.NewLine +
                            "    To find out the password, you should guess all the symbols in it by pressing the keys on the game keyboard. " +
                            "If this symbol is in the password, it will appear on the screen and the key will turn green. Otherwise the " +
                            "pressed key will become red and the number of errors will increase. With each error the UFO will approach to " +
                            "the Earth.  When you reach 6 errors game will end and the Earth will be destroyed by UFO." + Environment.NewLine +
                            Environment.NewLine +
                            "    You can also choose the level of difficulty in the settings. There are three: easy, middle and hard mode."
                            + Environment.NewLine + Environment.NewLine + "•  Easy: the password is an ordinary word" + Environment.NewLine +
                            "•  Middle: a password consists of 1-2 words, which contain one or more special characters (!, @, &, _)"
                            + Environment.NewLine + "•  Hard: a password contains a word or words and numbers" + Environment.NewLine;           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form2.ActiveForm.StartPosition = FormStartPosition.CenterScreen;
            textBox1.GotFocus += textBox1_GotFocus;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Program.f1.Activate();
            Program.f1.NewGame();
            Program.f1.timer1.Start();
            Program.f1.Enabled = true;
            Program.f1.textBox1.Select();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mode = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mode = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.f1.Activate();
            Program.f1.NewGame();
            Program.f1.timer1.Start();
            Program.f1.Enabled = true;
            Program.f1.textBox1.Select();
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            label1.Select();
        }
    }
}
