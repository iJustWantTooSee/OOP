using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Meal
    {
        public const int AMOUNT_APPLE = 1000;
        public const int AMOUNT_AVENA = 1000;
        public const int AMOUNT_CARROT = 1000;

        public int AmountAppearanceFood = 100;
        public const byte LENGTH_ROOT_SYSTEM_OF_GRASS = 7;
        private Point _grassCurrentPosition = new Point();
        private Random random = new Random();
        private Map _map;

        public Meal(Map map)
        {
            this._map = map;
            for (int i = 0; i <= AMOUNT_APPLE;)
            {
                _grassCurrentPosition.X = random.Next(_map.Size);
                _grassCurrentPosition.Y = random.Next(_map.Size);
                if (_map.IsMayAppear(_grassCurrentPosition.X, _grassCurrentPosition.Y))
                {
                    Apple grass = new Apple(_grassCurrentPosition.X, _grassCurrentPosition.Y, map);
                    map.AddGrassToMap(grass);
                    i++;
                }
            }
            for (int i = 0; i <= AMOUNT_AVENA;)
            {
                _grassCurrentPosition.X = random.Next(_map.Size);
                _grassCurrentPosition.Y = random.Next(_map.Size);
                if (_map.IsMayAppear(_grassCurrentPosition.X, _grassCurrentPosition.Y))
                {
                    Avena grass = new Avena(_grassCurrentPosition.X, _grassCurrentPosition.Y, map);
                    map.AddGrassToMap(grass);
                    i++;
                }
            }
            for (int i = 0; i <= AMOUNT_CARROT;)
            {
                _grassCurrentPosition.X = random.Next(_map.Size);
                _grassCurrentPosition.Y = random.Next(_map.Size);
                if (_map.IsMayAppear(_grassCurrentPosition.X, _grassCurrentPosition.Y))
                {
                    Carrot grass = new Carrot(_grassCurrentPosition.X, _grassCurrentPosition.Y, map);
                    map.AddGrassToMap(grass);
                    i++;
                }
            }
        }
        public void AppereanceFood()
        {
            if (_map.ListUnitAndGrass.Count > 0)
            {
                GrowsOfTheGrass();
            }
            else
            {
                BirthOfGrass();
            }
        }

        public bool CreateGrass(Point creationPoint)
        {
            var choice = random.Next(1, 4);
            switch (choice)
            {
                case 1:
                    if (_map.IsMayAppear(creationPoint.X, creationPoint.Y))
                    {
                        Apple apple = new Apple(creationPoint.X, creationPoint.Y, _map);
                        _map.AddGrassToMap(apple);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (_map.IsMayAppear(creationPoint.X, creationPoint.Y))
                    {
                        Carrot carrot = new Carrot(creationPoint.X, creationPoint.Y, _map);
                        _map.AddGrassToMap(carrot);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (_map.IsMayAppear(creationPoint.X, creationPoint.Y))
                    {
                        Avena avena = new Avena(creationPoint.X, creationPoint.Y, _map);
                        _map.AddGrassToMap(avena);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        private void BirthOfGrass()
        {
            for (int i = 0; i < AmountAppearanceFood;)
            {
                _grassCurrentPosition.X = random.Next(_map.Size);
                _grassCurrentPosition.Y = random.Next(_map.Size);
                if (_map.IsMayAppear(_grassCurrentPosition.X, _grassCurrentPosition.Y))
                {

                    _map.AddGrassToMap(new Apple(_grassCurrentPosition.X, _grassCurrentPosition.Y, _map));
                    _map.AddGrassToMap(new Carrot(_grassCurrentPosition.X, _grassCurrentPosition.Y, _map));
                    _map.AddGrassToMap(new Avena(_grassCurrentPosition.X, _grassCurrentPosition.Y, _map));
                    i++;
                }
            }
        }
        private void GrowsOfTheGrass()
        {
            List<Grass> grass = _map.ListUnitAndGrass.OfType<Grass>().ToList();
            GameObject oldGrass = grass[random.Next(grass.Count)];
            Point randomOffsetGrass = new Point();

            for (int i = 0; i < AmountAppearanceFood;)
            {
                randomOffsetGrass.X = random.Next(-LENGTH_ROOT_SYSTEM_OF_GRASS, LENGTH_ROOT_SYSTEM_OF_GRASS);
                randomOffsetGrass.Y = random.Next(-LENGTH_ROOT_SYSTEM_OF_GRASS, LENGTH_ROOT_SYSTEM_OF_GRASS);

                if (!_map.IsGoingOutTheMap(oldGrass.CurrentCoordinate.X, oldGrass.CurrentCoordinate.Y,
                    randomOffsetGrass.X, randomOffsetGrass.Y))
                {
                    if (_map.IsMayAppear(oldGrass.CurrentCoordinate.X + randomOffsetGrass.X, oldGrass.CurrentCoordinate.Y + randomOffsetGrass.Y))
                    {
                        AddNewGrassAroundOldGrass(oldGrass, randomOffsetGrass);
                        oldGrass = grass[random.Next(grass.Count)];
                        i++;
                    }
                }
            }
        }

        private void AddNewGrassAroundOldGrass(GameObject oldGrass, Point randomOffsetGrass)
        {
            int randomValue = random.Next(3);
            switch (randomValue)
            {
                case 0:
                    Apple newApple = new Apple(oldGrass.CurrentCoordinate.X + randomOffsetGrass.X,
                        oldGrass.CurrentCoordinate.Y + randomOffsetGrass.Y, _map);
                    _map.AddGrassToMap(newApple);
                    break;
                case 1:
                    Carrot newCarrot = new Carrot(oldGrass.CurrentCoordinate.X + randomOffsetGrass.X,
                       oldGrass.CurrentCoordinate.Y + randomOffsetGrass.Y, _map);
                    _map.AddGrassToMap(newCarrot);
                    break;
                case 2:
                    Avena newAvena = new Avena(oldGrass.CurrentCoordinate.X + randomOffsetGrass.X,
                      oldGrass.CurrentCoordinate.Y + randomOffsetGrass.Y, _map);

                    _map.AddGrassToMap(newAvena);
                    break;
                default:
                    break;
            }
        }

    }
}
