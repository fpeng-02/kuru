using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStopShotgun: AbilitySequence
{
    [SerializeField] private float interval;
    [SerializeField] private GameObject bullet;
    private int numShots = 12;
    private int numShotguns;
    private Sprite currSprite;
    private bool offset = false;

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
        if (offset) {
            return Quaternion.Euler(0, 0, index * (360 / numShots)+ (180/numShots));
        }
        else
        {
            return Quaternion.Euler(0, 0, index * (360 / numShots));

        }
    }

    public override IEnumerator cast()
    {
        Debug.Log("shotgun firing");
        numShotguns = this.gameObject.GetComponent<SquarePhase>().getRollNumber() + 1;
        currSprite = this.gameObject.GetComponent<SquarePhase>().getSprite();
        for (int j = 0; j <numShotguns; j++)
        {
            for (int i = 0; i < numShots; i++)
            {
                Vector3 spawnVector = getSpawnVector();
                Quaternion spawnAngle = getSpawnAngle(i);
                GameObject child = (GameObject)Instantiate(bullet, spawnVector, spawnAngle);
                child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
                child.GetComponent<Projectile>().initializeDirVector(spawnVector - this.transform.position);
                child.GetComponent<Projectile>().initializeQuaternion(spawnAngle);
                child.GetComponent<SpriteRenderer>().sprite = currSprite;
                child.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 1f);
            }
            yield return new WaitForSeconds(interval);
            offset = !offset;
        }
        yield return null;
    }
}