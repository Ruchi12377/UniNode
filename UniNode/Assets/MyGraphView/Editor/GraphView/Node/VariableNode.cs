using UnityEngine;

namespace Graph.Field
{
    public class Fields
    {
        /// <summary>
        /// Tは最終的に帰ってくる型,
        /// Oは引数に設定する型,
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="O"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T  returnValue<T>(T value)
        {
            if (value.GetType() == typeof(GameObject))
            {
                var gameObject = value as GameObject;
                return gameObject.GetComponent<T>();
            }
            return value;

        }
    }
}