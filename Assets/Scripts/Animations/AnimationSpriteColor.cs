using UnityEngine;

namespace SpaceShooter
{
    public class AnimationSpriteColor : AnimationBase
    {
        [SerializeField] private SpriteRenderer m_Renderer;

        [SerializeField] private Color m_ColorA;
        [SerializeField] private Color m_ColorB;

        [SerializeField] private AnimationCurve m_Curve;

        protected override void AnimateFrame()
        {
            m_Renderer.color = Color.Lerp(m_ColorA, m_ColorB, m_Curve.Evaluate(NormalizedAnimationTime));
        }

        protected override void OnAnimationEnd()
        {
            
        }

        protected override void OnAnimationStart()
        {
            
        }

        public override void PrepareAnimation()
        {

          
        }
    }
}
