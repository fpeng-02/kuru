using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailblazer : Entity
{
    void testCast()
    {
        this.cast("Pound Player Region");
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("testCast", 1.0f, 5.0f);       
    }
}
