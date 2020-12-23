using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Effect
{
    [SerializeField] private float damage;

    public override void applyEffect(Entity target)
    {
        target.setHitPoints(target.getHitPoints() - damage);
    }
}
