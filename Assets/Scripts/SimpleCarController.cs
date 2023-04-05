using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using TMPro;

public class SimpleCarController : MonoBehaviour
{

    private float m_horizontalAxis;
    public TextMeshProUGUI gear_text;
    public TextMeshProUGUI speed_text;
    private static float m_verticalAxis;
    public float m_maxSteering=20;
    public float m_speed;
    public float m_maxSpeed=1000;
    private float m_steeringVal;
    public bool rotate=true;
    public Transform steering;
    private float[] gearSpeeds = { 900f, 1200f, 1500f, 1700f, 1900f, 2000f }; // Speeds for each gear
    private float reverseSpeed = 900f; // Speed when reversing
    public static int currentGear = 1;

    public static void GetAccelerationInput(int gear, float multiplier)
    {
        currentGear = gear;
        m_verticalAxis = multiplier;
    }

    // colliders
    public WheelCollider m_frontLeft, m_frontRight, m_backLeft, m_backRight;

    // visuals
    public Transform m_frontLeftT, m_frontRightT, m_backLeftT, m_backRightT;


    void GetInput()
    {
        // it is getting input from old input system

#if UNITY_EDITOR
        m_horizontalAxis =Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Alpha1))
        {
            m_verticalAxis = -0.5f;
            currentGear = 0;
        }else if (Input.GetKey(KeyCode.Alpha2))
        {
            m_verticalAxis = -0.75f;
            currentGear = 1;
        }else if (Input.GetKey(KeyCode.Alpha3))
        {
            m_verticalAxis = -1.25f;
            currentGear = 2;
        }else if (Input.GetKey(KeyCode.Alpha4))
        {
            m_verticalAxis = -1.5f;
            currentGear = 3;
        }else if (Input.GetKey(KeyCode.Alpha5))
        {
            m_verticalAxis = -1.75f;
            currentGear = 4;
        }else if (Input.GetKey(KeyCode.R))
        {
            m_verticalAxis = 0.75f;
            currentGear = -1;
        }

#else
        float normalisedVal = ((steering.transform.localRotation.z)/120)*100;
        m_horizontalAxis = - normalisedVal;


#endif



        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_horizontalAxis = -m_horizontalAxis;
        }
    }

    void Steering()
    {
        // finding steer value then assigning 

        m_steeringVal =  m_maxSteering * m_horizontalAxis;
        m_frontLeft.steerAngle = - m_steeringVal;
        m_frontRight.steerAngle = - m_steeringVal;
    }

    void Acceleration()
    {
        float multiplier = 0;
        if (currentGear == -1)
        {
            multiplier = reverseSpeed;
        }
        else
        {
            multiplier = gearSpeeds[currentGear];
        }
        m_speed = multiplier * m_verticalAxis;

        //m_speed = Mathf.Clamp(m_speed, reverseSpeed, m_maxSpeed);

        m_frontLeft.motorTorque = - m_speed;
        m_frontRight.motorTorque = - m_speed;


        // Get max rpm
        float maxRPM = m_frontLeft.rpm > m_frontRight.rpm ? m_frontLeft.rpm : m_frontRight.rpm;

        // Get total power
        float totalPower = maxRPM * m_frontLeft.motorTorque;

        // Get engine rpm
        float engineRPM = Mathf.Abs(m_frontLeft.rpm);

        // Get acceleration
        float acceleration = m_verticalAxis * 9.81f; // Assuming gravity is 9.81 m/s^2

    }

    void UpdatePose()
    {
        // calling updatepose for all the wheels

        UpdatePose(m_frontRight, m_frontRightT);
        UpdatePose(m_frontLeft, m_frontLeftT);
        UpdatePose(m_backRight, m_backRightT);
        UpdatePose(m_backLeft, m_backLeftT);
    }

    void UpdatePose(WheelCollider _wheelCollider, Transform _transform)
    {
        Vector3 position;
        Quaternion rotation;

        _wheelCollider.GetWorldPose(out position, out rotation);
        if (rotate)
        {
        rotation *= Quaternion.Euler(0, 90, 0);
        }

        position.y = _transform.position.y;
        _transform.position = position;
        _transform.rotation = rotation;

    }

    private void FixedUpdate()
    {
        GetInput();
        Steering();
        Acceleration();
        UpdatePose();

        gear_text.text = currentGear.ToString();
        speed_text.text = m_speed.ToString();
    }
}