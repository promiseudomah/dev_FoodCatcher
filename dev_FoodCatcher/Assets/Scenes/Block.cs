using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject BlockDeath;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ScreenEnd")){

            GameObject splash = Instantiate(BlockDeath, transform.position, Quaternion.identity);
            gameObject.SetActive(false);  
        }
    }
}
