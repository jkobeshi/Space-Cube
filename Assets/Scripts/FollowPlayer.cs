using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 lastPos;
    public float transitionSpeed = 0.2f;
    public Vector3 offset;
    private void FixedUpdate()
    {
        if (player != null)
        {
            lastPos = player.position;
        }
        if (Input.GetKey("r"))
            offset = Vector3.zero;
        if ((Mathf.Abs(offset.x) + transitionSpeed) <= 9.5)
            offset.x += Input.GetAxisRaw("CamHorz") * transitionSpeed;
        else
        {
            if (offset.x < 0)
                offset.x = -9f;
            else
                offset.x = 9f;
        }
        if ((Mathf.Abs(offset.y) + transitionSpeed) <= 6)
            offset.y += Input.GetAxisRaw("CamVert") * transitionSpeed;
        else
        {
            if (offset.y < 0)
                offset.y = -5.5f;
            else
                offset.y = 5.5f;
        }
        Vector3 playerPosition = lastPos + offset; playerPosition.z = -10;
        Vector3 Transition = Vector3.Lerp(transform.position, playerPosition, transitionSpeed);
        transform.position = Transition;
        
    }
}
