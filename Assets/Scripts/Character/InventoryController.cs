using UnityEngine;
using System.Collections;

public class InventoryController
{
    public enum PickUpItemsEnum
    {
        ICE_CREAM,
        COIN
    }

    private static InventoryController instance;

    public delegate void PickUpItemEvent(PickUpItemsEnum pickUpItemEnum);
    public delegate void UseIceCreamEvent();
    public delegate void UseCoinEvent(int amount);
    public event PickUpItemEvent OnPickUpItem;
    public event UseIceCreamEvent OnUseIceCream;
    public event UseCoinEvent OnUseCoin;

    public int _coins { private set; get; }
    public bool hasIceCream { private set; get; }

    private InventoryController()
    {

    }

    public void Init()
    {
        _coins = 10;
        hasIceCream = false;
    }

    public static InventoryController GetInstance()
    {
        if (instance == null)
            instance = new InventoryController();
        return instance;
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

    public void PickUp(PickUpItemsEnum pickUpItemEnum)
    {
        switch (pickUpItemEnum)
        {
            case PickUpItemsEnum.ICE_CREAM:
                hasIceCream = true;
                break;
            case PickUpItemsEnum.COIN:
                _coins += 1;
                break;
        }
        OnPickUpItem?.Invoke(pickUpItemEnum);
    }

    public void UseIceCream()
    {
        hasIceCream = false;
        OnUseIceCream?.Invoke();
    }

    public void UseCoin(int amount)
    {
        if (_coins < amount)
            throw new System.Exception("No enough coins");
        _coins -= amount;
        OnUseCoin?.Invoke(amount);
    }
}
