using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;

public abstract class Effect: MonoBehaviour
{
    private readonly string effectName;
    private readonly string effectDescription;

    public string getEffectName() { return this.effectName; }
    public string getEffectDescription() { return this.effectDescription; }

    public abstract void applyEffect(Entity target);   
}
