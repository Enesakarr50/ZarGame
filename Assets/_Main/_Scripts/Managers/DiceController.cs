using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    public GameObject dicePrefab;    // Zar prefab'ý
    public float rotationSpeed = 100f;  // Fare hareketi ile döndürme hýzý

    private Vector3 previousMousePosition;
    private Vector3 rotationAxis;
    private bool isDragging = false;

    private void Start()
    {
        // Zar objesini görünür yap
        dicePrefab.SetActive(true);
    }

    private void Update()
    {
        // Fare inputlarýný kontrol et
        if (Input.GetMouseButtonDown(0))  // Sol týk ile döndürmeye baþla
        {
            previousMousePosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))  // Sol týk býrakýldýðýnda döndürmeyi býrak
        {
            isDragging = false;
        }

        if (isDragging)
        {
            RotateDice();
        }
    }

    // Zarý fare inputlarýyla döndür
    private void RotateDice()
    {
        Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
        rotationAxis = new Vector3(-deltaMousePosition.y, deltaMousePosition.x, 0);
        dicePrefab.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.World);

        previousMousePosition = Input.mousePosition;
    }
}
