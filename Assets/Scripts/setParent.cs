using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setParent : MonoBehaviour
{
    public GameObject parent;
    void Start()
    {
        transform.parent = parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
