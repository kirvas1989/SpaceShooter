using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Стрелка показывает на квесты.
    /// </summary>
    public class QuestArrow : NavigationArrow
    {
        [SerializeField] private Transform[] m_Targets;

        #region Наследуемые методы
        protected override void SetArrowPosition()
        {
            base.SetArrowPosition();
        }

        protected override void InitClosestTarget(Transform[] targetArray)
        {
            base.InitClosestTarget(targetArray);
        }
        #endregion

        private void Start()
        {
            SetArrowPosition();

            GameObject[] questTargets = GameObject.FindGameObjectsWithTag("Quest");

            m_Targets = new Transform[questTargets.Length];

            for (int i = 0; i < questTargets.Length; i++)
            {
                m_Targets[i] = questTargets[i].transform;
            }
        }

        private void Update()
        {
            InitClosestTarget(m_Targets);        
        }
    }
}


