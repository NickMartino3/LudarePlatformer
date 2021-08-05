using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBase : StationaryCollider
{
    public TextBoxController m_textBox;
    public string m_tutorialText;

    private bool m_isInTutorial = false;

    void Start()
    {
        Init();
        m_ignoreFrozen = true;
    }

    void Update()
    {
        UpdateLoop();

        if (Input.GetKeyDown(KeyCode.W) && m_isInTutorial)
        {
            m_isInTutorial = false;
            GlobalController.IsFrozen = false;
            Destroy(m_textBox.gameObject);
            Destroy(gameObject);
        }
    }

    protected override void OnTrigger()
    {
        GlobalController.IsFrozen = true;
        m_isInTutorial = true;
        m_textBox.Show(m_tutorialText + "  (W to continue)");
    }
}
