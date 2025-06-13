using System;
using System.Collections;
using UnityEngine;

public class SprayDamage : MonoBehaviour
{
    [Header("MovementSpeeds")]
    public float _OriginalSpeed;
    public float _NewSpeed = 3f;

    [Header("References")]
    public PlayerController _Player;
    public CanbeeSprayAttack _Spray;
    public static SprayDamage Instance;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // getting the playerController
        _Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void SprayEffect()
    {
        if (_Spray._DealDamage)
        {
            int damageAmount = 0;
            float stunDuration = 0.3f;
            _Player.TakeDamage(damageAmount, stunDuration);
            Debug.Log("Player took spray damage.");
        }

        else
        {
            _Player.TempStatChange(
                getStat: () => _Player.GetStats().walkSpeed,
                setStat: val => _Player.GetStats().walkSpeed = val,
                debuffAmount: 7f,
                duration: 5f
            );
            Debug.Log("Player slowed by spray.");
        }
       _Spray = null;
    }
}
