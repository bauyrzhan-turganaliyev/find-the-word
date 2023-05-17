using System.Collections.Generic;
using Data;
using Zenject;

public class GridDataService
{
    private CrosswordGenerator _crosswordGenerator;

    [Inject]
    public GridDataService(CrosswordGenerator crosswordGenerator)
    {
        _crosswordGenerator = crosswordGenerator;
    }
    
    public List<ILevelData> GetFilteredData(LevelsData levelsData)
    {
        if (levelsData is LevelsWordsData levelsWordsData)
        {
            List<ILevelData> levelDatas;
            for (int i = 0; i < levelsWordsData.Levels.Length; i++)
            {
                var grid = _crosswordGenerator.GetShuffledCrossword();
                
            }
        }
    }
}

interface ILevelData
{        
    public int GridSize { get; set; }
    public char[,] LettersArray { get; set; }
    public string[] WordsList { get; set; }
}