using UnityEngine;
using System.Collections.Generic;

public class HealthBarManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject healthBarPrefab;
    public Transform canvasTransform;
    private Dictionary<Enemy, HealthBar> healthBars = new Dictionary<Enemy, HealthBar>(); // Enemy와 Health Bar 매핑

    void Update()
    {
        foreach (var enemy in healthBars.Keys)
        {
            if (enemy != null)
            {
                UpdateHealthBarPosition(enemy);
            }
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (healthBarPrefab == null || canvasTransform == null)
        {
            return;
        }


        GameObject newHealthBarObj = Instantiate(healthBarPrefab, canvasTransform);
        
        if (newHealthBarObj == null)
        {
            return;
        }

        HealthBar healthBar = newHealthBarObj.GetComponent<HealthBar>();

        if (healthBar != null)
        {
            healthBars[enemy] = healthBar;
            enemy.AssignHealthBar(healthBar); 
            
        }
    }


    public void UnregisterEnemy(Enemy enemy)
    {
        if (healthBars.ContainsKey(enemy))
        {
            Destroy(healthBars[enemy].gameObject);
            healthBars.Remove(enemy);
        }
    }

    private void UpdateHealthBarPosition(Enemy enemy)
    {
        if (healthBars.ContainsKey(enemy))
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 2f, 0));

            if (screenPos.z < 0) 
            {
                return;
            }

            healthBars[enemy].transform.position = screenPos;
        }
    }

}