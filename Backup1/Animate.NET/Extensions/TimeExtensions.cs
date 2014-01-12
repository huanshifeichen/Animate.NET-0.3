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

public static class TimeExtensions
{
    public static TimeSpan hours(this double d)
    {
        return TimeSpan.FromHours(d);
    }

    public static TimeSpan minutes(this double d)
    {
        return TimeSpan.FromMinutes(d);
    }

    public static TimeSpan seconds(this double d)
    {
        return TimeSpan.FromSeconds(d);
    }

    public static TimeSpan milliseconds(this double d)
    {
        return TimeSpan.FromMilliseconds(d);
    }

    public static TimeSpan hours(this int d)
    {
        return TimeSpan.FromHours(d);
    }

    public static TimeSpan minutes(this int d)
    {
        return TimeSpan.FromMinutes(d);
    }

    public static TimeSpan seconds(this int d)
    {
        return TimeSpan.FromSeconds(d);
    }

    public static TimeSpan milliseconds(this int d)
    {
        return TimeSpan.FromMilliseconds(d);
    }
}