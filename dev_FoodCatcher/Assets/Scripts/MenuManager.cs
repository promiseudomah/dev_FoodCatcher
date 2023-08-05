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

        StartWithMenuEnabled();
        CheckAchievements();
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

        foreach (string foodName in FoodNames)
        {
            string FOODKEY = foodName + "Count";

            if(PlayerPrefs.HasKey(FOODKEY)){
                
                int foodCount = PlayerPrefs.GetInt(FOODKEY);
                Debug.Log($"{FOODKEY}: {foodCount}");

                foreach (GameObject stampObject in Stamps)
                {
                      
                    if(stampObject.name.Contains(foodName)){

                        if(foodCount >= 100){
                            
                            UpdateAchievements(stampObject);
                        }

                        else{

                            DefaultAchievements(stampObject);
                        }
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

    void OnEnable()
    {
        Refresh();
    }

}
