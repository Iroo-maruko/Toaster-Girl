using UnityEngine;

public class CanbeeSprayAttack : MonoBehaviour
{
    public ParticleSystem sprayParticle;
    public float detectionRange = 3f;
    public float attackCooldown = 2f;
    private float attackTimer = 0f;
    public Animator _BeeAni;
    public bool _DealDamage;

    private Transform targetPlayer;

    [Header("Attack sounds")]
    public FMODUnity.EventReference Spray;

    void Start()
    {
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.transform;
            }
        }

        if (targetPlayer == null)
            return;

        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        attackTimer -= Time.deltaTime;

        if (distance <= detectionRange && attackTimer <= 0f)
        {
            _BeeAni.SetBool("Spray", true);
            attackTimer = attackCooldown;
        }
    }

    public void Attack()
    {
        if (SprayDamage.Instance._Spray == null)
        {
            SprayDamage.Instance._Spray = this;
            Debug.Log("Adding reference");
        }

        sprayParticle.Play();
        AudioManager.Instance.PlaySound(Spray, transform.position);
    }

    public void AnimTrigger()
    {
        SprayDamage.Instance._Spray = null;
        _BeeAni.SetBool("Spray", false);
    }
}
