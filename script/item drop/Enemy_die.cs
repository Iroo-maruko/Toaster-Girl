using UnityEngine;

public class Enemy_die : MonoBehaviour
{
    public GameObject dropItem;
    public float energy = 100; 
    void Update()
    {
        if (energy <= 0) 
        {
            Die(); 
        }
    }

    void Die()
    {
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }

        Debug.Log("Enemy has died!");

        gameObject.SetActive(false);
    }
}
