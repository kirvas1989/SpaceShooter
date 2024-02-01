using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Стрелка показывает на Хаб порталов.
    /// </summary>
    public class HubArrow : NavigationArrow
    {
        private Transform m_Target;

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

            m_Target = GameObject.FindGameObjectWithTag("Hub").transform;

            if (m_Target == null)
                enabled = false;

        }

        private void Update()
        {
            if (m_Target == null)
            {
                m_Renderer.enabled = false;
            }
            else
            {
                m_Renderer.enabled = true;

                Vector3 direction = m_Target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                m_Arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            } 
        }
    }
}



