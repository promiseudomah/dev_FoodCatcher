using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    [Space(20)]
    public FoodController FoodController;

    [Space(10)]
    [Header("FLOW VARIABLES ")]
    [SerializeField] float FoodObjectMovementSpeed = 5;
    [SerializeField] float FoodObjectSpawnDelay = 1;

    #region Singleton

    public static FlowManager Instance;

    private void Awake()
    {
        
        Instance = this;
    }

    #endregion

    void Update()
    {

        FoodObjectMovementSpeed = FoodController.movementSpeed;
        FoodObjectSpawnDelay = FoodController.spawnDelay;
    }


    public void FLOW(int playerScore)
    {

        FoodController.movementSpeed = 7.5f + playerScore * 0.075f;
        FoodController.spawnDelay = 0.75f - playerScore * 0.015f;

        FoodController.spawnDelay = Mathf.Max(FoodController.spawnDelay, 0.75f);
    }


}
