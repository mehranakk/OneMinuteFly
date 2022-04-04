using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneLoad : MonoBehaviour
{
    private float themeVolumeUpLerpTime = 5f;
    float timer;

    void Start()
    {
        timer = 0;
        AudioManager.GetInstance().ChangeVolumeByName("main-menu", 0f);
        AudioManager.GetInstance().PlayByName("main-menu", transform.position);
    }

    private void Update()
    {
        if (timer < themeVolumeUpLerpTime)
        {
            timer += Time.deltaTime;
            AudioManager.GetInstance().ChangeVolumeByName("main-menu", Mathf.Lerp(0, 0.5f, timer / themeVolumeUpLerpTime));
        }
    }

    private void OnDisable()
    {
        AudioManager.GetInstance().StopByName("main-menu");
    }
}
