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
    
    /// <summary>
    /// Sets the current pixels to a lot less
    /// <para>Then it subtracts from there each iteration, making the scene seems to pixelate</para>
    /// </summary>
    /// <returns>The time between pixelations</returns>
    private IEnumerator PixelateOut()
    {
        const float startColumnPixelation = 384f;
        const float startRowPixelation = 216f;
        
        float currentColumnPixelation = startColumnPixelation;
        var i = 0;
        while (currentColumnPixelation >= _maxPixelation)
        {
            currentColumnPixelation = startColumnPixelation - 2f*i;
            var currentRowPixelation = startRowPixelation - 1.125f*i;
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
