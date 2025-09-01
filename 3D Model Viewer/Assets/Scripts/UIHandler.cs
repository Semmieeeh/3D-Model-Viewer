using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private int _selectedModel;
    [SerializeField] private GameObject[] _allModels;

    [Header("Chosen model")]
    [SerializeField] private GameObject _currentActivePrefab;
    [SerializeField] private GameObject _currentActiveModel;
   
    [Header("Materials")]

    [SerializeField] private Material _claymaterial;
    [SerializeField] private Material _unlit;
    [SerializeField] private Material _lit;

    [Header("Material Buttons")]

    [SerializeField] private Button _clayMaterialButton;
    [SerializeField] private Button _UnlitMaterialButton;
    [SerializeField] private Button _litMaterialButton;

    [SerializeField]private Material _originalMaterial;

    [Header("Audio")]

    [SerializeField] private AudioSource _audioSource;
    public static UIHandler Instance {  get; private set; }
    
    void Awake()
        // We gebruiken hier een singleton pattern waardoor we dit script op meerdere plekken kan aanroepen zonder reference
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // halen opgslagen model om als eerste in te laden
        _selectedModel = PlayerPrefs.GetInt("SelectedImageIndex");
        _currentActiveModel = _allModels[_selectedModel].gameObject;

    }

    private void Start()
    {
        // material subscriben tot action
        _clayMaterialButton.onClick.AddListener(SetClayMaterial);
        _litMaterialButton.onClick.AddListener(SetLitMaterial);
        _UnlitMaterialButton.onClick.AddListener(SetUnlitMaterial);

       

    }

    private void Update()
    {
        // Check of _original material niet leg is anders vul in
        if(_originalMaterial == null)
        {
            _originalMaterial = _currentActiveModel.GetComponent<Renderer>().material;
        }
    }


   // Button functions
    #region Material Switcher
    public void SetClayMaterial()
    {
        CallSound();
        SetMaterial(_claymaterial,true,Color.gray);
    }
    public void SetUnlitMaterial()
    {
        CallSound();
        SetMaterial(_unlit,true,Color.white);
    }
    public void SetLitMaterial()
    {
        CallSound();
        SetMaterial(_lit,true,Color.white);
        if(_originalMaterial != null && _originalMaterial.shader.name.Contains("Lit"))
        {
            Renderer renderer = _currentActiveModel.GetComponent<Renderer>();
            if(renderer != null)
            {
                renderer.material = new Material(_originalMaterial);
            }
        }
    }
    // Set material van de gekoze button.
    private void SetMaterial(Material material, bool useMaterial, Color color)
    {
        if(_currentActiveModel != null)
        {
            Renderer renderer = _currentActiveModel.GetComponent<Renderer>();
            if(renderer !=null)
            {
                if (useMaterial)
                {
                    Material newMat = new Material(material);
                    if (material == _claymaterial)
                    {
                        newMat.mainTexture = null;
                        newMat.color = color;
                    }
                    else
                    {
                        if(_originalMaterial.mainTexture != null)
                        {
                            newMat.mainTexture = _originalMaterial.mainTexture;
                        }
                    }
                   renderer.material = newMat;

                }
                else
                {
                    renderer.material.shader = material.shader;
                    renderer.material.color = color;
                }
            }
        }
    }
    //  Sett original material
    public void SetOriginalMaterial(Material newmat)
    {
       
        _originalMaterial = newmat;
    }
    #endregion


    #region SwitchModel
    public GameObject GetModel()
    {
        if (_currentActiveModel != null)
        {
            return _currentActiveModel;
        }
        return null;
    }

    public void SetCurrentModel(GameObject newmodel)
    {
        _currentActiveModel = newmodel;
    }
    #endregion 

    public void CallSound()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void LoadPrevieusScene()
    {
        SceneManager.LoadScene(0);
    }


}
