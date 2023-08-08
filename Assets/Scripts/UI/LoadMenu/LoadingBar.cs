using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
  
    private Progress progress;
    private Image barImg;

    private void Awake()
    {
        barImg = transform.Find("Bar").GetComponent<Image>();

        progress = new Progress();

    }
    private void Update()
    {
        progress.Update();
        barImg.fillAmount = progress.GetProgressNormalized();
    }
}
public class Progress
{
    public const int progress_max = 100;

    private float progressAmount;
    private float progressRegenAmount;

    public Progress()
    {
        progressAmount = 0;
        progressRegenAmount = 30f;
    }
    public void Update()
    {
        progressAmount += progressRegenAmount * Time.deltaTime;
        progressAmount = Mathf.Clamp(progressAmount, 0f, progress_max);
    }
    public float GetProgressNormalized()
    {
        return progressAmount / progress_max;
    }
}
