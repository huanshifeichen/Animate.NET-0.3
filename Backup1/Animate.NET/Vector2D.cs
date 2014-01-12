using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Animator
{
    public class Vector2D
    {
        public double Length;

        public double X
        {
            get { return Math.Cos(Radians) * Length; }
            set { SetComponentLengths(value, Y); }
        }

        public double Y
        {
            get { return Math.Sin(Radians) * Length; }
            set { SetComponentLengths(X, value); }
        }

        private void SetComponentLengths(double X, double Y)
        {
            Radians = Math.Atan2(Y, X);
            Length = Math.Sqrt(X * X + Y * Y);
        }

        private double _Radians;
        public double Radians
        {
            get { return _Radians; }
            set { _Radians = value; }
        }

        public double Degrees
        {
            get { return _Radians * 180.0 / Math.PI; }
            set { _Radians = value * Math.PI / 180.0; }
        }

        public double NormalizedDegrees
        {
            get
            {
                double temp = Degrees % 360;
                return temp < 0 ? temp + 360 : temp;
            }
        }

        public bool IsPointingLeft
        {
            get { return Length > 0 && NormalizedDegrees > 90 && NormalizedDegrees < 270; }
        }

        public bool IsPointingRight
        {
            get { return Length > 0 && (NormalizedDegrees > 270 || NormalizedDegrees < 90); }
        }

        public bool IsPointingUp
        {
            get { return Length > 0 && NormalizedDegrees > 180; }
        }

        public bool IsPointingDown
        {
            get { return Length > 0 && NormalizedDegrees.Between(0, 180); }
        }

        public void InvertX()
        {
            X = X * -1;
        }

        public void InvertY()
        {
            Y = Y * -1;
        }

        public void Scale(double ScaleFactor)
        {
            SetComponentLengths(X * ScaleFactor, Y * ScaleFactor);
        }

        public static Vector2D FromComponents(double X, double Y)
        {
            var vector = new Vector2D();
            vector.SetComponentLengths(X, Y);
            return vector;
        }

        public static Vector2D FromRadians(double Length, double Radians)
        {
            return new Vector2D() { Length = Length, Radians = Radians };
        }

        public static Vector2D FromDegrees(double Length, double Degrees)
        {
            return new Vector2D() { Length = Length, Degrees = Degrees };
        }

        public static Vector2D operator +(Vector2D Vector1, Vector2D Vector2)
        {
            return Vector2D.FromComponents(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y);
        }

        public static Vector2D operator +(Vector2D Vector, Point Point)
        {
            return Vector2D.FromComponents(Vector.X + Point.X, Vector.Y + Point.Y);
        }

        public static Vector2D operator -(Vector2D Vector1, Vector2D Vector2)
        {
            return Vector2D.FromComponents(Vector1.X - Vector2.X, Vector1.Y - Vector2.Y);
        }

        public static Vector2D operator -(Vector2D Vector, Point Point)
        {
            return Vector2D.FromComponents(Vector.X - Point.X, Vector.Y - Point.Y);
        }

        public static Vector2D operator *(Vector2D Vector, double ScaleFactor)
        {
            return Vector2D.FromComponents(Vector.X * ScaleFactor, Vector.Y * ScaleFactor);
        }

        public static Vector2D operator /(Vector2D Vector, double ScaleFactor)
        {
            return Vector2D.FromComponents(Vector.X / ScaleFactor, Vector.Y / ScaleFactor);
        }

        public override string ToString()
        {
            return "Degrees: " + Degrees.ToString() + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", Length: " + Length.ToString();
        }
    }
}