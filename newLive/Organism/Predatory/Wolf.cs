using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Wolf: Predatory<IEdibleForWolf>, IEdibleForBear, IEdibleForHuman, IEdibleForLion, IExtractionForMan
    {
        public Wolf(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
            : base(x, y, random, map, number, unitGender)
        {

        }

        protected override bool IsWentIntoHibernation()
        {
            if (_map.Season == season.winter)
                return true;
            return false;
        }

        protected override UnitWithoutGeneric GetBaby()
        {
            return new Wolf(CurrentCoordinate.X, CurrentCoordinate.Y,
                 GetRandom(), _map, ++_map.CurrentValuePopulation, ChooseGenderChildren());
        }
    }
}
