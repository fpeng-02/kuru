using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    void OnTriggerStay2D(Collider2D col)
    {
        Entity tempEntity = col.GetComponent<Entity>();

        // if you hit an entity that isn't yourself, apply effects
        if (tempEntity != null && tempEntity.getEntityName() != owner.getEntityName()) {
            Debug.Log("hurt something");
            tempEntity.setHitPoints(tempEntity.getHitPoints() - damage);
        }

        // if hit environment, destroy and short circuit if so no null pointer exception
        // if hit entity that is not owner, destroy (this is after applying effects to said entity)
        if (tempEntity == null || tempEntity.getEntityName() != owner.getEntityName()) {
            Destroy(this.gameObject);
        }
    }

    public override Vector3 getDirVector() { return this.initialDir; }
}
