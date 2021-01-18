using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class House : Construction
    {
        public const int RESOURCES_FOR_NEXT_LVL = 10;

        protected List<UnitWithoutGeneric> _listResidentOfTheHouse = new List<UnitWithoutGeneric>();

        private int _lvlHouse = 1;
        private int _currentResourcesAmount = 0;

        public House(int x, int y, Map map)
            : base(x, y, map)
        {
            MaxCapacity = 5;
        }

        private void UpgradeHouse()
        {
            if (_currentResourcesAmount >= RESOURCES_FOR_NEXT_LVL)
            {
                GetNewLvl();
                _currentResourcesAmount -= RESOURCES_FOR_NEXT_LVL;
            }

        }

        public bool AddResources(GameObject resource)
        {
            if (resource != null)
            {
                _currentResourcesAmount++;
                UpgradeHouse();
                return true;
            }
            return false;
        }

        private void GetNewLvl()
        {
            _lvlHouse++;
            switch (_lvlHouse)
            {
                case 2:
                    MaxCapacity += 2;
                    break;
                case 3:
                    MaxCapacity += 2;
                    break;
                case 4:
                    MaxCapacity += 2;
                    break;
                case 5:
                    MaxCapacity += 2;
                    break;
                default:
                    break;
            }
        }

        public void AddResident(Human human)
        {
            _listResidentOfTheHouse.Add(human);
        }

        public bool RemoveResident(UnitWithoutGeneric human)
        {
            if (IsThereUnitInHouse(human) == true)
            {
                _listResidentOfTheHouse.Remove((Human)human);
                return true;
            }
            return false;
        }

        public bool IsThereUnitInHouse(GameObject unit)
        {
            if (_listResidentOfTheHouse.Any())
            {
                if (_listResidentOfTheHouse.Find(obj=> obj==unit) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<UnitWithoutGeneric> GetListWithResidentOfTheHouse()
        {
            return _listResidentOfTheHouse;
        }

        public int GetLVLHouse()
        {
            return _lvlHouse;
        }

    }
}
