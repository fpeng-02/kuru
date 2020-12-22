    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShotgun : AbilitySequence
{
    [SerializeField] private List<float> spawnAngleOffsets;
    [SerializeField] private GameObject bullet;

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
        return Quaternion.Euler(0, 0, (angle) + spawnAngleOffsets[index]);
    }

    public override IEnumerator cast()
    {
        for (int i = 0; i < spawnAngleOffsets.Count; i++) {
            Vector3 spawnVector = getSpawnVector();
            Quaternion spawnAngle = getSpawnAngle(i);
            GameObject child = (GameObject)Instantiate(bullet, spawnVector, spawnAngle);
            child.GetComponent<Projectile>().setOwner(this.transform.parent.gameObject.GetComponent<Entity>());
            child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
            child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
        }
        yield return null;
    }
}
