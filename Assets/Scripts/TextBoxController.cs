using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxController : MonoBehaviour
{
    private static float TextBoxAppearSpeed = 0.03f;
    private static float TextBoxFadeSpeed = 0.01f;

    public SpriteRenderer m_background;
    public TMP_Text m_text;

    public void Show(string text)
    {
        m_text.text = text;

        if (m_background.color.a < 1.0f)
        {
            m_background.color = new Color(m_background.color.r, m_background.color.g, m_background.color.b, m_background.color.a + TextBoxAppearSpeed);
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, m_text.color.a + TextBoxAppearSpeed);
        }
    }

    public void Hide()
    {
        if (m_background.color.a > 0.0f)
        {
            m_background.color = new Color(m_background.color.r, m_background.color.g, m_background.color.b, m_background.color.a - TextBoxFadeSpeed);
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, m_text.color.a - TextBoxFadeSpeed);
        }
    }
}
