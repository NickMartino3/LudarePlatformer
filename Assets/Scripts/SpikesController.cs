using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : StationaryCollider
{
    protected override void OnTrigger()
    {
        PlayerController.Instance.Die();
    }
}
