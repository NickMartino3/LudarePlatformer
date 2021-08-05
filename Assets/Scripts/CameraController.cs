using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static float MaxCameraZoom = 12.0f;
    private static float MinCameraZoom = 6.0f;
    private static float MinVelAdjustCamera = 8.0f;
    private static float CameraFollowSpeed = 0.3f;
    private static float CameraZoomSpeedIn = 0.05f;

    public Transform m_trackingEntity;
    private Rigidbody2D m_trackingRigidBody;

    public bool m_isMoving;

    void Start()
    {
        m_trackingRigidBody = m_trackingEntity.gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Hard track x pos; slowly follow y pos
        transform.position = new Vector3(m_trackingEntity.position.x, Vector3.MoveTowards(transform.position, m_trackingEntity.position, CameraFollowSpeed).y, transform.position.z);

        //Zoom out at high y speeds
        float yVel = m_trackingRigidBody.velocity.y;
        if (Camera.main.orthographicSize <= MaxCameraZoom && yVel > MinVelAdjustCamera)
        {
            Camera.main.orthographicSize += yVel / 100.0f;
        }
        else if (Camera.main.orthographicSize > MinCameraZoom && PlayerController.Instance.m_isOnGround)
        {
            Camera.main.orthographicSize -= CameraZoomSpeedIn;
        }
    }
}
