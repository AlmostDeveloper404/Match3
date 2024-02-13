using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Main
{
    public static class SaveLoadProgress
    {
        private static string nameSave = "save";
        public static void SaveData<T>(T playerData)
        {
            var jsonString = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(nameSave, jsonString);
        }

        public static T LoadData<T>()
        {
            if (PlayerPrefs.HasKey(nameSave))
            {
                var jsonString = PlayerPrefs.GetString(nameSave);
                var data = JsonUtility.FromJson<T>(jsonString);
                return data;
            }
            else
            {
                return default(T);
            }
        }

        public static void DeleteData()
        {
            PlayerPrefs.DeleteKey(nameSave);
        }
    }
}


