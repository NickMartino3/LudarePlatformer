using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    private static float MaxFireworkHeight = 4.0f;
    private static float MinFireworkHeight = 2.5f;
    private static float MinInitialSpeed = 8.0f;
    private static float MaxInitialSpeed = 12.0f;
    private static float InitialTarXSpread = 1.0f;
    private static float ExplosionPartFadeSpeed = 0.01f;

    public SpriteRenderer m_initialProj;
    public List<SpriteRenderer> m_explosionParts;

    private Vector2 m_initialTar;
    private float m_initialMoveSpeed;
    private bool m_reachedInitialTar = false;

    private float m_explosionSpeed = 15.0f;
    private List<Vector2> m_explosionTargets = new List<Vector2>();

    void Start()
    {
        m_initialTar = new Vector2(Random.Range(-InitialTarXSpread, InitialTarXSpread), Random.Range(MinFireworkHeight, MaxFireworkHeight));
        m_initialMoveSpeed = Random.Range(MinInitialSpeed, MaxInitialSpeed);

        m_initialProj.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.8f, 1.0f); //Give the fireworks some color variance (bright colors to appear on black background)
    }

    void FixedUpdate()
    {
        if (GlobalController.IsFrozen)
        {
            return;
        }

        if (!m_reachedInitialTar)
        {
            m_initialProj.transform.Translate(m_initialTar.normalized * Time.deltaTime * m_initialMoveSpeed, Space.World);

            if (m_initialProj.transform.position.y >= m_initialTar.y)
            {
                SetupExplosion();
                StartCoroutine(ExplodeAnim());
                m_reachedInitialTar = true;
                m_initialProj.color = new Color(0, 0, 0, 0); //Simple hide until it can be cleaned up at the end of the explosion
            }
        }
    }

    private void SetupExplosion()
    {
        for (int i = 0; i < m_explosionParts.Count; i++)
        {
            m_explosionTargets.Add(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
            m_explosionParts[i].color = m_initialProj.color;
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
                    m_explosionParts[i].transform.Translate(m_explosionTargets[i] * Time.deltaTime * m_explosionSpeed, Space.World);
                    m_explosionParts[i].color = new Color(m_explosionParts[i].color.r, m_explosionParts[i].color.g, m_explosionParts[i].color.b, m_explosionParts[i].color.a - ExplosionPartFadeSpeed);
                }
            }
            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }
}
