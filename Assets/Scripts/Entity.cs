using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Entity : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
