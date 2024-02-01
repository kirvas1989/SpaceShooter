using UnityEngine;

namespace SpaceShooter
{
    public class UIButtonController : MonoBehaviour
    {
        private MovementController movementController;

        private void Start()
        {
            movementController = Player.Instance.GetComponent<MovementController>();
        }

        public void OnButtonClickOn()
        {          
            movementController.MobileJoystick.gameObject.SetActive(false);
            movementController.MobileFirePrimary.gameObject.SetActive(false);
            movementController.MobileFireSecondary.gameObject.SetActive(false);

            movementController.ControlKeyboard();
        }

        public void OnButtonClickOff()
        {
            movementController.MobileJoystick.gameObject.SetActive(true);
            movementController.MobileFirePrimary.gameObject.SetActive(true);
            movementController.MobileFireSecondary.gameObject.SetActive(true);

            movementController.ControlMobile();
        }
    }
}
