using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private ColorChanger[] colorChangers = null;
    private Color color = Color.magenta;
    private bool isOdd = false;

    void Awake()
    {
        if (animator) animator.enabled = false;
        colorChangers = GetComponentsInChildren<ColorChanger>(true);
    }

    public void Init(Color _color, bool _isOdd, Vector3 _position)
    {
        color = _color;
        isOdd = _isOdd;
        if (!isOdd) DestroyImmediate(animator);
        else animator.enabled = true;
        ChangeColor(color);
        transform.position += _position;
    }

    private void ChangeColor(Color _color)
    {
        foreach (var c in colorChangers)
        {
            c.ChangeColor(_color);
        }
    }
}
