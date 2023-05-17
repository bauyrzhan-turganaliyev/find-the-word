using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class LevelWordsData : IWords
    {
        public int GridSize { get; set; }
        public string[] WordsList { get; set; }
    }
    
    [System.Serializable]
    public class LevelLettersData : ILetters
    {
        public int GridSize { get; set; }
        public char[,] LettersArray { get; set; }
        public string[] WordsList { get; set; }
    }

    public interface ILetters
    {
        public int GridSize { get; set; }
        public char[,] LettersArray { get; set; }
        public string[] WordsList { get; set; }
    }

    public interface IWords
    {
        public int GridSize { get; set; }
        public string[] WordsList { get; set; }
    }

    public interface ILevel : ILetters, IWords
    {
    }
}