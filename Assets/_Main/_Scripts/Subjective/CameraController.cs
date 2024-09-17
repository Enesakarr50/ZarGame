using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;    // Kamera takip edilecek hedef
    public float distance = 10f;    // Kamera ile hedef arasýndaki baþlangýç mesafesi
    public float zoomSpeed = 2f;    // Zoom in/zoom out hýzý
    public float minDistance = 3f;  // Kamera'nýn hedefe yaklaþabileceði en yakýn mesafe
    public float maxDistance = 20f; // Kamera'nýn hedeften uzaklaþabileceði en uzak mesafe
    public float rotationSpeed = 100f; // Kamera'nýn hedef etrafýnda dönme hýzý
    public float smoothTime = 0.2f;    // Dönme ve zoom yumuþatma süresi

    private Vector3 currentVelocity;

    private float currentX = 0f;    // Yatay eksen (fare X hareketi)
    private float currentY = 0f;    // Dikey eksen (fare Y hareketi)
    private float scrollInput;      // Fare tekerleði girdisi (zoom için)

    public PlayerInput playerInput;  // Yeni Input System için Player Input referansý
    private InputAction lookAction;   // Mouse hareketleri için
    private InputAction scrollAction; // Zoom için

    private void Awake()
    {
        // PlayerInput komponentini al
        playerInput = GetComponent<PlayerInput>();

        // "Look" ve "Scroll" inputlarýný al
        lookAction = playerInput.actions["Look"];
        scrollAction = playerInput.actions["Scroll"];
    }


    private void Update()
    {
        // Fare hareketi girdisini al
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        currentX += lookInput.x * rotationSpeed * Time.deltaTime;
        currentY -= lookInput.y * rotationSpeed * Time.deltaTime;

        // Zoom girdisini al
        scrollInput = scrollAction.ReadValue<float>();
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Zoom mesafesini sýnýrla
    }

    private void LateUpdate()
    {
        // Dönüþ için Quaternion hesapla
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Kamera konumunu güncelle
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

        // Kamerayý hedefe baktýr
        transform.LookAt(target);
    }
}
