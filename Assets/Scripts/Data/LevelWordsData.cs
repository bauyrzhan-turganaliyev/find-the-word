namespace Data
{
    [System.Serializable]
    public class LevelWordsData : IWords, ILevelData
    {
        public int GridSize { get; set; }
        public char[,] LettersArray { get; set; }
        public string[] WordsList { get; set; }

        public LevelWordsData(int gridSize, char[,] lettersArray, string[] wordsList)
        {
            GridSize = gridSize;
            LettersArray = lettersArray;
            WordsList = wordsList;
        }
    }
}