using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStrike : MonoBehaviour
{
    [SerializeField] private GameObject fallingObjectPrefab;
    [SerializeField] private float impactDelay;
    private Collider2D fallingObjectCollider;
    private GameObject fallingObject;
    private Entity caster;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite meteorWarning;
    [SerializeField] private Sprite bossWarning;

    public void setCaster(Entity caster) { this.caster = caster; }

    public void boss(GameObject boss)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = bossWarning;
        // should we do something where we spawn in a dummy sprite of the boss instead of moving the boss directly?
        boss.transform.position = this.transform.position + new Vector3(-10f, 10f, 0f);
        Polygon polygonBoss = boss.GetComponent<Polygon>();
        polygonBoss.setFalling(true);
        polygonBoss.quat = Quaternion.Euler(0, 0, -45);
        polygonBoss.meteorStrikePosition = this.transform.position;
        polygonBoss.setSpeed(15);
        polygonBoss.spawner = this.gameObject;
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
