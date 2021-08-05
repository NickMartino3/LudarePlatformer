using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyAIType
    {
        Stationary,
        Rotate,
        Move,
        MoveRotate
    }

    private static float RotateSpeed = 20.0f;
    private static float MoveSpeed = 5.0f;
    private static float ExplosionSpeed = 20.0f;
    private static float FrozenFadeSpeed = 0.03f;
    private static float ExplosionFadeSpeed = 0.03f;

    public Transform m_wallCheck;
    public LayerMask m_groundLayer;

    public SpriteRenderer m_renderer;
    public SpriteRenderer m_topRenderer;

    public List<SpriteRenderer> m_explosionParts;
    private List<Vector2> m_explosionTargets = new List<Vector2>();

    public EnemyAIType m_aiType;

    private BoxCollider2D m_killPlayerCollider;
    private CircleCollider2D m_iDieCollider;

    private int m_dir;

    private bool m_isDead = false;

    void Start()
    {
        m_killPlayerCollider = GetComponent<BoxCollider2D>();
        m_iDieCollider = GetComponent<CircleCollider2D>();

        m_dir = Random.Range(0, 2)* 2 - 1; //Get either -1 or 1 for direction
    }

    void Update()
    {
        if (GlobalController.IsFrozen)
        {
            return;
        }

        if (m_isDead)
        {
            return;
        }

        if (m_iDieCollider.bounds.Intersects(PlayerController.Instance.m_collider.bounds))
        {
            PlayerController.Instance.KillEnemy(this);
            m_isDead = true;
            m_renderer.gameObject.SetActive(false);
            m_topRenderer.gameObject.SetActive(false);
            
            for (int i = 0; i < m_explosionParts.Count; i++)
            {
                m_explosionTargets.Add(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f,1.0f)));
            }

            StartCoroutine(ExplodeAnim());
        }
        else if (m_killPlayerCollider.bounds.Intersects(PlayerController.Instance.m_collider.bounds)) //Some player friendliness here.  In the even that they hit both, treat it as player win.
        {
            PlayerController.Instance.Die();
        }
    }

    void FixedUpdate()
    {
        if (GlobalController.IsFrozen)
        {
            m_renderer.color = new Color(m_renderer.color.r, m_renderer.color.g, m_renderer.color.b, m_renderer.color.a - FrozenFadeSpeed);
            m_topRenderer.color = new Color(m_topRenderer.color.r, m_topRenderer.color.g, m_topRenderer.color.b, m_topRenderer.color.a - FrozenFadeSpeed);
            return;
        }
        else
        {
            m_renderer.color = new Color(m_renderer.color.r, m_renderer.color.g, m_renderer.color.b, 1.0f);
            m_topRenderer.color = new Color(m_topRenderer.color.r, m_topRenderer.color.g, m_topRenderer.color.b, 1.0f);
        }

        if (m_isDead)
        {
            return;
        }

        if (m_aiType == EnemyAIType.Rotate || m_aiType == EnemyAIType.MoveRotate)
        {
            gameObject.transform.Rotate(-Vector3.forward * RotateSpeed * Time.deltaTime);
        }

        if (m_aiType == EnemyAIType.Move || m_aiType == EnemyAIType.MoveRotate)
        {
            gameObject.transform.Translate(m_dir * Vector3.right * Time.deltaTime * MoveSpeed, Space.World);

            int numColliders = Physics2D.OverlapBoxAll(m_wallCheck.position, new Vector2(1.1f, 1.1f), m_groundLayer).Length;

            if (numColliders > 3) //Hacky; done for time.  The 3 it should find are the 3 colliders on the enemy itself.  Does mean sometimes it 'avoids' the player by switching direction if the player gets close but doesn't hit it.
            {
                m_dir *= -1;
            }
        }
    }

    private IEnumerator ExplodeAnim()
    {
        while (m_explosionParts[0].color.a > 0)
        {
            if (!GlobalController.IsFrozen)
            {
                for (int i = 0; i < m_explosionParts.Count; i++)
                {
                    m_explosionParts[i].transform.Translate(m_explosionTargets[i] * Time.deltaTime * ExplosionSpeed, Space.World);
                    m_explosionParts[i].color = new Color(m_explosionParts[i].color.r, m_explosionParts[i].color.g, m_explosionParts[i].color.b, m_explosionParts[i].color.a - ExplosionFadeSpeed);
                }
            }
            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }
}
