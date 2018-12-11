using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField, Range(0.1f, 0.5f)] float percFromPlayer;
    [SerializeField] private float lerpTime;

    private Camera mainCam;
    private Transform player;

    private float velocity;
    private Vector3 targetPos;

    public void Init(Transform player)
    {
        if (mainCam == null)
            mainCam = Camera.main;

        this.player = player;
        mainCam.transform.position = new Vector3(player.position.x, player.position.y, mainCam.transform.position.z);
    }

    void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (player == null)
            return;

        //Find the point between the player's position and the mouse pointers
        Vector3 point = GetMousePosition() - player.position;
        point.z = mainCam.transform.position.z;

        point.Normalize();
        Vector3 target = player.position + (percFromPlayer * point);
        target.z = mainCam.transform.position.z;

        mainCam.transform.position = target;
    }

    public Vector3 GetMousePosition()
    {
        return mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3 GetScreenPoint(Vector3 worldPosition)
    {
        return mainCam.WorldToScreenPoint(worldPosition);
    }
}
