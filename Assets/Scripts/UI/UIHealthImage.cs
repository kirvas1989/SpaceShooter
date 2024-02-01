using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIHealthImage : MonoBehaviour
    {
        [SerializeField] private Image m_FillImage;

        [SerializeField] private float m_LerpTime;

        private void Start()
        {
            Player.Instance.ActiveShip.ChangeHitpointsEvent.AddListener(OnChangeHitpoints);
        }

        private void OnDestroy()
        {
            Player.Instance.ActiveShip.ChangeHitpointsEvent.RemoveListener(OnChangeHitpoints);
        }

        private void Update()
        {
            if (Player.Instance.ActiveShip == null)
            {
                m_FillImage.fillAmount = 1;
            }
            else
            {
                Player.Instance.ActiveShip.ChangeHitpointsEvent.AddListener(OnChangeHitpoints);
                return;
            }
        }

        private void OnChangeHitpoints()
        {
            if (Player.Instance.ActiveShip != null)
            {
                float currentAmount = m_FillImage.fillAmount;

                m_FillImage.fillAmount = Mathf.Lerp(currentAmount, 
                                                   (float)Player.Instance.ActiveShip.CurrentHitPoints / (float)Player.Instance.ActiveShip.HitPoints, 
                                                   m_LerpTime * Time.deltaTime);
            }
        }
    }
}