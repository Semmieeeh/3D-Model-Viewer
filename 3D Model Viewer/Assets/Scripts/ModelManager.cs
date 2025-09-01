using TMPro;
using UnityEngine;
using System.Collections;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private GameObject previousModel;
    [SerializeField] private ModelInfo modelInfo;
    [SerializeField] private float animationDuration;

    [Header("UI Elements")]
    public TextMeshProUGUI modelName;
    public TextMeshProUGUI creatorName;
    public TextMeshProUGUI tris;
    public TextMeshProUGUI verts;
    public TextMeshProUGUI generalInfo;
    

    private void Start()
    {
       LoadModel(UIHandler.Instance.GetModel());
    }

    // Load new model in if its not the same model else return
    public void LoadModel(GameObject newModel)
    {
        if (previousModel == newModel)
        {
            return;
        }
        else
        {
            UIHandler.Instance.CallSound();
        }
        StartCoroutine(SwitchModelRoutine(newModel));
    }

    // this Ienumerators calls the scale overtime ienumerator that  scales model to 0.1f before setting it inactive and sets new model active 
    // and scales this one up from 0.01f to 1
    private IEnumerator SwitchModelRoutine(GameObject newModel)
    {
       

        if (previousModel != null)
        {
            yield return StartCoroutine(ScaleOverTime(previousModel, Vector3.one, Vector3.one * 0.01f, animationDuration));
            previousModel.SetActive(false);
        }

       
        newModel.transform.localScale = Vector3.one * 0.01f;
        newModel.SetActive(true);
        UIHandler.Instance.SetCurrentModel(newModel);
        UIHandler.Instance.SetOriginalMaterial(newModel.GetComponent<ModelInfo>()._ownMaterial);
        
        
      
        yield return StartCoroutine(ScaleOverTime(newModel, Vector3.one * 0.01f, Vector3.one, animationDuration));

        
        previousModel = newModel;
        modelInfo = newModel.GetComponent<ModelInfo>();
        SetInfo(modelInfo);
    }
   
    
    // this Ienumerator handlees the scaling of objects 
    private IEnumerator ScaleOverTime(GameObject target, Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (target == null) yield break;

            target.transform.localScale = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.transform.localScale = to;
    }

    // Sets info for current Active Model
    private void SetInfo(ModelInfo model)
    {
        modelName.text = model.gameObject.name;
        creatorName.text = model.creatorName;
        tris.text = model.faceCount.ToString();
        verts.text = model.vertexCount.ToString();
        generalInfo.text = model.info;

    }

}
