using System.Collections;
using System.Collections.Generic;
using Data;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    public LetterBox cellPrefab; // Префаб ячейки
    public Transform parent; // Родитель ячейки
    public GridLayoutGroup gridLayoutGroup; // Родитель ячейки

    public void GenerateGrid(Level level)
    {
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = level.gridSizeY;
        for (int y = 0; y < level.gridSizeY; y++)
        {
            for (int x = 0; x < level.gridSizeY; x++)
            {
                var letterBox = Instantiate(cellPrefab, parent);
                letterBox.SetLetter(level.gridLetters[y][x]);
            }
        }
    }
}
