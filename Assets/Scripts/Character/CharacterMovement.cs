using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontalInput, verticalInput;

    [SerializeField] private float velocity;
    private Vector2 speed;

    private bool isDead = false;
        
    void Update()
    {
        if (isDead)
            return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        speed = new Vector2(horizontalInput * velocity, verticalInput * velocity);

        if (Input.GetKeyDown(KeyCode.F))
            InteractionSystem.GetInstance().EnterInteraction();

        transform.Translate(speed * Time.deltaTime);
    }

    public void Die()
    {
        Debug.Log("Player Died");
        isDead = true;
        StartCoroutine(WaitAndDeactive());
    }

    IEnumerator WaitAndDeactive()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
