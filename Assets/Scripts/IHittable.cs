using System;

interface IHittable
{
    event Action<HittableType> OnDeadEvent;

    float HitPoints { get; set; }
    void TakeDamage(float damage);
    void SetDefaultSettings();
}

public enum HittableType
{
    Player,
    SimpleInvader,
    ShootingInvader,
    MotherInvader
}

