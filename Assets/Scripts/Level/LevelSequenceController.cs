using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "scene_main_menu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            // —брасываем статы перед началом эпизода.

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistics();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistics()
        {
            LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelStatistics.Score = Player.Instance.Score;
            LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
                   
            if (LevelController.Instance.IsSilver == true)
                LevelStatistics.Score = LevelStatistics.Score * LevelController.Instance.SilverStarScoreMultiplier;

            if (LevelController.Instance.IsGold == true)
                LevelStatistics.Score = LevelStatistics.Score * LevelController.Instance.GoldStarScoreMultiplier;

            LevelStatistics.MaxKills += LevelStatistics.NumKills;
            LevelStatistics.MaxScore += LevelStatistics.Score;
            LevelStatistics.MaxTime += LevelStatistics.Time;

            LevelStatistics.Save();
        }
    }
}
