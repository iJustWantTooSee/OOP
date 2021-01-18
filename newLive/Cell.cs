using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Cell 
    {
        public Cell(float noiseValue)
        {
            _height = noiseValue;
        }
        public List<Brush> Colors { get; set; } = new List<Brush>() { Brushes.Black, Brushes.Black , Brushes.Black , Brushes.Black };
        //Dictionary
        public StateOfPoint State = StateOfPoint.Empty;
        private float _height;
        public float NoiseValue
        {
            get { return _height; }
            set
            {
                if (value < 1f && value > -1f)
                {
                    _height = value;
                }
            }

        }

    }
}
