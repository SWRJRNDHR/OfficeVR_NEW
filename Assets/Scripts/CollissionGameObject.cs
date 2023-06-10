using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionGameObject : MonoBehaviour
{


    /* Stop the telephone ring when headset is picked up. Basically checks for the collission of hands with telephone headset. 
     If colliding, then stop ringing. */

    public GameObject telephoneRing;
    public bool alreadyPickedUp;

    public void Start()
    {
        alreadyPickedUp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Right Hand" || other.gameObject.tag == "Left Hand")
        {
            telephoneRing.SetActive(false);
            alreadyPickedUp = true;
        }        
    }
}
