using UnityEngine;

public class SquareBlockController : MonoBehaviour
{
    public float movementSpeed = 5f; 
    public float spawnDelay = 1f; 
    public GameObject squareBlockPrefab; 

    [SerializeField] private float spawnTimer; 
    [SerializeField] protected float minXSpawn = -2.6f; 
    [SerializeField] protected float maxXSpawn = 2.6f;
    
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            SpawnSquareBlock();
            spawnTimer = 0f;
        }

        CheckIfOutOfScreen();
    }

    protected virtual void SpawnSquareBlock()
    {
        float randomX = Random.Range(minXSpawn, maxXSpawn);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        ObjectPooler.Instance.SpawnFromPool(squareBlockPrefab.tag, spawnPosition, Quaternion.identity);
//        squareBlock.transform.SetParent(transform);
    }

    private void CheckIfOutOfScreen()
    {
        float moveDistance = movementSpeed * Time.deltaTime;
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.down * moveDistance);
            if (child.position.y < Screen.height)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
