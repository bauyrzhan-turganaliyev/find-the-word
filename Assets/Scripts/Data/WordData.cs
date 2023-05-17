namespace Grid
{
    public class WordData
    {
        public int PY;
        public int PX;
        public Orientation Orientation;

        public WordData(int y, int x, Orientation orientation)
        {
            PY = y;
            PX = x;
            Orientation = orientation;
        }
    }
}