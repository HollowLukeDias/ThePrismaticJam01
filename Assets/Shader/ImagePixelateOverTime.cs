using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePixelateOverTime : MonoBehaviour
{
    [SerializeField] private float pixelationSpeed;
    [SerializeField] private float maxPixelation;
    [SerializeField] Material effectMaterial;

    private Coroutine pixelation;

    void Start()
    {
        effectMaterial.SetFloat("_Colummns", 1920);
        effectMaterial.SetFloat("_Rows", 1080);
    }

    public void CallPixelateOut()
    {
        pixelation = StartCoroutine(PixelateOut());
    }
    private IEnumerator PixelateOut()
    {
        var startCollumnPixelation = 384f;
        var startRowPixelation = 216f;
        float currentColumnPixelation = startCollumnPixelation;
        float currentRowPixelation = startRowPixelation;
        var i = 0;
        while (currentColumnPixelation >= maxPixelation)
        {
            currentColumnPixelation = startCollumnPixelation - 2f*i;
            currentRowPixelation = startRowPixelation - 1.125f*i;
            effectMaterial.SetFloat("_Colummns", currentColumnPixelation);
            effectMaterial.SetFloat("_Rows", currentRowPixelation);
            i++;
            yield return new WaitForSeconds(1/pixelationSpeed);
        }
    }

    IEnumerator PixelateIn()
    {
        yield break;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination){
        Graphics.Blit(source, destination, effectMaterial);
    }
}
