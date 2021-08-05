using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationaryCollider : MonoBehaviour
{
    protected BoxCollider2D m_collider;
    protected bool m_ignoreFrozen = false;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        m_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateLoop();
    }

    protected void UpdateLoop()
    {
        if (m_collider.bounds.Intersects(PlayerController.Instance.m_collider.bounds) && (!GlobalController.IsFrozen || m_ignoreFrozen))
        {
            OnTrigger();
        }
        else
        {
            OnNotTrigger();
        }
    }

    protected abstract void OnTrigger();
    protected virtual void OnNotTrigger() { }
}
