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
        private const int MAP_SIZE = 3001;
        private const int BREAK_BETWEEN_PERIODS = 300;
        private int _sizeOfUnit = 3;
        private int _sizeOfCell = 3;
        private bool _isStart = false;
        private int _scaleMap;
        private Unit _unitForDisplayInformation;
        private bool _cheakDisplayUnit = false;
        private int _stateCliclLeftButtonMause = 0;
        public Map map;
        public Form1()
        {
            InitializeComponent();
            currentSpeedLife.Enabled = false;
            pictureBox1.Enabled = false;
            _isStart = false;
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            _isStart = true;
            map = new Map();
            gameEngine = new GameEngine(map);

            _scaleMap = (int)scalingFactor.Value;
            pictureBox1.Width = MAP_SIZE * _scaleMap;
            pictureBox1.Height = MAP_SIZE * _scaleMap;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.DarkGreen);

            timer1.Interval = gameEngine.SetSpeedLife((int)currentSpeedLife.Value);
            timer2.Interval = gameEngine.SetSpeedLife((int)currentSpeedLife.Value) + BREAK_BETWEEN_PERIODS;

            timer1.Start();
            timer2.Start();

            settinginformationAtStartup();
        }

        private void stopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();
            timer2.Stop();

            button1.Enabled = false;
            buttonStart.Enabled = true;
            _cheakDisplayUnit = false;
        }

        private void DrawNextUpdateUnit()
        {
            List<Unit> listUnitsOfMap = gameEngine.NextUpdateUnit();

            foreach (var unit in listUnitsOfMap)
            {
                graphics.FillRectangle(Brushes.DarkGreen, unit.OldUnitCoordinate.X * _sizeOfCell * _scaleMap, unit.OldUnitCoordinate.Y * _scaleMap
                    * _sizeOfCell, _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
                if (unit.IsLife)
                {
                    switch (unit.UnitGender)
                    {
                        case 10:
                            graphics.FillRectangle(Brushes.Black, unit.CurrentUnitCoordinate.X * _sizeOfCell * _scaleMap, unit.CurrentUnitCoordinate.Y
                   * _sizeOfCell * _scaleMap, _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
                            break;
                        case 20:
                            graphics.FillRectangle(Brushes.White, unit.CurrentUnitCoordinate.X * _sizeOfCell * _scaleMap, unit.CurrentUnitCoordinate.Y
                   * _sizeOfCell * _scaleMap, _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
                            break;
                        case 30:
                            graphics.FillRectangle(Brushes.DarkBlue, unit.CurrentUnitCoordinate.X * _sizeOfCell * _scaleMap, unit.CurrentUnitCoordinate.Y
                   * _sizeOfCell * _scaleMap, _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
                            break;
                        default:
                            break;
                    }

                }

            }

            pictureBox1.Refresh();
        }

        private void DrawNextUpdateGrass()
        {
            List<Point> listGrassOfMap = gameEngine.NextUpdateGrass();
            foreach (var grass in listGrassOfMap)
            {
                graphics.FillRectangle(Brushes.Red, grass.X * _sizeOfCell * _scaleMap, grass.Y * _sizeOfCell * _scaleMap,
                                        _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
            }
            pictureBox1.Refresh();
        }
        private void ShowUnitUnfo()
        {
            label4.Text = $"Юнит: {_unitForDisplayInformation.UnitNumber} \n" +
                $"Гендер: {(GameEngine.Gender)_unitForDisplayInformation.UnitGender}\n" +
                $"Координата Х: {_unitForDisplayInformation.CurrentUnitCoordinate.X} \n" +
                $"Координата Y: {_unitForDisplayInformation.CurrentUnitCoordinate.Y}\n" +
                $"Сытость: {_unitForDisplayInformation.UnitSatiety}";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextUpdateUnit();
            label2.Text = $"Period of life: {gameEngine.PeriodOfLife}";
            if (_cheakDisplayUnit)
            {
                if (_unitForDisplayInformation.IsLife)
                {
                    ShowUnitUnfo();
                }
                else
                {
                    label4.Text = "Юнит умер";
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            DrawNextUpdateGrass();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stopGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gameEngine.CheakClickPause())
            {
                timer1.Stop();
                timer2.Stop();
                button1.Text = "CONTINUE";
                buttonStop.Enabled = false;
            }
            else
            {
                timer1.Start();
                timer2.Start();
                button1.Text = "PAUSE";
                buttonStop.Enabled = true;
            }

        }

        private void scalingFactor_ValueChanged(object sender, EventArgs e)
        {
            _scaleMap = (int)scalingFactor.Value;
            pictureBox1.Width = MAP_SIZE * _scaleMap;
            pictureBox1.Height = MAP_SIZE * _scaleMap;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.DarkGreen);
            if (_isStart)
            {
                DrawNextUpdateUnit();
                DrawNextUpdateGrass();
            }
            
        }

        private void currentSpeedLife_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = gameEngine.SetSpeedLife((int)currentSpeedLife.Value);
            timer2.Interval = gameEngine.SetSpeedLife((int)currentSpeedLife.Value) + BREAK_BETWEEN_PERIODS;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _unitForDisplayInformation = gameEngine.GetUnitInfo(e.X / _sizeOfUnit / _scaleMap, e.Y / _sizeOfUnit / _scaleMap);
                _cheakDisplayUnit = true;
                ShowUnitUnfo();
            }
            if (e.Button == MouseButtons.Right)
            {
                gameEngine.UpdateInfoUsingTheMause(e.X / _sizeOfUnit / _scaleMap, e.Y / _sizeOfUnit / _scaleMap, _stateCliclLeftButtonMause);
                paintUpdateInPeriod(e.X, e.Y);
                DrawNextUpdateUnit();
                DrawNextUpdateGrass();
            }
        }

        private void paintUpdateInPeriod(int x, int y)
        {
            if (_stateCliclLeftButtonMause == (int)GameEngine.StateButton.delGrass)
            {
                graphics.Clear(Color.DarkGreen);
            }
            if (_stateCliclLeftButtonMause == (int)GameEngine.StateButton.createGrass || _stateCliclLeftButtonMause 
                ==(int)GameEngine.StateButton.delUnit)
            {
                graphics.FillRectangle(Brushes.Red, x * _sizeOfCell * _scaleMap, y * _sizeOfCell * _scaleMap,
                                                        _sizeOfUnit * _scaleMap, _sizeOfUnit * _scaleMap);
            }


        }
        private void settinginformationAtStartup()
        {
            button1.Enabled = true;
            buttonStart.Enabled = false;
            currentSpeedLife.Enabled = true;
            pictureBox1.Enabled = true;
            label4.Text = "Юнит: \n" +
                "Гендер: \n" +
                "Координата X: \n" +
                "Координата Y: \n" +
                "Сытость: \n";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = (int)GameEngine.StateButton.createUnit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = (int)GameEngine.StateButton.createGrass;
        }

        private void buttonDelUnit_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = (int)GameEngine.StateButton.delUnit;
        }

        private void buttonDelGrass_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = (int)GameEngine.StateButton.delGrass;
        }
    }
}
