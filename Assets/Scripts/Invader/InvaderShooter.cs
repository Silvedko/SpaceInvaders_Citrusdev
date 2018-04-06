using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShooter : MonoBehaviour
{
    [HideInInspector]
    public bool StopShooting = false;
    Bullet bulletPrefab;

    private float bulletSpeed = Constants.BULLET_SPEED;
    private float shootDelay = Constants.INVADER_SHOOTDELAY; 
    private float timeToShoot = 0;
    
    public void SetBulletPrefab (Bullet bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }

	void Update ()
    {
        if (GameManager.IsGamePaused) return;
        if (StopShooting) return;

        if (Constants.INVADER_SHOOTDELAY <= timeToShoot)
        {
            Shoot(Constants.INVADER_SHOOT_DAMAGE, Vector2.down);
            timeToShoot = 0;
        }

        timeToShoot += Time.deltaTime;
    }
	
	void Shoot (float damage, Vector2 direction)
    {
        var bullet = Instantiate (bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.Shoot(LayerMask.GetMask("Player"), damage, direction, bulletSpeed);
    }
}
