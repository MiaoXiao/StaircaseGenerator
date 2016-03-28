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

    void Awake()
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

            //Remove all momentum from previous ball
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

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
        //Launch the ball, unless it is the last stair.
        if (stairTarget != GenerateStaircase.allStairs.Length) launchBall();
        else
        {
            resetBall();
        }
    }

    //Launch the ball from stairA to stairB
    void launchBall()
    {
        Debug.Log("Launching ball to stair: " + stairTarget);

        //Get start position
        Vector3 startPos = this.transform.position;
        //Get the landing location
        Vector3 endPos = GenerateStaircase.allStairs[stairTarget].transform.GetChild(1).position;

        Debug.DrawLine(startPos, endPos, Color.blue, 3f, false);

        //Have ball face towards the next stair
        Vector3 faceTarget = endPos;
        faceTarget.y = transform.position.y;
        this.transform.LookAt(faceTarget);

        //Get distance between start and end points, in terms of x and z
        Vector3 endPosTemp = new Vector3(endPos.x, 0, endPos.z);
        Vector3 startPosTemp = new Vector3(startPos.x, 0, startPos.z);
        float distance = Vector3.Distance(startPosTemp, endPosTemp);

        //Get initial velocity
        //http://physics.stackexchange.com/questions/27992/solving-for-initial-velocity-required-to-launch-a-projectile-to-a-given-destinat
        float vi = (1 / Mathf.Cos(radAngle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(radAngle) + GenerateStaircase.height));
        //Get velocity y and z components
        float vy = vi * Mathf.Sin(radAngle);
        float vz = vi * Mathf.Cos(radAngle);

        //Get final velocity and apply it
        Vector3 vf = transform.TransformVector(new Vector3(0, vy, vz));
        rb.velocity = vf;

        stairTarget++;
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
