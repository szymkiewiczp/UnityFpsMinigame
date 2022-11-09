using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flash : MonoBehaviour
{
    public float lightUpDuration = 0.1f;
    public float dimDuration = 1f;
    public bool lightUpOnStart = true;

    private float _remainingTime = 0f;
    private float _maxIntensity;
    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
        _maxIntensity = _light.intensity;

        _light.intensity = 0f;
        if (lightUpOnStart)
            StartFlash();
    }

    void Update()
    {
        if (_remainingTime <= 0f)
            return;

        _remainingTime = Mathf.Clamp(_remainingTime - Time.deltaTime, 0f, lightUpDuration + dimDuration);

        if (_remainingTime > dimDuration)
            _light.intensity = _maxIntensity * (1f - (_remainingTime - dimDuration) / lightUpDuration);
        else
            _light.intensity = _maxIntensity * _remainingTime / dimDuration;
    }

    public void StartFlash()
    {
        _remainingTime = lightUpDuration + dimDuration;
    }
}
