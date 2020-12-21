using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeSpan;
    [SerializeField] protected float damage;
    [SerializeField] protected private float speed;
    [SerializeField] protected List<Effect> effectList;
    protected Entity owner;
    public void setOwner(Entity owner) { this.owner = owner; }
    public float getLifeSpan() { return this.lifeSpan; }
    public float getDamage() { return this.damage; }
    public float getSpeed() { return this.speed; }
    public void setDamage(float damage) { this.damage = damage; }
    public void setSpeed(float speed) { this.speed = speed; }

}
