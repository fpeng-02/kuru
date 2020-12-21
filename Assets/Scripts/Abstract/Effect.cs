using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected Entity owner;
    public void setOwner(Entity owner) { this.owner = owner; }
    [SerializeField] private readonly string effectName;
    [SerializeField] private readonly string effectDescription;

    public string getEffectName() { return this.effectName; }
    public string getEffectDescription() { return this.effectDescription; }

    public abstract void applyEffect(Entity target);
}
