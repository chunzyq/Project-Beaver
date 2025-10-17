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
	}

    // private void LateUpdate()
    // {
    //     float camHeight = cam.orthographicSize * 2;
    //     float camWidth = camHeight * cam.aspect;

    //     Vector3 camPos = cam.transform.position;

    //     minX = camPos.x - camWidth / 2 + offset;
    //     maxX = camPos.x + camWidth / 2 - offset;
    //     minY = camPos.y - camHeight / 2 + offset;
    //     maxY = camPos.y + camHeight / 2 - offset;

    //     // Ограничиваем позицию игрока в этих пределах
    //     Vector3 pos = transform.position;
    //     pos.x = Mathf.Clamp(pos.x, minX, maxX);
    //     pos.y = Mathf.Clamp(pos.y, minY, maxY);
    //     transform.position = pos; 
    // }

    void FixedUpdate()
	{
        Vector2 targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        
		// float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
		// float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);
		// targetPos = new Vector2(clampedX, clampedY);

		rb.MovePosition(targetPos);
	}
}
