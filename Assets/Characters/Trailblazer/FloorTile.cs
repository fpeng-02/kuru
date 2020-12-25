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
    private FloorState state;

    private SpriteRenderer sR;

    public FloorState getState() { return this.state; }

    // Start is called before the first frame update
    void Start()
    {
        sR = this.GetComponent<SpriteRenderer>();
        setFloor();
    }

    public void setWarningTime(float warningTime) { this.warningTime = warningTime; }
    public void setLavaTime(float lavaTime) { this.lavaTime = lavaTime; }

    public void setFloor()
    {
        sR.sprite = floorSprite;
        state = FloorState.Floor;
    }

    public IEnumerator setWarning()
    {
        sR.sprite = warningSprite;
        state = FloorState.Warning;
        yield return new WaitForSeconds(warningTime);
        yield return setLava();
    }

    public IEnumerator setLava()
    {
        sR.sprite = lavaSprite;
        state = FloorState.Lava;
        yield return new WaitForSeconds(lavaTime);
        this.setFloor();
        yield return null;
    }

    public void setHighlight()
    {
        sR.sprite = highlightSprite;
        state = FloorState.Highlight;
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
