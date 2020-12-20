using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private double hitPoints;
    [SerializeField] private string entityName;
    [SerializeField] protected List<Ability> abilityList = new List<Ability>();  // maybe can del later
    [SerializeField] protected List<GameObject> abilitySpawners = new List<GameObject>();

    public void setHitPoints(double hp) { hitPoints = hp; }
    public double getHitPoints() { return hitPoints; }
    public string getEntityName() { return entityName; }

    public abstract void move();

    public void cast(ICastable ability)
    {
        ability.abilityAction();
    }
}
