using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

    //Changes current scene to scene_name
    public void changeScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
