using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private double hitPoints;
    [SerializeField] private readonly string entityName; 

    public void setHitPoints(double hp) { hitPoints = hp; }
    public double getHitPoints() { return hitPoints; }
    public string getEntityName() { return entityName; }

    public void move()
    {

    }

    public void cast(Ability ability)
    {
        
    }
}
