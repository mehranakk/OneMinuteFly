using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerFly : Interactable
{
    private Animator bouncerAnimator;
    private GameObject player;

    [SerializeField] private float velocity;

    private Vector3 partyCenter;

    protected new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        bouncerAnimator = GetComponent<Animator>();

        player = GameManager.GetInstance().GetPlayer();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        float playerDistanceToPartyCenter = Vector3.Distance(player.transform.position, partyCenter);
        if (playerDistanceToPartyCenter < 5f && distanceToPlayer > 1f)
        {
            MoveToPlayer();
        }
    }

    public override void Interact()
    {
        Debug.Log("Interact with bouncer fly");
    }

    public void Block()
    {
        bouncerAnimator.SetBool("ShouldBlock", true);
    }

    void MoveToPlayer()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        transform.Translate(dir * velocity * Time.deltaTime);
    }

    public void SetPartyCenter(Vector3 center)
    {
        partyCenter = center;
    }
}
