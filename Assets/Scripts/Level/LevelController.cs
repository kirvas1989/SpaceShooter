using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] private string m_LevelDescription;
        public string LevelDescription => m_LevelDescription;
          
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] public UnityEvent EventLevelCompleted;

        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        [Header("GoldMedal")]
        [SerializeField] private float m_GoldStarTime;

        [SerializeField] private int m_GoldStarScoreMultiplier;

        public int GoldStarScoreMultiplier => m_GoldStarScoreMultiplier;

        private bool isGold;
        public bool IsGold => isGold;

        [Header("SilverMedal")]
        [SerializeField] private float m_SilverStarTime;

        [SerializeField] private int m_SilverStarScoreMultiplier;
        public int SilverStarScoreMultiplier => m_SilverStarScoreMultiplier;

        private bool isSilver;
        public bool IsSilver => isSilver; 

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();

            isSilver = false;
            isGold = false; 
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach (var v in m_Conditions)
            {
                if (v.IsCompleted)                
                    numCompleted++;                
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;            
            
                if (m_LevelTime <= m_GoldStarTime)
                {
                    isGold = true;
                    isSilver = false;
                }             
                    
                if (m_LevelTime > m_GoldStarTime && m_LevelTime <= m_SilverStarTime)
                {
                    isGold = false;
                    isSilver = true;
                }
                    
                if (m_LevelTime > m_SilverStarTime)
                {
                    isGold = false;
                    isSilver = false;
                }

                EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

    }
}
