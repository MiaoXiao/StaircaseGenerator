using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Controls button behaviour
public class ButtonBehavior : MonoBehaviour {

    //Changes current scene to scene_name
    public void changeScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
