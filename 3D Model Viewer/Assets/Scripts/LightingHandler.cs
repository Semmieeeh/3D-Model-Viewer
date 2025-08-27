using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static System.TimeZoneInfo;


public class LightingHandler : MonoBehaviour
{

    [SerializeField] private Light _lightSource;
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private Color _dayColor = new Color(1f, 0.9f, 0.8f);
    [SerializeField] private Color _nightColor = new Color(0.1f, 0.1f, 0.3f);

    private float _dayIntensity =1f;
    private float _nightIntensity = 0.1f;

    private bool _isDay = true;


    [SerializeField]private float _transisionTime = 0f;
    [SerializeField] private float _transisionDuration = 2f;
    [SerializeField] private bool _isTransitioning = false;

    [SerializeField] private bool _activate = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("sssssssssss");
        }

        if (_activate && !_isTransitioning)
        {
            StartCoroutine(TransisionLight());
        }


    }

    private IEnumerator TurnAround(Transform OBJ)
    {
        
        yield return null;
    }
    private IEnumerator TransisionLight()
    {
        _isTransitioning = true;
        _isDay = !_isDay;
        _transisionTime = 0f;
        _transisionDuration = 2f;

        Color startColor = _isDay ? _nightColor : _dayColor;
        Color endColor = _isDay ? _dayColor : _nightColor;
        float startIntensity =  _isDay ? _nightIntensity : _dayIntensity;
        float endInteensity = _isDay ? _dayIntensity : _nightIntensity;

        while ( _transisionTime < _transisionDuration )
        {
            _transisionTime += Time.deltaTime;
            float t = _transisionTime / _transisionDuration;
            _lightSource.color = Color.Lerp(startColor, endColor, t);
            _lightSource.intensity = Mathf.Lerp(startIntensity, endInteensity, t);
            yield return null;  
        }

        _lightSource.color = endColor;
        _lightSource.intensity = endInteensity;
        _isTransitioning = false;
    }
  
}
