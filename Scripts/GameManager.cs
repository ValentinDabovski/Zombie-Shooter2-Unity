using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private bool hasLoadedLevel = false;

        void Awake()
        {
            Cursor.visible = true;

        }

        void Start()
        {


        }


        void Update()
        {
            if (Input.GetButton("Esc"))
            {
                this.PauseMenu();
            }
        }

        public void PauseMenu()
        {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }

        public void LoadingScreen()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}
