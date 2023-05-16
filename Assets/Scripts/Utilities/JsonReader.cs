using System.Collections.Generic;
using System.IO;
using Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Utilities
{
    public static class JsonReader 
    {
        public static Levels GetLevels(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                return JsonConvert.DeserializeObject<Levels>(jsonString);
                
            }

            return null;
        }
    }
}