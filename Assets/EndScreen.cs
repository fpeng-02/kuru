using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(GameObject.Find("MenuManager").GetComponent<MenuManager>().getPScene()) ;
    }
}
