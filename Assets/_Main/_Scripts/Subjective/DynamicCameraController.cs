using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class DynamicCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;  // Takip edilecek hedef
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minOrthoSize = 2f;  // Min Ortho size
    [SerializeField] private float maxOrthoSize = 5f; // Max Ortho size
    [SerializeField] private float minVerticalAngle = 0.139523f; // Minimum dikey açý sýnýrý
    [SerializeField] private float maxVerticalAngle = 60f;  // Maksimum dikey açý sýnýrý

    

    private Vector2 rotateInput;
    private float zoomInput;
    private CinemachineFreeLook freeLookCam;

    private float currentVerticalAngle = 0f;

    private void Awake()
    {
        // Cinemachine Freelook kamerayý buluyoruz
        freeLookCam = GetComponent<CinemachineFreeLook>();

        if (freeLookCam == null)
        {
            Debug.LogError("Cinemachine FreeLook Camera not found.");
        }
    }

    private void OnEnable()
    {
        // Input sistemindeki iþlemler
        
    }

    private void Update()
    {
        // Kamerayý döndürme
        RotateCamera();

        // Kamerayý zoom yapma
        ZoomCamera();

       
    }

    private void RotateCamera()
    {
        var inputControl = new InputControl();
        inputControl.Enable();

        inputControl.Camera.Rotate.performed += ctx => rotateInput = ctx.ReadValue<Vector2>();
        inputControl.Camera.Rotate.canceled += ctx => rotateInput = Vector2.zero;

        inputControl.Camera.Zoom.performed += ctx => zoomInput = ctx.ReadValue<float>();
        inputControl.Camera.Zoom.canceled += ctx => zoomInput = 0f;
        if (rotateInput != Vector2.zero && target != null && inputControl.Camera.Rotate.enabled)
        {
            // Yatay eksende döndürme
            float horizontalRotation = rotateInput.x * rotationSpeed * Time.deltaTime;

            // Dikey eksende döndürme ve sýnýr koyma
            float verticalRotation = rotateInput.y * rotationSpeed * Time.deltaTime;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle - verticalRotation, minVerticalAngle, maxVerticalAngle);

            // Yatay döndürme
            transform.RotateAround(target.position, Vector3.up, horizontalRotation);

            // Dikey döndürmeyi sýnýrlý bir þekilde uygula
            Vector3 rightAxis = transform.right;
            Quaternion verticalRotationQuat = Quaternion.AngleAxis(verticalRotation, rightAxis);
            transform.position = verticalRotationQuat * (transform.position - target.position) + target.position;
        }
    }

    private void ZoomCamera()
    {
        if (zoomInput != 0 && freeLookCam != null)
        {
            // Ortho size deðerini hesapla ve sýnýrlara göre kýsýtla
            float currentOrthoSize = freeLookCam.m_Lens.OrthographicSize;
            currentOrthoSize -= zoomInput * zoomSpeed * Time.deltaTime;
            currentOrthoSize = Mathf.Clamp(currentOrthoSize, minOrthoSize, maxOrthoSize);

            // Ortho size'ý güncelle
            freeLookCam.m_Lens.OrthographicSize = currentOrthoSize;
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("werty");
    }


}
