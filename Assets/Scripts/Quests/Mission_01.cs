using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Mission_01 : Quest
    {
        [Header("LevelTarget")]
        [SerializeField] private Teleporter m_Target;
        public Teleporter Target => m_Target;

        #region Наследуемые методы
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        protected override void OnOpenPortal()
        {
            base.OnOpenPortal();

            Debug.Log("Миссия 1: Портал открыт!");
        }

        protected override void OnAustranautSaved()
        {
            base.OnAustranautSaved();

            Debug.Log("Миссия 1: Найден астранавт!");
        }

        protected override void FindAustranauts()
        {
            base.FindAustranauts();
        }

        #endregion

        private void Start()
        {
            m_Target.EventOnTeleported.AddListener(FinishLevel);

            FindAustranauts();
        }

        #region  Действия при окончании уровня
        private void FinishLevel()
        {
            m_Target.EventOnTeleported.RemoveListener(FinishLevel);

            OnOpenPortal();

            OnComplete();

            StartCoroutine(WaitForLevelSummury());                    
        }

        IEnumerator WaitForLevelSummury()
        {
            yield return new WaitForSeconds(m_timer);

            Debug.Log("Уровень 1 пройден!");
        }

        #endregion
    }
}

