using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Man : Human
    {
        private Barn _nearestBarn = null;
        private House _nearestHouse = null;

        private int _currentTimeToBuilding = 0;
        
        public Man(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender, House myHouse, bool isChild)
            : base(x, y, random, map, number, unitGender, myHouse, isChild)
        {

        }

        protected override void MovementWhenSatiety()
        {
            if (MyHouse == null)
            {
                if (IsWantToReproduce() == true)
                {
                    BuildingHouse();
                }
            }
            else
            {
                if (barn == null && MyHouse != null && IsWantToReproduce() == false)
                {
                    BuildingBarn();
                }
                else
                {
                    SearchFoodAroundConstruction();
                }
            }
        }

        private void BuildingHouse()
        {
            if (_nearestHouse == null)
            {
                _nearestHouse = (House)GetNearestConstruction(_map.GetListHouses(), CurrentCoordinate, GetRange());
                if (_nearestHouse == null)
                {
                    _buildingSite = _map.GetRandomPointWithOffset(GetRange(), CurrentCoordinate, GetRandom());
                    _nearestHouse = new House(_buildingSite.X, _buildingSite.Y, _map);
                    BuildingConstruction(_nearestHouse, _timeBuildingHouse, true);
                }
                else
                {
                    _buildingSite = _map.GetRandomPointWithOffset(GetRange(), _nearestHouse.CurrentCoordinate, GetRandom());
                    _nearestHouse = new House(_buildingSite.X, _buildingSite.Y, _map);
                    BuildingConstruction(_nearestHouse, _timeBuildingHouse, true);
                }
                _map.AddHouse(_nearestHouse);
            }
            else
            {
                BuildingConstruction(_nearestHouse, _timeBuildingHouse, true);
            }
        }

        private void BuildingBarn()
        {
            if (_nearestBarn == null)
            {
                _nearestBarn = (Barn)GetNearestConstruction(_map.GetListBarn(), MyHouse.CurrentCoordinate, GetRange() * 2);
                if (_nearestBarn == null)
                {
                    _buildingSite = _map.GetRandomPointWithOffset(GetRange(), MyHouse.CurrentCoordinate, GetRandom());
                    _nearestBarn = new Barn(_buildingSite.X, _buildingSite.Y, _map);
                    BuildingConstruction(_nearestBarn, _timeBuildingHouse, false);
                    _map.AddBarn(_nearestBarn);
                }
                else
                {
                    InitalizingBarn(_nearestBarn);
                }
            }
            else
            {
                BuildingConstruction(_nearestBarn, _timeBuildingBarn, false);
            }
        }


        private void BuildingConstruction(Construction construction, int timeToBuilding, bool isNowBuildingHouse)
        {
            if (CurrentCoordinate == construction.CurrentCoordinate)
            {
                if (_currentTimeToBuilding < timeToBuilding)
                    _currentTimeToBuilding++;
                else
                {
                    _currentTimeToBuilding = 0;
                    if (isNowBuildingHouse == true)
                    {
                        InitalizingHouse((House)construction);
                    }
                    else
                    {
                        InitalizingBarn((Barn)construction);
                    }
                    _nearestHouse = null;
                }
            }
            MovementToPurpose(construction.CurrentCoordinate);
        }

        

        private void InitalizingHouse(House construction)
        {

            MyHouse = construction;
            MyHouse.AddResident(this);
            MyHouse.IsBuilt = true;
            if (_currentPartner != null)
            {
                ((Human)_currentPartner).MyHouse = construction;
                MyHouse.AddResident((Human)_currentPartner);
            }
        }

        private void InitalizingBarn(Barn construction)
        {
            barn = construction;
            barn.IsBuilt = true;
            if (_currentPartner != null)
            {
                ((Human)_currentPartner).barn = construction;
            }
        }

        protected override void SearchCaughtFood(Point searchCenter)
        {
            _caughtFood = SearchPointTheNearestFood<IExtractionForMan>(searchCenter);
        }

        protected override void UpgradeTerritory()
        {
            //рубит деревья и улучшает дом
            MovementToPurpose(MyHouse.CurrentCoordinate);
        }

        protected override UnitWithoutGeneric GetBaby()
        {
            return new Man(CurrentCoordinate.X, CurrentCoordinate.Y,
                GetRandom(), _map, ++_map.CurrentValuePopulation, ChooseGenderChildren(), MyHouse, true);
        }
    }

}
