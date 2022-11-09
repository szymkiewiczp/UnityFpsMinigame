using UnityEngine;

public class DespawnDelay : MonoBehaviour
{
    public float delay = 10f;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
