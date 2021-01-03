using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBPhase4 : Phase
{
    [SerializeField] private float survivalTime;
    [SerializeField] private float interval;
    [SerializeField] private GameObject timer;
    [SerializeField] private Image timerContent;
    private bool active = false;
    private float totalSurvivalTime;

    public override IEnumerator beginPhase()
    {
        totalSurvivalTime = survivalTime;
        timer.SetActive(true);
        // go back to the center
        owner.setInvulnerable(true);
        this.transform.position = new Vector3(0, 0, 0);
        // reset all tiles, set them to permanently disappear
        List<FloorTile> allTiles = GameObject.Find("FloorTileManager").GetComponent<FloorTileManager>().getAllTiles();
        foreach (FloorTile tile in allTiles) {
            tile.StopAllCoroutines();
            tile.setPermanentDisable(true);
            tile.setFloor();
        }
        active = true;
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            owner.cast("Bomb");
            yield return new WaitForSeconds(interval);
            owner.cast("Ring");
        }
    }

    public override void exitPhase()
    {
        base.exitPhase();
        active = false;
        timer.SetActive(false);
        owner.setInvulnerable(false);
        owner.setHitPointsBypass(owner.getHitPoints() - 10);
    }

    void Update()
    {
        if (active) {
            timerContent.fillAmount = survivalTime / totalSurvivalTime;
            survivalTime -= Time.deltaTime;
            if (survivalTime < 0) {
                active = false;
                Trailblazer boss = (Trailblazer)owner;
                boss.nextPhase();
            }
        }
    }
}
