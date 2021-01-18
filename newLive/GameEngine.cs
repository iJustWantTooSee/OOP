using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenderUnit;
namespace newLive
{
    public class GameEngine
    {

        private int _rows = 1000;
        public int Rows { get { return _rows; } }
        private int _cols = 1000;
        public int Cols { get { return _cols; } }
        public int SpeedLife { get; private set; } = 500;
        public const int INCREASED_SPEED_LIFE = 20;
        public long PeriodOfLife { get; private set; } = 0;
        #region количество объектов каждого типа
        public const int START_BEAR_NUMBER = 60;
        public const int START_FOX_NUMBER = 60;
        public const int START_LION_NUMBER = 60;
        public const int START_MOOSE_NUMBER = 200;
        public const int START_MOUSE_NUMBER = 200;
        public const int START_PIG_NUMBER = 50;
        public const int START_RABBIT_NUMBER = 200;
        public const int START_RACCOON_NUMBER = 60;
        public const int START_WOLF_NUMBER = 60;
        public const int START_MAN_NUMBER = 60;
        public const int START_WOMAN_NUMBER = 60;
        #endregion
        public const int ALL_UNITS = START_BEAR_NUMBER + START_FOX_NUMBER + START_LION_NUMBER
            + START_MAN_NUMBER + START_MOOSE_NUMBER + START_MOUSE_NUMBER + START_PIG_NUMBER + START_RABBIT_NUMBER
            + START_RACCOON_NUMBER + START_WOLF_NUMBER + START_WOMAN_NUMBER;
        public int NameOfUnit { get; private set; } = 0;
        public int PeriodGrowGrass { get; private set; } = 40;
        public season Season { get; private set; } = season.summer;

        private int _x;
        private int _y;
        private bool isClickPause = false;

        public Map map { get; private set; }
        private Meal GrassOnMap { get; set; }

        private Random random = new Random();
        public GameEngine(Map map)
        {
            NameOfUnit = 0;
            this.map = map;
            this.map.Season = Season;
            _cols = map.Size;
            _rows = map.Size;
            #region Первоначальное создание всех видов юнитов
            for (int i = 0; i < START_BEAR_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Bear(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }

                }
            }

            for (int i = 0; i < START_FOX_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Fox(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_LION_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Lion(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_MOOSE_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Moose(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_MOUSE_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Mouse(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_PIG_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Pig(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_RABBIT_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Rabbit(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_RACCOON_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Raccoon(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_WOLF_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Wolf(_x, _y, random, map, NameOfUnit++, SetUnitGender()));
                        break;
                    }
                }
            }

            for (int i = 0; i < START_MAN_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Man(_x, _y, random, map, NameOfUnit++, Gender.Man, null, false));
                        break;
                    }
                }
            }
            for (int i = 0; i < START_WOMAN_NUMBER; i++)
            {
                while (true)
                {
                    _x = random.Next(Cols);
                    _y = random.Next(Rows);
                    if (map.IsMayAppear(_x, _y))
                    {
                        map.ListUnitAndGrass.Add(new Woman(_x, _y, random, map, NameOfUnit++, Gender.Woman, null, false));
                        break;
                    }
                }
            }
            #endregion
            GrassOnMap = new Meal(map);
            map.SetMealOnMap(GrassOnMap);
            map.CurrentValuePopulation = ALL_UNITS;
            CreatingPairsBetweenPeople();
        }

        private void CreatingPairsBetweenPeople()
        {
            ICollection<Human> currentList = map.ListUnitAndGrass.OfType<Human>().ToList();
            foreach (var obj in currentList)
            {
                obj.SearchPair();
            }
        }

        public void NextUpdateWorld()
        {
            PeriodOfLife++;
            UpdateUnit();
            if (isNowShiftWeather() == true)
            {
                SetSeason();
                map.Season = Season;
                changeCharacterUnit();
            }
        }

