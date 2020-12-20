using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Entity : MonoBehaviour
{
    private double hitPoints;

    public void setHitPoints(double hp)
    {
        hitPoints = hp;
    }
    public double getHitPoints()
    {
        return hitPoints;
    }
    public void move()
    {
    }
    public void cast(ICastable ability)
    {

    }
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
