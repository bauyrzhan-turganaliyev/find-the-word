namespace Data
{
    [System.Serializable]
    public class Level
    {
        public int gridSizeX;
        public int gridSizeY;
        public char[][] gridLetters { get; set; }
    }
}