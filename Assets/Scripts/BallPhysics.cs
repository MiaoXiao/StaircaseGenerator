using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour
{
    //Simulate ball or not
    public bool simulateBall = false;
    //Launch angle
    public float angle = 70f;

    //Indexes of stairs
    int stairA = 0;
    int stairB = 1;

    //Distance between stairA and stairB
    float distance;

    //Gravity
    float gravity = Physics.gravity.magnitude;

    //Rigid body of ball
    Rigidbody rb;

    void Start()
    {
        //Get rigidbody
        rb = transform.GetComponent<Rigidbody>();
    }

    //Move ball to correct place, and unhide ball
    public void startBall()
    {
        if (simulateBall)
        {
            Debug.Log("Starting ball..");

            //Get child of first stair
            Transform sc = GenerateStaircase.allStairs[0].transform.GetChild(0);
            //Move ball above first step
            this.transform.position = sc.position + new Vector3(0, (GenerateStaircase.height / 2) + 5, GenerateStaircase.width / 4);

            stairA = 0;
            stairB = 1;
        }
    }

    //Every collision, give that object a random color, then launch the ball to the next stair
    void OnCollisionEnter(Collision col)
    {
        giveObjectRandomColor(col.gameObject);
        launchBall();
    }

    //Launch the ball from stairA to stairB
    void launchBall()
    {
        Debug.Log("Launching ball");

        Vector3 startPos = transform.position;
        Vector3 endPos = GenerateStaircase.allStairs[stairB].transform.position + new Vector3(0, GenerateStaircase.height / 2, GenerateStaircase.width / 4);
        distance = Vector3.Distance(startPos, endPos);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float v0 = distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);
        Vector3 finalVelocity = new Vector3(0, v0 * Mathf.Sin(angle), v0 * Mathf.Cos(angle));

        // Extract the X  Y componenent of the velocity
        float vx = Mathf.Sqrt(v0) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float vy = Mathf.Sqrt(v0) * Mathf.Sin(angle * Mathf.Deg2Rad);

        //this.transform.rotation = Quaternion.LookRotation(endPos - startPos);

        rb.AddForce(new Vector3(0, vx, vy), ForceMode.Impulse);
        /*
        //Get start and end points to launch between
        Vector3 startPos = GenerateStaircase.allStairs[stairA].transform.position + new Vector3(0, GenerateStaircase.height / 2, GenerateStaircase.width / 4);
        Vector3 endPos = GenerateStaircase.allStairs[stairB].transform.position + new Vector3(0, GenerateStaircase.height / 2, GenerateStaircase.width / 4);
        //Distance between those points
        distance = Vector3.Distance(startPos, endPos);

        //Positions of this object and the target on the same plane
        //Vector3 planarTarget = new Vector3(endPos.x, 0, endPos.z);
        //Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        //Initial velocity
        float v0 = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) - GenerateStaircase.height));
        //Velocity vector
        Vector3 v = new Vector3(0, v0 * Mathf.Sin(angle), v0 * Mathf.Cos(angle));
        //Get direction of launch
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, endPos - startPos);
        Vector3 vf = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * v;

        rb.velocity = vf;*/
        stairA++;
        stairB++;
        
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
