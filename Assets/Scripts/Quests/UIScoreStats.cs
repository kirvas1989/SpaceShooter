using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIScoreStats : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private int m_LastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (Player.Instance != null)
            {
                int curentScore = Player.Instance.Score;

                if (m_LastScore != curentScore)
                {
                    m_LastScore = curentScore;

                    m_Text.text = " " + m_LastScore.ToString();   
                }
            }
        }
    }
}
