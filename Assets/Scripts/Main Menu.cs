using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    // Method to load a scene by name with transition
    public void LoadScene(string sceneName)
    {
        
            
            SceneManager.LoadScene(sceneName);
        
    }

    // Optional: Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

   
}
