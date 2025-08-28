using UnityEngine;

[ExecuteInEditMode]
public class ModelInfo : MonoBehaviour
{
    [TextArea]
    public string info;
    public int vertexCount;
    public int faceCount;

    private void Start()
    {
        UpdateModelInfo();
    }
    private void OnValidate()
    {
        UpdateModelInfo();
    }

    private void Update()
    {
        if (Application.isPlaying) 
            UpdateModelInfo();
    }

    private void UpdateModelInfo()
    {
        // Collect all MeshFilters 
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        foreach (var mf in meshFilters)
        {
            if (mf.sharedMesh != null)
            {
                vertexCount = mf.sharedMesh.vertexCount;
                faceCount = mf.sharedMesh.triangles.Length / 3;
            }
        }
    }
}
