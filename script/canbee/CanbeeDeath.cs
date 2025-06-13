using UnityEngine;

public class CanbeeDeath : MonoBehaviour, IDamageable
{
    public int _Health = 3;
    private Enemy_die enemyDieScript;
    public Animator _BeeAni;

    [Header("Movement sounds")]
    public FMODUnity.EventReference DieSound;

    void Awake()
    {
        enemyDieScript = GetComponent<Enemy_die>();
    }

    public void TakeDamage(int damageAmount, float stun)
    {
    }

    public void TakeDamage(int damageAmount, float stun, GameObject attacker)
    {
        _Health -= damageAmount;
        Debug.Log($"Canbee took damage from {attacker?.name}! Remaining HP: {_Health}");
        _BeeAni.SetTrigger("Death");
    }

    public void Die()
    {
        AudioManager.Instance.PlaySound(DieSound, transform.position);
        if (enemyDieScript != null)
        {
            enemyDieScript.Die();
        }
        else
        {
            Debug.LogWarning("Enemy_die not found");
            Destroy(this.gameObject);
        }
    }
}
