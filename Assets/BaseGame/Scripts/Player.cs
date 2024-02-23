using UnityEngine;

public class Player : MonoBehaviour
{
    public float sensitivity = 50;
    public float normalSpeed = 50, fastSpeed = 100;
    public float zoomSpeed = 5;
    public float minHeight = 5, maxHeight = 100;



    private Camera playerCamera;
    private PlayerPosition playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        playerCamera.transform.SetParent(transform);
        playerCamera.transform.position = new Vector3(0, 0, 0);
        playerCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        selectPosition();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void selectPosition()
    {
        playerPosition = GameMaster.selectPlayerPosition(this);
        if (playerPosition)
        {
            this.transform.position = playerPosition.transform.position;
            this.transform.rotation = playerPosition.transform.rotation;
            Vector3 eulerRotation = playerCamera.transform.rotation.eulerAngles;
            playerCamera.transform.rotation = Quaternion.Euler(playerPosition.camareStartRotationX, eulerRotation.y, eulerRotation.z);
        }
    }

    void move()
    {
        // Rotate Camera
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
            transform.Rotate(mouseInput * sensitivity * Time.deltaTime);

            // Split x and y rotation to player and camera
            Vector3 playerEulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, playerEulerRotation.y, 0);
            playerCamera.transform.Rotate(playerEulerRotation.x, 0, 0);

            // Don't flip camera
            Vector3 cameraEulerRotation = playerCamera.transform.eulerAngles;
            if (System.Math.Abs(cameraEulerRotation.z - 180) < 10) // Camera is Flipped
            {
                playerCamera.transform.Rotate(-playerEulerRotation.x, 0, 0); // Rotate Back
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Move Player
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float speed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
        transform.Translate(input * speed * Time.deltaTime);

        // Zoom
        Vector3 zoomDirektion = playerCamera.transform.forward;
        Vector3 zoom = zoomDirektion * zoomSpeed * Input.mouseScrollDelta.y;
        transform.Translate(zoom, Space.World);
        if (transform.position.y < minHeight || transform.position.y > maxHeight)
        {
            transform.Translate(-zoom, Space.World); // Revert Zoom
        }

    }
}
