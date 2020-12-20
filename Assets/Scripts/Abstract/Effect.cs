using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    [SerializeField] private readonly string effectName;
    [SerializeField] private readonly string effectDescription;

    public string getEffectName() { return this.effectName; }
    public string getEffectDescription() { return this.effectDescription; }

    public abstract void applyEffect(Entity target);
}
