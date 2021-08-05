using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversionQuad : MonoBehaviour
{
    public SpriteRenderer m_spriteObj;

    void Update()
    {
        m_spriteObj.gameObject.SetActive(GlobalController.IsFrozen);
    }
}
