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
        public const int AMOUNT_GRASS_ON_MAP = 600;
        public const int AMOUNT_APPEARANCE_FOOD = 20;
        public const byte LENGTH_ROOT_SYSTEM_OF_GRASS = 7;
       // public const int MAX_AMOUnt_GRASS = 20000;
        private Point _grassCurrentPosition = new Point();
        private Random random = new Random();
        Map map;

        public Meal(Map map)
        {
            this.map = map;
            for (int i = 0; i <= AMOUNT_GRASS_ON_MAP; )
            {
                _grassCurrentPosition.X = random.Next(map.SizeMap);
                _grassCurrentPosition.Y = random.Next(map.SizeMap);
                if (map.MapGridWithInfotmation[_grassCurrentPosition.X, _grassCurrentPosition.Y] == (int)Map.StateOfPoint.EmptyPoint)
                {
                    map.AddGrassToMap(_grassCurrentPosition);
                    map.MapGridWithInfotmation[_grassCurrentPosition.X, _grassCurrentPosition.Y] = (int)Map.StateOfPoint.PointWithGrass;
                    i++;
                }
            }
        }
        public void AppereanceFood()
        {
            if (map.ListGrassOnMap.Count > 0)
            {
                growsOfTheGrass();
            }
            else
            {
                birthOfGrass();
            }
        }

        private void birthOfGrass()
        {
            for (int i = 0; i < AMOUNT_APPEARANCE_FOOD;)
            {
                _grassCurrentPosition.X = random.Next(map.SizeMap);
                _grassCurrentPosition.Y = random.Next(map.SizeMap);
                if (map.MapGridWithInfotmation[_grassCurrentPosition.X, _grassCurrentPosition.Y] == 0)
                {
                    map.AddGrassToMap(_grassCurrentPosition);
                    i++;
                }
            }
        }
        private void growsOfTheGrass()
        {
            Point newGrass = map.ListGrassOnMap[random.Next(map.ListGrassOnMap.Count)];
            Point randomOffsetGrass = new Point();

            for (int i = 0; i < AMOUNT_APPEARANCE_FOOD;)
            {
                randomOffsetGrass.X = random.Next(-LENGTH_ROOT_SYSTEM_OF_GRASS, LENGTH_ROOT_SYSTEM_OF_GRASS);
                randomOffsetGrass.Y = random.Next(-LENGTH_ROOT_SYSTEM_OF_GRASS, LENGTH_ROOT_SYSTEM_OF_GRASS);

                if (!map.checkGoingOutTheMap(newGrass.X, newGrass.Y, randomOffsetGrass.X, randomOffsetGrass.Y))
                {
                    newGrass.X += randomOffsetGrass.X;
                    newGrass.Y += randomOffsetGrass.Y;
                    map.AddGrassToMap(newGrass);
                    map.MapGridWithInfotmation[newGrass.X, newGrass.Y] = (int)Map.StateOfPoint.PointWithGrass;
                    newGrass = map.ListGrassOnMap[random.Next(map.ListGrassOnMap.Count)];
                    i++;
                }
            }
        }
       
    }
}
