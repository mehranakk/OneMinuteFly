using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhetherMatedUI : MonoBehaviour
{
    [SerializeField] private Sprite notMatedUIIcon;
    [SerializeField] private Sprite haveMatedUIIcon;

    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    void Start()
    {
        GameManager.GetInstance().OnPlayerDeath += OnPlayerDeath;
        MatingSystem.GetInstance().OnMate += OnMate;
    }

    private void OnMate()
    {
        iconImage.sprite = haveMatedUIIcon;
    }

    private void OnPlayerDeath()
    {
        iconImage.sprite = notMatedUIIcon;
    }

    private void OnDestroy()
    {
        GameManager.GetInstance().OnPlayerDeath -= OnPlayerDeath;
        MatingSystem.GetInstance().OnMate -= OnMate;
    }
}
