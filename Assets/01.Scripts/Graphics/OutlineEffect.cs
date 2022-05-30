using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    new private Renderer renderer;

    void Start()
    {
        renderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        renderer.enabled = true;
    }

    private Renderer CreateOutline(Material outlineMaterial, float outlineScaleFactor, Color color)
    {
        GameObject outline = Instantiate(gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outline.GetComponent<Renderer>();

        rend.material = outlineMaterial;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", outlineScaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outline.GetComponent<OutlineEffect>().enabled = false;
        //outline.GetComponent<Collider>()?.enabled = false;
        rend.enabled = false;

        return rend;
    }
}
