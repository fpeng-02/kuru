using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleShot : AbilitySequence
{
    [SerializeField] private GameObject bullet;

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    public Quaternion getSpawnAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.0F;
        Vector3 dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
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
        child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
        child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
        child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
        yield return null;
    }
}
