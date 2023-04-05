using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ChangeConstraint : MonoBehaviour
{
    //[System.Serializable]
    //public struct noRestriction
    //{
    //    public float minVal;
    //    public float maxVal;
    //}
    public TextMeshProUGUI text;
    public FloatConstraint nminZ = new FloatConstraint();
    public FloatConstraint nmaxZ = new FloatConstraint();
    public FloatConstraint nminX = new FloatConstraint();
    public FloatConstraint nmaxX = new FloatConstraint();

    //public noRestriction nr= new noRestriction();
    //public noRestriction nr2 = new noRestriction();
    //public noRestriction nr3 = new noRestriction();
    //public float tolerance;

    

    public OneGrabTranslateTransformer transformer;

    private void Start()
    {
        transformer = GetComponent<OneGrabTranslateTransformer>();
        if (transformer == null)
        {
            Debug.Log("Transformer not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MiddleLane"))
        {
            Debug.Log("In middle lane");
            text.text="In middle lane";
            transformer.Constraints.MinZ.Value= transform.position.z;
            transformer.Constraints.MaxZ.Value = transform.position.z;
            transformer.Constraints.MinX = nminX;
            transformer.Constraints.MaxX = nmaxX;
        }
        else if (other.CompareTag("NoRes"))
        {
            Debug.Log("In norestriction 1 lane");
            text.text= "In norestriction lane";
            transformer.Constraints.MinZ = nminZ;
            transformer.Constraints.MaxZ = nmaxZ;
            transformer.Constraints.MinX.Value = transform.position.x;
            transformer.Constraints.MaxZ.Value = transform.position.x;
            
        }
        else if (other.CompareTag("NoRes2"))
        {
            Debug.Log("In norestriction 2 lane");
            text.text = "In middle no restriction lane";
            transformer.Constraints.MinZ = nminZ;
            transformer.Constraints.MaxZ = nmaxZ;
            transformer.Constraints.MinX = nminX;
            transformer.Constraints.MaxX = nmaxX;

        }
            //else if (other.CompareTag("NoRes3"))
            //{
            //    Debug.Log("In norestriction 3 lane");
            //    text.text= "In norestriction 3 lane";
            //    transformer.Constraints.MinZ = nminZ;
            //    transformer.Constraints.MaxZ = nmaxZ;
            //    transformer.Constraints.MinX.Value = nr3.minVal;
            //    transformer.Constraints.MaxZ.Value = nr3.maxVal;

            //}

        }

    //private void FixedUpdate()
    //{
    //    float z = transform.position.z;
        

    //    if (z>=minZ.Value && z <= maxZ.Value)
    //    {
    //        text.text += "Value change\n";
    //        text.text += minZ.Value.ToString() + '\n';
    //        text.text += maxZ.Value.ToString() + '\n';

    //        transformer.Constraints.MinZ = minZ;
    //        transformer.Constraints.MaxZ = maxZ;
    //    }
    //    else
    //    {

            
    //        transformer.Constraints.MinZ = nminZ;
    //        transformer.Constraints.MaxZ = nmaxZ;
    //    }
    //}

    private bool isUnder(float min, float max, float comparer)
    {
        if (comparer >= min && comparer <= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
