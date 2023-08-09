using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    private Vector3 startPosition;
    [SerializeField] private float screenWidth;


    // Adjust this value to control the basket's speed.
    public float dragSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition.z = transform.position.z;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = transform.position.z;
            MoveBasket(currentPosition - startPosition);
            startPosition = currentPosition;
        }
    }

    private void MoveBasket(Vector3 delta)
    {
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        float newX = Mathf.Clamp(transform.position.x + delta.x * dragSpeed, -screenWidth / 2f, screenWidth / 2f);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
