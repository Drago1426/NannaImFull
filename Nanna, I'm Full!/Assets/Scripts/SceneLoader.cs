using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnMouseDown()
    {
        // This code will run when the sprite is clicked
        // Replace 'YourSceneName' with the actual scene name you want to load
        SceneManager.LoadScene("SampleScene");
    }
}