using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSingleShot : AbilitySequence
{
    [SerializeField] private GameObject bullet;

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    public Quaternion getSpawnAngle()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPos.z = 0.0F;
        Vector3 dirVector = playerPos - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public override IEnumerator cast()
    {
        Vector3 spawnVector = getSpawnVector();
        Quaternion spawnAngle = getSpawnAngle();
        GameObject child = (GameObject)Instantiate(bullet, spawnVector, spawnAngle);
        child.GetComponent<Projectile>().setOwner(this.transform.parent.gameObject.GetComponent<Entity>());
        child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
        child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
        yield return null;
    }
}
