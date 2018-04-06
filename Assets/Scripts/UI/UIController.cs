using UnityEngine.UI;
using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    [SerializeField] Text centerOfScreenTextLabel;
    [SerializeField] Text scoreLabel;
    [SerializeField] Text livesLabel;

    private float timeLeft = Constants.TIME_TO_START;
    private bool timerStopped = false;

    void Start ()
    {
        HideUIPanel();
    }


    private void Update()
    {
        StartTimer();
    }

    public void UpdateScore (float score)
    {
        if (scoreLabel.gameObject.activeInHierarchy)
            scoreLabel.text = "Score: " + score;
    }

    public void AfterGameScreenShow (bool isWin, float score)
    {
        restartButton.SetActive(true);
        centerOfScreenTextLabel.gameObject.SetActive(true);

        if (isWin)
            centerOfScreenTextLabel.text = "You win!";
        else
            centerOfScreenTextLabel.text = "You lose.";
    }

    public void HideUIPanel ()
    {
        restartButton.SetActive(false);
        centerOfScreenTextLabel.gameObject.SetActive (false);
    }

    public void UpdateLives (float lives)
    {
        livesLabel.text = "Lives: " + lives + "/" + Constants.MAX_PLAYER_LIFES;
    }

    public void RestartTimer ()
    {
        timerStopped = false;
        timeLeft = Constants.TIME_TO_START;
    }

    void StartTimer ()
    {
        if (timerStopped) return;
        centerOfScreenTextLabel.gameObject.SetActive(true);
        centerOfScreenTextLabel.text = ((int)timeLeft + 1).ToString();

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            centerOfScreenTextLabel.gameObject.SetActive(false);
            GameManager.IsGamePaused = false;
            timerStopped = true;
            timeLeft = Constants.TIME_TO_START;
        }
    }

	
}
