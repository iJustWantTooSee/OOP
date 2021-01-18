using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Construction : GameObject
    {
        private int _maxCapacity = 10;
        public int MaxCapacity
        {
            get
            {
                return _maxCapacity;
            }
            set
            {
                if (value>=1 && value < 25)
                {
                    _maxCapacity = value;
                }
            }
        }
        public const int SIZE = 3;
        public bool IsBuilt { get; set; } = false;

        private List<GameObject> _listWithFood = new List<GameObject>();
        
        public Construction(int x, int y, Map map)
           : base(x, y, map)
        {

        }

        public bool IsFullWarehouse()
        {
            // return _listWithFood.Count() >= _maxCapacity;
            if (_listWithFood.Count() < _maxCapacity)
                return false;
            else
                return true;
        }

        public List<GameObject> GetListWithFood()
        {
            return _listWithFood;
        }

        public bool RemoveFood()
        {
            if (!_listWithFood.Any())
            {
                return false;
            }
            _listWithFood.First().IsExists = false;
            _listWithFood.Remove(_listWithFood.First());
            return true;
        }

        public void AddFood(GameObject food)
        {
            if (IsFullWarehouse())
            {
                return;
            }
            _listWithFood.Add(food);
            // true/false от того произошло событие или нет
        }
    }
}
