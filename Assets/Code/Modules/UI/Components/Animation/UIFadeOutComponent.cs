using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFadeOutComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float TimeBeforeFade;
    public float FadeOut;
    public bool IsFadeInterruptable;

    protected delegate void DelSetColour(float manualSet = -1);

    protected DelSetColour SetColourDel;
    protected float currentAlpha;
    protected float fadeOutInterval;
    protected float startingAlpha;

    private Image image;

    public virtual void Awake()
    {
        SetColourDel = SetColour;

        image = gameObject.GetComponent<Image>();
        currentAlpha = image.color.a;
        startingAlpha = currentAlpha;

        fadeOutInterval = currentAlpha / FadeOut / 100;
    }

    public void StartFade()
    {
        currentAlpha = startingAlpha;
        SetColourDel();

        StartCoroutine(FadeTimer());
    }

    private IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(TimeBeforeFade);

        StartCoroutine(FadeElement());
    }

    private IEnumerator FadeElement()
    {
        while (currentAlpha > 0)
        {
            yield return new WaitForSeconds(0.01f);
            SetColourDel();
        }
        gameObject.SetActive(false);
    }

    private void SetColour(float manualSet = -1.0f)
    {

        if (manualSet >= 0)
            currentAlpha = manualSet;
        else
            currentAlpha -= fadeOutInterval;

        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsFadeInterruptable)
        {
            StopAllCoroutines();
            SetColourDel(startingAlpha);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsFadeInterruptable)
        {
            StartCoroutine(FadeTimer());
        }
    }
}