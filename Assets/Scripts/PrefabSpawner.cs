using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
