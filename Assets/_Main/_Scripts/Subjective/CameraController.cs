using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target; // Kamera'nýn odaklanacaðý obje
    public float zoomSpeed = 2.0f; // Zoom in/zoom out hýzý
    public float rotationSpeed = 100.0f; // Rotasyon hýzý
    public Vector2 zoomRange = new Vector2(2.0f, 10.0f); // Zoom sýnýrlarý (orthographicSize için)

    private float currentX = 0.0f; // Yatay rotasyon
    private float currentY = 30.0f; // Dikey rotasyon (Baþlangýç deðerini 10 yaptýk)
    private bool isRotating = false; // Mouse sürükleme kontrolü
    private Camera cam; // Kamera referansý

    public float initialRotationX = 30.0f; // Baþlangýç yatay rotasyonu
    public float initialRotationY = 30.0f; // Baþlangýç dikey rotasyonu

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true; // Kamerayý ortografik modda kullanýyoruz

        // Baþlangýç rotasyonunu ayarla
        currentX = initialRotationX;
        currentY = initialRotationY;

        // Kameranýn baþlangýç pozisyonunu ayarlýyoruz
        transform.position = new Vector3(0, 0f, 0f);

        // Baþlangýçta hedefe bakmasýný saðlýyoruz
        transform.LookAt(target);
    }

    void Update()
    {
        // Mouse tekerleði ile zoom in/zoom out
        float scrollInput = Mouse.current.scroll.ReadValue().y;
        cam.orthographicSize -= scrollInput * zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomRange.x, zoomRange.y); // Zoom aralýðýný sýnýrla

        // Mouse sol tuþuna basýlýyorsa rotasyonu etkinleþtir
        if (Mouse.current.rightButton.isPressed)
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }

        // Eðer mouse sürükleniyorsa objenin etrafýnda dön
        if (isRotating)
        {
            currentX += Mouse.current.delta.ReadValue().x * rotationSpeed * Time.deltaTime;
            currentY += Mouse.current.delta.ReadValue().y * rotationSpeed * -1 * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, 10, 85); // X rotasyonunu 10 ile 90 derece arasýnda sýnýrla
        }

        // Kamerayý hedefe göre pozisyonla
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -10); // Ortografik kamerada sabit uzaklýk kullanýyoruz
        transform.position = target.position + rotation * direction;
        transform.LookAt(target); // Kameranýn hedefe bakmasýný saðla
    }
}
