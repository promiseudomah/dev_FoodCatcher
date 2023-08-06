using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{

    public SquareBlockController squareBlockController;

    [Space(40)]
    [Header("FLOW Variables")]
    
    [SerializeField] float squareSpeed;
    [SerializeField] float ballSpeed;
    [SerializeField] float blockMovementSpeed;
    [SerializeField] float blockSpawnDelay;
    [Space(40)]

    #region Singleton

    public static FlowManager Instance;

    private void Awake()
    {
        Instance = this;

        
    }

    #endregion

    void Update()
    {

        blockMovementSpeed = squareBlockController.movementSpeed;
        blockSpawnDelay = squareBlockController.spawnDelay;
    }


    public void FLOW(int playerScore)
    {

        // Modify movement speed and spawn delay based on player score
        squareBlockController.movementSpeed = 1.5f + playerScore * 0.075f;
        squareBlockController.spawnDelay = 1.75f - playerScore * 0.015f;

        // Ensure spawn delay doesn't go below a certain value
        squareBlockController.spawnDelay = Mathf.Max(squareBlockController.spawnDelay, 0.75f);
    }


}
