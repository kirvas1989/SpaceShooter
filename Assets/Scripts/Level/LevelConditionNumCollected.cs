using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionNumCollected : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_NumToCollect;
        public int NumToCollect => m_NumToCollect;

        [HideInInspector] public int NumCollected;

        private bool m_Reached;

        private void Start()
        {
            NumCollected = 0;
        }

        public void Collect()
        {
            NumCollected++;
        }

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null && m_NumToCollect > 0)
                {
                   if (NumCollected == m_NumToCollect)
                        m_Reached = true;
                }

                return m_Reached;
            }
        }
    }
}

