using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]    
public class ExpandingRing : MonoBehaviour
{
    private List<Effect> effectList = new List<Effect>();
    private List<Collider2D> hits = new List<Collider2D>();
    private Entity caster;
    private LineRenderer lineRenderer;
    private CircleCollider2D circleCollider;
    [SerializeField] private float maxRadius;
    [SerializeField] private float expansionSpeed;
    [SerializeField] private float thickness;
    private Collider2D playerCol;
    private Entity playerEntity;
    private float radius;
    private int resolution = 60;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.endWidth = thickness;
        lineRenderer.startWidth = thickness;
        GetComponents<Effect>(effectList);
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0;
        radius = 0;
        Debug.Log("ring start");
        drawCircle();
    }

    public void setCaster(Entity caster) { this.caster = caster; }

    
    // "entering" the circle collider necessarily means that the border of the circle was crossed, which is the same thing as the border/ring itself doing damage
    private void OnTriggerEnter2D(Collider2D col)
    {
        Entity target = col.GetComponent<Entity>();
        if (target != null && target.getEntityName() != caster.getEntityName()) {
            if (target.getInvulnerable()) {
                return;
            }
            foreach (Effect effect in effectList) {
                effect.applyEffect(target);
            }
        }
    }

    public void drawCircle()
    {
        lineRenderer.positionCount = resolution + 1;
        float x;
        float y;
        float z = 0f;
        float angle = 0f;
        for (int i = 0; i < (resolution + 1); i++) {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, this.transform.position + new Vector3(x, y, z));
            angle += (360f / resolution);
        }
    }

    void FixedUpdate()
    {
        if (radius < maxRadius) {
            circleCollider.radius = radius;
            radius += expansionSpeed;
            drawCircle();
        } else {
            Destroy(this.gameObject);
        }
    }
}
