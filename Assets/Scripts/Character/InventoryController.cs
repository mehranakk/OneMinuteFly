using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    private int _coins;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasEnoughCoins(int coins)
    {
        return false;
    }

    public bool GetCoin(int coins)
    {
        if( _coins >= coins)
        {
            _coins -= coins;
            return true;
        }
        return false;
    }
}
