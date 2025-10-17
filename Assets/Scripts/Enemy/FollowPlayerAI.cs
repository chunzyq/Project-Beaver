using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerAI : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = (target.position - transform.position).normalized;
        transform.position += desiredPosition * followSpeed * Time.fixedDeltaTime;
    }
}
