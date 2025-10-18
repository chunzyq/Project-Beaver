using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
    private Vector2 movement;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
	}

    void FixedUpdate()
	{
        Vector2 targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

		rb.MovePosition(targetPos);
	}
}
