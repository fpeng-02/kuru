using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite warningSprite;
    [SerializeField] private Sprite lavaSprite;
    [SerializeField] private Sprite highlightSprite;

    [SerializeField] private float warningTime;
    [SerializeField] private float lavaTime;
    [SerializeField] private bool permanentDisable;
    private FloorState state;
    private SpriteRenderer sR;
    private bool highlight;

    public FloorState getState() { return this.state; }

    // Start is called before the first frame update
    void Start()
    {
        permanentDisable = false;
        sR = this.GetComponent<SpriteRenderer>();
        setFloor();
    }

    public void setPermanentDisable(bool permanentDisable) { this.permanentDisable = permanentDisable; }
    public void setWarningTime(float warningTime) { this.warningTime = warningTime; }
    public void setLavaTime(float lavaTime) { this.lavaTime = lavaTime; }

    public void setFloor() { sR.sprite = highlight ? highlightSprite : floorSprite; state = FloorState.Floor; }
    public void setWarning() { StartCoroutine("warning"); }
    public void setLava() { StartCoroutine("lava"); }
    public void setExtendedLava() { StartCoroutine("extendedLava"); }
    public void setPermanentLava() { sR.sprite = lavaSprite; state = FloorState.Lava; }
    //public void setHighlight() { sR.sprite = highlightSprite; state = FloorState.Highlight; }
    public void setHighlight(bool highlight) { this.highlight = highlight; }

    public IEnumerator warning()
    {
        sR.sprite = warningSprite;
        state = FloorState.Warning;
        yield return new WaitForSeconds(warningTime);
        if (permanentDisable) {
            setPermanentLava();
        } else {
            yield return lava();
        }
    }

    public IEnumerator lava()
    {
        sR.sprite = lavaSprite;
        state = FloorState.Lava;
        yield return new WaitForSeconds(lavaTime);
        this.setFloor();
        yield return null;
    }

    public IEnumerator extendedLava()
    {
        sR.sprite = lavaSprite;
        state = FloorState.Lava;
        yield return new WaitForSeconds(lavaTime + warningTime);
        this.setFloor();
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (state == FloorState.Floor || state == FloorState.Highlight) {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
                StartCoroutine("setWarning");
            }
        }
    }
    
}
