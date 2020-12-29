using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Projectile
{
    [SerializeField] private GameObject shard;
    [SerializeField] private int count;
    private Vector3 targetPos;
    private CircleCollider2D col;
    private GameObject spawner;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    public void setTargetPos(Vector3 targetPos) { this.targetPos = targetPos; }
    public void setSpawner(GameObject spawner) { this.spawner = spawner; }

    // splits the meteor into count shards radially, then destroys itself
    public void split()
    {
        Destroy(spawner);
        col.enabled = true;
        for (int i = 0; i < count; i++) {
            GameObject go = Instantiate(shard, this.transform.position, Quaternion.identity);
            go.GetComponent<Projectile>().setOwner(owner);
            go.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, i * 360f / count));
        }
        Destroy(this.gameObject);
    }

    public override Vector3 getDirVector() { return this.initialDir; }
    public override Quaternion getQuaternion() { return this.quat; }

    void FixedUpdate()
    {
       rb2d.MovePosition(rb2d.transform.position + getQuaternion() * Vector3.right * speed * Time.deltaTime);
        if ((this.transform.position - targetPos).magnitude <= 0.1) {
            split();
        }
    }
}
