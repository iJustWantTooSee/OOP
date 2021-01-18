using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Photo
    {
        private static Image _mooseImage = Image.FromFile(@"..\..\Icon\Moose.png");
        private static Image _mouseImage = Image.FromFile(@"..\..\Icon\Mouse.png");
        private static Image _rabbitImage = Image.FromFile(@"..\..\Icon\Rabbit.png");
        private static Image _bearImage = Image.FromFile(@"..\..\Icon\Bear.png");
        private static Image _pigImage = Image.FromFile(@"..\..\Icon\Pig.png");
        private static Image _raccoonImage = Image.FromFile(@"..\..\Icon\Raccoon.png");
        private static Image _foxImage = Image.FromFile(@"..\..\Icon\Fox.png");
        private static Image _lionImage = Image.FromFile(@"..\..\Icon\lion.png");
        private static Image _wolfImage = Image.FromFile(@"..\..\Icon\wolf.png");
        private static Image _manImage = Image.FromFile(@"..\..\Icon\man.png");
        private static Image _womanImage = Image.FromFile(@"..\..\Icon\woman.png");
        private static Image _appleImage = Image.FromFile(@"..\..\Icon\apple.png");
        private static Image _avenaImage = Image.FromFile(@"..\..\Icon\avena.png");
        private static Image _carrotImage = Image.FromFile(@"..\..\Icon\carrot.png");
        private static Image _houseImage = Image.FromFile(@"..\..\Icon\house.png");
        private static Image _treeImage = Image.FromFile(@"..\..\Icon\tree.png");
        private static Image _barnImage = Image.FromFile(@"..\..\Icon\barn.png");

        public Photo()
        {

        }


        public Image GetImage(string typeObject)
        {
            switch (typeObject)
            {
                case "Moose":
                    return _mooseImage;
                case "Mouse":
                    return _mouseImage;
                case "Rabbit":
                    return _rabbitImage;
                case "Bear":
                    return _bearImage;
                case "Pig":
                    return _pigImage;
                case "Raccoon":
                    return _raccoonImage;
                case "Fox":
                    return _foxImage;
                case "Lion":
                    return _lionImage;
                case "Wolf":
                    return _wolfImage;
                case "Apple":
                    return _appleImage;
                case "Avena":
                    return _avenaImage;
                case "Carrot":
                    return _carrotImage;
                case "Man":
                    return _manImage;
                case "Woman":
                    return _womanImage;
                case "House":
                    return _houseImage;
                case "Tree":
                    return _treeImage;
                case "Barn":
                    return _barnImage;
                default:
                    return null;
            }
        }
    }
}
