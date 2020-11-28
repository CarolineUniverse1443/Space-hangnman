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
    public partial class Form1 : Form
    {
        private int bw = 40, bh = 35;//buttons size
        private int dx = 3, dy = 3;//space between buttons
        private List<Button> btn = new List<Button>();
        public int x, y, i, j, k, c = 0, ty;// x, y - keys location, i-c - counters, ty - label1 location in y axis
        public int error = 0;//number of errors
        public int earth = 1;//earth position
        public int s = 1;//counter of timer4
        public bool end;//end of game            

        public char[] btnTextEng = {'1','2','3','4','5','6','7','8','9','0','q',
                          'w','e','r','t','y','u','i','o','p','a','s',
                          'd','f','g','h','j','k','l','z','!','@','x','c','v',
                          'b','n','m','&','_'};
        public string[] easyPass = {"admin","humanity", "victory", "galaxy", "password","cannon", "startrek",
                                  "humanoids", "invasion","martians", "destruction","singularity", "husky", "enceladus",
                                  "spaceship","curiosity","kepler","universe","alien","civilization","sirius",
                                  "student","welcome","qwerty","login"};
        public string[] middlePass = {"death_star","key_pass", "lets_go", "sixty_parsecs","men&&bl@ck", "access_granted", "milky!way",
                                      "secret_code","independence_d@y","black@hole","star&&wars","dont_know!","greenmen!","sun&&moon",
                                      "mars@ttack","arma!geddon","danger!","hot&&quasar","darth!vader","target&&destroy","nightm@re",
                                      "fireb@ll!","laser&&ray","my_pl@net","salvation!"};
        public string[] hardPass = { "hacker228","user123","activation321","12345678", "code103","hawking1942","bigbang138",
                                    "earth03","nuclearbomb987","turing1936","11111","sputnik1957","key268","ufo7019",
                                    "gliese163c","gagarin1961","armstrong1969","wasp33b","andromeda2537","supernova314","13billions",
                                    "11235813","lastchance999","weapon3000","apollo11"};
        
        public Random r = new Random();
        public string pass;
        public char let;//pressed symbol
        public string mist;
        public string act = "Starting activation Destroing Laser Ray 3000 (DLR)" + Environment.NewLine
                            + Environment.NewLine + "Loading resourses..." + Environment.NewLine + "Setting target coordinates... " 
                            + Environment.NewLine + "Laser calibration..." + Environment.NewLine + Environment.NewLine + "Ready!" 
                            + Environment.NewLine + Environment.NewLine + "Enter a password to start DLR" + Environment.NewLine 
                            + Environment.NewLine + "password: ";

        public void createBtn()//create keybuttons
        {
            y = label1.Bottom + 10 * dy;

            k = 0;
            for (i = 0; i < 4; i++)
            {
                x = textBox1.Left + 115;
                for (j = 0; j < 10; j++)
                {

                    Button butt = new Button();
                    btn.Add(butt);
                    btn[k].SetBounds(x, y, bw, bh);
                    btn[k].Text = btnTextEng[k].ToString();
                    if(k == 38)
                    {
                        btn[k].Text = "&&";
                    }
                    btn[k].Font = new Font("Gill Sans", 10, FontStyle.Bold);
                    btn[k].BackColor = SystemColors.ControlLight;
                    btn[k].Cursor = System.Windows.Forms.Cursors.Hand;
                    this.btn[k].Click += new System.EventHandler(this.Button_Click);
                    this.Controls.Add(this.btn[k]);

                    x = x + bw + dx;
                    k++;
                }
                y = y + bh + dy;
            }
        }

        //buttons
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form2Open();
            Program.f2.Text = "Game rules";
            Program.f2.textBox1.Visible = true;
            Program.f2.label1.Select();
            Program.f2.radioButton1.Visible = false;
            Program.f2.radioButton2.Visible = false;
            Program.f2.radioButton3.Visible = false;
            Program.f2.button1.Visible = false;
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form2Open();
            Program.f2.Text = "Settings";
            Program.f2.textBox1.Visible = false;
            Program.f2.radioButton1.Visible = true;
            Program.f2.radioButton2.Visible = true;
            Program.f2.radioButton3.Visible = true;
            Program.f2.button1.Visible = true;
            Program.f2.Size = new Size(512,350);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//restart the game
        {
            if (e.KeyCode == Keys.R)
            {
                NewGame();
            }
        }

        public void Form2Open()
        {
            Form1.ActiveForm.Enabled = false;
            Form2 f2 = new Form2();
            timer1.Stop();
            f2.Show();
        }

        public void NewGame()
        {
            pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO1;
            pictureBox5.Location = new Point(187, 264);
            pictureBox5.Size = new Size(90, 50);
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox10.Visible = true;
            timer1.Start();
            textBox2.Clear();
            c = 0;
            timer4.Start();
            newPass();

            ty = label1.Location.Y;
            label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2, ty);
            error = 0;
            earth = 1;
            label2.Text = mist + error.ToString();
            end = true;
            GameOver();
            end = false;
            k = 0;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    this.btn[k].Click += new System.EventHandler(this.Button_Click);
                    btn[k].BackColor = SystemColors.ControlLight;
                    btn[k].ForeColor = SystemColors.ControlText;
                    btn[k].Enabled = true;
                    k++;
                }
            }
        }

        private ToolTip t1 = new ToolTip();
        private ToolTip t2 = new ToolTip();
        private ToolTip t3 = new ToolTip();

        private void Form1_Load(object sender, EventArgs e)
        {
            timer4.Start();

            t1.AutoPopDelay = 2000;
            t1.InitialDelay = 800;
            t1.ReshowDelay = 500;
            t1.ShowAlways = true;

            t2.AutoPopDelay = 2000;
            t2.InitialDelay = 800;
            t2.ReshowDelay = 500;
            t2.ShowAlways = true;

            t3.AutoPopDelay = 2000;
            t3.InitialDelay = 800;
            t3.ReshowDelay = 500;
            t3.ShowAlways = true;
        }

        //pop-up tips
        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {
            t1.SetToolTip(this.pictureBox9, "Info");
        }
        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            t2.SetToolTip(this.pictureBox8, "Settings");
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            t3.SetToolTip(this.pictureBox1, "Restart");
        }

        public void UfoComes()//animation of approaching ufo
        {
            switch(error)
            {
                case 0:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO1;
                    pictureBox5.Location = new Point(187,264);
                    break;
                case 1:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO2;
                    pictureBox5.Size = new Size(85,45);
                    pictureBox5.Location = new Point(230, 220);
                    break;
                case 2:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO3;
                    pictureBox5.Size = new Size(75, 35);
                    pictureBox5.Location = new Point(225, 200);
                    break;
                case 3:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO3;
                    pictureBox5.Size = new Size(65, 32);
                    pictureBox5.Location = new Point(223, 175);
                    break;
                case 4:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO3;
                    pictureBox5.Size = new Size(55, 28);
                    pictureBox5.Location = new Point(213, 140);
                    break;
                case 5:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO4;
                    pictureBox5.Size = new Size(40, 30);
                    pictureBox5.Location = new Point(213, 130);
                    break;
                case 6://you lose
                    pictureBox6.Visible = true;
                    timer2.Start();
                    break;
            }
        }

        public void newPass()//create new random pass
        {
            int choose = Form2.mode;
            int a = r.Next(0, 25);
            switch(choose)
            {
                case 1:
                    pass = easyPass[a];
                    break;
                case 2:
                    pass = middlePass[a];
                    break;
                case 3:
                    pass = hardPass[a];
                    break;
            }
            label1.Text = "";
            for (i = 0; i < pass.Length; i++)
            {
                label1.Text += "*";
            }
        }

        public void ReplaceChar(ref String str, int index, Char newSymb)//replacing star with guessed symbol
        {
            str = str.Remove(index, 1).Insert(index, newSymb.ToString());
        }

        public void showSym()//opening of guessed symbol
        {
            string nlab = label1.Text;
            for (i = 0; i < pass.Length; i++)
            {
                if(pass[i] == let)
                {
                    ReplaceChar(ref nlab, i, let);
                    label1.Text = nlab;
                }
            }
            ty = label1.Location.Y;
            label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2, ty);
        }

        public void Check()
        {
            if(!label1.Text.Contains("*"))//if you guessed all symbols, you win
            {
                pictureBox10.Visible = false;
                textBox2.Text += pass + Environment.NewLine + "   Access granted!";
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.ScrollToCaret();
                MessageBox.Show("You win! Humanity was saved :)");
                timer3.Start();
                end = true;
            }
            if(error > 5)//if you make 6 mistakes, you lose
            {
                timer1.Stop();
                label1.Text = pass;
                end = true;
            }
        }

        //when game is over, you cant press the keys anymore
        public void GameOver()
        {
            if(end)
            {
                k = 0;
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 10; j++)
                    {
                        this.btn[k].Click -= new System.EventHandler(this.Button_Click);
                        k++;
                    }
                }
            }
        }

        public Form1()
        {
            Program.f1 = this;
            InitializeComponent();
            createBtn();
            newPass();
            btn[0].Select();
            ty = label1.Location.Y;
            label1.Location = new Point(ClientSize.Width / 2 - label1.Width / 2, ty);
            label1.ForeColor = Color.LimeGreen;
            textBox1.Location = new Point(ClientSize.Width / 2 - textBox1.Width / 2, ty);

            ty = pictureBox1.Location.Y;
            pictureBox1.Location = new Point(ClientSize.Width - pictureBox1.Width - 6, ty);
            pictureBox8.Location = new Point(ClientSize.Width - 2*pictureBox1.Width - 12, ty);
            pictureBox9.Location = new Point(ClientSize.Width - 3*pictureBox1.Width - 18, ty);

            mist = label2.Text;
            label2.Text = mist + error.ToString();
            textBox2.SelectionStart = textBox2.TextLength;
            end = false;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            if (!end)
            {
                btn.Enabled = false;
                btn.ForeColor = Color.White;
                if (pass.Contains(btn.Text.ToLower()))//you guessed a symbol
                {
                    btn.BackColor = Color.LimeGreen;
                    let = btn.Text[0];
                    showSym();
                }
                else //you were wrong
                {
                    btn.BackColor = Color.Red;
                    error++;
                    label2.Text = mist + error.ToString();
                    if (error < 6)
                    {
                        textBox2.Text += Environment.NewLine + "   Access denied!" + Environment.NewLine + "password: ";
                    }
                    else
                    {
                        textBox2.Text += Environment.NewLine + "   Access denied!";
                    }
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.ScrollToCaret();
                }
                Check();
                GameOver();
                UfoComes();
            }
        }
        
        //timers begin
        private void timer1_Tick(object sender, EventArgs e)//earth rotation animation
        {
            switch (earth)
            {
                case 1:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Земля_1;
                    pictureBox10.BackgroundImage = Hangman.Properties.Resources.AttentionW;
                    break;
                case 2:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Земля_2;
                    pictureBox10.BackgroundImage = Hangman.Properties.Resources.AttentionR;
                    break;
                case 3:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Земля_3;
                    pictureBox10.BackgroundImage = Hangman.Properties.Resources.AttentionW;
                    break;
                case 4:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Земля_4;
                    pictureBox10.BackgroundImage = Hangman.Properties.Resources.AttentionR;
                    earth = 1;
                    break;
            }
            earth++;
        }

        private void timer2_Tick(object sender, EventArgs e)//animation of destroying earth
        {
            switch(s)// 1-3 - alien laser ray flies to the earth
            {
                case 1:
                    pictureBox6.Location = new Point(200,160);
                    break;
                case 2:
                    pictureBox6.Location = new Point(192, 170);
                    break;
                case 3:
                    pictureBox6.Location = new Point(188, 174);
                    break;
                case 4: //alien laser ray hit the earth
                    pictureBox6.Visible = false;
                    break;
                case 5:// 5-10 - earth explodes
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.ЗемляВз1;
                    break;
                case 6:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.ЗемляВз2;
                    break;
                case 7:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.ЗемляВз3;
                    break;
                case 8:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Взрыв4;
                    break;
                case 9:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Взрыв5;
                    break;
                case 10:
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Взрыв6;
                    break;
                case 11:
                    pictureBox6.Location = new Point(208,150);
                    
                    pictureBox4.Visible = false;
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.UFO2;
                    pictureBox4.BackgroundImage = Hangman.Properties.Resources.Земля_1;
                    timer2.Stop();
                    s = 1;
                    MessageBox.Show("Earth and all humanity was destroyed by UFO", "The end", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

            }
            s++;
        }

        public int win = 1;

        private void timer3_Tick(object sender, EventArgs e)
        {
            switch (win)//ufo explodes from dlr
            {
                case 1:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.Взрыв4;
                    break;
                case 2:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.Взрыв5;
                    break;
                case 3:
                    pictureBox5.BackgroundImage = Hangman.Properties.Resources.Взрыв6;
                    break;
                case 4:
                    pictureBox5.Visible = false;
                    timer3.Stop();
                    win = 1;
                    break;
            }
            win++;
        }

        private void timer4_Tick(object sender, EventArgs e)//displaying text om the cmd about dlr activation
        {
            if (c < act.Length)
            {
                textBox2.Text += act[c];
                textBox2.SelectionStart = textBox2.TextLength;
                c++;
            }
            else
            {
                timer4.Stop();
                textBox2.Select();
            }
        }//timers end
    }
}
