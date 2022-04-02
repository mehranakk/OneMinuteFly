using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class FlowerInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetComponent<LDtkFields>().GetColor("Color");
        transform.localScale *= GetComponent<LDtkFields>().GetInt("Size");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
