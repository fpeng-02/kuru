using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBRadialShotgun : AbilitySequence
{
    [SerializeField] private int number;
    [SerializeField] private GameObject bullet;

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    public override IEnumerator cast()
    {

        for (int i = 0; i < number; i++) {
            Vector3 spawnVector = getSpawnVector();
            Quaternion spawnAngle = Quaternion.Euler(0, 0, i * (360 / number));
            GameObject child = (GameObject)Instantiate(bullet, spawnVector, spawnAngle);
            child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
            child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
            child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
        }
        yield return null;
    }
}
