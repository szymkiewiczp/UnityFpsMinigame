using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onKilled;
    public float defaultHealth = 100f;
    private float _health;

    void Start()
    {
        _health = defaultHealth;
    }

    public void Damage(float damage)
    {
        if (_health <= 0f)
            return;

        _health -= damage;

        if(_health <= 0f)
        {
            _health = 0f;
            onKilled?.Invoke();
        }
    }
}
