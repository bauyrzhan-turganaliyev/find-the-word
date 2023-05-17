using System.Collections;
using System.Collections.Generic;
using Data;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneratorService : MonoBehaviour
{
    public LetterBox cellPrefab; // Префаб ячейки
    public Transform parent; // Родитель ячейки
    public GridLayoutGroup gridLayoutGroup; // Родитель ячейки

    public void GenerateGrid(LevelsData levelsData)
    {
        
        CrosswordGenerator crosswordGenerator = new CrosswordGenerator();
        /*var grid = crosswordGenerator.GetShuffledCrossword(levelData.wordsList, levelData.gridSize);
        
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = levelData.gridSize;
        for (int y = 0; y < levelData.gridSize; y++)
        {
            for (int x = 0; x < levelData.gridSize; x++)
            {
                var letterBox = Instantiate(cellPrefab, parent);
                letterBox.SetLetter(grid[y,x]);
            }
        }*/
    }
}
