using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    public class LetterBox : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public void SetLetter(char c)
        {
            if (c == '.')
                _image.enabled = false;
            else
                _text.text = c.ToString().ToLower();
        }
    }
}