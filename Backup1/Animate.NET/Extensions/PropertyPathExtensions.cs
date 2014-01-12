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

public static class PropertyPathExtensions
{
    public static PropertyPath Combine(this PropertyPath path1, PropertyPath path2)
    {
        return new PropertyPath(path1.Path + "." + path2.Path);
    }
}