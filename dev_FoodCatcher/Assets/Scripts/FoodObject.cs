using UnityEngine;

public class FoodObject : MonoBehaviour
{
    public int points = 1;
    public float rotationSpeed = 100f;
    public Vector3 minScale;
    public Vector3 maxScale;
    
    private SpriteRenderer spriteRenderer;
    private GameObject bomb;
    private CameraShake cameraShake;
    private BasketController playerController;
    private SpriteRenderer playerColor;
    private new BoxCollider2D collider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = FoodController.Instance.RandomFood();
        AdjustColliderToBounds(spriteRenderer.sprite.bounds);
        
        bomb = GameManager.Instance.bombSplash;
        cameraShake = GameManager.Instance.cameraShake;
        playerController = GameManager.Instance.Player.GetComponent<BasketController>();
        playerColor = GameManager.Instance.Player.GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        RotateAndScale();
    }

    private void RotateAndScale()
    {
        // Rotate the object around its local forward axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);

        // Calculate scale based on sine function for smoother scaling
        float scale = Mathf.PingPong(Time.time, 1.0f);
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, scale);

        // Apply the calculated scale
        transform.localScale = targetScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (spriteRenderer.sprite.name != "Bomb")
            {
                string foodName = spriteRenderer.sprite.name;
                HandleFoodCatch(foodName);
            }
            else
            {
                HandleBombCollision();
            }

            gameObject.SetActive(false);
        }
    }

    private void AdjustColliderToBounds(Bounds newBounds)
    {
        if (collider is BoxCollider2D boxCollider2D)
        {
            
            boxCollider2D.size = newBounds.size;
        }
        
    }

    private void AddScore()
    {
        GameManager.Instance.AddScore();
    }
    

    private void HandleFoodCatch(string foodName){
        
        string FOODKEY = foodName + "Count";

        if(PlayerPrefs.HasKey(FOODKEY)){

            int foodCount = PlayerPrefs.GetInt(FOODKEY);
            foodCount += 1;

            PlayerPrefs.SetInt(FOODKEY, foodCount);
            
        }

        GameManager.Instance.basketSplash.Play();
        GameManager.Instance.anim.Play("AddPoint");
        AddScore();
    }

    private void HandleBombCollision()
    {
        
        GameObject obj = Instantiate(bomb, transform.position, Quaternion.identity);
        obj.transform.SetParent(null);

        cameraShake.ShakeCamera();

        playerColor.color = Color.black;
        GameManager.Instance.anim.Play("Bomb");

        playerController.enabled = false;

        Invoke("GameOver", 0.55f);
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
