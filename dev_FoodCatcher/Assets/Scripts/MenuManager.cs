using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Space(10)]
    [Header("Menu Pages")]
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Achievements;
    [SerializeField] GameObject Settings;

    [Space(10)]
    [Header("Achievement Objects")]
    public string[] FoodNames;
    public GameObject[] Stamps;
    

    void Awake()
    {

        Refresh();
    }
    public void ClickPlay(){
        
        SceneManager.LoadScene(1);   
    }

    void StartWithMenuEnabled(){

        MainMenu.SetActive(true);

        Achievements.SetActive(false);
        Settings.SetActive(false);
    }

    void CheckAchievements(){

        for (int i = 0; i < FoodNames.Length; i++)
        {
            string FOODKEY = FoodNames[i] + "Count";

            if(PlayerPrefs.HasKey(FOODKEY)){
        
                int foodCount = PlayerPrefs.GetInt(FOODKEY);
                int remainingCount = 100 - foodCount;

                Debug.Log($"{FOODKEY}: {foodCount} remainingCount: {remainingCount}");

                if(Stamps[i].name.Contains(FoodNames[i])){

                    if(foodCount >= 100){
                        
                        UpdateAchievements(Stamps[i]);
                    }

                    else{

                        DefaultAchievements(Stamps[i]);
                    }
                } 
            }  
        
            else{

                PlayerPrefs.SetInt(FOODKEY, 0); 
                
            }
        }
    }

    void UpdateAchievements(GameObject stampGameObject){

        CanvasGroup cv = stampGameObject.GetComponent<CanvasGroup>();
        Text text = stampGameObject.GetComponent<Text>();

        text.color = Color.white;
        cv.alpha = 1f;
        cv.interactable = true;
    }

    void DefaultAchievements(GameObject stampGameObject){

        CanvasGroup cv = stampGameObject.GetComponent<CanvasGroup>();
        Text text = stampGameObject.GetComponentInChildren<Text>();


        text.color = Color.black;
        cv.alpha = 0.3f;
        cv.interactable = false;
    }

    void Refresh(){

        StartWithMenuEnabled();
        CheckAchievements();
    }



}
