using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private double hitPoints;
    [SerializeField] private string entityName;
    [SerializeField] public List<GenericAbility> abilities;

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
        GenericAbility toCast = (GenericAbility)Instantiate(abilities[index], this.transform);
        StartCoroutine(toCast.cast());
    }
}
