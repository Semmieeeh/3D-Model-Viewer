using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class LightingHandler : MonoBehaviour
{
    [Header("Light Sources")]

    [SerializeField] private Light _lightSource;
    [SerializeField] private Light _pointLight;
    
    
    [Header("Colour skybox")]

    [SerializeField] private Color _dayColorLight;
    [SerializeField] private Color _nightColorNight;
    [SerializeField] private Color _dayColorSkyBox;
    [SerializeField] private Color _nightColorSkyBox;
    [SerializeField] private Color _groundColorDay;
    [SerializeField] private Color _groundColorNight;

    private float _transisionTime = 0f;
    private float _transisionDuration = 2f;
    private float _dayIntensity = 1f;
    private float _nightIntensity = 0.1f;
    private float _rotationSpeed;

    private bool _isTransitioning = false;
    private bool _isDay = true;

    private bool _wasEKeyPressed;
    private bool _wasQKeyPressed;
    private Coroutine _turnAroundCoroutine;

    private Transform _pivotPoint;
    [Header("Buttons Toggle ")]
    [SerializeField] private Button _toggleButtonDayNight;
    [SerializeField] private Button _clockWiseButton;
    [SerializeField] private Button _counterClockwise;

    private bool _clockwiseButtonHeld;
    private bool _counterClockwiseButtonHeld;

    [Header("Skybox reference")]
    [SerializeField] private Material _skyboxMaterial;

    [Header("Animator button day night")]
    [SerializeField] private Animator _animatorButtonDay;
    [SerializeField] private Animation _rotateSunIconRight;
    [SerializeField] private Animation _rotateSunIconLeft;



    private void Start()
    {
        //subscribing handlers  voor button dag nacht
        if (_toggleButtonDayNight != null)
        {
            _toggleButtonDayNight.onClick.AddListener(SwitchDayNightMode);
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
        // Set dag eerst active
        _skyboxMaterial.SetColor("_Sky_Color", _dayColorSkyBox);
        _skyboxMaterial.SetFloat("_starIntesity", 0);
        _skyboxMaterial.SetColor("_GroundColor",_groundColorDay);
    }

    private void Update()
    {
     
        // pak pivotpoint als leeg is
        if(_pivotPoint == null)
        {
            _pivotPoint = UIHandler.Instance.GetModel().transform;
        }
        _animatorButtonDay.SetBool("Activate", _isDay);
        // f key voor shortcut dag/ nacht
        if (Input.GetKeyDown((KeyCode.F)) && !_isTransitioning)
        {
            SwitchDayNightMode();
        }

        bool isEKeyPressed = Input.GetKey(KeyCode.E);
        bool isQKeyPressed = Input.GetKey(KeyCode.Q);
        bool isRotating = isEKeyPressed || isQKeyPressed || _clockwiseButtonHeld || _counterClockwiseButtonHeld;
        if (isRotating && _turnAroundCoroutine == null && _pivotPoint != null)
        {
         
            float rotationSpeed = isEKeyPressed || _clockwiseButtonHeld ? _rotationSpeed : -_rotationSpeed;
            _turnAroundCoroutine = StartCoroutine(TurnAround(_lightSource.transform, _pointLight.transform, _pivotPoint.position, rotationSpeed));
            Debug.Log($"{(isEKeyPressed ? "E" : "Q")} key held: Starting rotation");
        }
        else if (!isRotating && _turnAroundCoroutine != null)
        {
            // als je de button los laat  reset het de speed een maaakt het de couritine leeg
            StopCoroutine(_turnAroundCoroutine);
            _rotationSpeed = 20;
            _turnAroundCoroutine = null;
            Debug.Log("Keys released: Stopping rotation");
        }
    }



    #region Rotation Lightspeeeler z
    // Deze Ienumerator zorg voor de rotatie van het licht voor zolang de user de knop ingedrukt houdt
    private IEnumerator TurnAround(Transform objToRotate, Transform pointLight, Vector3 pivotPoint, float rotationSpeed)
    {
        UIHandler.Instance.CallSound();
        while (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q) || _clockwiseButtonHeld || _counterClockwiseButtonHeld)
        {
          
            // zolang de rotatie speed onder 100 is voegt hij snelheid toe
            if (rotationSpeed < 100)
            {
                _rotationSpeed += 5f * Time.deltaTime;
            }
            float currentSpeed = (Input.GetKey(KeyCode.E) || _clockwiseButtonHeld) ? _rotationSpeed : -_rotationSpeed;
            objToRotate.RotateAround(pivotPoint, Vector3.up, currentSpeed * Time.deltaTime);
            pointLight.LookAt(pivotPoint);
            Animation animation = (Input.GetKey(KeyCode.E) || _clockwiseButtonHeld) ? _rotateSunIconRight : _rotateSunIconLeft;
            animation.Play();
            yield return null;
        }
        _turnAroundCoroutine = null;

    }
    #endregion


    #region SwitchLights
    private IEnumerator TransisionLight()
    {

        _isTransitioning = true;
        _transisionTime = 0f;
        _transisionDuration = 2f;
        // kiest de kleur voor de skybox en Light sources.

        Color startColor = _isDay ? _nightColorNight : _dayColorLight;
        Color endColor = _isDay ? _dayColorLight : _nightColorNight;
        float startIntensity = _isDay ? _nightIntensity : _dayIntensity;
        float endIntensity = _isDay ? _dayIntensity : _nightIntensity;

        Color startSkyColor = _isDay ? _nightColorSkyBox : _dayColorSkyBox;
        Color endSkyColor = _isDay ? _dayColorSkyBox : _nightColorSkyBox;

        Color startGroundColor = _isDay ? _groundColorNight : _groundColorDay;
        Color endGroundColor = _isDay ? _groundColorDay : _groundColorNight;
        float startStarsCount = _isDay ? 298 : 0;
        float endStarsCount = _isDay ? 0 : 298;
       
        // over tijd verander de kleur
        while (_transisionTime < _transisionDuration)
        {
            _transisionTime += Time.deltaTime;
            float t = _transisionTime / _transisionDuration;
            _lightSource.color = Color.Lerp(startColor, endColor, t);
            _lightSource.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
            _skyboxMaterial.SetColor("_Sky_Color", Color.Lerp(startSkyColor, endSkyColor, t));
            _skyboxMaterial.SetFloat("_starIntesity", Mathf.Lerp(startStarsCount,endStarsCount,t));
            _skyboxMaterial.SetColor("_GroundColor", Color.Lerp(startGroundColor,endGroundColor,t));
            yield return null;
        }
        // zet eind kleur als kleur
        _lightSource.color = endColor;
        _lightSource.intensity = endIntensity;
        _isTransitioning = false;
    }
   
    

    // Toggle button dag nacht
    private void SwitchDayNightMode()
    {
        UIHandler.Instance.CallSound();

        if (!_isTransitioning)
        {
            _isDay = !_isDay;
            StartCoroutine(TransisionLight());
        }


        if (_animatorButtonDay != null)
        {
            _animatorButtonDay.Play("SwitchDayButton");

        }


    }

    #endregion

}

