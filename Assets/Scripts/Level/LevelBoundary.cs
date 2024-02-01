using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        public enum Shape
        {
            Circle,
            Rectangle
        }

        [SerializeField] private Shape m_ShapeMode;
        public Shape ShapeMode => m_ShapeMode;

        [SerializeField] private float m_Radius;
        public float Radius
        {
            get { return m_Radius; }
            set { m_Radius = value; }
        }
        [SerializeField] private Rect m_Bounds;
        public Rect Bounds => m_Bounds;

        public enum Mode
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_LimitMode;
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = UnityEngine.Color.green;
            if (m_ShapeMode == Shape.Circle)
                UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);

            if (m_ShapeMode == Shape.Rectangle)
                UnityEditor.Handles.DrawSolidRectangleWithOutline(m_Bounds, UnityEngine.Color.green.WithAlpha(0.1f), UnityEngine.Color.green);
        }
#endif

    }
}
