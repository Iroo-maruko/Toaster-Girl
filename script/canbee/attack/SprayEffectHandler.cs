using UnityEngine;

public class SprayEffectHandler : MonoBehaviour
{
    public static SprayEffectHandler Instance;

    [Header("Effect Toggle")]
    public bool doDamage = true;
    public bool doSlow = false;

    private void Awake()
    {
        Instance = this;
    }

    //public void ApplyEffect(Transform target)
    //{
    //    if (doDamage)
    //        SprayDamage.Instance.SprayEffect(target);

    //    if (doSlow)
    //        SpraySlow.Instance.ApplySlow(target);
    //}
}
