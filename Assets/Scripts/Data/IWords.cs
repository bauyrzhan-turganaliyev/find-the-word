using System.Collections.Generic;

namespace Data
{
    public interface IWords
    {
        public int GridSize { get; set; }
        public string[] WordsList { get; set; }
    }
}