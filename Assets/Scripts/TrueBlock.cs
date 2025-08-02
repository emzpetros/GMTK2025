using Unity.VisualScripting;
using UnityEngine;

public class TrueBlock : MonoBehaviour, IBlock {
    private BooleanBlock trueBlock;

    private void Start() {
        trueBlock = new BooleanBlock(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Player") {
            Player.Instance.SetItem(trueBlock);
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


