using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI scoreText;
    public PlatePlacer platePlacer;
    private int score = 0;
    public int health = 3;
    private float scoreTimer = 0f;
    private float scoreInterval = 1f;
    
    public Image[] hearts;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && platePlacer.numOfPlates > 0)
        {
            //animator.SetBool("IsEating", true);
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= scoreInterval)
            {
                score++;
                scoreTimer = 0f;
                Debug.Log("Score: " + score);
                scoreText.text = "   Score: " + score;
            }

            if (platePlacer.plateHealth > 0 && platePlacer.numOfPlates > 0)
            {
                platePlacer.plateHealth -= Time.deltaTime;
                //Debug.Log("Plate Health: " + platePlacer.plateHealth);
            }
            else if (platePlacer.plateHealth <= 0 && platePlacer.numOfPlates > 0)
            {
                platePlacer.RemovePlate();
            }
        }
        else
        {
            //animator.SetBool("IsEating", false);
        }
        
    }

    public void PlayerHurt()
    {
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
