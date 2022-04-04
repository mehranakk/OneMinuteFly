using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : Button
{
    protected override void OnClicked()
    {
        GameManager.GetInstance().ResumeGameFromMenu();
    }
}
