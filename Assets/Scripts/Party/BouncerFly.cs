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
        if (!IsInteractionLock())
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            float playerDistanceToPartyCenter = Vector3.Distance(player.transform.position, partyCenter);
            if (playerDistanceToPartyCenter < 5f && distanceToPlayer > 1f)
            {
                MoveToPlayer();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!isInteractionLock)
            ShouldBlock(true);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (!isInteractionLock)
            ShouldBlock(false);
    }

    public override void Interact()
    {
        Debug.Log("Interact with bouncer fly");

        player = GameManager.GetInstance().GetPlayer();

        if (player.GetComponent<InventoryController>().GetCoin(1))
        {
            ShouldBlock(false);
            LockInteraction();
            GetComponentInParent<PartyManager>().EnterParty();
            Debug.Log("fly can join the party");
        }
    }

    public void ShouldBlock(bool state)
    {
        bouncerAnimator.SetBool("ShouldBlock", state);
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
