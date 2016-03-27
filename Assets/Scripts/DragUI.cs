using UnityEngine;
using System.Collections;

public class DragUI : MonoBehaviour
{
    static Vector2 uiPosition = new Vector2(-1, -1);

    //Initially center the ui
    void Start()
    {
        //Check if uiPosition should be set to center, or to its last position
        if (uiPosition == new Vector2(-1, -1))
        {
            this.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
            uiPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        }
        else
        {
            this.transform.position = uiPosition;
        }
    }

    //Called upon dragging a UI
    public void onDrag()
    {
        this.transform.position = Input.mousePosition;
        uiPosition = this.transform.position;
    }
}
