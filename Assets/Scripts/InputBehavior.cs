using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Handles Input box behavior, and updates variables in Generate Staircase script
public class InputBehavior : MonoBehaviour
{
    void Start()
    {
        //Set text to default value
        this.GetComponent<InputField>().text = GenerateStaircase.stairCount.ToString();
    }

    //Update variables
    public void updateVariables(string value)
    {
        GenerateStaircase.stairCount = int.Parse(value);
    }
}
