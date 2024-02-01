using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIPlayerRecordController : MonoBehaviour
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        private PlayerStatistics playerStatistics;

        private void Start()
        {
            playerStatistics = new PlayerStatistics();
            
            playerStatistics.Load();
            
            m_Kills.text = "KILLS: " + playerStatistics.MaxKills.ToString();
            m_Score.text = "SCORE: " + playerStatistics.MaxScore.ToString();
            m_Time.text = "TIME: " + playerStatistics.MaxTime.ToString();
        }
        
        public void OnButtonReset()
        {
            playerStatistics.Delete();
            
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);            
        }
    }
}
