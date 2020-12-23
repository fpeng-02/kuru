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

    void OnTriggerEnter2D(Collider2D col)
    {
        Entity target = col.gameObject.GetComponent<Entity>();
        Projectile testProjectile = col.GetComponent<Projectile>();
        bool hitSelf = target != null && target.getEntityName() == owner.getEntityName();
        bool hitProjectile = testProjectile != null;
        bool hitOtherEntity = target != null && target.getEntityName() != owner.getEntityName();

        // if you hit an entity that isn't yourself, apply effects and destroy
        // if the target is invuln, don't; just pass through it
        if (hitOtherEntity) {
            if (target.getInvulnerable()) {
                return;
            }
            foreach (Effect effect in effectList) {
                effect.applyEffect(target);
            }
            Destroy(this.gameObject);
        }

        // if didn't hit projectile or entity, it hit the environment--destroy
        // projectile hitting other projectiles would be implemented here maybe?
        if (!hitProjectile && (target == null || target.getEntityName() != owner.getEntityName())) {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.transform.position + getQuaternion() * Vector3.right * speed * Time.deltaTime);
    }

}
