using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase1 : Phase
{
    [SerializeField] private float playerSafeRadius;
    [SerializeField] private float interval;
    [SerializeField] private float waitAfterTeleport;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float yLowerBound;

    public override IEnumerator beginPhase()
    {
        // animations maybe
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            yield return new WaitForSeconds(interval);

            // choose a random position to teleport to, but make sure it's far enough away from the player
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Vector3 randomPos = playerPos;
            while ((randomPos - playerPos).magnitude < playerSafeRadius) {
                randomPos = new Vector3(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound), 0);
            }
            this.transform.position = randomPos;
            yield return new WaitForSeconds(waitAfterTeleport);
            // then shoot a shotgun
            owner.cast("Shotgun");
        }
    }
}
