using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartGame : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
           
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StartGameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
