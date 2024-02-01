//using UnityEngine;

//namespace SpaceShooter
//{

//    public class UIGamePanel : MonoBehaviour
//    {
//        [SerializeField] private GameObject startPanel;
//        [SerializeField] private GameObject restartPanel;
//        [SerializeField] private GameObject passedPanel;
//        [SerializeField] private GameObject menuPanel;
//        [SerializeField] private GameObject menuPanel_shop;
//        [SerializeField] private GameObject shopPanel;
//        [SerializeField] private GameObject bonusPanel;
//        [SerializeField] private GameObject quitPanel;

//        [Header("HUD")]
//        [SerializeField] private GameObject progressPanel;
//        [SerializeField] private GameObject scorePanel;
//        [SerializeField] private GameObject hUDPanel;

//        [SerializeField] private AudioSource passedAudio;
//        [SerializeField] private AudioSource defeatAudio;

//        private void Awake()
//        {
//            ShowMainMenu();
//        }

//        private void ShowMainMenu()
//        {
//            startPanel.SetActive(true);
//            restartPanel.SetActive(false);
//            passedPanel.SetActive(false);
//            menuPanel.SetActive(false);
//            menuPanel_shop.SetActive(false);
//            shopPanel.SetActive(true);
//            bonusPanel.SetActive(false);
//            quitPanel.SetActive(true);

//            progressPanel.SetActive(false);
//            scorePanel.SetActive(false);
//            hUDPanel.SetActive(false);
//        }

//        private void OpenShop()
//        {
//            startPanel.SetActive(false);
//            restartPanel.SetActive(false);
//            passedPanel.SetActive(false);
//            menuPanel.SetActive(false);
//            menuPanel_shop.SetActive(true);
//            shopPanel.SetActive(false);
//            bonusPanel.SetActive(true);
//            quitPanel.SetActive(true);

//            progressPanel.SetActive(false);
//            scorePanel.SetActive(false);
//            hUDPanel.SetActive(true);
//        }

//        public void LevelPassed()
//        {
//            startPanel.SetActive(false);
//            restartPanel.SetActive(false);
//            passedPanel.SetActive(true);
//            menuPanel.SetActive(false);
//            menuPanel_shop.SetActive(false);
//            shopPanel.SetActive(true);
//            bonusPanel.SetActive(false);
//            quitPanel.SetActive(true);

//            progressPanel.SetActive(true);
//            scorePanel.SetActive(true);
//            hUDPanel.SetActive(true);

//            passedAudio.Play();
//        }

//        public void LevelDefeated()
//        {
//            startPanel.SetActive(false);
//            restartPanel.SetActive(true);
//            passedPanel.SetActive(false);
//            menuPanel.SetActive(false);
//            menuPanel_shop.SetActive(false);
//            shopPanel.SetActive(true);
//            bonusPanel.SetActive(false);
//            quitPanel.SetActive(true);

//            progressPanel.SetActive(true);
//            scorePanel.SetActive(true);
//            hUDPanel.SetActive(true);

//            defeatAudio.Play();
//        }

//        public void HideUI()
//        {
//            startPanel.SetActive(false);
//            restartPanel.SetActive(false);
//            passedPanel.SetActive(false);
//            menuPanel.SetActive(false);
//            menuPanel_shop.SetActive(false);
//            shopPanel.SetActive(false);
//            bonusPanel.SetActive(false);
//            quitPanel.SetActive(false);

//            progressPanel.SetActive(true);
//            scorePanel.SetActive(true);
//            hUDPanel.SetActive(true);
//        }
//    }
//}