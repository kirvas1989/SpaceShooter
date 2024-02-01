using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics 
    {
        public int NumKills;
        public int Score;
        public int Time;

        public int MaxKills;
        public int MaxScore;
        public int MaxTime;

        public void Reset()
        {
            NumKills = 0;
            Score = 0;
            Time = 0;
        }

        public void Save()
        {
            PlayerPrefs.SetInt("PlayerStatistics:MaxKills", MaxKills);
            PlayerPrefs.SetInt("PlayerStatistics:MaxScore", MaxScore);
            PlayerPrefs.SetInt("PlayerStatistics:MaxTime", MaxTime);
        }

        public void Load()
        {
            MaxKills = PlayerPrefs.GetInt("PlayerStatistics:MaxKills", 0);
            MaxScore = PlayerPrefs.GetInt("PlayerStatistics:MaxScore", 0);
            MaxTime = PlayerPrefs.GetInt("PlayerStatistics:MaxTime", 0);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteAll();

            MaxKills = 0;
            MaxScore = 0;
            MaxTime = 0;

            Save();
        }
    }
}
