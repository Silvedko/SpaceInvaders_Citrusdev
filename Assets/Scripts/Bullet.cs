using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private float raycastDistance = 0.2f;
    private float startTime;
    private float damage;
    private int bulletLayerMask;

    public void Shoot (int shootingLayerMask, float damage, Vector2 direction, float speed)
    {

        bulletLayerMask = shootingLayerMask;
        this.damage = damage;
        moveSpeed = speed;
        moveDirection = direction;
        startTime = Time.time;
    }


	
	void Update ()
    {

        MoveBullet();
        FindTargets();
    }

    

    private void MoveBullet ()
    {
        transform.position = Vector3.MoveTowards (  transform.position, 
                                                    new Vector2(transform.position.x, moveDirection.y * (Camera.main.orthographicSize + 1)), 
                                                    Time.deltaTime * moveSpeed);

        if (transform.position.y > Camera.main.orthographicSize  || transform.position.y < -Camera.main.orthographicSize )
        {
            //Better need move to pool
            Destroy(gameObject);           
        }
    }


    private void FindTargets ()
    {
        // Don't need use Rigidbodies
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, raycastDistance, bulletLayerMask, 0);   


        if (hit.collider != null)
        {

            //Better need move to pool
            Destroy(gameObject);

            var hittableObj = hit.collider.gameObject.GetComponent<IHittable>();

            if (hittableObj != null)
            {
                //Debug.Log("damage " + damage);
                hittableObj.TakeDamage(damage);
            }         
        }
    }
}
