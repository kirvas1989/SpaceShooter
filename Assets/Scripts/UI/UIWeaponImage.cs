using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIWeaponImage : MonoBehaviour
    {
        [SerializeField] private Image m_EnergyImage;
        [SerializeField] private Image m_AmmoImage;

        private void Start()
        {
            Player.Instance.ActiveShip.FireEvent.AddListener(OnFireEvent);//
        }

        private void OnDestroy()
        {
            Player.Instance.ActiveShip.FireEvent.RemoveListener(OnFireEvent);//
        }

        private void Update()
        {
            if (Player.Instance.ActiveShip == null)
            {
                m_EnergyImage.fillAmount = 1;
                m_AmmoImage.fillAmount = 1;
            }
            else
            {
                Player.Instance.ActiveShip.FireEvent.AddListener(OnFireEvent);//
                return;
            }
        }

        private void OnFireEvent()
        {
            if (Player.Instance.ActiveShip != null)
            {
                m_EnergyImage.fillAmount = ((float)Player.Instance.ActiveShip.PrimaryEnergy / (float)Player.Instance.ActiveShip.MaxEnergy);
                m_AmmoImage.fillAmount = ((float)Player.Instance.ActiveShip.SecondaryAmmo / (float)Player.Instance.ActiveShip.MaxAmmmo);
            }
        }
    }
}
