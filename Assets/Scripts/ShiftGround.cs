using System;
using UnityEngine;

public class ShiftGround : MonoBehaviour
{
    private float moveDistance = -1f;

    private void Start() {
        //Player.Instance.OnAttackedBreak += MoveGround;
    }
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
