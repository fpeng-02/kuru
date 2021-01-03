using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadBoss(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }
}
