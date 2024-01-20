using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Animator dogAnimator;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI yourScoreText;

    public PlatePlacer platePlacer;
    private int score;
    public int health = 3;
    private float scoreTimer;
    private float scoreInterval = 1f;
    
    public Image[] hearts;
    
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space) && platePlacer.numOfPlates > 0)
        {
            animator.SetBool("IsEating", false);
            dogAnimator.SetBool("IsEating", true);
            animator.SetBool("IsGivingFood", true);
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= scoreInterval)
            {
                score++;
                scoreTimer = 0f;
                Debug.Log("Score: " + score);
                scoreText.text = "  Score: " + score;

            }

            if (platePlacer.plateHealth > 0 && platePlacer.numOfPlates > 0)
            {
                platePlacer.plateHealth -= Time.deltaTime;
                Debug.Log("Plate Health: " + platePlacer.plateHealth);
            }
            else if (platePlacer.plateHealth <= 0 && platePlacer.numOfPlates > 0)
            {
                platePlacer.PlaceCleanPlate();
                platePlacer.RemovePlate();
            }
        }
        else if (platePlacer.numOfPlates > 0)
        {
            animator.SetBool("IsGivingFood", false);
            dogAnimator.SetBool("IsEating", false);
            animator.SetBool("IsEating", true);
        }
        else
        {
            dogAnimator.SetBool("IsEating", false);
            animator.SetBool("IsGivingFood", false);
        }
        
        
    }

    public void PlayerHurt()
    {
        health--;
        UpdateHeartDisplay();
        if (health == 0)
        {
            Debug.Log("You died!");
            PlayerPrefs.SetInt("CurrentScore", score);
            CheckForHighScore(score);
            PlayerPrefs.Save();

            SceneManager.LoadScene("EndScreen");
        }
        else if (health > 0)
        {
            Debug.Log("You got hurt! Health: " + health);
        }
        
    }
    
    private void CheckForHighScore(int currentScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }
    
    void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true; // Show heart
            }
            else
            {
                hearts[i].enabled = false; // Hide heart
            }
        }
    }
    
}
