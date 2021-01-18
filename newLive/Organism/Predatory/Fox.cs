using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{   
    public class Fox:Predatory<IEdibleForFox>, IEdibleForBear, IEdibleForHuman, IEdibleForLion, IEdibleForWolf,
        IExtractionForMan
    {
        public Fox(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
          : base(x, y, random, map, number, unitGender)
        {
      
        }
        protected override UnitWithoutGeneric GetBaby()
        {
            return new Fox(CurrentCoordinate.X, CurrentCoordinate.Y,
                 GetRandom(), _map, ++_map.CurrentValuePopulation, ChooseGenderChildren());
        }
    }
}
