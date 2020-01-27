using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePixelateOverTime : MonoBehaviour
{
    [SerializeField] private float _pixelationSpeed;
    [SerializeField] private float _maxPixelation;
    [SerializeField] private Material _effectMaterial;

    private Coroutine pixelation;

    void Start()
    {
        _effectMaterial.SetFloat("_Colummns", 1920);
        _effectMaterial.SetFloat("_Rows", 1080);
    }

    public void CallPixelateOut()
    {
        pixelation = StartCoroutine(PixelateOut());
    }
    private IEnumerator PixelateOut()
    {
        const float startColumnPixelation = 384f;
        const float startRowPixelation = 216f;
        
        float currentColumnPixelation = startColumnPixelation;
        float currentRowPixelation = startRowPixelation;
        var i = 0;
        while (currentColumnPixelation >= _maxPixelation)
        {
            currentColumnPixelation = startColumnPixelation - 2f*i;
            currentRowPixelation = startRowPixelation - 1.125f*i;
            _effectMaterial.SetFloat("_Colummns", currentColumnPixelation);
            _effectMaterial.SetFloat("_Rows", currentRowPixelation);
            i++;
            yield return new WaitForSeconds(1/_pixelationSpeed);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination){
        Graphics.Blit(source, destination, _effectMaterial);
    }
}
