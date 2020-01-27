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
