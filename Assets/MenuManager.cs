using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int previousScene;

    [SerializeField] private GameObject deathScreen;
    public void setPScene(int scene)
    {
        previousScene = scene;
    }
    public int getPScene()
    {
        return previousScene;
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void death(bool playerDeath)
    {
        SceneManager.LoadScene(0);
        
        Debug.Log("maybe working...");
        Transform mainMenu = GameObject.FindGameObjectWithTag("MainMenu").gameObject.transform;
        Debug.Log("Working");

        mainMenu.Find("LevelSelect").gameObject.SetActive(false);
        deathScreen.SetActive(true);
        if (playerDeath)
        {
            deathScreen.transform.Find("Die").GetComponent<TextMesh>().text = "lol u died";
        }
        else
        {
            deathScreen.transform.Find("Die").GetComponent<TextMesh>().text = "u won";
        }
        
    }
}
