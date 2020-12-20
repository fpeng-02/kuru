using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private double hitPoints;
    private readonly string name; 

    public void setHitPoints(double hp) { hitPoints = hp; }
    public double getHitPoints() { return hitPoints; }
    public string getName() { return name; }

    public void move()
    {

    }

    public void cast(Ability ability)
    {
        
    }
}
