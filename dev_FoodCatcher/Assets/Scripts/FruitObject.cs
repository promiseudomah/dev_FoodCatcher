using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitObject : MonoBehaviour
{

    public int points = 1;
    public float rotationSpeed = 100f;
    public Vector3 minScale = new Vector3(0.25f, 0.25f, 0.25f);
    public Vector3 maxScale = new Vector3(1f, 1f, 1f);

    SpriteRenderer spriteRenderer;

    void Start(){

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FruitController.Instance.RandomFruit();
    }

    private void Update()
    {
        // Rotate the object around its local forward axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);

        // Calculate scale based on sine function for smoother scaling
        float scale = Mathf.PingPong(Time.time, 1.0f);
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, scale);

        // Apply the calculated scale
        transform.localScale = targetScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){

            Debug.Log("Entered Player!");

            gameObject.SetActive(false);
            AddScore();
        }
    }

    void AddScore(){

        GameManager.Instance.AddScore();
    }
}
