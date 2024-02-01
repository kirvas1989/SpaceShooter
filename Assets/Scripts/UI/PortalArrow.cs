using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Стрелка показывает на ближайшие порталы.
    /// </summary>
    public class PortalArrow : NavigationArrow
    {
        private Teleporter[] teleporterArray;
        private Transform[] tpTransformArray;

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
            
            teleporterArray = FindObjectsOfType<Teleporter>();

            tpTransformArray = new Transform[teleporterArray.Length];

            for (int i = 0; i < teleporterArray.Length; i++)
            {
                tpTransformArray[i] = teleporterArray[i].transform;
            }
        }
 
        private void Update()
        {
            InitClosestTarget(tpTransformArray);               
        }
    }
}


