using System.Threading.Tasks;
using Infrastructure;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Task = System.Threading.Tasks.Task;

namespace UI
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private Image _inputFieldVisual;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _submitButton;
        private MessageBus _messageBus;

        [Inject]
        public void Construct(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public void Init()
        {
            SubscribeServices();
        }

        private void SubscribeServices()
        {
            _messageBus.OnVisualFeedback += GiveVisualFeedback;
            _submitButton.onClick.AddListener(SubmitWord);
        }

        private async void GiveVisualFeedback(bool isCorrect)
        {
            _inputField.interactable = false;
            _inputFieldVisual.color = isCorrect ? Color.green : Color.red;
            await Task.Delay(2000);
            _inputFieldVisual.color = Color.white;
            _inputField.text = "";
            _inputField.interactable = true;
        }

        private void SubmitWord()
        {
            _messageBus.OnSubmit?.Invoke(_inputField.text.ToLower());
        }
    }
}
