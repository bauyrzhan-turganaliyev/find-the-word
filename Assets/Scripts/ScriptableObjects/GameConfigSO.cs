﻿using Infrastructure;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
    public class GameConfigSO : ScriptableObject
    {
        public ReadFrom ReadFrom;
        
        [Range(10, 1000)]
        public int AnimationDuration;
    }
}