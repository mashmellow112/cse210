using System;

namespace Shapes
{
    public class Shape
    {
        private string color;
        public string Color { get; set; }

        public Shape(string color)
        {
            Color = color;
        }

        public virtual double GetArea()
        {
            // To be overridden
            return 0;
        }
    }
}