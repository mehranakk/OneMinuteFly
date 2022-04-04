using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private GameObject icecreamGameObject;
    private GameObject coinGameObject;
    private TextMeshProUGUI cointAmountText;

    private void Awake()
    {
        icecreamGameObject = transform.Find("IceCream").gameObject;
        coinGameObject = transform.Find("Coin").gameObject;
        cointAmountText = coinGameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        InventoryController.GetInstance().OnPickUpItem += OnPickUpItem;
        InventoryController.GetInstance().OnUseCoin += OnUseCoin;
        InventoryController.GetInstance().OnUseIceCream += OnUseIceCream;
    }

    private void OnPickUpItem(InventoryController.PickUpItemsEnum pickUpItemEnum)
    {
        switch (pickUpItemEnum)
        {
            case InventoryController.PickUpItemsEnum.ICE_CREAM:
                icecreamGameObject.SetActive(true);
                break;
            case InventoryController.PickUpItemsEnum.COIN:
                Debug.Log("adding coin to inventory");
                //coinGameObject.SetActive(true);
                int coinAmount = InventoryController.GetInstance()._coins;
                cointAmountText.text = coinAmount.ToString();
                break;
        }
    }

    private void OnUseIceCream()
    {
        icecreamGameObject.SetActive(false);
    }

    private void OnUseCoin(int amount)
    {
        int coinAmount = InventoryController.GetInstance()._coins;
        cointAmountText.text = coinAmount.ToString();
    }

    private void OnDestroy()
    {
        InventoryController.GetInstance().OnPickUpItem -= OnPickUpItem;
        InventoryController.GetInstance().OnUseCoin -= OnUseCoin;
        InventoryController.GetInstance().OnUseIceCream -= OnUseIceCream;
    }
}
