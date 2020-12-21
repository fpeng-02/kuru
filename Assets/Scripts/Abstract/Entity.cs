using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private double hitPoints;
    [SerializeField] private string entityName;
    [SerializeField] public List<GenericAbility> abilities;
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    

    public void setHitPoints(double hp) { hitPoints = hp; }
    public double getHitPoints() { return hitPoints; }
    public string getEntityName() { return entityName; }

    public abstract void move();

    /// <summary>
    /// Use this signature for bosses; it's more likely you'll need exact names for abilities for them
    /// </summary>
    /// <param name="abilityName"></param>
    public void cast(string abilityName)
    {
        foreach (GenericAbility ability in abilities) {
            if (ability.getAbilityName() == abilityName) {
                GenericAbility toCast = (GenericAbility)Instantiate(ability, this.transform);
                StartCoroutine(toCast.cast());
            }
        }
    }

    /// <summary>
    /// Use this signature for players; it's more likely that a key is mapped to an index instead of an ability name
    /// </summary>
    /// <param name="index"></param>
    public void cast(int index)
    {
        // check CDs here 
        if (!cooldownTimers.ContainsKey(abilities[index].getAbilityName())) {
            GenericAbility toCast = (GenericAbility)Instantiate(abilities[index], this.transform);
            StartCoroutine(toCast.cast());
            cooldownTimers.Add(abilities[index].getAbilityName(), toCast.getCooldown());
        }
        // TODO: cooldown UI?
    }

    protected void updateCooldowns()
    {
        Dictionary<string, float> tmpCooldownTimers = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> timer in cooldownTimers) {
            if (timer.Value - Time.deltaTime < 0) {
                continue;
            }
            tmpCooldownTimers.Add(timer.Key, timer.Value - Time.deltaTime);
        }
        cooldownTimers = tmpCooldownTimers;
    }
}
