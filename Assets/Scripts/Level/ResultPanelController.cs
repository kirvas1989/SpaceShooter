using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Image m_WinImage;
        [SerializeField] private Image m_LoseImage;
        
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Button m_Accept;

        public UnityEvent ResultEvent;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            m_Kills.text = "KILLS: " + levelResults.NumKills.ToString();
            m_Score.text = "SCORE: " + levelResults.Score.ToString();
            m_Time.text = "TIME: " + levelResults.Time.ToString();

            if (success == true)
            {
                m_WinImage.enabled = true;
                m_LoseImage.enabled = false;
            }
            else
            {
                m_WinImage.enabled = false;
                m_LoseImage.enabled = true;
            }

            Time.timeScale = 0; // Все объекты на сцене останавливаются.

            ResultEvent?.Invoke();
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel(); // Проверить!
            }
        }
    }
}
