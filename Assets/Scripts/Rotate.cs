using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool spin = true;
    public bool clockwise = true;
    public float rotationspeed;
    // Update is called once per frame
    void Update()
    {
        if(spin){
            if(!clockwise) transform.Rotate(new Vector3(0,0,90*Time.deltaTime*rotationspeed));
            else transform.Rotate(new Vector3(0,0,-90*Time.deltaTime*rotationspeed));
        }
    }
}
