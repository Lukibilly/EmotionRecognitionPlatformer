using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageOnCollision : MonoBehaviour
{
    public float rebound = 1;

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.tag == "Player"){
            float lastDamageTime = collision.gameObject.GetComponent<LifeCount>().lastDamageTime;
            float noDamageTimeFrame = collision.gameObject.GetComponent<LifeCount>().noDamageTimeFrame;
            collision.gameObject.GetComponent<LifeCount>().LoseLife();

           /* Vector3 otherPosition = collision.gameObject.GetComponent<Transform>().position;
            otherPosition = otherPosition.normalized;
            Vector3 direction = otherPosition-gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x*rebound,direction.y*rebound);*/
        }
    }
}
