using UnityEngine;
using UnityEngine.Events;

public class MouseEventStore: MonoBehaviour
{
    public UnityEvent onLeftClick;
    public UnityEvent onLeftHold;
    public UnityEvent onScrollUp;
    public UnityEvent onScrollDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            onLeftClick?.Invoke();
        if (Input.GetMouseButton(0))
            onLeftHold?.Invoke();

        float verticalScroll = Input.mouseScrollDelta.y;
        if (verticalScroll > 0f)
            onScrollUp?.Invoke();
        if (verticalScroll < 0f)
            onScrollDown?.Invoke();
    }
}
