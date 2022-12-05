using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space
{
    public partial class Form1 : Form
    {
        private SoundPlayer soundPlayer;
        SoundPlayer simpleSound = new SoundPlayer(@"shots.wav");

        public Form1()
        {
            InitializeComponent();
            lbl_over.Hide();
            
        }

        bool right, left, space;
        int score;


        void AddMusic()
        {
            foreach (Control j in this.Controls)
            {
                if (j is PictureBox && j.Tag == "bullet") 
                {
                   if( 10 < j.Top)
                    {
                        simpleSound.PlayLooping();
                    }
                   if(j.Top <= 10 && j.Top > 400){
                        simpleSound.Stop();
                    }

                }
            }
        }

        // khi đạn bắn trúng tàu bay thì nó sẽ biến mất
        void Game_Result()
        {
            SoundPlayer soundPlayer = new SoundPlayer("boom.wav");

            foreach (Control j in this.Controls)
            {
                foreach (Control i in this.Controls)
                {
                    if (j is PictureBox && j.Tag == "bullet")
                    {

                        if (i is PictureBox && i.Tag == "enemy")
                        {
                            if (j.Bounds.IntersectsWith(i.Bounds))
                            {

                                soundPlayer.Play(); // k hoạt động (
                                i.Top = -100;

                                ((PictureBox)j).Image = Properties.Resources.explosion; // khi bắn trúng thì sẽ có hiệu ứng nổ

                                // khi bắn trúng thì điểm được cộng vào
                                score++;
                                lbl_score.Text = "Score:" + score;

                            }
                            else
                            {
                                soundPlayer.Stop();
                            }
                        }
                    }
                }
            }

            // khi tàu bay đam trúng ng chơi game sẽ kết thúc hiển thị chữ game over
            if (player.Bounds.IntersectsWith(ship.Bounds) || player.Bounds.IntersectsWith(alien.Bounds))
            {
        
                timer1.Stop();
                simpleSound.Stop(); 
                lbl_over.Show();
                lbl_over.BringToFront();
            }
 
        }

       
        // tạo nền ngôi sao
        void Star()
        {
            foreach(Control j in this.Controls)
            {
                if(j is PictureBox && j.Tag == "stars")
                {
                    j.Top += 10;
                    if(j.Top > 400)
                    {
                        j.Top = 0;
                    }
                }
            }
        }

 
        void Add_Bullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.SizeMode = PictureBoxSizeMode.AutoSize;
            bullet.Image = Properties.Resources.bullet_img;
            bullet.BackColor = System.Drawing.Color.Transparent;
            bullet.Tag = "bullet";
            bullet.Left = player.Left + 15;
            bullet.Top = player.Top -30;
            this.Controls.Add(bullet);
            bullet.BringToFront();
    
        }

        void Bullet_Movement()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && x.Tag == "bullet")
                {
                    x.Top -= 10;
 
                    
                    if (x.Top < 50)
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }
        void Arrow_key_Movement()
        {
            if (right == true)
            {
                if (player.Left < 425)
                {
                    player.Left += 20;
                }
            }
            if (left == true)
            {
                if (player.Left > 10)
                {
                    player.Left -= 20;
                }
            }
        }


        void Enemy_Movement()
        {
            Random rnd = new Random();
            int x, y;
            if(alien.Top >= 500)
            {
                x = rnd.Next(0,300);
                alien.Location = new Point(x, 0);
            }
            if(ship.Top >= 500)
            {
                y = rnd.Next(0,300);
                ship.Location = new Point(y, 0);
            }
            else
            {
                alien.Top += 15;
                ship.Top += 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Arrow_key_Movement();
            Enemy_Movement();
            AddMusic();
            Add_Bullet();
            Bullet_Movement();
            Star();
            Game_Result();
 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = true;
                Add_Bullet();
              
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = false;
            }

        }
    }
}
