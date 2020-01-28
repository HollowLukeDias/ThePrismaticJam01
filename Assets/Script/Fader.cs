using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{

    #region Inspector Variables
    
    [SerializeField] private Image _fadeImage;
    [SerializeField] private AnimationCurve _curveIn;
    [SerializeField] private AnimationCurve _curveOut;
    [SerializeField] private float _timeToFadeOut;
    [SerializeField] private float _timeToFadeIn;
    [SerializeField] private ImagePixelateOverTime _pixelation;
    
    #endregion
    
    #region Fade In and Out logic
    
    /// <summary>
    /// Make an image go from totally black to completely transparent using a curve as reference
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        var timeToFade = 1f;
        _fadeImage.gameObject.SetActive(true);
        while (timeToFade > 0f)
        {
            timeToFade -= Time.deltaTime/_timeToFadeIn;
            float a = _curveIn.Evaluate(timeToFade);
            _fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        _fadeImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Make an image go from totally transparent to completely black using a curve as reference
    /// <para>And after it is completely black it load the scene</para>
    /// </summary>
    /// <param name="sceneName">The name of the scene that will be loaded after the image is black</param>
    /// <returns></returns>
    private IEnumerator FadeOut(string sceneName)
    {
        var timeToFade = 0f;
        _fadeImage.gameObject.SetActive(true);
        while (timeToFade < 1f)
        {
            
            timeToFade += Time.deltaTime/_timeToFadeOut;
            float a = _curveOut.Evaluate(timeToFade);
            _fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneName);
    }
    
    #endregion
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        _pixelation.CallPixelateOut();
        StartCoroutine(FadeOut(scene));
    }


}
