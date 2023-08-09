using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Space(10)]
    public float movementSpeed = 5f;
    public float spawnDelay = 1f;
    public float burstDelay = 3f; // Time between bursts
    public int burstAmount = 3; // Number of objects spawned in a burst
    public GameObject foodPrefab;

    [Space(20)]
    public Sprite[] FoodTypes;

    [Space(20)]
    [SerializeField] private float spawnTimer;
    [SerializeField] private float burstTimer; // Timer for burst spawning
    [SerializeField] protected float minXSpawn = -2.6f;
    [SerializeField] protected float maxXSpawn = 2.6f;

    #region Singleton

    public static FoodController Instance;

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
            SpawnFood();
            spawnTimer = 0f;
        }

        burstTimer += Time.deltaTime;
        if (burstTimer >= burstDelay)
        {
            for (int i = 0; i < burstAmount; i++)
            {
                SpawnFood();
            }
            burstTimer = 0f;
        }

        MoveAndDeactivateObjects();
    }

    protected virtual void SpawnFood()
    {
        float randomX = Random.Range(minXSpawn, maxXSpawn);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        GameObject spawnedFood = ObjectPooler.Instance.SpawnFromPool(foodPrefab.tag, spawnPosition, Quaternion.identity);

        // Sprite randomFoodSprite = RandomFood();
        // spawnedFood.GetComponent<SpriteRenderer>().sprite = randomFoodSprite;
    }

    private void MoveAndDeactivateObjects()
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

    public Sprite RandomFood()
    {
        int randomIndex = Random.Range(0, FoodTypes.Length);
        return FoodTypes[randomIndex];
    }
}
