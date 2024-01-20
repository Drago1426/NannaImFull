using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonScript : MonoBehaviour
{
    public GameObject[] mainMenuUI; // Assign the objects you want to show here
    public GameObject creditsImage; // Assign your credits image here
    public GameObject backButton; // Assign your back button here

    public void OnMouseDown()
    {
        // Hide the credits and back button
        creditsImage.SetActive(false);
        backButton.SetActive(false);

        // Show the specified main menu objects
        foreach (GameObject obj in mainMenuUI)
        {
            obj.SetActive(true);
        }
    }
}
