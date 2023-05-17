namespace Data
{
    public interface ILevelData
    {        
        public int GridSize { get; set; }
        public char[,] LettersArray { get; set; }
        public string[] WordsList { get; set; }
    }
}