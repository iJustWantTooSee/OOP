using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Raccoon:Omnivorous<IEdibleForRaccoon>, IEdibleForBear, IEdibleForHuman, IEdibleForWolf,
        IEdibleForFox, IEdibleForLion, IExtractionForMan
    {
        public Raccoon(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
            : base(x, y, random, map, number, unitGender)
        {
          
        }

        protected override UnitWithoutGeneric GetBaby()
        {
            return new Raccoon(CurrentCoordinate.X, CurrentCoordinate.Y,
                 GetRandom(), _map, ++_map.CurrentValuePopulation, ChooseGenderChildren());
        }
    }
}
