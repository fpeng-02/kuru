using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Projectile
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float interval;
    [SerializeField] private int count;

    public void callStart() { StartCoroutine("startShooting"); }

    // every interval seconds, shoot a bullet from each side of the bee. do this until the bee is destroyed.
    public IEnumerator startShooting()
    {
        GameObject go;
        while (true) {
            yield return new WaitForSeconds(interval);
            for (int i = 0; i < count; i++) {
                go = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, i * 360 / count));
                Projectile beeProj = go.GetComponent<Projectile>();
                beeProj.initializeQuaternion(Quaternion.Euler(0, 0, i * 360 / count));
                beeProj.setOwner(owner);
            }
            /*
            go = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, this.transform.eulerAngles.z + 90));
            go.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, this.transform.eulerAngles.z + 90));
            go.GetComponent<Projectile>().setOwner(this.owner);

            go = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, this.transform.eulerAngles.z - 90));
            go.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, this.transform.eulerAngles.z - 90));
            go.GetComponent<Projectile>().setOwner(this.owner);*/
        }
    }

    /*
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponents<Effect>(effectList);
        StartCoroutine("startShooting");
    }*/

    public override Vector3 getDirVector() { return this.initialDir; }
    public override Quaternion getQuaternion() { return this.quat; }
}
