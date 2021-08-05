using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorController : StationaryCollider
{
    public TextBoxController m_textBox;
    public FireworksController m_fireworks;

    protected override void OnTrigger()
    {
        if (PlayerController.Instance.GetCoinCount() >= GlobalController.CoinsToWin)
        {
            m_fireworks.Trigger();

            m_textBox.Show($"You have unlocked the great beyond!  (But it's the end of the game, congrats!)");
        }
        else
        {
            m_textBox.Show($"Gather the {GlobalController.CoinsToWin - PlayerController.Instance.GetCoinCount()} remaining coins to go into the great beyond...");
        }
    }

    protected override void OnNotTrigger()
    {
        base.OnNotTrigger();

        m_textBox.Hide();
    }
}
