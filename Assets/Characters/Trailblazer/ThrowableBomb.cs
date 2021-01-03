using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBomb : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float throwTime;
    [SerializeField] private float explodeDelay;
    private Entity owner;

    public void setTargetPos(Vector3 targetPos) { this.targetPos = targetPos; }
    public void setOwner(Entity owner) { this.owner = owner; }

    public IEnumerator throwBomb()
    {
        Vector3 currentPos = this.transform.position;
        float t = 0f;
        while (t < 1) {
            t += Time.deltaTime / throwTime;
            transform.position = Vector2.Lerp(currentPos, targetPos, t);
            this.transform.Rotate(0f, 0f, 6f);
            yield return null;
        }
        yield return explode();
    }

    public IEnumerator explode()
    {
        yield return new WaitForSeconds(explodeDelay);
        GameObject go = (GameObject)Instantiate(explosion, this.transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().setOwner(owner);
        Destroy(this.gameObject);
    }
}
