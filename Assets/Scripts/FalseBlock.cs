using Unity.VisualScripting;
using UnityEngine;

public class FalseBlock : MonoBehaviour, IBlock {
    private BooleanBlock falseBlock;

    private void Start() {
        falseBlock = new BooleanBlock(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            Player.Instance.SetItem(falseBlock);
            Destroy(this.gameObject);
        }
    }

    public BlockType GetBlockType() {
        return BlockType.Boolean;
    }

    public object GetValue() {
        return true;
    }
}


