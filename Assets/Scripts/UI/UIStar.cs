using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIStar : MonoBehaviour
    {
        [SerializeField] private GameObject m_BlueStar;
        [SerializeField] private GameObject m_SiverStar;
        [SerializeField] private GameObject m_GoldStar;

        private void Start()
        {
            LevelController.Instance.EventLevelCompleted.AddListener(OnEventLevelCompleted);

            m_BlueStar.SetActive(false);
            m_SiverStar.SetActive(false); 
            m_GoldStar.SetActive(false);
        }

        private void OnDestroy()
        {
            LevelController.Instance.EventLevelCompleted.RemoveListener(OnEventLevelCompleted);
        }

        private void OnEventLevelCompleted()
        {
            m_BlueStar.SetActive(true);

            if (LevelController.Instance.IsSilver == true)
            {
                m_BlueStar.SetActive(false);
                m_SiverStar.SetActive(true);
                m_GoldStar.SetActive(false);
            }
                
                    

            if (LevelController.Instance.IsGold == true)
            {
                m_BlueStar.SetActive(false);
                m_SiverStar.SetActive(false);
                m_GoldStar.SetActive(true);
            }
        }
    }
}
