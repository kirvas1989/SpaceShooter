using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private CircleArea m_Area;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null && m_Area != null)
                {
                    bool isInsideTargetArea = (m_Area.transform.position - Player.Instance.ActiveShip.transform.position).sqrMagnitude
                                              < m_Area.Radius * m_Area.Radius;

                    if (isInsideTargetArea == true)
                        m_Reached = true;
                }

                return m_Reached;
            }
        }   
    }   
}
