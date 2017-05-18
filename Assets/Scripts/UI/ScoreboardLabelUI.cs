using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardLabelUI : Photon.MonoBehaviour {

    public Text nameText;
    public Text deathsText;
    public Text killsText;
    public Text damageText;

    [PunRPC]
    public void BroadcastNameTextChange(string newText)
    {
        nameText.text = newText;
    }

    [PunRPC]
    public void BroadcastDeathsTextChange(string newText)
    {
        deathsText.text = newText;
    }

    [PunRPC]
    public void BroadcastKillsTextChange(string newText)
    {
        killsText.text = newText;
    }

    [PunRPC]
    public void BroadcastDamageTextChange(string newText)
    {
        damageText.text = newText;
    }
}
