using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTriLaser : AbilitySequence
{
    [SerializeField] private GameObject laserSpawner;

    public override IEnumerator cast()
    {
        for (int i = 0; i > 3; i++)
        {
            GameObject laser = Instantiate(laserSpawner, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 120*i), this.gameObject.transform);
        }
        yield return null;
    }
}
