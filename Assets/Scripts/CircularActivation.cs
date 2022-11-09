using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unmodifiable circular list that keeps all the objects inactive apart from the currently selected one
/// </summary>
public class CircularActivation : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objects = new List<GameObject>();
    private int _selectedIndex = -1;

    void Start()
    {
        Next();
    }

    private void Select(int id)
    {
        if (_selectedIndex >= 0)
        {
            _objects[_selectedIndex].SetActive(false);
            _selectedIndex = -1;
        }
            
        if(id >= 0 || id < _objects.Count)
        {
            _selectedIndex = id;
            _objects[_selectedIndex].SetActive(true);
        }
    }

    public void Next()
    {
        if (_selectedIndex == -1 || _selectedIndex >= _objects.Count - 1)
        {
            Select(0);
            return;
        }
        Select(_selectedIndex + 1);
    }

    public void Previous()
    {
        if (_selectedIndex < 1)
        {
            Select(_objects.Count - 1);
            return;
        }
        Select(_selectedIndex - 1);
    }
}
