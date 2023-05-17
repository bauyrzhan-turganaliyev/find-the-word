namespace Data
{
    public interface ILetters 
    {
        public int GridSize { get; set; }
        public char[,] LettersArray { get; set; }
        public string[] WordsList { get; set; }
    }
}