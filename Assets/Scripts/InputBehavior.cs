using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputBehavior : MonoBehaviour
{
    GenerateStaircase GenerateStaircaseScript;
    
    void Start()
    {
        //Get access to GenerateStaircase script
        GameObject g = GameObject.FindGameObjectWithTag("Generation");
        GenerateStaircaseScript = g.GetComponent<GenerateStaircase>();

        //Set text to default value
        this.GetComponent<InputField>().text = GenerateStaircaseScript.numberOfSteps.ToString();
    }

    //Update variables
    public void updateVariables(string value)
    {
        GenerateStaircaseScript.numberOfSteps = int.Parse(value);
    }
}
