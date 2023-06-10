using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/*It conly enables the ray for interacting with the instruction screen(Canvas). Other times it stays disabled. */

public class enableRayInteractor : MonoBehaviour
{


    public XRBaseInteractor rayInteractor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Canvas"))
        {
            rayInteractor.enabled = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
