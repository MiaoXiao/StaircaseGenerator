using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GenerateStaircase : MonoBehaviour
{
    //Original stair cube, used for instantiating and its start position
    public GameObject stairClone;
    //Ball for bouncing
    GameObject ball;
    //Value to rotate each stair
    public int stairRotation;

    //Array for holding all current stair objects
    GameObject[] allStairs;

    //Stair modifiers
    public static int length;
    public static int width;
    public static int height;
    public static int stairCount;

    //Stair modifiers for inspector
    public int setLength;
    public int setWidth;
    public int setHeight;
    public int setStairCount;

    void Start()
    {
        //Assign default values
        length = setLength;
        width = setWidth;
        height = setHeight;
        stairCount = setStairCount;

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

        //Create staircase, given stair count
        for (int i = 0; i < stairCount; ++i)
        {
            //Move stair position down, forwards, and rotate it
            currPosition.transform.Translate(Vector3.down * height);
            currPosition.transform.Translate(Vector3.forward * (width / 2));
            currPosition.transform.Rotate(0, stairRotation, 0);

            //Create the new stair in the current position and rotation
            allStairs[i] = (GameObject)Instantiate(stairClone, currPosition.transform.position, currPosition.transform.rotation);
        }
        
        //Hidest stair clone
        stairClone.SetActive(false);
        //Set stair clone back to original position/rotation
        stairClone.transform.position = new Vector3(0, 0, 0);
        stairClone.transform.localRotation = new Quaternion(0, 0, 0, 0);

    }

    //Simulate ball physics
    void simulateBall()
    {

    }

    void FixedUpdate()
    {

    }

    //Given index i, assigns that stair in array allStairs a random color
    void giveStairRandomColor(int i)
    {
        //Get mesh renderer of stair i
        MeshRenderer meshRenderer = allStairs[i].transform.GetChild(0).GetComponent<MeshRenderer>();

        //Create random color
        Color c = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        //Assign color to stair
        meshRenderer.material.color = c;
    }
}
