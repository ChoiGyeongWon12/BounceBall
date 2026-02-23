using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameStartSequence : MonoBehaviour
{
    [SerializeField] Image gameImage;
    [SerializeField] Image gameImage_2;
    [SerializeField] float fadeSpeed = 1f;

    [SerializeField] Button but;


    WaitForSeconds waitTime = new WaitForSeconds(2);



    void Start()
    {
        StartCoroutine(StartSequence());
    }


    public void OnButton()
    {
        SceneManager.LoadScene(1);
    }


    IEnumerator StartSequence()
    {
        yield return StartCoroutine(FadeIn(gameImage));
        yield return waitTime;
        yield return StartCoroutine(FadeOut(gameImage));

        yield return StartCoroutine(FadeIn(gameImage_2));
        yield return waitTime;
        but.gameObject.SetActive(true);
    }


    IEnumerator FadeIn(Image _image)
    {
        _image.gameObject.SetActive(true);
        Color color = _image.color;
        color.a = 0;

        while (color.a < 1)
        {
            color.a += fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            _image.color = color;
            yield return null;
        }

        color.a = 1;
        _image.color = color;
    }


    IEnumerator FadeOut(Image _image)
    {
        Color color = _image.color;
        color.a = 1;

        while (color.a > 0.5)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            _image.color = color;
            yield return null;
        }
        color.a = 0.5f;
        _image.color = color;

        _image.gameObject.SetActive(false);
    }
}
