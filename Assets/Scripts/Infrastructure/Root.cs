using System;
using UnityEngine;
using Utilities;

namespace Infrastructure
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private int _level;
        private void Awake()
        {
            string filePath = Application.dataPath + "/Configs/JSONs/levels.json";
            var levels = JsonReader.GetLevels(filePath);
            if (levels == null)
                Debug.LogError("JSON file does not exist at the specified path: " + filePath);
            
            _gridGenerator.GenerateGrid(levels.levels[_level]);
        }
    }
}
