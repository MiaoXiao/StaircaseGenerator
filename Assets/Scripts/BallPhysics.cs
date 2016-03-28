using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour
{
    //Simulate ball or not
    public bool simulateBall;
    //Launch angle
    public float initialAngle;
    //First drop height
    public float dropHeight;

    //Index of next stair to jump to
    int stairTarget;

    //Angle in radians
    float radAngle;

    //Gravity
    float gravity = -Physics.gravity.y;

    //Rigid body of ball
    Rigidbody rb;

    void Start()
    {
        //Get rigidbody
        rb = transform.GetComponent<Rigidbody>();
        //Get angle in radians
        radAngle = initialAngle * Mathf.Deg2Rad;
    }

    //Reset ball to top of first stair
    public void resetBall()
    {
        if (simulateBall)
        {
            Debug.Log("Starting ball..");

            //Get top of stair
            Transform stairTop = GenerateStaircase.allStairs[0].transform.GetChild(1);

            //Move ball above first step
            transform.position = stairTop.position + new Vector3(0, dropHeight, 0);

            stairTarget = 1;
        }
    }

    //Every collision, give that object a random color, then launch the ball to the next stair
    void OnCollisionEnter(Collision col)
    {
        giveObjectRandomColor(col.gameObject);
        if (simulateBall) launchBall();
    }

    //Launch the ball from stairA to stairB
    void launchBall()
    {
        Debug.Log("Launching ball to stair: " + stairTarget);

        //Get start position
        Vector3 startPos = this.transform.position;
        //Get the landing location
        Vector3 endPos = GenerateStaircase.allStairs[stairTarget].transform.GetChild(1).position;

        //Debug.DrawLine(startPos, endPos, Color.blue, 20f, false);

        //Have ball face towards the next stair
        Vector3 targetPosition = endPos;
        targetPosition.y = transform.position.y;
        this.transform.LookAt(targetPosition);

        //Distance between ball and next stair
        float distance = Vector3.Distance(startPos, endPos);

        //Get initial velocity
        float vi = Mathf.Sqrt(distance * gravity / (Mathf.Sin(radAngle * 2)));
        //Get velocity y and z components
        float vy = vi * Mathf.Sin(radAngle);
        float vz = vi * Mathf.Cos(radAngle);

        //Get final velocity
        Vector3 vf = transform.TransformVector(new Vector3(0, vy, vz));

        rb.velocity = vf;

        //If the next stair exists, increment stair index, else turn off simulation
        if (stairTarget + 1 != GenerateStaircase.allStairs.Length)
        {
            stairTarget++;
        }
        else
        {
            simulateBall = false;
        }


    }

    //Assigns the given object a random color
    void giveObjectRandomColor(GameObject obj)
    {
        //Get mesh renderer
        MeshRenderer meshRenderer = obj.transform.GetComponent<MeshRenderer>();

        //Assign random color
        meshRenderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}
