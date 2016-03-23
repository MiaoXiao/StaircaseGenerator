using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBehavior : MonoBehaviour
{
    enum StairVariables {Length, Width, Height};

    //Text object to update
    public Text text;
    //Which variable to update in GenerateStaircase Script
    public int variableToUpdate;

    //Script to access staircase modifiers
    GenerateStaircase GenerateStaircaseScript;

    void Start()
    {
        //Get access to GenerateStaircase script
        GameObject g = GameObject.FindGameObjectWithTag("Generation");
        GenerateStaircaseScript = g.GetComponent<GenerateStaircase>();

        //Update UI to default values
        updateUI(variableToUpdate);
    }

    //Update UI to current variables set in GenerateStaircase
    void updateUI(int modifier)
    {
        //Get the correct value from GenerateStaircase Script
        int value = 0;
        switch (variableToUpdate)
        {
            case (int)StairVariables.Length:
                value = GenerateStaircaseScript.length;
                break;
            case (int)StairVariables.Width:
                value = GenerateStaircaseScript.width;
                break;
            case (int)StairVariables.Height:
                value = GenerateStaircaseScript.height;
                break;
        }
        
        //Update slider value and text value
        this.GetComponent<Slider>().value = value;
        text.text = value.ToString();
    }

    //Update variables in GenerateStaircase script
    void updateVariable(float value, int modifier)
    {
        //Update actual variable in Generatetaircase script
        switch (modifier)
        {
            case (int)StairVariables.Length:
                GenerateStaircaseScript.length = (int)value;
                break;
            case (int)StairVariables.Width:
                GenerateStaircaseScript.width = (int)value;
                break;
            case (int)StairVariables.Height:
                GenerateStaircaseScript.height = (int)value;
                break;
        }
    }

    //Slider feedback
    public void updateSlider(float value)
    {
        updateUI(variableToUpdate);
        updateVariable(value, variableToUpdate);
    }
}
