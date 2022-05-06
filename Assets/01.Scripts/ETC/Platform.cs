using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Platform : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        SetUpGrid();
    }

    void SetUpGrid()
    {
        Vector2 newOffset = new Vector2(transform.localScale.x, transform.localScale.z);
        meshRenderer.material.mainTextureScale = new Vector2(newOffset.x, newOffset.y);
    }
}
