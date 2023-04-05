using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject closestObject;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger occur");
        if (other.transform.CompareTag("wall"))
        {
            Transform closestWall = FindClosest();
            float offset = (closestObject.transform.localScale.x) / 2;
            float x = closestWall.transform.position.x - offset;
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
    }


    

    Transform FindClosest()
    {
        float closestDistance = Mathf.Infinity;
        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }

        }
        return closestObject.transform;
    }
}

