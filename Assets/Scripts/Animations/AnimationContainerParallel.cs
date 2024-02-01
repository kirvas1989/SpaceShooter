using UnityEngine;

namespace SpaceShooter
{
    public class AnimationContainerParallel : AnimationBase
    {
        /// <summary>
        /// Последовательные шаги.
        /// </summary>
        [SerializeField] private AnimationBase[] m_Animations;
        
        public override void PrepareAnimation()
        {
            m_AnimationTime = 0;

            foreach (var v in m_Animations)
            {
                v.SetAnimationScale(m_AnimationScale);
                m_AnimationTime = Mathf.Max(m_AnimationTime, v.AnimationTime);

                v.PrepareAnimation();
            }
        }

        protected override void AnimateFrame()
        {

        }

        protected override void OnAnimationEnd()
        {

        }

        protected override void OnAnimationStart()
        {
            foreach (var v in m_Animations)
                v.StartAnimation();
        }
    }
}
