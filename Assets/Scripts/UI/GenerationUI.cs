using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerationUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().OnPlayerDeath += OnPlayerDeath;
    }

    private void OnEnable()
    {
        //GameManager.GetInstance().OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        GameManager.GetInstance().OnPlayerDeath -= OnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerDeath()
    {
        GetComponent<TextMeshProUGUI>().text = "Generation: " + (GameManager.GetInstance().GetCurrentGeneration() + 1);
    }
}
