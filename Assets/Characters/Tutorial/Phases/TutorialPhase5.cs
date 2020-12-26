using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase5 : Phase
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float reviveTo;


    public override IEnumerator beginPhase()
    {
        // turn laser off if it was active in the last phase (really janky bad solution)
        TutorialLaserAttack laserAttack = owner.GetComponent("TutorialLaserAttack") as TutorialLaserAttack;
        laserAttack.turnOff();
        owner.setHitPoints(reviveTo);
        owner.setSpeed(0);
        owner.setInvulnerable(true);
        owner.gameObject.transform.position = new Vector3(0, 0, 0);
        Debug.Log("REVIVING");
        yield return new WaitForSeconds(3.0f);
        owner.setInvulnerable(false);
        // Animations would go here
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // small dash, then pause
            Quaternion randDir = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            TutorialBoss boss = (TutorialBoss)owner;
            boss.setDirVector(randDir * Vector3.right);
            boss.setSpeed(dashSpeed);
            yield return new WaitForSeconds(0.2f);
            boss.setSpeed(0);
            yield return new WaitForSeconds(0.5f);

            // attack
            owner.cast("Tutorial Delayed Strike");
            owner.cast("Tutorial Laser Attack");
            owner.cast("Tutorial 5 Shotgun");
            yield return new WaitForSeconds(2.2f);
            owner.cast("Tutorial 5 Shotgun");
        }
    }
}
