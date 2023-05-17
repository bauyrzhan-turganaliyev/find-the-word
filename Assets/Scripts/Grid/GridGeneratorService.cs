using Data;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneratorService : MonoBehaviour
{
    [SerializeField] private LetterBox _cellPrefab;
    [SerializeField] private Transform _parent; 
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    public LetterBox[,] GenerateGrid(ILevelData levelsData)
    {
        LetterBox[,] visualGrid = new LetterBox[levelsData.GridSize, levelsData.GridSize];
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroup.constraintCount = levelsData.GridSize;
        for (int y = 0; y < levelsData.GridSize; y++)
        {
            for (int x = 0; x < levelsData.GridSize; x++)
            {
                var letterBox = Instantiate(_cellPrefab, _parent);
                letterBox.SetLetter(levelsData.LettersArray[y,x]);
                visualGrid[y, x] = letterBox;
            }
        }

        return visualGrid;
    }
}
