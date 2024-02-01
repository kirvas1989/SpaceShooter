using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PauseMenuPanel : MonoBehaviour
    {
        [SerializeField] private Image m_Preview;
        [SerializeField] private Image m_Statistics;

        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        private PlayerStatistics m_Stats;

        private void Start()
        {
            gameObject.SetActive(false);

            m_Preview.sprite = LevelSequenceController.Instance.CurrentEpisode.PreviewImage;
        }

        public void OnButtonShowPause()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);           
        }

        public void OnButtonShowStats()
        {
            m_Preview.gameObject.SetActive(false);
            m_Statistics.gameObject.SetActive(true);

            m_Kills.text = "KILLS: " + m_Stats.NumKills.ToString();
            m_Score.text = "SCORE: " + m_Stats.Score.ToString();
            m_Time.text = "TIME: " + m_Stats.Time.ToString();
        }

        public void OnButtonHideStats()
        {
            m_Preview.gameObject.SetActive(true);
            m_Statistics.gameObject.SetActive(false);
        }

        public void OnButtonContinue()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);           
        }

        public void OnButtonMusicPause()
        {
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.AudioSource.Pause();
        }

        public void OnButtonMusicPlay()
        {
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.AudioSource.Play();
        }

        public void OnButtonMainMenu()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
        }

        public void OnButtonShowCurrentStats()
        {
            LevelSequenceController.Instance.LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelSequenceController.Instance.LevelStatistics.Score = Player.Instance.Score;
            LevelSequenceController.Instance.LevelStatistics.Time = (int)LevelController.Instance.LevelTime;

            m_Kills.text = "KILLS: " + LevelSequenceController.Instance.LevelStatistics.NumKills.ToString();
            m_Score.text = "SCORE: " + LevelSequenceController.Instance.LevelStatistics.Score.ToString();
            m_Time.text = "TIME: " + LevelSequenceController.Instance.LevelStatistics.Time.ToString();
        }
    }
}
