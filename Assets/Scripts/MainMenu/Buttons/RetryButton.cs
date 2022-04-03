using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    protected override void OnClicked()
    {
        GameManager.GetInstance().Retry();
    }
}
