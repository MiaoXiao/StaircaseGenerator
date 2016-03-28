using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//Utilities for generating the staircase
public class GenerateStaircase : MonoBehaviour
{
    //Original stair cube, used for instantiating and its start position
    public GameObject stairClone;

    //Value to rotate each stair
    int stairRotation = 0;

    //Array for holding all current stair objects
    public static GameObject[] allStairs;
   
    //Stair modifiers
    public static int length = -1;
    public static int width;
    public static int height;
    public static int stairCount;

    //Stair modifiers for inspector
    public int setLength;
    public int setWidth;
    public int setHeight;
    public int setStairCount;

    //Access to ball physics script
    BallPhysics bp;

    void Awake()
    {
        //Get ball physics script
        bp = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallPhysics>();

        //Assign default values
        if (length == -1)
        {         
            length = setLength;
            width = setWidth;
            height = setHeight;
            stairCount = setStairCount;
        }

        //Set size of stair list
        allStairs = new GameObject[stairCount];

        //Default staircase
        generateStairs();
    }

    //Generate stairs with current length, width, height, and numbSteps values
    public void generateStairs()
    {
        //Destroy all existing stairs, then clear array of stairs
        foreach (GameObject stair in allStairs) Destroy(stair);
        Array.Clear(allStairs, 0, allStairs.Length);

        //Set array size to number of steps
        allStairs = new GameObject[stairCount];

        //Set stair clone to correct dimensions
        stairClone.transform.localScale = new Vector3(length, height, width);

        //Current stair position
        GameObject currPosition = stairClone;

        //Show stair clone, only for instantiating
        stairClone.SetActive(true);

        stairRotation = 0;

        //Create staircase, given stair count
        for (int i = 0; i < stairCount; ++i)
        {
            //Move stair position down, forwards, and rotate it
            currPosition.transform.Translate(Vector3.down * height);
            currPosition.transform.Translate(Vector3.forward * (width / 2));
            currPosition.transform.Rotate(0, stairRotation, 0);

            //Create the new stair in the current position and rotation
            allStairs[i] = (GameObject)Instantiate(stairClone, currPosition.transform.position, currPosition.transform.rotation);
            
            //After creating the second stair, find the stair rotation to be used for all stairs
            if (i == 1)
            {
                stairRotation = getCorrectAngle(i);
                //Apply the new rotation to the second stair
                allStairs[i].transform.Rotate(0, stairRotation, 0);
                currPosition.transform.Rotate(0, stairRotation, 0);
            }
        }
        
        //Hides stair clone
        stairClone.SetActive(false);
        //Set stair clone back to original position/rotation
        stairClone.transform.position = new Vector3(0, 0, 0);
        stairClone.transform.localRotation = new Quaternion(0, 0, 0, 0);

        //Attempt to start ball
        bp.resetBall();
    }

    //Get the correct angle to rotate each stair
    int getCorrectAngle(int i)
    {
        //Debug.Log("Looking for angle");
        int angle = 0;
        while (angle < 90)
        {
            //Rotate stair by 1 unit
            allStairs[i].transform.Rotate(0, 1, 0);
            //Compare the z position of stair i and i - 1 to see if we have rotated far enough
            if (allStairs[i].transform.GetChild(3).transform.position.z > allStairs[i - 1].transform.GetChild(2).transform.position.z)
            {
                allStairs[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                return angle;
            }
            angle++;

        }
        Debug.Log("ERROR");
        return angle;
    }
}
