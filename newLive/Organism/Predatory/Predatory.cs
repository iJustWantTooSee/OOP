using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public abstract class Predatory<TFood> : Unit<TFood>, IEdibleForOmnivorous, IEdibleForPredatory
        where TFood : IEdibleForPredatory
    {
        public Predatory(int x, int y, Random random, Map map, int number, GenderUnit.Gender unitGender)
          : base(x, y, random, map, number, unitGender)
        {
            SetNutritionalValue(random.Next(150, 250));
            SetVergeStarvation(random.Next(150, 250));
            InstallDefaultValues();
        }



        protected override bool IsFoodOnMap()
        {
            if (_map.ListUnitAndGrass.Count > 0)
                return true;
            else
                return false;
        }

    }
}
