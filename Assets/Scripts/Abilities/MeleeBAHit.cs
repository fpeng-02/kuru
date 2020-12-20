using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBAHit : MonoBehaviour
{

    private Entity owner;
    [SerializeField] private float lifespan;
    [SerializeField] private float damage = 2;
    void OnTriggerStay2D(Collider2D col)
    {
        Entity tempEntity = col.GetComponent<Entity>();
        if (tempEntity != null && tempEntity.getEntityName() != owner.getEntityName()) {
            Debug.Log("hurt something");
            tempEntity.setHitPoints(tempEntity.getHitPoints() - damage);
            Destroy(this.gameObject);
        }
    }

    public void setOwner(Entity owner)
    {
        this.owner = owner;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0) {
            Destroy(this.gameObject);
        }
    }
}
