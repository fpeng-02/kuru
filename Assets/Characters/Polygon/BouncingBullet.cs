using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : MonoBehaviour
{
    [SerializeField] private int bounces;
    [SerializeField] private float speed;
    private int bounceCount = 0;
    protected List<Effect> effectList = new List<Effect>();
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponents<Effect>(effectList);
    }

    void FixedUpdate()
    {
        // move the bullet
        rb2d.MovePosition(rb2d.transform.position + this.transform.rotation * Vector3.right * speed * Time.deltaTime);
        // raycast ahead of the bullet to see if it hits anything
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.rotation * Vector2.right, Time.deltaTime * speed + 0.1f, LayerMask.GetMask("Environment", "Player", "Entity"));
        if (hit.collider != null && hit.transform != this.transform) {
            // if it hits a wall, bounce
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Environment")) {
                Vector2 reflectDir = Vector2.Reflect(this.transform.rotation * Vector2.right, hit.normal);
                float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, rot);
                bounceCount++;
                if (bounceCount == bounces) {
                    Destroy(this.gameObject);
                }
            }
            // if it hits a player, apply effects
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Entity")) {
                Entity target = hit.transform.gameObject.GetComponent<Entity>();
                if (target.getInvulnerable()) {
                    return;
                }
                foreach (Effect effect in effectList) {
                    effect.applyEffect(target);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
