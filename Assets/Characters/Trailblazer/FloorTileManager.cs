using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileManager : MonoBehaviour
{
    [SerializeField] private GameObject tile; 
    // Start is called before the first frame update
    void Start()
    {
        for (float hor = -5; hor <= 5; hor += 0.5f)
        {
            for (float ver = -5; ver <= 5; ver += 0.5f)
            {
                Instantiate(tile, new Vector3(hor, ver, 2), Quaternion.Euler(0,0,0), this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
