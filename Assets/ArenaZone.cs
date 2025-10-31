using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ArenaZone : MonoBehaviour
{
    public Transform centerPoint;
    public bool isActive = false;
    public float cameraFocusDuration = 1f;
    public float cameraBlendSpeed = 2f;

    private CinemachineVirtualCamera vcam;
    private Transform originalFollowTarget;

    void Start()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if (vcam != null)
            originalFollowTarget = vcam.Follow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            isActive = true;

            StartCoroutine(FocusCameraOnZone());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            isActive = false;
            vcam.Follow = originalFollowTarget;
        }
    }

    private IEnumerator FocusCameraOnZone()
    {
        if (vcam == null) yield break;

        vcam.Follow = centerPoint;

        yield return new WaitForSeconds(cameraFocusDuration);
    }
}
