using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using newLive.Sounds;

namespace newLive
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private GameEngine _gameEngine;
        private int _mapSize = 3001;
        private int _sizeOfCell = 4;
        private bool _isStart = false;
        private int _scaleMap;
        private Brush _backgroundColor { get; set; } = Brushes.DarkGreen;
        private Color _bcg = Color.DarkGreen;
        private UnitWithoutGeneric _unitToDisplay = null;
        private bool _cheakDisplayUnit = false;
        private bool isFirstGrass = true;
        private StateButton _stateCliclLeftButtonMause = 0;
        private Map _map;
        private Photo _imageGameObject = new Photo();
        public Form1()
        {
            InitializeComponent();
            currentSpeedLife.Enabled = false;
            pictureBox1.Enabled = false;
            _isStart = false;
            isFirstGrass = true;
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;
            Effects.PlaySoundStart();

            _isStart = true;
            _map = new Map(_mapSize / _sizeOfCell);
            _gameEngine = new GameEngine(_map);


            _scaleMap = (int)scalingFactor.Value;
            pictureBox1.Width = _mapSize * _scaleMap;
            pictureBox1.Height = _mapSize * _scaleMap;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Interval = _gameEngine.GetSpeedLife((int)currentSpeedLife.Value);

            DrawBackground();

            timer1.Start();


            settinginformationAtStartup();
        }

        private void stopGame()
        {
            if (!timer1.Enabled)
                return;
           Effects.PlaySoundOfTheEnd();
            timer1.Stop();

            button1.Enabled = false;
            buttonStart.Enabled = true;
            _cheakDisplayUnit = false;
        }

        #region отрисовка Юнитов, Домов, Сезона года, Ресурсов
        private void DrawBackground()
        {
            for (int y = 0; y < _map.Size; y++)
            {
                for (int x = 0; x < _map.Size; x++)
                {
                    _graphics.FillRectangle(_map.CellOnMap[x, y].Colors[(int)_map.Season], x * _sizeOfCell * _scaleMap,
                    y * _sizeOfCell * _scaleMap, _sizeOfCell * _scaleMap, _sizeOfCell * _scaleMap);
                }
            }
        }

        private void DrawMap()
        {
            //закрасить мертвых юнитов
            DrawDeadUnit();
            //закрасить предыдущую позицию
            PaintOverPreviousPosition(_map.ListUnitAndGrass, _sizeOfCell * _scaleMap);
            //отрисовка травы и юнитов
            DrawExistsObjects(_map.ListUnitAndGrass, _sizeOfCell * _scaleMap, _sizeOfCell * _scaleMap, 0);
            //отривоска ресурсов
            DrawExistsObjects(_map.GetListWithResouces(), _sizeOfCell * _scaleMap, _sizeOfCell * _scaleMap, 0);
            //отрисовка конструкций
            DrawExistsObjects(_map.GetListConstruction(),
                 _sizeOfCell * _scaleMap
                ,_sizeOfCell * _scaleMap * Construction.SIZE,
                (_sizeOfCell * _scaleMap * Construction.SIZE) / 3);

            pictureBox1.Refresh();

        }


        private void DrawExistsObjects(List<GameObject> currentList, int sizeCell, int size, int offset)
        {
            foreach (var obj in currentList)
            {
                _graphics.DrawImage(_imageGameObject.GetImage(obj.GetType().Name),
                  new RectangleF(
                              obj.CurrentCoordinate.X * sizeCell - offset
                             , obj.CurrentCoordinate.Y * sizeCell - offset
                             , size
                             , size)
                  );
            }
        }

        private void PaintOverPreviousPosition(List<GameObject> currentList, int size)
        {
            foreach (var obj in currentList)
            {
                _graphics.FillRectangle(_map.CellOnMap[obj.PreviousCoordinate.X, obj.PreviousCoordinate.Y].Colors[(int)_map.Season],
                    obj.PreviousCoordinate.X * size,
                     obj.PreviousCoordinate.Y * size, size, size);
            }
        }

        private void DrawDeadUnit()
        {
            foreach (var obj in _map.GetListDeadUnit())
            {
                _graphics.FillRectangle(_backgroundColor, obj.CurrentCoordinate.X * _sizeOfCell * _scaleMap
                    , obj.CurrentCoordinate.Y * _sizeOfCell * _scaleMap
                    , _sizeOfCell * _scaleMap
                    , _sizeOfCell * _scaleMap);
            }
        }
        #endregion

        #region Вывод информации о выбранном юните
        private void ShowSelectedUnit()
        {
            if (_unitToDisplay != null && _cheakDisplayUnit)
            {
                if (_unitToDisplay.IsExists)
                {
                    ShowUnitUnfo(_unitToDisplay.Number, _unitToDisplay.GetType().Name, _unitToDisplay.Gender,
                                               _unitToDisplay.CurrentCoordinate.X, _unitToDisplay.CurrentCoordinate.Y, _unitToDisplay.Satiety,
                                                _unitToDisplay.GetNutritionalValue(), _unitToDisplay.GetBirth(),
                                                _unitToDisplay.MyHouse == null ? "Нет" : "Есть",
                                                _unitToDisplay.IsHavePartner() == false ? "Нет" : "Есть");
                }
                else
                {
                    label4.Text = "Юнит умер";
                }
            }
            else
            {
                clearPanel();
            }
        }

        private void ShowUnitUnfo(int number, string type, GenderUnit.Gender gender, int x, int y,
            int satiety, int plusToHP, int KD, string house, string partner)
        {
            if (_unitToDisplay != null)
            {
                label4.Text = $"Юнит: {number} \n" +
                $"Тип: {type}\n" +
                $"Гендер: {gender}\n" +
                $"Координата Х: {x} \n" +
                $"Координата Y: {y}\n" +
                $"Сытость: {satiety} \n" +
                $"Еда добавляет: {plusToHP} \n" +
                $"Размножение КД: {KD} \n" +
                $"Дом: {house} \n" +
                $"Партнёр: {partner} ";
            }
        }

        private void clearPanel()
        {
            label4.Text = $"Юнит:  \n" +
               $"Тип: \n" +
               $"Гендер: \n" +
               $"Координата Х:  \n" +
               $"Координата Y: \n" +
               $"Сытость:  \n" +
               $"Еда добавляет:  \n" +
               $"Размножение КД:  \n" +
               $"Дом:  \n" +
               $"Партнёр:  ";
        }


        #endregion

        private void DrawNextUpdate()
        {
            //  graphics.Clear(_bcg);
            if (_gameEngine.isNowShiftWeather())
            {
                DrawBackground();
                _gameEngine.NextUpdateWorld();
            }
            else
            {
                _gameEngine.NextUpdateWorld();
            }
            if (_gameEngine.IsCanGrowGrass() == true || isFirstGrass == true)
            {
                _gameEngine.NextUpdateGrass();
                isFirstGrass = false;
            }
            DrawMap();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextUpdate();
            label2.Text = $"Season: {_gameEngine.Season} \n" +
                $"Period of life: {_gameEngine.PeriodOfLife}";
            ShowSelectedUnit();
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
            if (_isStart == true)
            {
                if (_gameEngine.CheakClickPause())
                {
                    timer1.Stop();
                    button1.Text = "CONTINUE";
                    buttonStop.Enabled = false;
                }
                else
                {
                    timer1.Start();
                    button1.Text = "PAUSE";
                    buttonStop.Enabled = true;
                }
            }
        }

        private void scalingFactor_ValueChanged(object sender, EventArgs e)
        {
            _scaleMap = (int)scalingFactor.Value;
            pictureBox1.Width = _mapSize * _scaleMap;
            pictureBox1.Height = _mapSize * _scaleMap;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);
            DrawBackground();
            if (_isStart == true)
            {
                DrawBackground();
                DrawNextUpdate();
            }

        }

        private void currentSpeedLife_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = _gameEngine.GetSpeedLife((int)currentSpeedLife.Value);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _unitToDisplay = _gameEngine.GetUnitForInfo(
                    e.X / (_scaleMap * _sizeOfCell)
                    , e.Y / (_scaleMap * _sizeOfCell)
                    );
                _cheakDisplayUnit = true;

                if (_unitToDisplay != null)
                {
                    ShowUnitUnfo(_unitToDisplay.Number, _unitToDisplay.GetType().Name, _unitToDisplay.Gender,
                           _unitToDisplay.CurrentCoordinate.X, _unitToDisplay.CurrentCoordinate.Y, _unitToDisplay.Satiety,
                            _unitToDisplay.GetNutritionalValue(), _unitToDisplay.GetBirth(),
                             _unitToDisplay.MyHouse == null ? "Нет" : "Есть",
                            _unitToDisplay.IsHavePartner() == false ? "Нет" : "Есть");
                }
                else
                {
                    clearPanel();
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                PlaingMusicPanel();
                if (_stateCliclLeftButtonMause == StateButton.showHouse)
                {
                    _gameEngine.UpdateInfoUsingTheMause(e.X / _sizeOfCell / _scaleMap / House.SIZE,
                        e.Y / _sizeOfCell / _scaleMap / House.SIZE, _stateCliclLeftButtonMause);
                }
                _gameEngine.UpdateInfoUsingTheMause(e.X / _sizeOfCell / _scaleMap
                    , e.Y / _sizeOfCell / _scaleMap
                    , _stateCliclLeftButtonMause);
                DrawBackground();
                DrawMap();

            }
        }

        private void PlaingMusicPanel()
        {
            switch (_stateCliclLeftButtonMause)
            {
                case StateButton.createUnit:
                    Effects.PlaySpeechLuntik();
                    break;
                case StateButton.delUnit:
                    Effects.PlaySoundDie();
                    break;
                case StateButton.createGrass:
                    Effects.PlaySoundPlant();
                    break;
                case StateButton.delGrass:
                    Effects.PlaySoundDeadPlant();
                    break;
                default:
                    Effects.PlaySoundVylet();
                    break;
                
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
                "Сытость: \n" +
                "Еда добавляет: \n" +
                "Размножение КД: \n";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = StateButton.createUnit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = StateButton.createGrass;
        }

        private void buttonDelUnit_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = StateButton.delUnit;
        }

        private void buttonDelGrass_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = StateButton.delGrass;
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (_isStart)
            {
                _graphics.Clear(_bcg);
               Effects.PlaySpeechTanos();
                _gameEngine.DeathOfHalf();
                DrawBackground();
                DrawMap();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _stateCliclLeftButtonMause = StateButton.showHouse;
        }
    }
}
