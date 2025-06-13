using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private HealthBar healthBar;
    private HealthBarManager healthBarManager;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarManager = FindFirstObjectByType<HealthBarManager>(); 

/*        if (healthBarManager != null)
        {
            healthBarManager.RegisterEnemy(this); // Health Bar 등록
        }
        else
        {
            Debug.LogError("HealthBarManager를 찾을 수 없습니다!");
        }*/
    }

    public void AssignHealthBar(HealthBar newHealthBar)
    {
        healthBar = newHealthBar;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력이 0 이하로 내려가지 않도록 제한
        UpdateHealthUI();
        Debug.Log($"Enemy HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthBar.UpdateHealthBar(healthPercent);
        }
        else
        {
            Debug.LogError("Not connected");
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        
/*        if (healthBarManager != null)
        {
            healthBarManager.UnregisterEnemy(this); // Health Bar 제거
        }*/

        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            TakeDamage(1);
        }
    }
}
