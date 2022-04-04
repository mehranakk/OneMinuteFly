using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneLoad : MonoBehaviour
{
    private float themeVolumeUpLerpTime = 5f;
    float timer;

    void Start()
    {
        AudioManager.GetInstance().ChangeVolumeByName("main-menu", 0);
        AudioManager.GetInstance().PlayByName("main-menu", transform.position);
        timer = 0;
    }

    private void Update()
    {
        if (timer < themeVolumeUpLerpTime)
        {
            timer += Time.deltaTime;
            AudioManager.GetInstance().ChangeVolumeByName("main-menu", Mathf.InverseLerp(0, themeVolumeUpLerpTime, timer));
        }
    }
}
