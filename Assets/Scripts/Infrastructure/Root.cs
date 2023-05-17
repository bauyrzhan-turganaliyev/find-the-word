using UnityEngine;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class Root : MonoBehaviour
    {
        private GridGeneratorService _gridGeneratorService;

        [Inject]
        public void Construct(GridGeneratorService gridGeneratorService)
        {
            _gridGeneratorService = gridGeneratorService;
        }
        private void Awake()
        {
            _gridGeneratorService.GenerateGrid(JsonReader.GetLevels());
        }
    }

    public enum ReadFrom
    {
        ByLetters,
        ByWords,
    }
}
