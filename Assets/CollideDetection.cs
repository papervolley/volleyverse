using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDetection : MonoBehaviour
{
 void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //Output the Collider's GameObject's name
        Debug.Log(collision.collider.name);
    }

    //If your GameObject keeps colliding with another GameObject with a Collider, do something
   
}
