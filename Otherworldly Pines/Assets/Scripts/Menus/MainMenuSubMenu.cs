using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSubMenu : MonoBehaviour
{

    public void Back()
    {
        SceneManager.LoadScene(SceneIdentifier.MainMenu);
    }
    
}
