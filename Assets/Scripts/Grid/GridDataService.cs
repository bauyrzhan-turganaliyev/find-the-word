using System;
using System.Collections.Generic;
using Data;
using Infrastructure;
using Models;
using Zenject;

namespace Grid
{
    public class GridDataService
    {
        private CrosswordGenerator _crosswordGenerator;
        private GridGeneratorService _gridGeneratorService;
        private GridVisualService _gridVisualService;
        private MessageBus _messageBus;

        private Dictionary<string, WordData> _wordsData;
        private LetterBox[,] _visualGrid;

        private int _currentLevel;

        [Inject]
        public GridDataService(
            CrosswordGenerator crosswordGenerator,
            GridGeneratorService gridGeneratorService,
            GridVisualService gridVisualService,
            MessageBus messageBus)
        {
            _crosswordGenerator = crosswordGenerator;
            _gridGeneratorService = gridGeneratorService;
            _gridVisualService = gridVisualService;
            _messageBus = messageBus;

            SubscribeServices();
        }

        private void SubscribeServices()
        {
            _messageBus.OnReceiveWord += CheckWord;
        }

        private async void CheckWord(string word)
        {
           await _gridVisualService.Visualize(word);

           _wordsData.Remove(word);
        }

        public void StartGeneration(LevelsData levelsData)
        {
            if (levelsData == null) throw new Exception("Levels data is null");

            _wordsData = new Dictionary<string, WordData>();
            var currentLevel = GetCurrentLevel(levelsData);
            SetupWordsData(currentLevel);
            _visualGrid = _gridGeneratorService.GenerateGrid(currentLevel);
        
            _gridVisualService.SetVariables(_visualGrid, _wordsData);
        }

        private void SetupWordsData(ILevelData currentLevel)
        {
            ToLower(currentLevel);
            
            for (int i = 0; i < currentLevel.WordsList.Length; i++)
            {
                for (int j = 0; j < currentLevel.GridSize; j++)
                {
                    for (int k = 0; k < currentLevel.GridSize; k++)
                    {
                        if (currentLevel.LettersArray[j, k] == currentLevel.WordsList[i][0])
                        {
                            var wordData = FindWord(currentLevel.LettersArray, j, k, currentLevel.WordsList[i]);
                            var isFound = wordData.Item1;
                            var wordOrientation = wordData.Item2;
                            if (isFound && !_wordsData.ContainsKey(currentLevel.WordsList[i]))
                            {
                                _wordsData.Add(currentLevel.WordsList[i], new WordData(j, k, wordOrientation));
                            }
                        }
                    }
                }
            }
        }

        private static void ToLower(ILevelData currentLevel)
        {
            for (int i = 0; i < currentLevel.WordsList.Length; i++)
            {
                currentLevel.WordsList[i] = currentLevel.WordsList[i].ToLower();
            }

            for (int i = 0; i < currentLevel.GridSize; i++)
            {
                for (int j = 0; j < currentLevel.GridSize; j++)
                {
                    currentLevel.LettersArray[i,j] = Char.ToLower(currentLevel.LettersArray[i, j]);
                }
            }
        }

        private (bool, Orientation) FindWord(char[,] grid, int y, int x, string word)
        {
            bool isHorizontally = true;
            if (x + word.Length > grid.GetLength(1))
            {
                isHorizontally = false;
            }
            else
            {

                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[y, x + i] != word[i])
                    {
                        isHorizontally = false;
                        break;
                    }
                }
            }

            bool isVertically = true;
        
            if (y + word.Length > grid.GetLength(1))
            {
                isVertically = false;
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[y + i, x] != word[i])
                    {
                        isVertically = false;
                        break;
                    }
                }
            }

            if (isHorizontally || isVertically)
            {
                var orientation = isHorizontally ? Orientation.Horizontal : Orientation.Vertical;
                return (true, orientation);
            }

            return (false, Orientation.Horizontal);
        }

        private ILevelData GetCurrentLevel(LevelsData levelsData)
        {
            var levels = GetFilteredData(levelsData);
            var currentLevel = levels[_currentLevel];
            return currentLevel;
        }

        private List<ILevelData> GetFilteredData(LevelsData levelsData)
        {
            if (levelsData is LevelsWordsData levelsWordsData)
            {
                List<ILevelData> levelDatas = new List<ILevelData>();
                for (int i = 0; i < levelsWordsData.Levels.Length; i++)
                {
                    var grid = _crosswordGenerator.GetShuffledCrossword(levelsWordsData.Levels[i].WordsList, levelsWordsData.Levels[i].GridSize);
                    levelDatas.Add(new LevelWordsData(levelsWordsData.Levels[i].GridSize, grid, levelsWordsData.Levels[i].WordsList));
                }

                return levelDatas;
            } 
         
            if (levelsData is LevelsLettersData levelsLettersData)
            {
                List<ILevelData> levelDatas = new List<ILevelData>();
                for (int i = 0; i < levelsLettersData.Levels.Length; i++)
                {
                    levelDatas.Add(new LevelWordsData(levelsLettersData.Levels[i].GridSize, levelsLettersData.Levels[i].LettersArray, levelsLettersData.Levels[i].WordsList));
                }

                return levelDatas;
            }

            return null;
        }
    }
}