using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Базовый класс анимации.
    /// </summary>
    public abstract class AnimationBase : MonoBehaviour
    {
        /// <summary>
        /// Полное время анимации.
        /// </summary>
        [SerializeField] protected float m_AnimationTime;

        /// <summary>
        /// Полное время анимации с учетом скейлинга.
        /// </summary>
        public float AnimationTime => m_AnimationTime / m_AnimationScale;

        /// <summary>
        /// Скейлинг времени анимации.
        /// </summary>
        [SerializeField] protected float m_AnimationScale;

        public void SetAnimationScale(float scale)
        {
            m_AnimationScale = scale;
        }

        /// <summary>
        /// Флаг повторяющейся анимации.
        /// </summary>
        [SerializeField] private bool m_Looping;

        [SerializeField] private bool m_Reverse;

        /// <summary>
        /// Нормализованное время анимации от 0 до 1.
        /// </summary>
        public float NormalizedAnimationTime
        {
            get
            {
                var t = Mathf.Clamp01(m_Timer / AnimationTime);

                return m_Reverse ? (1.0f - t) : t;
            }
        }

        [SerializeField] private UnityEvent m_EventStart;
        [SerializeField] private UnityEvent m_EventEnd;
        public UnityEvent OnEventEnd => m_EventEnd;

        private float m_Timer;

        private bool m_IsAnimationPlaying;

        public float Timer => Timer;

       
        #region Unity Events

        private void Update()
        {
            if (m_IsAnimationPlaying)
            {
                m_Timer += Time.deltaTime;

                AnimateFrame();

                if (m_Timer > m_AnimationTime)
                {
                    if (m_Looping)
                    {
                        m_Timer = 0; // Для избавлния проскальзывания кадров: m_Timer -= AnimationTime;
                    }
                    else
                    {
                        StopAnimation();
                    }
                }
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Метод запуска анимации.
        /// </summary>
        /// <param name="prepare"></param>
        public void StartAnimation(bool prepare = true)
        {
            if (m_IsAnimationPlaying) 
                return;

            if (prepare)
                PrepareAnimation();

            m_IsAnimationPlaying = true;    

            OnAnimationStart();

            m_EventStart?.Invoke();
        }

        /// <summary>
        /// Метод приостановки анимации.
        /// </summary>
        public void StopAnimation()
        {
            if (!m_IsAnimationPlaying) 
                return;

            m_IsAnimationPlaying = false;

            OnAnimationEnd();

            m_EventEnd?.Invoke();
        }

        #endregion

        /// <summary>
        /// Анимируем текущий фрейм аниации.
        /// </summary>
        protected abstract void AnimateFrame();

        protected abstract void OnAnimationStart();

        protected abstract void OnAnimationEnd();

        /// <summary>
        /// Подготовка начального состояния анимации.
        /// </summary>
        public abstract void PrepareAnimation();
    }
}
