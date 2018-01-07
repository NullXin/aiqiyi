using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

[ExecuteInEditMode]
public class MeshImage : Graphic
{
    public Mesh mesh; 
     
    public Texture m_Texture;

    public override Texture mainTexture
    {
        get
        {
            return (m_Texture == null ? s_WhiteTexture : m_Texture);
        }
    } 

    protected override void OnPopulateMesh(VertexHelper vh)
    { 
        base.OnPopulateMesh(vh);
        vh.Clear(); 
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            vh.AddVert(mesh.vertices[i], this.color, mesh.uv[i]);
        } 
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            vh.AddTriangle(mesh.triangles[i], mesh.triangles[i + 1], mesh.triangles[i + 2]);
        }
    }  
}
