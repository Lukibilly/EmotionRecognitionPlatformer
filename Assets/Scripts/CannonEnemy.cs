using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    public float movingSpeed = 20f;
    public float shootFrequency = 2f;
    public float totalAliveTime = 10f;
    private float nextShot = -1;
    private float direction = -1;
    private bool moves = true;
    
    private float aliveStart;
    void Start()
    {
        aliveStart = Time.time;
        Physics2D.IgnoreLayerCollision(7,9);
        Physics2D.IgnoreLayerCollision(8,9);
        Physics2D.IgnoreLayerCollision(9,9);
        transform.localScale = new Vector3(direction,1,1);   
    }
    void Update()
    {
        if(nextShot==-1){
            nextShot=Time.time+shootFrequency;
        }else if(nextShot<Time.time){
            Shoot();
            nextShot = Time.time+shootFrequency;
        }
        if(transform.position.y<-10 || (Time.time-aliveStart)>totalAliveTime){
            Destroy(gameObject);
        }
        
    }
    void FixedUpdate(){
        if(moves) move();
    }
    void move(){
        float xvel = movingSpeed*Time.fixedDeltaTime*direction;
        rb.velocity = new Vector2(xvel,rb.velocity.y);
    }
    void Shoot(){
        var bullet = Instantiate(bulletPrefab,firePoint.position,transform.rotation);
        bullet.GetComponent<Bullet>().direction = direction;
        bullet.transform.localScale = new Vector3(direction*bullet.transform.localScale.x,bullet.transform.localScale.y,bullet.transform.localScale.z);
    }
    public void setDirection(float direction){
        this.direction = direction;
        transform.localScale = new Vector3(direction,1,1);
    }
    public void setAliveTime(float aliveTime){
        this.totalAliveTime = aliveTime;
    }
    public void setShootFrequency(float shootFrequency){
        this.shootFrequency = shootFrequency;
    }

    public void setMoves(bool moves){
        this.moves = moves;
    }
}
