using UnityEngine;
using System.Collections;

public class DragUI : MonoBehaviour
{
    static Vector2 uiPosition;

    //Initially center the ui
    void Start()
    {

        if (uiPosition == NULL) this.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

    }

    //Called upon dragging a UI
    public void onDrag()
    {
        //Debug.Log("Current ui position: " + this.transform.position);
        this.transform.position = Input.mousePosition;
        uiPosition = this.transform.position;
    }
}
