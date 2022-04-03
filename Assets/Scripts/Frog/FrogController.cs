using System.Collections;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Animator frogAnimator;

    [SerializeField] private float buildUpTime;

    private bool flyInRange;
    private bool attackInProgress;

    // Start is called before the first frame update
    void Start()
    {
        frogAnimator = GetComponent<Animator>();

        ResetFrog();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyInRange && !attackInProgress)
            StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flyInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flyInRange = false;
        }
    }

    IEnumerator Attack()
    {
        attackInProgress = true;

        yield return new WaitForSeconds(buildUpTime);

        frogAnimator.SetTrigger("Attack");

        if (flyInRange)
        {
            Debug.Log("Frog Attacked Fly: Successful");
            GameManager.GetInstance().KillFly();
        }
        else
        {
            Debug.Log("Frog Attacked Fly: Failed");
        }

        ResetFrog();
    }

    private void ResetFrog()
    {
        flyInRange = false;
        attackInProgress = false;
    }
}
