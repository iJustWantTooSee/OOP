using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace newLive
{
    public abstract class Unit<TFood> : UnitWithoutGeneric
        where TFood : IEdible
    {
        protected int _defaultNatritionalValue;
        protected int _defaultTimeToBirth;
        protected int _defaultVergeStarvation;
        private bool _isCanGo = true;

        public Unit(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
            : base(x, y, random, map, number, unitGender)
        {


            if (Gender == GenderUnit.Gender.Super_Creature)
                setIncreaseSuperCreature();

        }
        public override void GetNewUnitPosition()
        {
            PreviousCoordinate = CurrentCoordinate;
            if (!IsWentIntoHibernation())
            {
                if (_isCanGo)
                {
                    if (Satiety > _vergeStarvation)
                    {
                        if (!_isChild)
                        {
                            UpdaeteSatietyUnit();
                        }
                        else
                        {
                            GrowingUp();
                        }
                    }
                    else if (Satiety > 0)
                    {
                        UpdateHungryUnit();
                    }
                    else
                    {
                        Die();
                    }
                    if (!_map.IsMayAppear(CurrentCoordinate.X, CurrentCoordinate.Y))
                        _isCanGo = false;
                    ChangeParametrs();
                }
                else
                {
                    _isCanGo = true;
                }
                
            }

        }



        protected override void GrowingUp()
        {
            _currentTimeGrowingUp++;
            if (_currentTimeGrowingUp >= _timeGrowingUp)
            {
                _isChild = false;
                if (MyHouse.RemoveResident(this))
                {
                    MyHouse = null;
                }
                else
                {
                    throw new Exception($"{GetType().Name} не живёт в доме");
                }
            }
        }

        protected override void UpdaeteSatietyUnit()
        {
            Reproduction();
            MovementWhenSatiety();

        }

        protected virtual void Reproduction()
        {
            if (IsWantToReproduce() != true || !IsPartnerOnMap())
            {
                return;
            }
            if (_currentPartner == null)
            {
                _currentPartner = SearchPointTheNearestNeighbours(this.GetType());
                MovementWhenSatiety();
            }
            else
            {
                if (_currentPartner.IsExists)
                {
                    MovedToCouple(_currentPartner.CurrentCoordinate);
                }
                else
                {
                    _currentPartner = SearchPointTheNearestNeighbours(this.GetType());
                }
            }
        }



        protected override void MovedToFood()
        {
            if (IsFoodOnMap())
            {
                if (_currentFood == null || !IsFoodDontEat(_currentFood))
                {
                    _currentFood = SearchPointTheNearestFood<TFood>(CurrentCoordinate);
                    if (_currentFood != null)
                    {
                        movedToMeal(_currentFood.CurrentCoordinate);
                    }
                    else
                    {
                        MovementWhenSatiety();
                    }

                }
                else
                {
                    if (_currentFood != null)
                    {
                        movedToMeal(_currentFood.CurrentCoordinate);
                    }
                    else
                    {
                        MovementWhenSatiety();
                    }
                }
            }
            else
            {
                MovementWhenSatiety();
            }
        }

        protected virtual void MovementWhenSatiety()
        {
            Point offset = new Point();

            for (int i = 0; i < 1;)
            {
                offset.X = _random.Next(-1, 2);
                offset.Y = _random.Next(-1, 2);
                if (!_map.IsGoingOutTheMap(CurrentCoordinate.X, CurrentCoordinate.Y,
                    offset.X, offset.Y))
                {
                    CurrentCoordinate = new Point(CurrentCoordinate.X + offset.X, CurrentCoordinate.Y + offset.Y);
                    i++;
                }
            }

        }

        protected virtual void MovedToCouple(Point currentPurpose)
        {
            if (CurrentCoordinate.X == currentPurpose.X && CurrentCoordinate.Y == currentPurpose.Y)
            {
                if (Gender == GenderUnit.Gender.Woman || Gender == GenderUnit.Gender.Super_Creature)
                {
                    _map.AddChildren(GetBaby());
                }
                _currentTimeToBirth = 0;
                _currentPartner = null;
            }
            MovementToPurpose(currentPurpose);
        }

        protected void movedToMeal(Point currentPurpose)
        {
            if (CurrentCoordinate.X == currentPurpose.X && CurrentCoordinate.Y == currentPurpose.Y)
            {
                _currentFood.IsEaten = true;
                _currentFood.IsExists = false;
                _map.RemoveEatenFood(_currentFood);
                Satiety += _nutritionalValue;
                _currentFood = null;
            }
            MovementToPurpose(currentPurpose);
        }

        protected override void MovementToPurpose(Point currentPurpose)
        {
            if (CurrentCoordinate.X > currentPurpose.X)
            {
                CurrentCoordinate = new Point(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
            }
            else if (CurrentCoordinate.X < currentPurpose.X)
            {
                CurrentCoordinate = new Point(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
            }

            if (CurrentCoordinate.Y > currentPurpose.Y)
            {
                CurrentCoordinate = new Point(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
            }
            else if (CurrentCoordinate.Y < currentPurpose.Y)
            {
                CurrentCoordinate = new Point(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
            }
        }




        protected GameObject SearchPointTheNearestNeighbours(Type type)
        {
            IEnumerable<Unit<TFood>> listHeighbours = _map.GetListWhithPossiblePartners<TFood>(type);
            var tempUnit = listHeighbours
                .Where(g => this.Gender != g.Gender && IsLonely(g) == true)
                .MinBy(unit => calculationDistance(this.CurrentCoordinate, unit.CurrentCoordinate))
                .FirstOrDefault();
            return tempUnit;
        }

        protected virtual bool IsLonely(Unit<TFood> unit)
        {
            return true;
        }

        protected GameObject SearchPointTheNearestFood<T>(Point coordinate) where T : IEdible
        {
            var purpose = _map.GetFood<T>()
                .Where(obj=>IsSeesTheFood(calculationDistance(coordinate, obj.CurrentCoordinate)))
                .MinBy(f => calculationDistance(coordinate, f.CurrentCoordinate))
                .FirstOrDefault();
            return purpose;
        }

        protected virtual GenderUnit.Gender ChooseGenderChildren()
        {
            switch (_random.Next(0, 3))
            {
                case 0:
                    return GenderUnit.Gender.Man;
                case 1:
                    return GenderUnit.Gender.Woman;
                case 2:
                    return GenderUnit.Gender.Super_Creature;
                default:
                    return GenderUnit.Gender.Man;
            }
        }

        protected bool IsFoodDontEat(GameObject purpuse)
        {
            GameObject tempFood = null;
            if (purpuse != null)
            {
                tempFood = _map.ListUnitAndGrass.Find(obj => obj.CurrentCoordinate == purpuse.CurrentCoordinate);
            }
            if (tempFood == null)
                return false;
            else
                return true;
        }
        protected abstract bool IsFoodOnMap();

        protected bool IsPartnerOnMap()
        {
            if (_map.ListUnitAndGrass.Count > 1)
                return true;
            else
                return false;

        }

        protected virtual bool IsSeesTheFood(double value)
        {
            return true;
        }

        protected double calculationDistance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2));
        }


        private void ChangeForTheWeather()
        {
            if (typeof(Man) != this.GetType() && typeof(Woman) != this.GetType())
            {
                switch (_map.Season)
                {
                    case season.summer:
                        _nutritionalValue = _defaultNatritionalValue;
                        break;
                    case season.autumn:
                        _nutritionalValue = _random.Next(110, 130);
                        _timeToBirth += _random.Next(100, 200);
                        break;
                    case season.winter:
                        _nutritionalValue = _random.Next(150, 200);
                        break;
                    case season.spring:
                        _nutritionalValue = _random.Next(100, 150);
                        _timeToBirth -= _random.Next(100, 200);
                        break;
                    default:
                        break;
                }

            }

        }

        protected void ChangeParametrs()
        {
            Satiety -= WASTE_SATIETY_PER_TURN;
            _currentTimeToBirth += WASTE_SATIETY_PER_TURN;
        }

        private void setIncreaseSuperCreature()
        {
            _vergeStarvation += 150;
            _nutritionalValue += 200;
            _timeToBirth = 450;
        }

        public override void SetNewCharacter()
        {
            ChangeForTheWeather();
        }

        public override int GetNutritionalValue()
        {
            return _nutritionalValue;
        }

        public void SetNutritionalValue(int value)
        {
            if (value >= 100 && value < 500)
            {
                _nutritionalValue = value;
            }
        }

        public void SetVergeStarvation(int value)
        {
            if (value >= 150 && value < 250)
            {
                _nutritionalValue = value;
            }
        }

        public override int GetBirth()
        {
            return _timeToBirth;
        }

        protected void SetRange(int range)
        {
            _range = range;
        }
        protected int GetRange()
        {
            return _range;
        }

        protected Random GetRandom()
        {
            return _random;
        }

        public override bool IsHavePartner()
        {
            return _currentPartner == null ? false : true;
        }

        public GameObject GetUnitPartner()
        {
            return _currentPartner;
        }

        public bool IsWantToReproduce()
        {
            return _currentTimeToBirth >= _timeToBirth;
        }

        protected virtual bool IsWentIntoHibernation()
        {
            return false;
        }

        public void InstallDefaultValues()
        {
            _defaultNatritionalValue = _nutritionalValue;
            _defaultTimeToBirth = _timeToBirth;
            _defaultVergeStarvation = _vergeStarvation;
        }
    }
}
