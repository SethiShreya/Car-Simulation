using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float[] gearSpeeds = { 0f, 1f, 2f, 3f, 4f, 5f }; // Speeds for each gear
    public float reverseSpeed = -1f; // Speed when reversing
    public int currentGear = 1; // Current gear (default 1)
    public Transform car;

    private Rigidbody rb;

    private void Start()
    {
        rb = car.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float speed = rb.velocity.magnitude;
        
            float targetSpeed = gearSpeeds[currentGear];
            if (speed < targetSpeed) // Accelerate
            {
                rb.AddForce(-transform.right * 1f);
            }
            else if (speed > targetSpeed) // Decelerate
            {
                rb.AddForce(transform.right * 1f);
            }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "1":
                currentGear = 1;
                break;
            case "2":
                currentGear = 2;
                break;
            case "3":
                currentGear = 3;
                break;
            case "4":
                currentGear = 4;
                break;
            case "5":
                currentGear = 5;
                break;
            case "r":
                rb.velocity = -transform.forward * reverseSpeed;
                break;
                break;
            default:
                Debug.Log("Invalid gear choice");
                break;
        }
    }
}
