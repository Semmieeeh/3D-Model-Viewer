using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private GameObject previousModel;
    private void Start()
    {
        
    }
    public void LoadModel(GameObject model)
    {
        if (previousModel)
        {
            previousModel.SetActive(false);
        }
        model.SetActive(true);
        previousModel = model;
        
    }

}
