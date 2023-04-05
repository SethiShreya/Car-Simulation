
using UnityEngine;

public class GearShifter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "r":
                SimpleCarController.GetAccelerationInput(-1, - 0.75f);
                Debug.Log(other.tag);
                break;
            case "1":
                SimpleCarController.GetAccelerationInput(0, 0.75f);
                Debug.Log(other.tag);
                break;
            case "2":
                SimpleCarController.GetAccelerationInput(1, 1.25f);
                Debug.Log(other.tag);
                break;
            case "3":
                SimpleCarController.GetAccelerationInput(2, 1.75f);
                Debug.Log(other.tag);
                break;
            case "4":
                SimpleCarController.GetAccelerationInput(3, 2.25f);
                Debug.Log(other.tag);
                break;
            case "5":
                SimpleCarController.GetAccelerationInput(4, 2.75f);
                Debug.Log(other.tag);
                break;
            default:
                Debug.Log("wrong choice");
                break;
        }
    }
}
