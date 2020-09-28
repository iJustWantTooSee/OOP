using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newLive
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private GameEngine gameEngine;



        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            gameEngine = new GameEngine
                (
                rows: pictureBox1.Height,
                cols: pictureBox1.Width
                );
   
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();

        }

        private void stopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();
        }

        private void NextUpdate()
        {
            graphics.Clear(Color.DarkGreen);
            List<Pair<int, int>> people = gameEngine.NextUpdate();

            foreach (var item in people)
            {
                graphics.FillRectangle(Brushes.Black, item.First*4, item.Second * 4, 4, 4);
            }
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextUpdate();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stopGame();
        }

    }
}
