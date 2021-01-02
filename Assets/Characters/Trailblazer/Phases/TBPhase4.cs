using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBPhase4 : Phase
{
    [SerializeField] private float survivalTime;
    [SerializeField] private float interval;
    [SerializeField] private GameObject timer; // might be a cooler GameObject if we make the timer actually look good 
    private Text timerText;
    private bool active = false;

    public override IEnumerator beginPhase()
    {
        timerText = timer.GetComponent<Text>();
        timer.SetActive(true);
        // go back to the center
        owner.setInvulnerable(true);
        this.transform.position = new Vector3(0, 0, 0);
        // reset all tiles, set them to permanently disappear
        // mark the whole floor for permanent destruction, then interrupt the states of the island tiles
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
            // delay
            owner.cast("Bomb");
            yield return new WaitForSeconds(interval);
            // laser!!

            owner.cast("Ring");
        }
    }

    public override void exitPhase()
    {
        base.exitPhase();
        active = false;
        timer.SetActive(false);
        owner.setInvulnerable(false);
    }

    void Update()
    {
        if (active) {
            timerText.text = string.Format("{0:##.##}", survivalTime);
            survivalTime -= Time.deltaTime;
            if (survivalTime < 0) {
                active = false;
                Trailblazer boss = (Trailblazer)owner;
                boss.nextPhase();
            }
        }
    }
}
