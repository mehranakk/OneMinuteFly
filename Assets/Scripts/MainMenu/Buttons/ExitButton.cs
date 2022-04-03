using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : Button
{
    protected override void OnClicked()
    {
        Application.Quit();
    }
}
