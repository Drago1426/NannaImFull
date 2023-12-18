using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlateDetector : MonoBehaviour
{
    public string sceneToLoad;

    public void PlateCrashing()
    {
        Debug.Log("All the plates are crashing!!!!");
        //SceneManager.LoadScene(sceneToLoad);
    }
}
