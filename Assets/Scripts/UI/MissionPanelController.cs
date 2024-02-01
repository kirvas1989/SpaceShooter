using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class MissionPanelController : MonoBehaviour
    {
        [SerializeField] private Text m_MissionDescription;

        private void Start()
        {
            if(m_MissionDescription != null)
                m_MissionDescription.text = "MISSION: " + LevelController.Instance.LevelDescription;  
        }
    }
}
