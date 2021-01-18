using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public abstract class UnitWithoutGeneric : GameObject
    {
        public int Number { get; protected set; }
        public GenderUnit.Gender Gender { get; protected set; }
        public House MyHouse { get; set; } = null;
        public int Satiety { get; protected set; } = 300;

        protected GameObject _currentFood = null;
        protected GameObject _currentPartner = null;
        protected Random _random { get; set; } = new Random();
        protected int _vergeStarvation = 200;
        protected int _range = 30;
        protected int _currentTimeGrowingUp = 0;
        protected int _timeGrowingUp = 500;
        protected int _currentTimeToBirth = 0;
        protected int _timeToBirth = 300;
        protected const int WASTE_SATIETY_PER_TURN = 1;
        protected int _nutritionalValue = 200;
        protected bool _isChild = false;



        public UnitWithoutGeneric(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
            : base(x, y, map)
        {
            Number = number;
            Gender = unitGender;
            this._random = random;
        }

        public abstract void GetNewUnitPosition();
        public abstract int GetBirth();
        public abstract int GetNutritionalValue();
        public abstract bool IsHavePartner();
        public abstract void SetNewCharacter();

        protected abstract UnitWithoutGeneric GetBaby();
        protected abstract void GrowingUp();
        protected abstract void UpdaeteSatietyUnit();
        protected abstract void MovedToFood();
        protected abstract void MovementToPurpose(Point currentPurpose);
        protected virtual void UpdateHungryUnit()
        {
            MovedToFood();
        }

        protected virtual void Die()
        {
            IsExists = false;
        }
    }
}
