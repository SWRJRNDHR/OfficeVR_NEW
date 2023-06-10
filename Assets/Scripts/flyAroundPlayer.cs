using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Makes teh flies fly around players hear. Added some veriations to their transfor, to make it look like it is actually flying. 
 Not using this feature in final project.
 */

public class flyAroundPlayer : MonoBehaviour
{

    public float speed;
    private GameObject mainCamera;
    float txPos, tyPos, tzPos,cxPos,cyPos, czPos;
    public Vector3 Target;
    public Vector3 currentPos;
    float i = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CameraPoint");
    }


   /* Update function calls the chase method which makes fly to go towards players position slowly. As fly is going towards players head position, keeps changing the location of fly  just a tiny bit. If its more than 0.5, it resets it. 
    This was just to make it look line fly is actually flying towards the player.
    */
    // Update is called once per frame
    void Update()
    { 
        if (i > 0.05f) {
            i = 0.02f;
        }

        cxPos = this.transform.position.x;
        cyPos = this.transform.position.y + i;
        czPos = this.transform.position.z + i;
        i += .001f;

        currentPos = new Vector3(cxPos, cyPos, czPos);


        txPos = mainCamera.transform.position.x;
        tyPos = mainCamera.transform.position.y;
        tzPos = mainCamera.transform.position.z+1;
        

        Target = new Vector3(txPos,tyPos,tzPos); 
        Chase();

    }

    void Chase()
    {
        transform.position = Vector3.MoveTowards(currentPos, Target, speed * Time.deltaTime);

    }

}
