using System;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Player.Instance.OnItemUsed += Player_OnItemUsed;
        tmp = this.GetComponent<TextMeshProUGUI>();
    }

    private void Player_OnItemUsed(object sender, EventArgs e) {
        tmp.text = "Empty";
    }

    public void SetInventoryText(string inventoryText) {
        tmp.text = inventoryText;
    }
}
