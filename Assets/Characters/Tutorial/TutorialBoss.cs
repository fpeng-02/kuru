using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : Entity
{
    private List<Phase> phases = new List<Phase>();
    [SerializeField] private List<float> phaseThresholds;
    private int currentPhase;
    private bool revived = false;
    private Vector3 dirVector = new Vector3(0, 0, 0);
    private Rigidbody2D rb2d;

    public void setDirVector(Vector3 dirVector) { this.dirVector = dirVector; }

    private void nextPhase()
    {
        phases[currentPhase].exitPhase();
        currentPhase++;
        phases[currentPhase].init(this);
    }

    void Start()
    {
        GetComponents<Phase>(phases);
        rb2d = GetComponent<Rigidbody2D>();
        currentPhase = 0;
        phases[currentPhase].init(this);
    }
    
    protected override void checkDie()
    {
        if (hitPoints < 0) {
            if (!revived) {
                nextPhase();
                revived = true;
            } else {
                Destroy(this.gameObject);
            }
        }
    }

    public override void customUpdate()
    {
        if (hitPoints < phaseThresholds[currentPhase]) {
            nextPhase();
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.transform.position + dirVector * speed * Time.deltaTime);
    }
}
