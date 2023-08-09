using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    [Space(10)]
    [Header("Game Manager Screens")]
    public GameObject GameOverScreen;
    public GameObject Spawner;
    public GameObject Player;

    [Space(10)]
    public Text Score; // Track the current pause state
    public Text EndScore; // Track the current pause state
    public int scoreCount = 0; // Track the current pause state

    [Space(10)]
    public ParticleSystem basketSplash;

    [Space(10)]
    public GameObject bombSplash;

    [Space(10)]
    public CameraShake cameraShake;

    [Space(10)]
    public Animator anim;


    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    void Start(){

        Spawner.SetActive(true);
        GameOverScreen.SetActive(false);
        Player.SetActive(true);
    }
    public void LoadMenu(){
        
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }

    public void RestartGame(){

        AddPlayCounts();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        Spawner.SetActive(false);
        GameOverScreen.SetActive(true);
        Player.SetActive(false);
    }
    void AddPlayCounts(){

        int temp = PlayerPrefs.GetInt("PLAYED");
        temp++;
        PlayerPrefs.SetInt("PLAYED", temp);
    }
}
