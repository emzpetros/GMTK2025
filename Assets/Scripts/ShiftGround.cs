using UnityEngine;

public class ShiftGround : MonoBehaviour
{
    private float moveDistance = -1f;
    public void MoveGround(bool value) {
        Vector3 pos = transform.position;
        if (value) {
            this.transform.position = new Vector3(pos.x, pos.y + moveDistance, pos.z);
        }
        else {
            this.transform.position = new Vector3(pos.x, pos.y - moveDistance, pos.z);
        }
    }
}
