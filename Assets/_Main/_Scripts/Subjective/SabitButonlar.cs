using UnityEngine;

public class SabitButonlar : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons; // 4 adet butonun referanslarý
    [SerializeField] private float distanceFromEdges = 0.5f; // Kenardan uzaklýk

    private void Start()
    {
        // Butonlarý uygun konumda baþlat
        SetButtonsPosition();
    }

    private void Update()
    {
        // Butonlarý sürekli güncelle
        SetButtonsPosition();
    }

    private void SetButtonsPosition()
    {
        if (buttons.Length != 4)
        {
            Debug.LogError("4 adet buton eklemelisiniz!");
            return;
        }

        // Zarýn boyutunu hesaplayýn
        float diceSize = transform.localScale.x; // Zarýn ölçeðini kullanarak boyutunu belirleyin

        // Zarýn dört bir kenarýna butonlarý konumlandýr
        Vector3[] positions = new Vector3[4];
        positions[0] = new Vector3(diceSize / 2, 0, 0); // Sað
        positions[1] = new Vector3(-diceSize / 2, 0, 0); // Sol
        positions[2] = new Vector3(0, 0, diceSize / 2); // Ýleri
        positions[3] = new Vector3(0, 0, -diceSize / 2); // Geri

        // Butonlarý zemin konumunda sabitle
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
            {
                Vector3 buttonPosition = positions[i] + transform.position + new Vector3(0, distanceFromEdges, 0);
                buttons[i].transform.position = buttonPosition;
            }
        }
    }
}
