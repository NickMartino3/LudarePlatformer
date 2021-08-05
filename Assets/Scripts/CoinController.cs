using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : StationaryCollider
{
    protected override void OnTrigger()
    {
        PlayerController.Instance.AddCoin();
        Destroy(gameObject);
    }
}