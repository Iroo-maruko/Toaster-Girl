using UnityEngine;

public class SpraySlow : MonoBehaviour
{
    public static SpraySlow Instance;

    public float slowDuration = 2f;
    public float slowMultiplier = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplySlow(Transform target)
    {
        /* (change player's speed)
        var movement = target.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.ApplySlow(slowMultiplier, slowDuration);
        }
        */
    }
}
