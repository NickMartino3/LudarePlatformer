using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private static float GroundRadiusCheck = 0.8f;
    private static float JumpPower = 400.0f;
    private static float ExplodePower = 1000.0f;
    private static float Speed = 3.0f;
    private static float MaxSpeed = 10.0f;
    private static float RespawnDelaySeconds = 2.0f;
    private static float RespawnMoveSpeed = 0.1f;

    public LayerMask m_groundLayer;
    public Transform m_groundCheck;
    public BoxCollider2D m_collider;

    private Rigidbody2D m_rigidBody;

    public bool m_isOnGround;

    private int m_jumpCount = 0;

    private int m_coins = 0;

    private Vector3 m_respawnPoint;

    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
        if (Instance == null)
        {
            Instance = this;
        }

        m_respawnPoint = transform.position;
    }

    void Update()
    {
        if (GlobalController.IsFrozen)
        {
            m_rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        else
        {
            m_rigidBody.constraints = RigidbodyConstraints2D.None;
        }

        m_rigidBody.AddForce(new Vector2(Speed * Input.GetAxisRaw("Horizontal"), 0.0f));
        m_rigidBody.velocity = Vector3.ClampMagnitude(m_rigidBody.velocity, MaxSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_jumpCount < 1)
            {
                m_rigidBody.AddForce(new Vector2(0.0f, JumpPower));
                m_jumpCount++;
            }
        }

        if (transform.position.y < -15.0f)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (GlobalController.IsFrozen)
        {
            return;
        }

        m_isOnGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, GroundRadiusCheck, m_groundLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_isOnGround = true;
                m_jumpCount = 0;
            }
        }
    }

    public void AddCoin()
    {
        m_coins++;
    }

    public void KillEnemy(EnemyController killed)
    {
        Vector3 playerPos = transform.position;
        Vector3 enemyPos = killed.transform.position;

        Vector2 dirVec = new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y);
        dirVec.Normalize();

        m_rigidBody.AddForce(new Vector2(dirVec.x * ExplodePower, dirVec.y * ExplodePower));
    }

    public void Die()
    {
        GlobalController.IsFrozen = true;

        StartCoroutine(DieAnim());
    }

    public int GetCoinCount()
    {
        return m_coins;
    }

    private IEnumerator DieAnim()
    {
        yield return new WaitForSeconds(RespawnDelaySeconds);

        while (transform.position != m_respawnPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_respawnPoint, RespawnMoveSpeed);
            m_rigidBody.velocity = new Vector2(0, 0);
            yield return null;
        }

        GlobalController.IsFrozen = false;
        yield return null;
    }
}
