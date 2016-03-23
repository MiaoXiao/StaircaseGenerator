using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GenerateStaircase : MonoBehaviour
{

    //Original stair cube, used for instantiating
    public GameObject StairClone;
    //Start position of first stair
    public GameObject startPosition;

    //Array for holding all current stair objects
    GameObject[] allStairs;

    //Stair modifiers
    public int length;
    public int width;
    public int height;
    public int numberOfSteps;

    void Start()
    {
        //Set array size to number of steps
        allStairs = new GameObject[numberOfSteps];
    }

    //Generate stairs with current length, width, height, and numbSteps values
    public void generateStairs()
    {
        //Destroy all existing stairs, then clear array of stairs
        foreach (GameObject stair in allStairs) Destroy(stair);
        Array.Clear(allStairs, 0, numberOfSteps);

        //Get original start position of first stair
        Vector3 currPos = startPosition.transform.position;

        //Set stair clone to correct dimensions
        StairClone.transform.localScale = new Vector3(length, height, width);

        for (int i = 0; i < numberOfSteps; ++i)
        {
            //Create new stair in correct position, then add to array
            allStairs[i] = (GameObject)Instantiate(StairClone, currPos, Quaternion.identity);
            //Move next stair to correct position
            currPos += new Vector3(0, height, width);
        }
    }

}
