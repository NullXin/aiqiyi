using UnityEngine;
using System.Collections;

public class WeaponFlicker : MonoBehaviour
{
    private Material[] childMaterials;

    private void Start()
    {
        var childRenderers = GetComponentsInChildren<MeshRenderer>();
        childMaterials = new Material[childRenderers.Length];
        for (int i = 0; i < childMaterials.Length; i++)
        {
            childMaterials[i] = childRenderers[i].material;
        } 
    }

    public AnimationCurve curve;
    private float x = 0;
    public float duration = 3;
    private void Update()
    {
        if (IsFlickering)
        {
            x += Time.deltaTime / duration;
            SetMaterial(1 - curve.Evaluate(x));
        }
    }

    private void SetMaterial(float value)
    {
        for (int i = 0; i < childMaterials.Length; i++)
        {
            childMaterials[i].SetFloat("_Shininess", value);
        }
    }

    private bool IsFlickering = true;

    /// <summary>
    /// 设置闪烁状态
    /// </summary>
    /// <param name="state"></param>
    public void SetFlickerState(bool state)
    {
        IsFlickering = state;
        if (state)
            x = 0;
        else
            SetMaterial(1); 
    }

}
