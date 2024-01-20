using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Animator dogAnimator;
    public TextMeshProUGUI scoreText;
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
            animator.SetBool("IsEating", false);
        }
        
        
    }

    public void PlayerHurt()
    {
        animator.SetBool("IsHurt", true);
        health--;
        UpdateHeartDisplay();
        if (health == 0)
        {
            Debug.Log("You died!");
        }
        else if (health > 0)
        {
            Debug.Log("You got hurt! Health: " + health);
        }
        animator.SetBool("IsHurt", false);
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
