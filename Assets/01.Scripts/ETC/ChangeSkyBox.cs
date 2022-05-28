using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;
    private Material skyboxMaterial;
    private float timer = 0f;
    private int index = 0;
    private int nextIndex = 1;

    [SerializeField]
    [Range(0.01f, 1f)]
    float speed = 0.5f;

    [SerializeField]
    private bool isReverse = false;

    private void Start()
    {
        if (isReverse)
        {
            materials = materials.Reverse().ToArray();
        }

        skyboxMaterial = new Material(materials[0]);
        RenderSettings.skybox = skyboxMaterial;
        StartCoroutine(ChangeSkyColor());
    }


    private IEnumerator ChangeSkyColor()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            timer += Time.deltaTime * speed;

            ChangeAscendingColor(timer);

            if (timer > 3f)
            {
                timer = 0f;
                index = (index + 1) % materials.Length;
                nextIndex = (index + 1) % materials.Length;
            }

            yield return null;
        }
    }

    private void ChangeAscendingColor(float lerpTime)
    {
        Color sunDiscColor = Color.Lerp(materials[index].GetColor("_SunDiscColor"),
            materials[nextIndex].GetColor("_SunDiscColor"), lerpTime);
        skyboxMaterial.SetColor("_SunDiscColor", sunDiscColor);

        Color sunHaloColor = Color.Lerp(materials[index].GetColor("_SunHaloColor"),
            materials[nextIndex].GetColor("_SunHaloColor"), lerpTime);
        skyboxMaterial.SetColor("_SunHaloColor", sunHaloColor);

        Color horizonColor = Color.Lerp(materials[index].GetColor("_HorizonLineColor"),
            materials[nextIndex].GetColor("_HorizonLineColor"), lerpTime);
        skyboxMaterial.SetColor("_HorizonLineColor", horizonColor);

        Color skyTopColor = Color.Lerp(materials[index].GetColor("_SkyGradientTop"),
            materials[nextIndex].GetColor("_SkyGradientTop"), lerpTime);
        skyboxMaterial.SetColor("_SkyGradientTop", skyTopColor);

        Color skyBottomColor = Color.Lerp(materials[index].GetColor("_SkyGradientBottom"),
            materials[nextIndex].GetColor("_SkyGradientBottom"), lerpTime);
        skyboxMaterial.SetColor("_SkyGradientBottom", skyBottomColor);
    }
}
