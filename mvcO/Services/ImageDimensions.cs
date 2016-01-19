using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{
    public struct ImageDimensions
    {
        private readonly string _sizeName;
        private readonly int _width;
        private readonly int _height;

        public ImageDimensions(string sizeName, int width, int height)
        {
            this._sizeName = sizeName;
            this._width = width;
            this._height = height;
        }
        public string SizeName { get {return _sizeName;} }
        public int Width {get {return _width;}}
        public int Height { get { return _height; } }
    }
}