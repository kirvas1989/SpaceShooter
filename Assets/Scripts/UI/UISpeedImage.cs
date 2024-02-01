using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UISpeedImage : MonoBehaviour
    {
        [SerializeField] private Image m_SpeedImage;
        [SerializeField] private PowerupSpeed m_SpeedPrefab;

        private float shieldTime;
        private float timer;

        private bool isActivated;

        private void Start()
        {
            m_SpeedImage.fillAmount = 0f;
            shieldTime = m_SpeedPrefab.Time;
            timer = shieldTime;

            Player.Instance.ActiveShip.SpeedEvent.AddListener(OnShieldEvent);//
        }

        private void OnDestroy()
        {
            Player.Instance.ActiveShip.SpeedEvent.RemoveListener(OnShieldEvent);//
        }

        private void Update()
        {
            if (isActivated == false) return;

            if (isActivated == true)
            {
                timer -= Time.deltaTime;
                m_SpeedImage.fillAmount = (float)timer / (float)shieldTime;

                if (m_SpeedImage.fillAmount == 0f)
                    isActivated = false;
            }
        }

        private void OnShieldEvent()
        {
            if (Player.Instance.ActiveShip != null)
            {
                isActivated = true;
            }
        }
    }
}
