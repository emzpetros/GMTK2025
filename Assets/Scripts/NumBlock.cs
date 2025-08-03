using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NumBlock : MonoBehaviour, IBlock {
    private NumberBlock numBlock;

    private void Start() {
        float num = Random.Range(5, 10);
        numBlock = new NumberBlock(num);
        this.GetComponentInChildren<TextMeshProUGUI>().text = num.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            Player.Instance.SetItem(numBlock);
            Destroy(this.gameObject);
        }
    }

    public BlockType GetBlockType() {
        return BlockType.Number;
    }

    public object GetValue() {
        return true;
    }
}


