public enum BlockType { Boolean, Number }

public interface IBlock {
    BlockType GetBlockType();
    object GetValue();

    public void OnPickup() {
        Player.Instance.SetItem(this);
    }
  
}

public class BooleanBlock : IBlock {
    private bool value;
    public BooleanBlock(bool val) { value = val; }
    public BlockType GetBlockType() => BlockType.Boolean;
    public object GetValue() => value;
}

public class NumberBlock : IBlock {
    private float value;
    public NumberBlock(float val) { value = val; }
    public BlockType GetBlockType() => BlockType.Number;
    public object GetValue() => value;
}
