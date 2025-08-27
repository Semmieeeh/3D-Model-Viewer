using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class LightingHandler : MonoBehaviour
{

    [SerializeField] private Light _lightSource;

    [SerializeField] private Color _dayColor = new Color(1f, 0.9f, 0.8f);
    [SerializeField] private Color _nightColor = new Color(0.1f, 0.1f, 0.3f);

    private float _dayIntensity = 1f;
    private float _nightIntensity = 0.1f;

    [SerializeField] private bool _isDay = true;

    [SerializeField] private Transform _pivotPoint;

    [SerializeField] private float _transisionTime = 0f;
    [SerializeField] private float _transisionDuration = 2f;
    [SerializeField] private bool _isTransitioning = false;

    [SerializeField] private bool _activate = false;


    [SerializeField] private bool _wasEKeyPressed;
    [SerializeField] private bool _wasQKeyPressed;
    [SerializeField]private Coroutine _turnAroundCoroutine;

    [SerializeField] private Button _toggleButton;
    [SerializeField] private Button _clockWiseButton;
    [SerializeField] private Button _counterClockwise;

    [SerializeField] private bool _clockwiseButtonHeld;
    [SerializeField] private bool _counterClockwiseButtonHeld;


    [SerializeField] private float _orbitDistance;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        if (_toggleButton != null)
        {
            _toggleButton.onClick.AddListener(SwitchDayNightMode);
        }
        if (_clockWiseButton != null)
        {
            var holdHandler = _clockWiseButton.gameObject.AddComponent<ButtonHoldHandler>();
            holdHandler.OnHoldButton += () => _clockwiseButtonHeld = true;
            holdHandler.OnReleaseButton += () => _clockwiseButtonHeld = false;
        }
        if (_counterClockwise)
        {
            var holdHandler = _counterClockwise.gameObject.AddComponent<ButtonHoldHandler>();
            holdHandler.OnHoldButton += () => _counterClockwiseButtonHeld = true;
            holdHandler.OnReleaseButton += () => _counterClockwiseButtonHeld = false;
        }
    }
    private void Update()
    {
        if(_pivotPoint == null)
        {
            _pivotPoint = UIHandler.Instance.GetCurrentActiveModel().transform;
        }

        if (Input.GetKeyDown((KeyCode.Space)) && !_isTransitioning)
        {
            SwitchDayNightMode();
            Debug.Log("ork");
        }

        bool isEKeyPressed = Input.GetKey(KeyCode.E);
        bool isQKeyPressed = Input.GetKey(KeyCode.Q);
        bool isRotating = isEKeyPressed || isQKeyPressed || _clockwiseButtonHeld || _counterClockwiseButtonHeld;
        if (isRotating && _turnAroundCoroutine == null && _pivotPoint != null)
        {
           
            float rotationSpeed = isEKeyPressed || _clockwiseButtonHeld ? _rotationSpeed : -_rotationSpeed;
            _turnAroundCoroutine = StartCoroutine(TurnAround(_lightSource.transform, _pivotPoint.position, rotationSpeed));
            Debug.Log($"{(isEKeyPressed ? "E" : "Q")} key held: Starting rotation");
        }
        else if (!isRotating && _turnAroundCoroutine != null)
        {
            StopCoroutine(_turnAroundCoroutine);
            _turnAroundCoroutine = null;
            Debug.Log("Keys released: Stopping rotation");
        }
    }

    private IEnumerator TurnAround(Transform objToRotate, Vector3 pivotPoint, float rotationSpeed)
    {
        while (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q) || _clockwiseButtonHeld || _counterClockwiseButtonHeld)
        {

            float currentSpeed = (Input.GetKey(KeyCode.E) || _clockwiseButtonHeld) ? _rotationSpeed : -_rotationSpeed;
            objToRotate.RotateAround(pivotPoint, Vector3.up, currentSpeed * Time.deltaTime);
            objToRotate.LookAt(pivotPoint);
            yield return null;
        }
        _turnAroundCoroutine = null;

    }
    private IEnumerator TransisionLight()
    {

        _isTransitioning = true;
        _transisionTime = 0f;
        _transisionDuration = 2f;

        Color startColor = _isDay ? _nightColor : _dayColor;
        Color endColor = _isDay ? _dayColor : _nightColor;
        float startIntensity = _isDay ? _nightIntensity : _dayIntensity;
        float endIntensity = _isDay ? _dayIntensity : _nightIntensity;

        while (_transisionTime < _transisionDuration)
        {
            _transisionTime += Time.deltaTime;
            float t = _transisionTime / _transisionDuration;
            _lightSource.color = Color.Lerp(startColor, endColor, t);
            _lightSource.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
            yield return null;
        }

        _lightSource.color = endColor;
        _lightSource.intensity = endIntensity;
        _isTransitioning = false;
    }
    private void SwitchDayNightMode()
    {
        _isDay = !_isDay;
        StartCoroutine(TransisionLight());
        //update button
    }
  

}

