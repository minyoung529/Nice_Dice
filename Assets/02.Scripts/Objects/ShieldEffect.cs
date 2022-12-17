using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    private Material material;
    private int hashFill = Shader.PropertyToID("_fill");

    void Start()
    {
        material ??= GetComponentInChildren<Renderer>().material;
    }

    private void OnEnable()
    {
        material ??= GetComponentInChildren<Renderer>().material;

        material.SetFloat(hashFill, -1f);
        material.DOFloat(0.05f, hashFill, 0.5f);
    }
}
