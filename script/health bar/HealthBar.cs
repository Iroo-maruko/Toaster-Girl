using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;

    public void UpdateHealthBar(float healthPercent)
    {
        healthFill.fillAmount = healthPercent;
    }
}
