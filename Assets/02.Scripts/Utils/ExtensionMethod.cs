using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLib
{
    public static class ExtensionMethod
    {
        public static void Divide(ref this Vector3 curVec, Vector3 vec)
        {
            curVec.x /= vec.x;
            curVec.y /= vec.y;
            curVec.z /= vec.z;
        }

        public static List<T> RandomSelect<T>(this List<T> list, int count)
        {
            List<T> newList = new List<T>();
            List<T> copyList = new List<T>(list);

            for (int i = 0; i < count; i++)
            {
                int rand = Random.Range(0, copyList.Count);
                newList.Add(copyList[rand]);
                copyList.RemoveAt(rand);
            }

            return newList;
        }

        /// <summary>
        /// Is array null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] collection) => collection == null || collection.Length == 0;

        /// <summary>
        /// Is list null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> collection) => collection == null || collection.Count == 0;

        /// <summary>
        /// Is collection null or empty. IEnumerable is relatively slow. Use Array or List implementation if possible
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null /*|| !collection.Any()*/;

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this T[] collection) => !collection.IsNullOrEmpty();

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this IList<T> collection) => !collection.IsNullOrEmpty();

        /// <summary>
        /// Collection is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty<T>(this IEnumerable<T> collection) => !collection.IsNullOrEmpty();
    }
}
