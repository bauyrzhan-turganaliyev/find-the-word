using System;
using UnityEngine;

namespace Infrastructure
{
    public class MessageBus
    {
        public Action<string> OnSubmit;

        public Action<string> OnReceiveWord;

        public Action<bool> OnVisualFeedback;

        public MessageBus()
        {
            OnSubmit += s => OnReceiveWord?.Invoke(s);
        }
    }
}