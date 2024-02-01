using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class SceneHelper : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
