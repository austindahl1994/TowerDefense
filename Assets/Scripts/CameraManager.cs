using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] private float zoomSize = 5;
    [SerializeField] private float cameraMoveSpeed = 1;
    [SerializeField] private Vector3 dragOrigin;
    [SerializeField] private Vector3 CameraPosition;
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start()
    {
        //helps with camera position code
        CameraPosition = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //allows player to zoom in and out with scroll wheel
        Zoom();

        //allows player to pan the camera with the middle mouse button
        PanCamera();

        //allows player to move camera with keyboard controls, WASD
        MoveWithKeyboard();
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 2)
            {
                zoomSize -= 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize < 10)
            {
                zoomSize += 1;
            }
        }
        GetComponent<Camera>().orthographicSize = zoomSize;
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            CameraPosition += difference;

        }
        this.transform.position = CameraPosition;
    }

    private void MoveWithKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            CameraPosition.y += cameraMoveSpeed / 10;
        }

        if (Input.GetKey(KeyCode.S))
        {
            CameraPosition.y -= cameraMoveSpeed / 10;
        }

        if (Input.GetKey(KeyCode.A))
        {
            CameraPosition.x -= cameraMoveSpeed / 10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            CameraPosition.x += cameraMoveSpeed / 10;
        }

        this.transform.position = CameraPosition;
    }
}
