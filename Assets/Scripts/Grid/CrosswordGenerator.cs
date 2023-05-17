using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace Grid
{
    public class CrosswordGenerator
    {
        private int _gridSize; 
        private string[] _wordList;
        private char[,] _grid;
    
        public char[,] GetShuffledCrossword(string[] wordsList, int gridSize)
        {
            _wordList = wordsList;
            _gridSize = gridSize;
            _wordList = _wordList.OrderByDescending(x => x.Length).ToArray();
            _grid = new char[_gridSize, _gridSize];
            for (int y = 0; y < _gridSize; y++)
            {
                for (int x = 0; x < _gridSize; x++)
                {
                    _grid[y, x] = '-';
                }
            }
            
            PlaceFirstWord(_wordList[0]);
            
            for (int i = 1; i < _wordList.Length; i++)
            {
                var maxAttempts = 100;
                while (maxAttempts > 0)
                {
                    if (PlaceWord(_wordList[i]))
                    {
                        break;
                    }
                    else
                    {
                        maxAttempts--;
                        ResetGrid();
                        PlaceFirstWord(_wordList[0]);
                    }
                }
            }

            return _grid;
        }

        private void ResetGrid()
        { 
            for (int y = 0; y < _gridSize; y++)
            {
                for (int x = 0; x < _gridSize; x++)
                {
                    _grid[y, x] = '-';
                }
            }
        }

        private bool PlaceWord(string word)
        {

            for (int i = 0; i < word.Length; i++)
            {
                var maxAttempts = 10;
                FindLetterData resultOfFindingLetter = null;
                
                while (maxAttempts > 0)
                {
                    if (resultOfFindingLetter == null) resultOfFindingLetter = TryFindLetter(word[i], 0, 0, 10 - maxAttempts);
                    else
                        resultOfFindingLetter = TryFindLetter(word[i], resultOfFindingLetter.PY, resultOfFindingLetter.PX,  10 -maxAttempts);
                    
                    if (resultOfFindingLetter != null)
                    {
                        if (TryToSetWordToGrid(word, i, resultOfFindingLetter.PY, resultOfFindingLetter.PX,
                                resultOfFindingLetter.Orientation))
                        {
                            return true;
                        } 
                        
                        maxAttempts--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (maxAttempts == 0) return false;
            }

            return true;
        }

        private FindLetterData TryFindLetter(char c, int pY = 0, int pX = 0, int offset = 0)
        {
            Orientation orientation = Orientation.Horizontal;
            bool finish = false;

            pX += offset;
            while (pX > _gridSize)
            {
                pX -= _gridSize;
                pY++;
            }
            
            for (int y = pY; y < _gridSize; y++)
            {
                for (int x = pX; x < _gridSize; x++)
                {
                    if (c == _grid[y, x])
                    {
                        pY = y;
                        pX = x;

                        if (x == 0)
                        {
                            if (_grid[y, x + 1] != '-')
                            {
                                if (y == 0)
                                {
                                    if (_grid[y + 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        throw new Exception("Letter is Busy");
                                    }
                                }
                                else if (y == _gridSize-1)
                                {
                                    if (_grid[y - 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        return null;
                                    } 
                                }
                                else
                                {
                                    if (_grid[y + 1, x] == '-' && _grid[y - 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        return null;
                                    } 
                                }
                            }
                            else
                            {
                                orientation = Orientation.Horizontal;
                                finish = true;
                                break;
                            }
                        }
                        else
                        {
                            if (_grid[y, x - 1] != '-' || _grid[y, x + 1] != '-')
                            {
                                if (y == 0)
                                {
                                    if (_grid[y + 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else if (y == _gridSize-1)
                                {
                                    if (_grid[y - 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        return null;
                                    } 
                                }
                                else
                                {
                                    if (_grid[y + 1, x] == '-' && _grid[y - 1, x] == '-')
                                    {
                                        orientation = Orientation.Vertical;
                                        finish = true;
                                        break;
                                    }
                                    else
                                    {
                                        return null;
                                    } 
                                }
                            }
                            else
                            {
                                if (_grid[y, x - 1] == '-' && _grid[y, x + 1] == '-')
                                {
                                    orientation = Orientation.Horizontal;
                                    finish = true;
                                    break;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }

                if (finish) break;
            }

            if (finish)
                return new FindLetterData(pY, pX, orientation);
            else 
                return null;
        }

        private bool TryToSetWordToGrid(string word, int indexOfLetter, int pY, int pX, Orientation orientation = Orientation.Horizontal)
        {
            var indexOfLetterInNewWord = indexOfLetter;
            
            if (orientation == Orientation.Vertical)
            {
                if (pY >= indexOfLetterInNewWord && pY + (word.Length - indexOfLetterInNewWord) <= _gridSize)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        _grid[pY - indexOfLetterInNewWord + i, pX] = word[i];
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (pX >= indexOfLetterInNewWord && pX + (word.Length - indexOfLetterInNewWord) <= _gridSize)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        _grid[pY, pX - indexOfLetterInNewWord + i] = word[i];
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void PlaceFirstWord(string word)
        {
            var randomInt = Random.Range(0, _gridSize);

            for (int i = 0; i < word.Length; i++)
            {
                _grid[randomInt, i] = word[i];
            }
        }
    }

    public class FindLetterData
    {
        public int PY;
        public int PX;
        public Orientation Orientation;

        public FindLetterData(int pY, int pX, Orientation orientation)
        {
            PY = pY;
            PX = pX;
            Orientation = orientation;
        }
    }
}