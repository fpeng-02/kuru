using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySequence : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private string abilityName;
    [SerializeField] private float uptime;
    protected Entity caster;
    public void setCaster(Entity caster) { this.caster = caster; }
    public string getAbilityName() { return this.abilityName; }
    public float getCooldown() { return this.cooldown; }
    public float getUptime() { return this.uptime; }
    public abstract IEnumerator cast();
}
