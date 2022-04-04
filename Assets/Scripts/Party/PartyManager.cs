using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> styles = new List<GameObject>();

    private int partySize;

    [SerializeField] private GameObject bouncerPrefab;
    private GameObject bouncerFly;

    void Start()
    {
        partySize = GetComponent<LDtkFields>().GetInt("Size");

        for (int i = 0; i < partySize; i++)
        {
            int randIndex = Random.Range(0, styles.Count);
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-2f, 2f),
                transform.position.y + Random.Range(-2f, 2f),
                transform.position.z);
            GameObject partyFly = Instantiate(styles[randIndex], spawnPos, Quaternion.identity, transform);

            if (Random.Range(0, 1f) > 0.5f)
                partyFly.GetComponent<PartyFly>().Flip();
        }

        bouncerFly = Instantiate(bouncerPrefab, transform.position, Quaternion.identity, transform);
        bouncerFly.GetComponent<BouncerFly>().SetPartyCenter(transform.position);
    }

    public void EnterParty()
    {
        GetComponent<CircleCollider2D>().enabled = false;
    }

}
