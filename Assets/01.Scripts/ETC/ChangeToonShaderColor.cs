using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToonShaderColor : MonoBehaviour
{
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    new private Renderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        Material material = new Material(renderer.material);

        material.SetColor("_BaseColor", firstColor);
        material.SetColor("_1st_ShadeColor", secondColor);

        renderer.material = material;
    }
}
