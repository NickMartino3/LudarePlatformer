using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksController : MonoBehaviour
{
    private static int MinLaunch = 2;
    private static int MaxLaunch = 4;
    private static float LaunchDelay = 0.2f;

    public GameObject m_fireworkPrefab;

    private bool m_isLaunching = false;

    public void Trigger()
    {
        if (!m_isLaunching)
        {
            StartCoroutine(RunFireworks());
            m_isLaunching = true;
        }
    }

    private IEnumerator RunFireworks()
    {
        while (true)
        {
            if (!GlobalController.IsFrozen)
            {
                int toLaunch = Random.Range(MinLaunch, MaxLaunch);
                for (int i = 0; i < toLaunch; i++)
                {
                    Instantiate(m_fireworkPrefab, transform, false);
                }
            }
            yield return new WaitForSeconds(LaunchDelay);
        }
    }
}
