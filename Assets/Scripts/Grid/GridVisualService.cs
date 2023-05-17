using System.Collections.Generic;
using System.Threading.Tasks;
using Grid;
using Infrastructure;
using Models;
using ScriptableObjects;
using UnityEngine;

public class GridVisualService
{
    private LetterBox[,] _visualGrid;
    private Dictionary<string, WordData> _wordsData;
    private GameConfigSO _configSo;
    private MessageBus _messageBus;

    public GridVisualService(GameConfigSO configSo, MessageBus messageBus)
    {
        _configSo = configSo;
        _messageBus = messageBus;
    }
    public void SetVariables(LetterBox[,] visualGrid, Dictionary<string, WordData> wordsData)
    {
        _visualGrid = visualGrid;
        _wordsData = wordsData;
    }

    public async Task Visualize(string word)
    {
        if (_wordsData.ContainsKey(word))
        {
            _messageBus.OnVisualFeedback?.Invoke(true);
            await OnCorrect(word);
        }
        else
        {
            _messageBus.OnVisualFeedback?.Invoke(false);
        }
    }

    private async Task OnCorrect(string word)
    {
        switch (_wordsData[word].Orientation)
        {
            case Orientation.Horizontal:
                for (int x = _wordsData[word].PX; x < _wordsData[word].PX +word.Length; x++)
                {
                    _visualGrid[_wordsData[word].PY, x].Show();
                    await Task.Delay(_configSo.AnimationDuration);
                }
                break;
            case Orientation.Vertical:
                for (int y = _wordsData[word].PY; y < _wordsData[word].PY +word.Length; y++)
                {
                    _visualGrid[y, _wordsData[word].PX].Show();
                    await Task.Delay(_configSo.AnimationDuration);
                }
                break;
        }
    }
}