        public void changeCharacterUnit()
        {
            List<UnitWithoutGeneric> listUnits = map.ListUnitAndGrass.OfType<UnitWithoutGeneric>().ToList();
            foreach (UnitWithoutGeneric unit in listUnits)
            {
                unit.SetNewCharacter();
            }
        }
        public bool isNowShiftWeather()
        {
            if (PeriodOfLife % 300 == 0 && PeriodOfLife != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SetSeason()
        {
            switch (Season)
            {
                case season.summer:
                    Season = season.autumn;
                    break;

                case season.autumn:
                    Season = season.winter;
                    PeriodGrowGrass += random.Next(20, 40);
                    GrassOnMap.AmountAppearanceFood = 100;
                    break;

                case season.winter:
                    Season = season.spring;
                    GrassOnMap.AmountAppearanceFood += 20;
                    PeriodGrowGrass -= random.Next(0, 20);
                    break;

                case season.spring:
                    Season = season.summer;
                    PeriodGrowGrass = 20;
                    GrassOnMap.AmountAppearanceFood += random.Next(20, 50);
                    break;
                default:
                    break;
            }

        }

        private void UpdateUnit()
        {
            List<UnitWithoutGeneric> currentList = map.ListUnitAndGrass.OfType<UnitWithoutGeneric>().ToList();
            for (int i = currentList.Count() - 1; i >= 0; i--)
            {

                currentList[i].GetNewUnitPosition();
                if (i < currentList.Count() - 1 && currentList[i].IsExists == false && currentList[i].IsEaten == false)
                {
                    map.ListDeadUnit.Add(currentList[i]);
                    if (map.IsMayAppear(currentList[i].CurrentCoordinate.X, currentList[i].CurrentCoordinate.Y))
                    {
                        GrassOnMap.CreateGrass(currentList[i].CurrentCoordinate);
                    }
                    map.ListUnitAndGrass.Remove(currentList[i]);
                }
            }

        }

        public void NextUpdateGrass()
        {
            GrassOnMap.AppereanceFood();
        }

        public bool CheakClickPause()
        {
            if (!isClickPause)
            {
                isClickPause = true;
                return isClickPause;
            }
            else
            {
                isClickPause = false;
                return isClickPause;
            }
        }
        public int GetSpeedLife(int currentSpeedLife)
        {
            if (currentSpeedLife > 10)
            {
                return SpeedLife + (-INCREASED_SPEED_LIFE * currentSpeedLife);
            }
            else if (currentSpeedLife < 10)
            {
                return SpeedLife + (INCREASED_SPEED_LIFE * currentSpeedLife);
            }
            else
            {
                return SpeedLife;
            }
        }

        public UnitWithoutGeneric GetUnitForInfo(int x, int y)
        {
            UnitWithoutGeneric foundUnit = null;
            foreach (var item in map.ListUnitAndGrass)
            {
                if (item.CurrentCoordinate.X == x && item.CurrentCoordinate.Y == y && (item is UnitWithoutGeneric))
                {
                    foundUnit = (UnitWithoutGeneric)item;
                    break;
                }
            }
            return foundUnit;
        }

        public void UpdateInfoUsingTheMause(int x, int y, StateButton state)
        {
            Point currentPoint = new Point();
            currentPoint.X = x;
            currentPoint.Y = y;
            switch (state)
            {
                case StateButton.createUnit:
                    CreateUnit(currentPoint);
                    break;
                case StateButton.delUnit:
                    UnitToDelet(currentPoint);
                    break;
                case StateButton.createGrass:
                    if (!GrassOnMap.CreateGrass(currentPoint))
                    {
                        MessageBox.Show("Сюда нельзя добавлять траву");
                    };
                    break;
                case StateButton.delGrass:
                    RemoveGrass(currentPoint);
                    break;
                case StateButton.showHouse:
                    ShowHouse(currentPoint);
                    break;
                default:
                    break;
            }

        }

        private void RemoveGrass(Point point)
        {
            Grass grass = (Grass)map.ListUnitAndGrass
                .Find(g => g.CurrentCoordinate.X == point.X && g.CurrentCoordinate.Y == point.Y
                && !(g is UnitWithoutGeneric));
            map.ListUnitAndGrass.Remove(grass);
        }


        private void CreateUnit(Point point)
        {
            int value = random.Next(1, 4);
            switch (value)
            {
                case 1:
                    AddHerbivorous(point);
                    break;
                case 2:
                    AddPredatory(point);
                    break;
                case 3:
                    AddOmnivorous(point);
                    break;
                default:
                    break;
            }
        }
        private void AddHerbivorous(Point point)
        {
            var choice = random.Next(1, 4);
            switch (choice)
            {
                case 1:
                    map.ListUnitAndGrass.Add(new Mouse(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 2:
                    map.ListUnitAndGrass.Add(new Rabbit(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 3:
                    map.ListUnitAndGrass.Add(new Moose(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                default:
                    break;
            }
        }
        private void AddPredatory(Point point)
        {
            var choice = random.Next(1, 4);
            switch (choice)
            {
                case 1:
                    map.ListUnitAndGrass.Add(new Lion(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 2:
                    map.ListUnitAndGrass.Add(new Wolf(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 3:
                    map.ListUnitAndGrass.Add(new Fox(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                default:
                    break;
            }
        }
        private void AddOmnivorous(Point point)
        {
            var choice = random.Next(1, 4);
            switch (choice)
            {
                case 1:
                    map.ListUnitAndGrass.Add(new Bear(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 2:
                    map.ListUnitAndGrass.Add(new Pig(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                case 3:
                    map.ListUnitAndGrass.Add(new Raccoon(point.X, point.Y, random, map, ++map.CurrentValuePopulation, SetUnitGender()));
                    break;
                default:
                    break;
            }
        }
        public Gender SetUnitGender()
        {
            int value = random.Next(1, 4);
            Gender gender = Gender.Man;
            switch (value)
            {
                case 1:
                    gender = Gender.Man;
                    break;
                case 2:
                    gender = Gender.Woman;
                    break;
                case 3:
                    gender = Gender.Super_Creature;
                    break;
                default:
                    gender = Gender.Super_Creature;
                    break;
            }
            return gender;
        }

        public void UnitToDelet(Point unitToDel)
        {
            GameObject delUnit = null;
            foreach (var unit in map.ListUnitAndGrass)
            {
                if (unit.CurrentCoordinate.X == unitToDel.X && unit.CurrentCoordinate.Y == unitToDel.Y && unit is UnitWithoutGeneric)
                {
                    unit.IsExists = false;
                    delUnit = unit;
                }
            }
            if (delUnit != null)
            {
                GrassOnMap.CreateGrass(delUnit.CurrentCoordinate);
                map.ListUnitAndGrass.Remove(delUnit);
            }
        }

        public void DeathOfHalf()
        {

            int half = (map.ListUnitAndGrass.OfType<UnitWithoutGeneric>().ToList()).Count / 2;
            GameObject delUnit = null;
            while (half > 0)
            {
                delUnit = map.ListUnitAndGrass
                    .OfType<UnitWithoutGeneric>()
                    .ToList()[random.Next(map.ListUnitAndGrass.OfType<UnitWithoutGeneric>().ToList().Count)];
                delUnit.IsExists = false;
                map.ListUnitAndGrass.Remove(delUnit);
                half--;
            }

        }

        private void ShowHouse(Point currentPoint)
        {
            GameObject constructionForDisplay = map.GetHouseByCoordinate(currentPoint);
            if (constructionForDisplay != null)
            {
                string text = "";

                if (constructionForDisplay is House)
                {
                    text = GetInformationAboutHouse(text, (House)constructionForDisplay);
                }
                else
                {
                    text = GetInformationAboutBarn(text, (Barn)constructionForDisplay);
                }

                MessageBox.Show(text);
            }
        }

        private string GetInformationAboutHouse(string text, House houseForDisplay)
        {
            string tempText = "";
            text = "Информация о доме: \n"
            + $"Уровень дома: {houseForDisplay.GetLVLHouse()}\n"
            + $"X: {houseForDisplay.CurrentCoordinate.X}\n" +
            $"Y: {houseForDisplay.CurrentCoordinate.Y}\n";

            int currentShelf = 1;
            foreach (var food in houseForDisplay.GetListWithFood())
            {
                tempText += $"{currentShelf++} полка содержит: {food.GetType().Name} \n";
            }

            if (tempText != "")
            {
                text += tempText + "\n";
            }
            else
            {
                text += "Еды в доме пока нет :( \n";
            }
            tempText = "";
            foreach (Human human in houseForDisplay.GetListWithResidentOfTheHouse())
            {
                tempText += $"Живет юнит: {human.Number} c гендером {human.GetType().Name} \n";
            }
            text += tempText;
            return text;
        }
        private string GetInformationAboutBarn(string text, Barn houseForDisplay)
        {
            text = "Информация об амбаре: \n"
           + $"X: {houseForDisplay.CurrentCoordinate.X}\n" +
           $"Y: {houseForDisplay.CurrentCoordinate.Y}\n"
           + $"Амбар содержит: {houseForDisplay.GetListWithFood().Count()} еды";

            return text;
        }
        public bool IsCanGrowGrass()
        {
            if (PeriodOfLife % PeriodGrowGrass == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
