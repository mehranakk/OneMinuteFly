using System.Collections;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float minDist = 1, maxDist = 2;
    [SerializeField] private float maxVolume = 0.2f;

    private Animator frogAnimator;

    [SerializeField] private float buildUpTime;

    private bool flyInRange;
    private bool attackInProgress;

    private Sound frogIdleSound;

    // Start is called before the first frame update
    void Start()
    {
        frogIdleSound = AudioManager.GetInstance().CopyByName("frog-idle");

        frogIdleSound.source = gameObject.AddComponent<AudioSource>();

        frogIdleSound.source.clip = frogIdleSound.clip;
        frogIdleSound.source.volume = frogIdleSound.volumeDefault;
        frogIdleSound.source.pitch = frogIdleSound.pitchDefault;
        frogIdleSound.source.loop = frogIdleSound.loopDefault;

        frogAnimator = GetComponent<Animator>();

        ResetFrog();

        frogIdleSound.source.PlayDelayed(Random.Range(0, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (flyInRange && !attackInProgress)
            StartCoroutine(Attack());

        HandleAudioVolume();
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

        AudioManager.GetInstance().PlayByName("frog-attack", transform.position);

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

    private void OnGameOver() => frogIdleSound.source.Stop();
    private void OnPlayerDeath()
    {
        //frogIdleSound.source.Stop();
    }

    private void OnEnable()
    {
        GameManager.GetInstance().OnGameOver += OnGameOver;
        GameManager.GetInstance().OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        GameManager.GetInstance().OnGameOver -= OnGameOver;
        GameManager.GetInstance().OnPlayerDeath -= OnPlayerDeath;
    }

    private void HandleAudioVolume()
    {
        Vector3 distance = GameManager.GetInstance().GetPlayer().transform.position - transform.position;
        float playerDistance = distance.magnitude;
        float audiblePercentage = Mathf.InverseLerp(maxDist, minDist, playerDistance);

        frogIdleSound.source.volume = maxVolume * audiblePercentage;
    }
}
