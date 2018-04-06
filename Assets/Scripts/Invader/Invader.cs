using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class Invader : MonoBehaviour, IHittable
{
    public HittableType hittableType = HittableType.SimpleInvader;
    public event Action<HittableType> OnDeadEvent;

    public float HitPoints
    {
        get { return hitPoints; }
        set
        {
            if (value <= 0)
            {
                ShowObject(false);
                if (OnDeadEvent != null)
                    OnDeadEvent(hittableType);
            }

            hitPoints = value < 0 ? 0 : value;
        }
    }
    private SpriteRenderer selfRenderer;
    private Collider2D selfCollider;
    private float hitPoints = Constants.INVADER_HITPOINTS;
    private Vector3 startPosition;

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
    }


    public void SetDefaultSettings()
    {
        ShowObject(true);
        transform.position = startPosition;
        HitPoints = Constants.INVADER_HITPOINTS;
    }
    private void ShowObject(bool value)
    {
        if (GetComponent<InvaderShooter>())
            GetComponent<InvaderShooter>().StopShooting = !value;

        selfRenderer.enabled = value;
        selfCollider.enabled = value;
    }


    void Start ()
    {
        startPosition = transform.position;
        selfRenderer = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();

        if(hittableType == HittableType.ShootingInvader || hittableType == HittableType.MotherInvader)
        {
            var shootingComponent = GetComponent<InvaderShooter>();
            shootingComponent = shootingComponent == null ? gameObject.AddComponent<InvaderShooter>() : shootingComponent;
            var bulletPrefab = (Bullet)Resources.Load("Bullet", typeof(Bullet));
            shootingComponent.SetBulletPrefab(bulletPrefab);
        }

    }

    
}
