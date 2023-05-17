using Grid;
using UI;
using UnityEngine;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class Root : MonoBehaviour
    {
        private GridDataService _gridDataService;
        private InputService _inputService;

        [Inject]
        public void Construct(GridDataService gridDataService, InputService inputService)
        {
            _gridDataService = gridDataService;
            _inputService = inputService;
        }
        private void Awake()
        {
            _gridDataService.StartGeneration(JsonReader.GetLevels());
            _inputService.Init();
        }
    }
}
