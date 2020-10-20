using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Map
    {
        public  int SizeMap = 1000;
        public List<Unit> CurrentGenerationOfUnits = new List<Unit>();
        public List<Unit> CurrentGenerationOfChildUnits = new List<Unit>();
        public int currentValuePopulation { get; set; } = 0;
 
        public List<Point> ListGrassOnMap = new List<Point>();
        public int[,] MapGridWithInfotmation;
        
        //public Type [,]MapGridWithInfotmation;  Хранить не значения, которые лежат в клетке, а сразу класс, который принадлежит клетке

        public Map()
        {
            MapGridWithInfotmation = new int[SizeMap, SizeMap];
            for (int i = 0; i < SizeMap; i++)
            {
                for (int j = 0; j < SizeMap; j++)
                {
                    MapGridWithInfotmation[i, j] = (int)StateOfPoint.EmptyPoint; 
                }
            }
            
        }

        public enum StateOfPoint
        {
            EmptyPoint = 0,
            PointWithUnit = 1,
            PointWithGrass = 2
        }
        public void AddUnitToMap(Unit unit)
        {
            CurrentGenerationOfChildUnits.Add(unit);
           // MapGridWithInfotmation[unit.CurrentUnitCoordinate.X, unit.CurrentUnitCoordinate.Y] = (int)StateOfPoint.PointWithUnit;
        }

        public void AddGrassToMap(Point pointGrassOnMap)
        {
            ListGrassOnMap.Add(pointGrassOnMap);
            MapGridWithInfotmation[pointGrassOnMap.X, pointGrassOnMap.Y] = (int)StateOfPoint.PointWithGrass;
        }
        
        public void UpdateUnitPosition(Point oldUnitPosition, Point unitPosition)
        {
            MapGridWithInfotmation[oldUnitPosition.X, oldUnitPosition.Y] = (int)StateOfPoint.EmptyPoint;
           // MapGridWithInfotmation[unitPosition.X, unitPosition.Y] = (int)StateOfPoint.PointWithUnit;
        }
        public void UppdateInformationGrass(Point point, StateOfPoint state)
        {          
            ListGrassOnMap.Remove(point);
            MapGridWithInfotmation[point.X, point.Y] = (int)state;
        }

      
        public bool checkGoingOutTheMap(int currentX, int currentY, int offsetX, int offsetY)
        {
            if (currentX + offsetX > SizeMap - 1 || currentX + offsetX < 1 ||
                currentY + offsetY > SizeMap - 1 || currentY + offsetY < 1)
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
