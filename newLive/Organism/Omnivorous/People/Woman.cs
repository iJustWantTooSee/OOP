using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Woman : Human
    {
        private int _timeForGrewPlant = 15;
        private bool _isPlanted = false;
        private Point _placeOfCulculation;
        public Woman(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender, House myHouse, bool isChild)
            : base(x, y, random, map, number, unitGender, myHouse, isChild)
        {

        }

        protected override void MovementWhenSatiety()
        {
            if (MyHouse != null)
            {
                if (SearchPointTheNearestFood<IExtractionForWoman>(MyHouse.CurrentCoordinate) != null)
                    SearchFoodAroundConstruction();
                else
                {
                    UpgradeTerritory();
                }
            }

        }

        protected override void UpgradeTerritory()
        {
            if (_isPlanted)
            {
                if (_timeForGrewPlant > 0)
                    _timeForGrewPlant--;
                else
                {
                    _map.MealOnMap.CreateGrass(CurrentCoordinate);
                    _timeForGrewPlant = 15;
                    _isPlanted = false;
                }
            }
            else
            {
                if (_placeOfCulculation == new Point(0, 0))
                {
                    _placeOfCulculation = _map.GetRandomPointWithOffset(GetRange() / 2, MyHouse.CurrentCoordinate, GetRandom());
                }
                else
                {
                    MovementToPurpose(_placeOfCulculation);
                    if (CurrentCoordinate == _placeOfCulculation)
                    {
                        _isPlanted = true;
                    }
                }
            }

        }

        protected override void SearchCaughtFood(Point searchCenter)
        {
            _caughtFood = SearchPointTheNearestFood<IExtractionForWoman>(searchCenter);
        }


        protected override UnitWithoutGeneric GetBaby()
        {
            switch ((GenderUnit.Gender)GetRandom().Next(0, 2))
            {
                case GenderUnit.Gender.Man:
                    return new Man(CurrentCoordinate.X, CurrentCoordinate.Y,
                GetRandom(), _map, ++_map.CurrentValuePopulation, GenderUnit.Gender.Man, MyHouse, true);

                case GenderUnit.Gender.Woman:
                    return new Woman(CurrentCoordinate.X, CurrentCoordinate.Y,
                GetRandom(), _map, ++_map.CurrentValuePopulation, GenderUnit.Gender.Woman, MyHouse, true);
                default:
                    break;
            }
            return new Woman(CurrentCoordinate.X, CurrentCoordinate.Y,
                GetRandom(), _map, ++_map.CurrentValuePopulation, GenderUnit.Gender.Woman, MyHouse, true);
        }
    }
}
