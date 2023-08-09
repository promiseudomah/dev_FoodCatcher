using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{

    public FruitController fruitController;

    [Space(20)]
    [Header("FLOW Variables")]
    
    [SerializeField] float fruitObjectMovementSpeed = 5;
    [SerializeField] float fruitObjectSpawnDelay = 1;

    #region Singleton

    public static FlowManager Instance;

    private void Awake()
    {
        
        Instance = this;
    }

    #endregion

    void Update()
    {

        fruitObjectMovementSpeed = fruitController.movementSpeed;
        fruitObjectSpawnDelay = fruitController.spawnDelay;
    }


    public void FLOW(int playerScore)
    {

        // Modify movement speed and spawn delay based on player score
        fruitController.movementSpeed = 5 + playerScore * 0.075f;
        fruitController.spawnDelay = 1 - playerScore * 0.015f;

        // Ensure spawn delay doesn't go below a certain value
        fruitController.spawnDelay = Mathf.Max(fruitController.spawnDelay, 0.75f);
    }


}
