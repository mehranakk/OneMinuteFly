using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThinking : MonoBehaviour
{
    public enum ThinkingEnum
    {
        COIN,
        MATE,
        ICE_CREAM
    }

    private GameObject cloudGameObject;
    private GameObject coinGameObject;
    private GameObject mateGameObject;
    private GameObject iceCreamGameObject;

    private void Awake()
    {
        cloudGameObject = transform.Find("Cloud").gameObject;
        coinGameObject = transform.Find("ThinkCoin").gameObject;
        mateGameObject = transform.Find("ThinkMate").gameObject;
        iceCreamGameObject = transform.Find("ThinkIceCream").gameObject;
    }

    public void ClearThinking()
    {
        coinGameObject.SetActive(false);
        mateGameObject.SetActive(false);
        iceCreamGameObject.SetActive(false);
        cloudGameObject.SetActive(false);
    }

    public void Think(ThinkingEnum thinkingEnum)
    {
        ClearThinking();
        cloudGameObject.SetActive(true);
        switch (thinkingEnum)
        {
            case ThinkingEnum.COIN:
                coinGameObject.SetActive(true);
                break;
            case ThinkingEnum.MATE:
                mateGameObject.SetActive(true);
                break;
            case ThinkingEnum.ICE_CREAM:
                iceCreamGameObject.SetActive(true);
                break;
        }
    }
}
