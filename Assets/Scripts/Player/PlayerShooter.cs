using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    float bulletSpeed = Constants.BULLET_SPEED;

    private float timeToShoot = 0;
    void Update ()
    {
        if (GameManager.IsGamePaused) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (Constants.PLAYER_SHOOT_DELAY < timeToShoot)
            {
                Shoot(Constants.PLAYER_SHOOT_DAMAGE, Vector2.up);
                timeToShoot = 0;
            }
        }
        timeToShoot += Time.deltaTime;
    }

    void Shoot (float damage, Vector2 direction)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.Shoot(LayerMask.GetMask("Invader"), damage, direction, bulletSpeed);
    }
}
