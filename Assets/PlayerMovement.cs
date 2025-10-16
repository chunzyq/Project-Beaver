using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
    private Vector2 movement;
    
    [Header("Movement Bounds")]
	public float minX = -10f;
	public float maxX = 10f;
	public float minY = -5f;
	public float maxY = 5f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
        Vector2 targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        
		float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
		float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);
		targetPos = new Vector2(clampedX, clampedY);

		rb.MovePosition(targetPos);
	}
}
