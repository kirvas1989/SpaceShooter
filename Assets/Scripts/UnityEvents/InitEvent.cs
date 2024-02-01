using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class InitEvent : MonoBehaviour
    {
        public UnityEvent OnClick;

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }
    }
}

