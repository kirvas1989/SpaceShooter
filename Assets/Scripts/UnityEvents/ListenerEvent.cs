using UnityEngine;

namespace SpaceShooter
{
    public class ListenerEvent : MonoBehaviour
    {
        public InitEvent initEvent;

        private void Start()
        {
            initEvent.OnClick.AddListener(OnClickEvent);
        }

        private void OnClickEvent()
        {
            Debug.Log("Нажали");
        }
    }
}
