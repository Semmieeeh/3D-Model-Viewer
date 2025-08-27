using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private int _selectedModel;
    [SerializeField] private GameObject[] _allModels;
 
    [SerializeField] private GameObject _currentActivePrefab;
    [SerializeField] private GameObject _currentActiveModel;
    [SerializeField] private Material _claymaterial;
    [SerializeField] private Material _unlit;
    [SerializeField] private Material _lit;


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
        _currentActivePrefab = _allModels[_selectedModel].gameObject;

        _currentActiveModel = Instantiate(_currentActivePrefab);
        
    }

   public void SetMaterial( Shader shader, bool isLit, Color color)
    {
        Renderer[] renders = _currentActiveModel.GetComponentsInChildren<Renderer>();
        if(renders.Length > 0 )
        {
            foreach(Renderer render in renders)
            {
                Material currentMaterial = render.material;
                currentMaterial.shader = shader ?? currentMaterial.shader;
                currentMaterial.color = color;
            }
        }
        

        
    }

   public GameObject GetModel()
   {
        return _currentActiveModel;
   }
   
    public void ChangeMatUnlit()
    {
        Shader unlitShader = Shader.Find("Unlit/Color");
        if(unlitShader == null)
        {
            return;
        }
        SetMaterial(unlitShader ,false, Color.white);
    }

    public GameObject GetCurrentActiveModel()
    {
        return _currentActiveModel;
    }
}
