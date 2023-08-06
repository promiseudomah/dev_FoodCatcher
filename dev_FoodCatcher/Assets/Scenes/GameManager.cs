using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    [Space(10)]
    [Header("Game Manager Screens")]
    public GameObject PauseScreen;
    public GameObject GameOverScreen;

    [Space(10)]
    [SerializeField] private bool isPaused = false; // Track the current pause state

    [Space(10)]
    public Text Score; // Track the current pause state
    public Text EndScore; // Track the current pause state
    public int scoreCount = 0; // Track the current pause state

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    /// <summary>
    ///? Load Scene ( 0 - Menu Scene, 1 - Game Scene)
    /// </summary>

    public void LoadMenu(){
        
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }

    public void RestartGame(){

        AddPlayCounts();
        SceneManager.LoadScene(1);
    }

    public void AddScore(){

        scoreCount += 1;
        Score.text = scoreCount.ToString();

        FlowManager.Instance.FLOW(scoreCount);
    }

    public void GameOver()
    {
        
        SetScore();
        EnableGameOverScreen();
    }

    void SetScore(){
        
        int currentScore = PlayerPrefs.GetInt("SCORE", 0);

        if (scoreCount > currentScore)
        {
            PlayerPrefs.SetInt("SCORE", scoreCount);
            currentScore = scoreCount;
        }

        string score = currentScore.ToString();
        EndScore.text = score;

    }
    
    void EnableGameOverScreen(){

        GameOverScreen.SetActive(true);
    }
    void AddPlayCounts(){

        int temp = PlayerPrefs.GetInt("PLAYED");
        temp++;
        PlayerPrefs.SetInt("PLAYED", temp);
    }
}
