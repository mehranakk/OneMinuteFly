using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenuButton: Button
{
    protected override void OnClicked()
    {
        GameManager.GetInstance().LoadScene(0);
    }
}
