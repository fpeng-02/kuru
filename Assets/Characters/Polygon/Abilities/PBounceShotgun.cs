using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBounceShotgun : AbilitySequence
{
    [SerializeField] private float angleOffset;
    [SerializeField] private GameObject bullet;
    private int numBullets;

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    public Quaternion getSpawnAngle(int index)
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPos.z = 0.0F;
        Vector3 dirVector = playerPos - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, (angle) - (((numBullets-1)/2)*angleOffset) + (index * angleOffset)); //- ((numBullets*angleOffset-1)/2) + index * numBullets
    }

    public override IEnumerator cast()
    {
        numBullets = this.gameObject.GetComponent<SquarePhase>().getRollNumber() + 1;
        for (int i = 0; i < numBullets; i++)
        {
            Debug.Log("Casted Bullet: " + i);
            Vector3 spawnVector = getSpawnVector();
            Quaternion spawnAngle = getSpawnAngle(i);
            GameObject child = (GameObject)Instantiate(bullet, spawnVector, spawnAngle);
            child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
            child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
            child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
        }
        yield return null;
    }
}
