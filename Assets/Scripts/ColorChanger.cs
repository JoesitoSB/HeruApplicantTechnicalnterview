using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(Color _color)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        
        if(!meshRenderer)
            return;

        if (meshRenderer.materials.Length >= 0)
        {
            foreach (var m in meshRenderer.materials)
            {
                m.color = _color;
            }
        }
    }
}
