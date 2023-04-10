using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Controls 
    [SerializeField] private GameControls gameControls;

    [SerializeField] private Transform cameraTransform;

    //Movement Variables
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    private Vector3 newPosition;

    //Rotation Variables 
    [SerializeField] private float rotationAmount;
    private Quaternion newRotation;

    //Zoom Variables 
    [SerializeField] Vector3 zoomAmount;
    private Vector3 newZoom;

    private Vector4 camBounds;

    private void Awake()
    {
        //EnableControls 
        gameControls = new GameControls();
        gameControls.CameraControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup Camera Bounds, Left, Right, Up, Down
        camBounds = new Vector4(-7.5f, 7.5f, 7.5f, -7.5f);

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update() 
    {
        HandleInputForMovement();
        HandleInputForRotation();
        HandleInputForZoom();
    }

    private void HandleInputForMovement()
    {
        //Get Input Values from Bindings 
        float vMovement = gameControls.CameraControls.Vertical_Movement.ReadValue<float>();
        float hMovement = gameControls.CameraControls.Horizontal_Movement.ReadValue<float>();

        //Make temp variables for bounds check
        Vector3 newVerticalPos = newPosition + (transform.forward * (movementSpeed * vMovement));
        Vector3 newHorizontalPos = newPosition + (transform.right * (movementSpeed * hMovement));

        //Bounds Check
        if (newVerticalPos.z > camBounds.w && newVerticalPos.z < camBounds.z
            && newHorizontalPos.x > camBounds.x && newHorizontalPos.x < camBounds.y)
        {
            //Update Values if within bounds 
            newPosition += (transform.forward * (movementSpeed * vMovement));  
            newPosition += (transform.right * (movementSpeed * hMovement));
        }

        //Lerp Camera to new Position
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    private void HandleInputForRotation()
    {
        float rotation = gameControls.CameraControls.Rotate.ReadValue<float>();

        newRotation *= Quaternion.Euler(Vector3.up * (rotationAmount * rotation));

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }

    private void HandleInputForZoom()
    {
        float zoom = gameControls.CameraControls.Zoom.ReadValue<float>();

        newZoom += (zoomAmount * zoom);

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

}
