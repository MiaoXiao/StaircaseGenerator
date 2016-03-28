using UnityEngine;
using System.Collections;

public class CameraPanning : MonoBehaviour
{
    public Vector3 myPos;
    public Transform ball;
 
    void Update()
    {
        transform.LookAt(ball);
        transform.position = ball.position + myPos;
    }
}
