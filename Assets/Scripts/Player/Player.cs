using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]
[RequireComponent(typeof (SpriteRenderer))]
public class Player : MonoBehaviour, IHittable
{
    public event Action<HittableType> OnDeadEvent;
    public event Action<float> OnHitpointsChanged;
    public float HitPoints
    {
        get { return hitPoints; }
        set
        {
            if (value <= 0)
            {
                ShowObject(false);
                if (OnDeadEvent != null)
                    OnDeadEvent(HittableType.Player);
            }
            

            if (value != hitPoints)
            {
                hitPoints = value < 0 ? 0 : value;

                if (OnHitpointsChanged != null)
                    OnHitpointsChanged(value);
            }

        }
    }

    [SerializeField] SpriteRenderer selfSprite;
    [SerializeField] Collider2D selfCollider;

    private float hitPoints = 0;


    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
    }
    

    public void SetDefaultSettings()
    {
        HitPoints = Constants.MAX_PLAYER_LIFES;
        ShowObject(true);
    }

    private void Start()
    {
        HitPoints = Constants.MAX_PLAYER_LIFES;
    }

    private void ShowObject(bool value)
    {
        selfSprite.enabled = value;
        selfCollider.enabled = value;
    }
}

