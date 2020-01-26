using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private AnimationCurve curveIn;
    [SerializeField] private AnimationCurve curveOut;
    [SerializeField] private float timeToFadeOut;
    [SerializeField] private float timeToFadeIn;
    [SerializeField] private ImagePixelateOverTime pixelation;
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        pixelation.CallPixelateOut();
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        var timeToFade = 1f;
        fadeImage.gameObject.SetActive(true);
        while (timeToFade > 0f)
        {
            timeToFade -= Time.deltaTime/timeToFadeIn;
            float a = curveIn.Evaluate(timeToFade);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        var timeToFade = 0f;
        fadeImage.gameObject.SetActive(true);
        while (timeToFade < 1f)
        {
            
            timeToFade += Time.deltaTime/timeToFadeOut;
            float a = curveOut.Evaluate(timeToFade);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneName);
    }
    
}
