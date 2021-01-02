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

    [SerializeField] private float jitterMagnitude;
    [SerializeField] private float jitterInterval;

    private FloorState state;
    private SpriteRenderer sR;
    private bool highlight;

    // these fields are used during warning to jitter the tile and are here to avoid allocation overhead
    private Vector3 originalPos;
    private float elapsedWarningTime;
    private float xOffset;
    private float yOffset;

    public FloorState getState() { return this.state; }

    // Start is called before the first frame update
    void Awake()
    {
        permanentDisable = false;
        sR = this.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        setFloor();
    }

    public void setOriginalPos(Vector3 originalPos) { this.originalPos = originalPos; }
    public void setPermanentDisable(bool permanentDisable) { this.permanentDisable = permanentDisable; }
    public void setWarningTime(float warningTime) { this.warningTime = warningTime; }
    public void setLavaTime(float lavaTime) { this.lavaTime = lavaTime; }

    public void setFloor() 
    { 
        this.transform.position = originalPos; 
        sR.sprite = highlight ? highlightSprite : floorSprite; 
        state = FloorState.Floor; 
    }
    public void setWarning() { StartCoroutine("warning"); }
    public void setPermanentWarning() { StartCoroutine("permanentWarning"); }

    public void setLava() { StartCoroutine("lava"); }
    public void setExtendedLava() { StartCoroutine("extendedLava"); }
    public void setPermanentLava() { sR.sprite = lavaSprite; state = FloorState.Lava; }

    public void setHighlight(bool highlight) { this.highlight = highlight; }

    public IEnumerator warning()
    {
        sR.sprite = warningSprite;
        state = FloorState.Warning;
        elapsedWarningTime = 0f;
        while (elapsedWarningTime < warningTime) {
            xOffset = Random.Range(-1f, 1f) * jitterMagnitude;
            yOffset = Random.Range(-1f, 1f) * jitterMagnitude;
            this.transform.position = originalPos + new Vector3(xOffset, yOffset, 0);
            yield return new WaitForSeconds(jitterInterval);
            elapsedWarningTime += jitterInterval;
        }
        this.transform.position = originalPos;
        if (permanentDisable) {
            setPermanentLava();
        } else {
            yield return lava();
        }
    }

    // only used at the beginning of the boss fight.
    public IEnumerator permanentWarning()
    {
        sR.sprite = warningSprite;
        state = FloorState.Warning;
        while (true) {
            xOffset = Random.Range(-1f, 1f) * jitterMagnitude;
            yOffset = Random.Range(-1f, 1f) * jitterMagnitude;
            this.transform.position = originalPos + new Vector3(xOffset, yOffset, 0);
            yield return new WaitForSeconds(jitterInterval);
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
