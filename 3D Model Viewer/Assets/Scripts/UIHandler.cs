using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private int _selectedModel;
    [SerializeField] private GameObject[] _allModels;
 
    [SerializeField] private GameObject _currentActivePrefab;
    [SerializeField] private GameObject _currentActiveModel;

    [SerializeField] private Material _claymaterial;
    [SerializeField] private Material _unlit;
    [SerializeField] private Material _lit;


    [SerializeField] private Button _clayMaterialButton;
    [SerializeField] private Button _UnlitMaterialButton;
    [SerializeField] private Button _litMaterialButton;

    [SerializeField]private Material _originalMaterial;
    public static UIHandler Instance {  get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        _selectedModel = PlayerPrefs.GetInt("SelectedImageIndex");
        _currentActiveModel = _allModels[_selectedModel].gameObject;
       
    }

    private void Start()
    {
        _clayMaterialButton.onClick.AddListener(SetClayMaterial);
        _litMaterialButton.onClick.AddListener(SetLitMaterial);
        _UnlitMaterialButton.onClick.AddListener(SetUnlitMaterial);
    }

    private void Update()
    {
        if(_originalMaterial == null)
        {
            _originalMaterial = _currentActiveModel.GetComponent<Renderer>().material;
        }
    }

    public GameObject GetModel()
   {
        return _currentActiveModel;
   }
   
    public void SetClayMaterial()
    {
       SetMaterial(_claymaterial,true,Color.gray);
    }
    public void SetUnlitMaterial()
    {
      SetMaterial(_unlit,true,Color.white);
    }
    public void SetLitMaterial()
    {
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
    public void SetOriginalMaterial(Material newmat)
    {
        _originalMaterial = newmat;
    }
    public void SetCurrentModel(GameObject newmodel)
    {
        
        _currentActiveModel = newmodel;
    }
    public void LoadPrevieusScene()
    {
        SceneManager.LoadScene(0);
    }


}
