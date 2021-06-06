using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageOnCollision : MonoBehaviour
{
    public float noDamageTimeFrame = 1;
    public float rebound = 1;
    float lastDamageTime = -2;
    private void Reset(){
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag == "Player" && ((lastDamageTime+noDamageTimeFrame)<Time.time)){
            collision.gameObject.GetComponent<LifeCount>().LoseLife();
            lastDamageTime = Time.time;

            Vector3 otherPosition = collision.gameObject.GetComponent<Transform>().position;
            Vector3 direction = otherPosition-gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x*rebound,direction.y*rebound);
        }
    }
}
