namespace JengaGame
{
    using UnityEngine;

    public static class JsonHelper
    {
        public static string AdjustJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }

        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
      
        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}