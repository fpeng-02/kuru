using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeSpan;
    [SerializeField] protected float damage;
    [SerializeField] protected private float speed;
    [SerializeField] protected List<Effect> effectList;
    protected Vector3 initialDir;
    protected Quaternion quat;
    protected Rigidbody2D rb2d;
    protected Entity owner;

    public void setOwner(Entity owner) { this.owner = owner; }
    public float getLifeSpan() { return this.lifeSpan; }
    public float getDamage() { return this.damage; }
    public float getSpeed() { return this.speed; }
    public void setDamage(float damage) { this.damage = damage; }
    public void setSpeed(float speed) { this.speed = speed; }

    public abstract Vector3 getDirVector();
    public void initializeDirVector(Vector3 initialDir) { this.initialDir = initialDir; }
    public abstract Quaternion getQuaternion();
    public void initializeQuaternion(Quaternion quat) { this.quat = quat; }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponents<Effect>(effectList);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Entity target = col.gameObject.GetComponent<Entity>();
        Projectile hitProjectile = col.GetComponent<Projectile>();

        // if you hit an entity that isn't yourself, apply effects
        if (target != null && target.getEntityName() != owner.getEntityName())
        {
            foreach (Effect effect in effectList) {
                effect.applyEffect(target);
            }
        }

        // if hit environment, destroy and short circuit if so no null pointer exception
        // if hit entity that is not owner, destroy (this is after applying effects to said entity)
        if (hitProjectile == null && (target == null || target.getEntityName() != owner.getEntityName()))
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.transform.position + getQuaternion() * Vector3.right * speed * Time.deltaTime);
    }

}
