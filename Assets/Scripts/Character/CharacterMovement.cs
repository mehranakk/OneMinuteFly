using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontalInput, verticalInput;

    [SerializeField]
    private float velocity;

    private Vector2 speed;
        
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        speed = new Vector2(horizontalInput * velocity, verticalInput * velocity);

        if (Input.GetKeyDown(KeyCode.F))
            InteractionSystem.GetInstance().EnterInteraction();

        transform.Translate(speed * Time.deltaTime);
    }
}
