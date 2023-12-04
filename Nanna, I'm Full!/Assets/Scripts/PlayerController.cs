using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider meterSlider; // Reference to the UI Slider
    private float meterValue = 0f;
    public float meterIncreaseRate = 0.1f; // Rate at which the meter increases

    public Animator animator;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public int health = 3;
    private float timer = 0f;
    private float scoreInterval = 1f;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            //animator.SetBool("IsEating", true);
            timer += Time.deltaTime;
            if (timer >= scoreInterval)
            {
                score++;
                timer = 0f;
                Debug.Log("Score: " + score);
                scoreText.text = "   Score: " + score;
            }
        }
        else
        {
            meterValue += meterIncreaseRate * Time.deltaTime;
            meterValue = Mathf.Clamp(meterValue, 0, 1); // Clamp value between 0 and 1
            meterSlider.value = meterValue; // Update the UI Slider
            //animator.SetBool("IsEating", false);
            timer = 0f;
        }
    }

    public void PlayerHurt()
    {
        health--;
        if (health == 0)
        {
            Debug.Log("You died!");
        }
        else if (health > 0)
        {
            Debug.Log("You got hurt! Health: " + health);
        }
    }
    
}
