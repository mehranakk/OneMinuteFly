using System.Collections.Generic;
using UnityEngine;

public class PartyFly : MonoBehaviour
{
    public void Flip()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
