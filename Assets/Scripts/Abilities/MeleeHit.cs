using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : Projectile
{
    void OnTriggerStay2D(Collider2D col)
    {
        Entity tempEntity = col.GetComponent<Entity>();
        if (tempEntity != null && tempEntity.getEntityName() != owner.getEntityName()) {
            Debug.Log("hurt something");
            tempEntity.setHitPoints(tempEntity.getHitPoints() - damage);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) {
            Destroy(this.gameObject);
        }
    }
}
