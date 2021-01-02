using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStrike : MonoBehaviour
{
    [SerializeField] private GameObject fallingObjectPrefab;
    [SerializeField] private GameObject bossPlaceholder;

    [SerializeField] private float impactDelay;
    [SerializeField] private float damageInterval;

    private GameObject fallingObject;
    private Entity caster;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite meteorWarning;
    [SerializeField] private Sprite bossWarning;


    public void setCaster(Entity caster) { this.caster = caster; }

    public IEnumerator bossStrike()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = bossWarning;
        // should we do something where we spawn in a dummy sprite of the boss instead of moving the boss directly?
        GameObject bossPlaceHolder = Instantiate(bossPlaceholder, this.transform.position + new Vector3(-10f, 10f, 0f), Quaternion.Euler(0,0,0));
        BossPlaceholder polygonBoss = bossPlaceHolder.GetComponent<BossPlaceholder>();
        polygonBoss.startPos = this.transform.position + new Vector3(-10f, 10f, 0f);
        polygonBoss.endPos = transform.position;
        polygonBoss.timeToDestination = impactDelay;
        yield return new WaitForSeconds(impactDelay);
        spriteRenderer.sprite = null;
        yield return new WaitForSeconds(damageInterval);
        GameObject.FindGameObjectWithTag("Boss").transform.position = new Vector3(-20f, -20f, 0);
    }

    public void meteor()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = meteorWarning;
        fallingObject = (GameObject)Instantiate(fallingObjectPrefab, this.transform.position + new Vector3(-10f, 10f, 0f), Quaternion.identity);
        fallingObject.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, -45));
        fallingObject.GetComponent<Projectile>().setOwner(caster);
        fallingObject.GetComponent<Meteor>().setTargetPos(this.transform.position);
        fallingObject.GetComponent<Meteor>().setSpawner(this.gameObject);
    }
}
