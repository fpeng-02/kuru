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
    private floorState state;

    private SpriteRenderer sR;

    // Start is called before the first frame update
    void Start()
    {
        sR = this.GetComponent<SpriteRenderer>();
        setFloor();
    }
    private enum floorState
    {
        floor, warning, lava, highlight

    }

    public void setWarningTime(float warningTime) { this.warningTime = warningTime; }
    public void setLavaTime(float lavaTime) { this.lavaTime = lavaTime; }

    public void setFloor()
    {
        sR.sprite = floorSprite;
        state = floorState.floor;
    }
    public IEnumerator setWarning()
    {
        sR.sprite = warningSprite;
        state = floorState.warning;
        yield return new WaitForSeconds(warningTime);
        yield return setLava();
    }
    public IEnumerator setLava()
    {
        sR.sprite = lavaSprite;
        state = floorState.lava;
        yield return new WaitForSeconds(lavaTime);
        this.setFloor();
        yield return null;
    }
    public void setHighlight()
    {
        sR.sprite = highlightSprite;
        state = floorState.highlight;
    }
    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D col)
    {

        if (state == floorState.floor || state == floorState.highlight)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
                StartCoroutine("setWarning");
            }
        }
    }
    
}
