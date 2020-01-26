using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private AnimationCurve curve;
    
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        var timeToFade = 1f;
        while (timeToFade > 0f)
        {
            timeToFade -= Time.deltaTime;
            float a = curve.Evaluate(timeToFade);
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string sceneName)
    {
        var timeToFade = 0f;
        while (timeToFade < 1f)
        {
            
            timeToFade += Time.deltaTime;
            float a = curve.Evaluate(timeToFade);
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneName);
    }
    
}
