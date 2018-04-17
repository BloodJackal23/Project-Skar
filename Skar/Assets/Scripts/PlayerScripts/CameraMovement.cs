using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;

    [SerializeField] private Camera playerCam;
    public Transform V_camAnchor;
    [SerializeField] private Transform H_camAnchor;
    public float camera_X_Distance;
    public float camera_Y_Distance = 2;
    public float camera_Z_Distance = -5;

    // Use this for initialization
    void Start () {
        if (playerCam == null)
        {
            playerCam = Camera.main;
        }
        V_camAnchor.rotation = transform.rotation;
        V_camAnchor.position = transform.position;
        PositionCamera(V_camAnchor);
        CursorSettings(false, CursorLockMode.Locked);
    }
	
	// Update is called once per frame
	void Update () {
        V_camAnchor.position = transform.position;
        RotateCamera(GetMouseMovement());
    }

    public void CursorSettings(bool visibility, CursorLockMode lockState)
    {
        Cursor.lockState = lockState;
        Cursor.visible = visibility;
    }

    //Adjusts the camera's position relative to a reference point (usually the V anchor)
    void PositionCamera(Transform reference)
    {
        playerCam.transform.localPosition = new Vector3(reference.position.x + camera_X_Distance, reference.position.y + camera_Y_Distance, reference.position.z + camera_Z_Distance);
    }

    //Gets the mouse input
    Vector2 GetMouseMovement()
    {
        return new Vector2(Input.GetAxis("Mouse X") * XSensitivity, Input.GetAxis("Mouse Y") * YSensitivity);
    }

    //Rotates the V anchor and H anchor respectively
    void RotateCamera(Vector2 mouseInput)
    {
        V_camAnchor.Rotate(0, mouseInput.x, 0, Space.Self);
        H_camAnchor.Rotate(-mouseInput.y, 0, 0, Space.Self);
    }
}
