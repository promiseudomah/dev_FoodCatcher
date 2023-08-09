using UnityEngine;

public class FruitController : MonoBehaviour
{
    [Space(10)]
    public float movementSpeed = 5f; 
    public float spawnDelay = 1f; 
    public GameObject fruitPrefab; 

    [Space(20)]
    public Sprite[] FruitTypes;

    [Space(20)]
    [SerializeField] private float spawnTimer; 
    [SerializeField] protected float minXSpawn = -2.6f; 
    [SerializeField] protected float maxXSpawn = 2.6f;

    
    #region Singleton

    public static FruitController Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            SpawnFruits();
            spawnTimer = 0f;
        }

        CheckIfOutOfScreen();
    }

    protected virtual void SpawnFruits()
    {
        float randomX = Random.Range(minXSpawn, maxXSpawn);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        ObjectPooler.Instance.SpawnFromPool(fruitPrefab.tag, spawnPosition, Quaternion.identity);
//        squareBlock.transform.SetParent(transform);
    }

    private void CheckIfOutOfScreen()
    {
        float moveDistance = movementSpeed * Time.deltaTime;
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.down * moveDistance);

            if (child.position.y < -15)
            {   
                child.gameObject.SetActive(false);
            }
        }
    }

    public Sprite RandomFruit(){

        Sprite randomSprite;
        int randomX;

        randomX = Random.Range(0, FruitTypes.Length);

        randomSprite = FruitTypes[randomX];
        return randomSprite;
    }
}
