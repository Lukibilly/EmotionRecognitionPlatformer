using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float direction;
    public float maxAliveTime = 20f;
    private float birthTime;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        rb.velocity = transform.right * speed * direction;       
        birthTime = Time.time; 
        Physics2D.IgnoreLayerCollision(8,10);
        Physics2D.IgnoreLayerCollision(6,10);
    }
    void FixedUpdate(){
        if(birthTime+maxAliveTime<Time.time) Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D collider){
        if(collider.tag == "Player"){
            collider.gameObject.GetComponent<LifeCount>().LoseLife();
        }
        if(collider.tag != "Cannon") Destroy(gameObject);
    }
}
