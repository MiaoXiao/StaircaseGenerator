using UnityEngine;
using System.Collections;

//Controls camera movement for Staircase and Staircase&Ball
public class CameraPanning : MonoBehaviour
{
    //Set to true if there is no ball
    public bool regularOrbit;
    //How high above the first step the camera should be set to
    public float cameraHeight = 10;

    //Pointer to ball object
    GameObject ball;

    //Start position of camera
    Vector3 startPos;
    //End position of camera
    Vector3 endPos;

    //Start timer for lerp
    float startTime;
    //Total y distance that camera will move
    float ydistance;
    //Speed of camera
    float speed = 1f;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    //Camera movement depends on regularOrbit bool
    void FixedUpdate()
    {
        //Rotate around first stair if using regular orbit
        if (regularOrbit)
        {
            if (GenerateStaircase.allStairs.Length != 0)
            {
                transform.RotateAround(GenerateStaircase.allStairs[0].transform.position, Vector3.up, 20 * Time.fixedDeltaTime);
            }
        }
        else //Look at ball otherwise
        {
            transform.LookAt(ball.transform);

            float ydistanceCovered = (Time.time - startTime) * speed;
            transform.position = Vector3.Lerp(startPos, endPos, ydistanceCovered / ydistance);
        }
    }

    //Reset camera to the first step
    public void resetCamera()
    {
        //Put camera back at start position, which is near the center of the staircase
        startPos = transform.position;
        transform.position = new Vector3(GenerateStaircase.length / 2 + GenerateStaircase.width / 2, cameraHeight, GenerateStaircase.width / 2);
        startPos = transform.position;

        //Get vertical camera speed
        speed = Mathf.Sqrt(GenerateStaircase.height);

        //Get end position
        endPos = startPos;
        endPos -= new Vector3(0, GenerateStaircase.height * GenerateStaircase.stairCount, 0);

        Debug.DrawLine(startPos, endPos, Color.green, 100f);

        //Reset timer for moving camera vertically
        startTime = Time.time;
        ydistance = Vector3.Distance(startPos, endPos);
    }
}
