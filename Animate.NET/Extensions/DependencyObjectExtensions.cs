using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

public static class DependencyObjectExtensions
{
    public static DependencyObject FindVisualAncestor(this DependencyObject obj, Type type,
        Func<DependencyObject, Type, bool> Predicate)
    {
        //while (obj != null && obj as Visual == null)
        //    obj = LogicalTreeHelper.GetParent(obj);

        obj = VisualTreeHelper.GetParent(obj);

        while (obj != null && !Predicate(obj, type))
            obj = VisualTreeHelper.GetParent(obj);

        return obj;
    }

    public static T FindVisualAncestor<T>(this DependencyObject obj, Func<DependencyObject, Type, bool> Predicate)
    where T : DependencyObject
    {
        return (T)FindVisualAncestor(obj, typeof(T), Predicate);
    }

    public static T FindVisualAncestor<T>(this DependencyObject obj)
        where T : DependencyObject
    {
        return (T)FindVisualAncestor(obj, typeof(T), (o, t) => o.GetType() == t);
    }

    public static DependencyObject FindVisualDescendent(this DependencyObject obj, Type type,
        Func<DependencyObject, Type, bool> Predicate)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);

            if (Predicate(child, type))
                return child;

            child = FindVisualDescendent(child, type, Predicate);
            if (child != null)
                return child;
        }

        return null;
    }

    public static T FindVisualDescendent<T>(this DependencyObject obj, Func<DependencyObject, Type, bool> Predicate)
        where T : DependencyObject
    {
        return (T)FindVisualDescendent(obj, typeof(T), Predicate);
    }

    public static T FindVisualDescendent<T>(this DependencyObject obj)
        where T : DependencyObject
    {
        return (T)FindVisualDescendent(obj, typeof(T), (o, t) => o.GetType() == t);
    }
}