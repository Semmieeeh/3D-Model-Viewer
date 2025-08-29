using TMPro;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private GameObject previousModel;
    public TextMeshProUGUI creatorName;
    private ModelInfo modelInfo;
    private void Start()
    {
       LoadModel(UIHandler.Instance.GetModel());
    }
    public void LoadModel(GameObject model)
    {
        
        if (previousModel)
        {
            previousModel.SetActive(false);
            
        }
        modelInfo = model.GetComponent<ModelInfo>();
        creatorName.text = model.name;
        // previousmodel animate
        // model animate
        model.SetActive(true);
        previousModel = model;
        
    }

}
