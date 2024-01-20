using UnityEngine;

public class CreditsButtonScript : MonoBehaviour
{
    public GameObject[] mainMenuUI; // Assign all the UI elements of the main menu here
    public GameObject creditsImage; // Assign your credits image here
    public GameObject backButton; // Assign your back button here

    private void OnMouseDown()
    {
        // Hide the specified main menu objects
        foreach (GameObject obj in mainMenuUI)
        {
            obj.SetActive(false);
        }
        
        // Show the back button
        creditsImage.SetActive(true);
        backButton.SetActive(true);
    }
}
