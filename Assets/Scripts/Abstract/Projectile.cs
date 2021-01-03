using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeSpan;
    [SerializeField] protected private float speed;
    protected List<Effect> effectList = new List<Effect>();
    protected Vector3 initialDir;
    protected Quaternion quat;
    protected Rigidbody2D rb2d;
    protected Entity owner;

    public void setOwner(Entity owner) { this.owner = owner; }
    public float getLifeSpan() { return this.lifeSpan; }
    public float getSpeed() { return this.speed; }
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

    public virtual void OnTriggerStay2D(Collider2D col)
    {
        //

        Entity target = col.gameObject.GetComponent<Entity>();
        Projectile testProjectile = col.GetComponent<Projectile>();
        bool hitEnvironment = col.gameObject.layer == LayerMask.NameToLayer("Environment");
        bool hitSelf = target != null && target.getEntityName() == owner.getEntityName();
        bool hitProjectile = testProjectile != null;
        bool hitOtherEntity = target != null && target.getEntityName() != owner.getEntityName();

        // if you hit an entity that isn't yourself, apply effects and destroy
        // if the target is invuln, don't; just pass through it
        if (hitOtherEntity) {
            if (target.getInvulnerable()) {
                Debug.Log("projectile tried to hurt invuln thing");
                return;
            }
            foreach (Effect effect in effectList) {
                effect.applyEffect(target);
            }
            Destroy(this.gameObject);
        }

        // if didn't hit projectile or entity, it hit the environment--destroy
        // projectile hitting other projectiles would be implemented here maybe?
        if (hitEnvironment) {
            Destroy(this.gameObject);
        }
    }

    //We should add a method doing movement (virtual) that we call in fixedupdate
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.transform.position + getQuaternion() * Vector3.right * speed * Time.deltaTime);
    }

}
