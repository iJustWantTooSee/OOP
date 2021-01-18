using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public abstract class Human : Omnivorous<IEdibleForHuman>
    {
        protected GameObject _caughtFood = null;
        protected Point _buildingSite = new Point();
        protected int _timeBuildingHouse = 20;
        protected int _timeBuildingBarn = 60;
        protected bool _firstBuilding = true;
        protected bool _isTookFood = false;

        private Barn nearestBarn = null;
        public Barn barn { get; set; } = null;

        public Human(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender, House myHouse, bool isChild)
            : base(x, y, random, map, number, unitGender)
        {
            SetNutritionalValue(350);
            this.MyHouse = myHouse;
            this._isChild = isChild;
            if (isChild == true)
            {
                this.MyHouse.AddResident(this);
            }
        }

        protected override void Reproduction()
        {
            if (MyHouse != null)
            {
                if (IsWantToReproduce() == true && IsPartnerOnMap())
                {
                    if (_currentPartner != null && _currentPartner.IsExists)
                    {
                        MovedToCouple(MyHouse.CurrentCoordinate);
                    }
                    else
                    {
                        SearchPair();
                    }
                }
            }
            else
            {
                if (_currentPartner != null && ((Human)_currentPartner).MyHouse != null)
                {
                    MyHouse = ((Human)_currentPartner).MyHouse;
                    MyHouse.AddResident(this);
                }
            }
        }



        public void SearchPair()
        {
            if (_currentPartner == null)
            {
                _currentPartner = SearchPointTheNearestNeighbours(
                    Gender == GenderUnit.Gender.Man ? typeof(Woman) : typeof(Man));
                if (_currentPartner != null && ((Human)_currentPartner)._currentPartner == null)
                {
                    ((Human)_currentPartner)._currentPartner = this;
                    if (MyHouse != null)
                    {
                        ((Human)_currentPartner).MyHouse = MyHouse;
                        MyHouse.AddResident((Human)_currentPartner);
                    }
                }
            }
            else
            {
                if (!_currentPartner.IsExists)
                {
                    _currentPartner = null;
                    SearchPair();
                }
            }

        }

        protected override bool IsLonely(Unit<IEdibleForHuman> unit)
        {

            return unit.GetUnitPartner() == null ? true : false;
        }

        protected void MoveToMealInConstruction(Construction construction)
        {
            MovementToPurpose(construction.CurrentCoordinate);
            if (CurrentCoordinate == construction.CurrentCoordinate)
            {
                if (construction.RemoveFood())
                {
                    Satiety += _nutritionalValue;
                }
                else
                {
                    throw new Exception("Еды в доме не было");
                }

            }
        }

        protected override void MovedToCouple(Point currentPurpose)
        {
            if (CurrentCoordinate == currentPurpose
                && CurrentCoordinate == _currentPartner.CurrentCoordinate)
            {
                if (Gender == GenderUnit.Gender.Woman)
                {
                    _map.AddChildren(GetBaby());
                }
                _currentTimeToBirth = 0;
            }
            MovementToPurpose(currentPurpose);
        }

        protected void SearchFoodAroundConstruction()
        {
            if (MyHouse.IsFullWarehouse() == false && IsWantToReproduce() == false)
            {
                GetFoodAroundConstruction(MyHouse);
            }
            else
            {
                if (MyHouse.IsFullWarehouse() == true && IsWantToReproduce() == false
                    && barn != null && barn.IsFullWarehouse() == false)
                {
                    GetFoodAroundConstruction(barn);
                }
                else
                {
                    if (MyHouse.IsFullWarehouse() && IsWantToReproduce() == false)
                    {
                        UpgradeTerritory();
                    }
                    else
                    {
                        MovementToPurpose(MyHouse.CurrentCoordinate);
                    }
                }
            }
        }

        private void GetFoodAroundConstruction(Construction construction)
        {
            if (_isTookFood == false && IsFoodOnMap())
            {
                GetFood(construction.CurrentCoordinate);
            }
            else
            {
                BringFoodIntoTheConstruction(construction);
            }
        }


        protected abstract void UpgradeTerritory();

        protected override void UpdateHungryUnit()
        {
            if (MyHouse != null && MyHouse.GetListWithFood().Count > 0)
            {
                MoveToMealInConstruction(MyHouse);
            }
            else
            {
                if (_isChild == false)
                {
                    if (IsSearchNearestBarn() == true && nearestBarn.GetListWithFood().Any())
                    {
                        MoveToMealInConstruction(nearestBarn);
                    }
                    else
                    {
                        if (_isTookFood)
                        {
                            Satiety += GetNutritionalValue();
                            _caughtFood = null;
                            _isTookFood = false;
                        }
                        MovedToFood();
                    }
                }
            }

        }

        private bool IsSearchNearestBarn()
        {
            nearestBarn = (Barn)GetNearestConstruction(_map.GetListBarn(), CurrentCoordinate, GetRange());
            return nearestBarn == null ? false : true;
        }

        protected void BringFoodIntoTheConstruction(Construction construction)
        {
            if (CurrentCoordinate == construction.CurrentCoordinate)
            {
                construction.AddFood(_caughtFood);
                _isTookFood = false;
                _caughtFood = null;
            }
            MovementToPurpose(construction.CurrentCoordinate);
        }

        protected void GetFood(Point pointConstruction)
        {
            if (IsFoodDontEat(_caughtFood) == false || _caughtFood == null)
            {
                SearchCaughtFood(pointConstruction);
            }
            else
            {
                if (_caughtFood.CurrentCoordinate == CurrentCoordinate)
                {
                    _caughtFood.IsEaten = true;
                    _map.RemoveEatenFood(_caughtFood);
                    _isTookFood = true;
                }
                MovementToPurpose(_caughtFood.CurrentCoordinate);
            }
        }

        protected abstract void SearchCaughtFood(Point searchCenter);

        protected override bool IsSeesTheFood(double distance)
        {
            if (distance <= GetRange() * 2.5)
            {
                return true;
            }
            return false;
        }

        protected override bool IsFoodOnMap()
        {
            if (_map.ListUnitAndGrass.Count > 0)
                return true;
            else
                return false;
        }

        protected override void Die()
        {
            IsExists = false;
            if (MyHouse!=null )
            {
                if (MyHouse.RemoveResident(this))
                {
                    MyHouse = null;
                }
                else
                {
                    throw new Exception($"{GetType().Name} не жила в доме, который хранился в её поле");
                }
            }
           
        }

        protected Construction GetNearestConstruction(List<GameObject> constructionOnMap, Point searchCenter, int rangeOfSearch)
        {
            GameObject nearestHouse = constructionOnMap
                   .Where(house => calculationDistance(searchCenter, house.CurrentCoordinate) < rangeOfSearch)
                   .MinBy(house => calculationDistance(searchCenter, house.CurrentCoordinate))
                   .FirstOrDefault();
            return (Construction)nearestHouse;
        }

    }
}
