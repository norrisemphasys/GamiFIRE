using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    /// <summary>
    /// Checks if any object in a list contains a specific property value.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <typeparam name="TValue">The type of the property value to search for.</typeparam>
    /// <param name="list">The list of objects.</param>
    /// <param name="propertySelector">A function that selects the property to check.</param>
    /// <param name="value">The value to look for.</param>
    /// <returns>True if any object's property matches the value, otherwise false.</returns>
    public static bool ContainsProperty<T, TValue>(this List<T> list, Func<T, TValue> propertySelector, TValue value)
    {
        if (list == null || list.Count == 0) return false; // Handle null or empty lists

        return list.Any(item => propertySelector(item).Equals(value));
    }
}