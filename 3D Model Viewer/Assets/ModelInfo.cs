using UnityEngine;

public class ModelInfo : MonoBehaviour
{
    public string creatorName;
    [TextArea]
    public string info;
    public int vertexCount;
    public int faceCount;
    private Material _ownMaterial;
    private void Awake()
    {
        UpdateModelInfo();
    }
    private void Update()
    {
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
