using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour
{
    void Start()
    {
        GetComponent<Health>().onKilled.AddListener(() => Destroy(gameObject));
    }
}
