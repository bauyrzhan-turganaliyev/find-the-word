using System.IO;
using Data;
using Infrastructure;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEngine;

namespace Utilities
{
    public class JsonReader 
    {
        private static readonly string _levelsByLettersPath = "/Configs/JSONs/LevelsByLetter.json";
        private static readonly string _levelsByWordsPath = "/Configs/JSONs/LevelsByWord.json";
        private static ReadFrom _readFrom;

        public JsonReader(GameConfigSO gameConfigSo)
        {
            _readFrom = gameConfigSo.ReadFrom;
        }
        public static LevelsData GetLevels()
        {
            string path = "";
            switch (_readFrom)
            {
                case ReadFrom.ByLetters:
                    path = Application.dataPath + _levelsByLettersPath;
                    break;
                case ReadFrom.ByWords:
                    path = Application.dataPath + _levelsByWordsPath;
                    break;
            }
            
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                switch (_readFrom)
                {
                    case ReadFrom.ByLetters:
                        var a = JsonConvert.DeserializeObject<LevelsLettersData>(jsonString);
                        return a;
                    case ReadFrom.ByWords:
                        var b = JsonConvert.DeserializeObject<LevelsWordsData>(jsonString);
                        return b;
                }
            }
            else
            {
                    Debug.LogError("JSON file does not exist at the specified path: " + path);
            }

            return null;
        }
    }
}