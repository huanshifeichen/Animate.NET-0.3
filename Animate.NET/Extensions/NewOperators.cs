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

namespace Animator
{
    public static class NewOperators
    {
        public static bool Between(this double number, double from, double to)
        {
            if (from == to)
                return number == from;

            if (from > to)
            {
                double temp = from;
                from = to;
                to = temp;
            }

            return from < number && number < to;
        }
    }
}