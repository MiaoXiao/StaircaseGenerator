using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
