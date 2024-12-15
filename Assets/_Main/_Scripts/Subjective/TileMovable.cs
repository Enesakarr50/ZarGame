using UnityEngine;

public class TileMovable : MonoBehaviour
{
    [SerializeField] private Renderer objectRenderer; // Reference to the Renderer component
    [SerializeField] private Color collisionColor = Color.red; // Color to change to upon collision
    [SerializeField] private GameObject dice; // Reference to the dice object

    private Material originalMaterial;
    private Color originalColor;
    private LevelLoader levelLoader;

    private Dice diceScript; // Reference to the Dice script component

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>(); 
    
        // Store the original material and color
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
            originalColor = originalMaterial.color;
        }
        else
        {
            Debug.LogError("Renderer component not assigned.");
        }

        // Get the Dice script component
        if (dice != null)
        {
            diceScript = dice.GetComponent<Dice>();
            if (diceScript == null)
            {
                Debug.LogError("Dice script not found on the dice object.");
            }
        }
        else
        {
            Debug.LogError("Dice object not assigned.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            // Change material color to red upon collision
            if (objectRenderer != null)
            {
                objectRenderer.material.color = collisionColor;
            }
        }

        if (other.gameObject.CompareTag("EndTile"))
        {

            if (objectRenderer != null)
            {
                SetObjectTransparency(0.5f);
                Debug.Log("deydi");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            // Restore original color when exiting collision
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
            }
        }

        if (other.gameObject.CompareTag("EndTile"))
        {
           
            if (objectRenderer != null)
            {
                SetObjectTransparency(1f);
                Debug.Log("cýktý");
            }
        }
    }

    private void SetObjectTransparency(float alpha)
    {
        if (objectRenderer != null)
        {
            Color color = objectRenderer.material.color;
            color.a = alpha; // Alfa deðerini deðiþtir
            objectRenderer.material.color = color; // Yeni rengi uygula
        }
    }

    private void Update()
    {
        if (diceScript != null)
        {
            // Make objects invisible while the dice is moving
            if (diceScript.isMoving)
            {
                SetObjectVisibility(false);
            }
            else
            {
                SetObjectVisibility(true);
            }
        }
    }

    private void SetObjectVisibility(bool isVisible)
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = isVisible;
        }
    }

    public void Move()
    {
        if (diceScript == null)
        {
            Debug.LogError("Dice script not assigned.");
            return;
        }

        // Burada hareket iþlemi yapýlýr
        Debug.Log("Tile hareket etti.");

        // Hareket sýnýrýný azalt
        if (levelLoader != null)
        {
            levelLoader.DecreaseMoveLimit();
        }

        switch (gameObject.name)
        {
            case "Right":
                if (!diceScript.isMoving)
                {
                    StartCoroutine(diceScript.I_Roll(Vector3.right)); // Move dice to the right
                }
                break;

            case "Left":
                if (!diceScript.isMoving)
                {
                    StartCoroutine(diceScript.I_Roll(Vector3.left)); // Move dice to the left
                }
                break;

            case "Up":
                if (!diceScript.isMoving)
                {
                    StartCoroutine(diceScript.I_Roll(Vector3.forward)); // Move dice forward
                }
                break;

            case "Down":
                if (!diceScript.isMoving)
                {
                    StartCoroutine(diceScript.I_Roll(Vector3.back)); // Move dice backward
                }
                break;
        }

    }

    // Example input detection if you want to trigger movement based on touch or click
    private void OnMouseDown()
    {
        Move(); // Call the Move method when the object is clicked
    }
}
