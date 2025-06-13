using UnityEngine;

public class Item_move : MonoBehaviour
{
    public float floatSpeed = 3f;  // Floating speed
    public float floatHeight = 0.1f;  // Floating height
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Floating movement using Sin function
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
