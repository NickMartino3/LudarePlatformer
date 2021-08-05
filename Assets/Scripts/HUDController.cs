using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TMP_Text m_coinText;

    private void Update()
    {
        m_coinText.text = $"Coins: {PlayerController.Instance.GetCoinCount()}/{GlobalController.CoinsToWin}";
    }
}
