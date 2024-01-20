using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI yourScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("CurrentScore", 0);
        yourScoreText.text = "Your Score: " + finalScore;
    }
}