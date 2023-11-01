using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Slider progressBar;

    private void Awake()
    {
        progressBar = GetComponent<Slider>();
    }

    private void Update()
    {
        progressBar.value = Loader.GetLoadingProgress();
    }
}
