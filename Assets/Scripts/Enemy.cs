using UnityEngine;

[RequireComponent(typeof(RandomStroll))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public float power = 1f;
    public float scaleMultiplier = 1f;
    public float walkSpeedMultiplier = 0.5f;
    public float jumpSpeedMultiplier = 0.2f;
    public float healthMultiplier = 3f;

    void Awake()
    {
        transform.localScale = Vector3.one * power;

        RandomStroll stroll = GetComponent<RandomStroll>();
        stroll.walkSpeed *= GetMultiplier(power, walkSpeedMultiplier);
        stroll.jumpSpeed *= GetMultiplier(power, jumpSpeedMultiplier);

        Health health = GetComponent<Health>();
        health.defaultHealth *= GetMultiplier(power, healthMultiplier);
    }

    private float GetMultiplier(float power, float baseMultiplier)
    {
        return 1f + (power - 1f) * baseMultiplier;
    }
}
