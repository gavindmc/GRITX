using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float sensitivity = 0.1f;

    [Header("Pitch Clamp (degrees)")]
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    [Tooltip("The Player body to rotate horizontally (yaw). Usually the parent.")]
    [SerializeField] private Transform playerBody;

    private float pitch = 0f;

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        Vector2 delta = mouse.delta.ReadValue() * sensitivity;

        // Yaw: rotate the whole Player body left/right
        if (playerBody != null)
            playerBody.Rotate(Vector3.up, delta.x);

        // Pitch: rotate just the camera up/down, clamped so you can't flip over
        pitch -= delta.y;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Escape to free the cursor (useful while testing)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}