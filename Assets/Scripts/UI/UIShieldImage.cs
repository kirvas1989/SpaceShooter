using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIShieldImage : MonoBehaviour
    {
        [SerializeField] private Image m_ShieldImage;
        [SerializeField] private PowerupShield m_ShieldPrefab;

        private float shieldTime;
        private float timer;

        private bool isActivated;

        private void Start()
        {
            m_ShieldImage.fillAmount = 0f;
            shieldTime = m_ShieldPrefab.Time;
            timer = shieldTime;

            Player.Instance.ActiveShip.ShieldEvent.AddListener(OnShieldEvent);//
        }

        private void OnDestroy()
        {
            Player.Instance.ActiveShip.ShieldEvent.RemoveListener(OnShieldEvent);//
        }

        private void Update()
        {
            if (isActivated == false) return;

            if (isActivated == true)
            {
                timer -= Time.deltaTime;
                m_ShieldImage.fillAmount = (float)timer / (float)shieldTime;

                if (m_ShieldImage.fillAmount == 0f)
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
