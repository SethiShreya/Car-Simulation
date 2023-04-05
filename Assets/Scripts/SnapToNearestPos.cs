using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToNearestPos : MonoBehaviour
{
    public Transform[] Points;
    public float[] gearPositions;

    private void Start()
    {
        if (Points.Length == 0)
        {
            Debug.Log("No points");
            return;
        }

        gearPositions = new float[Points.Length];

        for (int i = 0; i < Points.Length; i++)
        {
            gearPositions[i] = Points[i].localPosition.z;
        }

        Debug.Log("values assigned");
    }

    float FindNearestGearPosition(float currentLeverPosition)
    {
        int leftIndex = 0;
        int rightIndex = gearPositions.Length - 1;
        int midIndex;
        while (leftIndex <= rightIndex)
        {
            midIndex = (leftIndex + rightIndex) / 2;
            if (currentLeverPosition < gearPositions[midIndex])
            {
                rightIndex = midIndex - 1;
            }
            else if (currentLeverPosition > gearPositions[midIndex])
            {
                leftIndex = midIndex + 1;
            }
            else
            {
                return gearPositions[midIndex];
            }
        }
        if (leftIndex >= gearPositions.Length)
        {
            return gearPositions[gearPositions.Length - 1];
        }
        else if (rightIndex < 0)
        {
            return gearPositions[0];
        }
        else
        {
            float leftDistance = Mathf.Abs(currentLeverPosition - gearPositions[leftIndex]);
            float rightDistance = Mathf.Abs(currentLeverPosition - gearPositions[rightIndex]);
            return leftDistance <= rightDistance ? gearPositions[leftIndex] : gearPositions[rightIndex];
        }
    }


    void Update()
    {
        float currentLeverPosition = transform.localPosition.z; // Or whatever axis you're using
        float nearestGearPosition = FindNearestGearPosition(currentLeverPosition);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, nearestGearPosition); // Snap to the nearest gear position
        currentLeverPosition = nearestGearPosition;
    }


    
}


