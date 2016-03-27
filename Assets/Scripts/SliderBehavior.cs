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

    void Start()
    {
        //Update UI to default values
        updateUI(variableToUpdate);
    }

    //Slider feedback
    public void updateSlider(float value)
    {
        updateUI(variableToUpdate);
        updateVariable(Mathf.FloorToInt(value), variableToUpdate);
    }

    //Update UI to current variables set in GenerateStaircase
    void updateUI(int modifier)
    {
        //Get the correct value from GenerateStaircase Script
        int value = 0;
        switch (variableToUpdate)
        {
            case (int)StairVariables.Length:
                value = GenerateStaircase.length;
                break;
            case (int)StairVariables.Width:
                value = GenerateStaircase.width;
                break;
            case (int)StairVariables.Height:
                value = GenerateStaircase.height;
                break;
        }
        //Update slider value and text value
        this.GetComponent<Slider>().value = value;
        text.text = value.ToString();
    }

    //Update variables in GenerateStaircase script
    void updateVariable(int value, int modifier)
    {
        //Update actual variable in Generatetaircase script
        switch (modifier)
        {
            case (int)StairVariables.Length:
                GenerateStaircase.length = value;
                break;
            case (int)StairVariables.Width:
                GenerateStaircase.width = value;
                break;
            case (int)StairVariables.Height:
                GenerateStaircase.height = value;
                break;
        }
    }
}
