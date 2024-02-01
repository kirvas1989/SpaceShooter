using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Mission_02 : Quest
    {
        [Header("LevelTarget")]
        [SerializeField] private Teleporter m_teleportA;
        [SerializeField] private Teleporter m_teleportB;
        [SerializeField] private Teleporter m_teleportC;

        [Header("Previous Mission")]
        [SerializeField] private Mission_01 mission01;

        #region Наследуемые методы
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        protected override void OnOpenPortal()
        {
            base.OnOpenPortal();

            Debug.Log("Миссия 2: Портал открыт!");
        }

        protected override void OnAustranautSaved()
        {
            base.OnAustranautSaved();

            CheckTarget();

            Debug.Log("Миссия 2: Найден астранавт!");
        }

        protected override void FindAustranauts()
        {
            base.FindAustranauts();
        }
        #endregion

        #region UnityEvents
        private void Start()
        {
            FindAustranauts();

            if (mission01 != null)
                mission01.Target.EventOnTeleported.AddListener(OnOpenPortal);
        }

        #endregion

        /// <summary>
        /// Регистрация событий подбора квестовых итемов.
        /// </summary>
        private void CheckTarget()
        {         
            if (m_AustranautsSaved == 3)
            { 
                ActivateTeleport(m_teleportA);

                Debug.Log("Телепорт A активирован!");
            }
            
            if (m_AustranautsSaved == 6) 
            {
                ActivateTeleport(m_teleportB);

                Debug.Log("Телепорт B активирован!");
            }
                
            if (m_AustranautsSaved == 9)
            {
                ActivateTeleport(m_teleportC);

                FinishLevel();

                Debug.Log("Телепорт С активирован!");
            }
        }

        /// <summary>
        /// Включение квестовых телепортов.
        /// </summary>
        /// <param name="teleporter"></param>
        private void ActivateTeleport(Teleporter teleporter)
        {
            teleporter.ActivateTeleport(true);

            OnOpenPortal();
        }

        #region Действия при окончании уровня
        private void FinishLevel()
        {
            OnComplete();

            StartCoroutine(WaitForLevelSummury());
        }

        IEnumerator WaitForLevelSummury()
        {
            yield return new WaitForSeconds(m_timer);

            Debug.Log("Второй уровень пройден!");          
        }
        #endregion
    }
}

