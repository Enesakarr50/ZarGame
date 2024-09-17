using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceRotationController : MonoBehaviour
{
    public Button button1;         
    public Button button2;         
    public Button button3;         
    public Button button4;         
    public Button button5;         
    public Button button6;         

    
    public Vector3 rotation6 = new Vector3(0, 90, 90);     
    public Vector3 rotation5 = new Vector3(90, 0, 180);    
    public Vector3 rotation4 = new Vector3(0, 90, 0);      
    public Vector3 rotation3 = new Vector3(-180, -90, 0);  
    public Vector3 rotation2 = new Vector3(-90, 0, 0);     
    public Vector3 rotation1 = new Vector3(0, 90, -90);    

    private Rigidbody diceRigidbody;  
    public bool isOnTile = false;    

    private void Start()
    {
       
        diceRigidbody = gameObject.GetComponent<Rigidbody>();

        
        button1.onClick.AddListener(() => StartCoroutine(RotateDice(rotation1)));
        button2.onClick.AddListener(() => StartCoroutine(RotateDice(rotation2)));
        button3.onClick.AddListener(() => StartCoroutine(RotateDice(rotation3)));
        button4.onClick.AddListener(() => StartCoroutine(RotateDice(rotation4)));
        button5.onClick.AddListener(() => StartCoroutine(RotateDice(rotation5)));
        button6.onClick.AddListener(() => StartCoroutine(RotateDice(rotation6)));
    }

    
    private IEnumerator RotateDice(Vector3 targetRotation)
    {
        if (isOnTile)  // Eðer zar bir Tile üzerindeyse iþlem yapýlmasýn
        {
            Debug.Log("Cannot rotate, dice is on a Tile.");
            yield break;
        }

        gameObject.GetComponent<Dice>().isMoving = true;
        diceRigidbody.isKinematic = false;

       
        diceRigidbody.AddForce(Vector3.up * 10, ForceMode.VelocityChange);

        
        yield return new WaitForSeconds(0.5f);

       
        float duration = 0.8f;
        float elapsedTime = 0f;
        Vector3 initialRotation = gameObject.transform.rotation.eulerAngles;

        while (elapsedTime < duration)
        {
            
            float t = elapsedTime / duration;
            Vector3 currentRotation = Vector3.Lerp(initialRotation, initialRotation + new Vector3(720, 720, 720), t);
            gameObject.transform.rotation = Quaternion.Euler(currentRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(targetRotation);

        yield return new WaitForSeconds(1.0f);

        diceRigidbody.isKinematic = true;
        gameObject.GetComponent<Dice>().isMoving = false;
    }

    // Dice bir Tile ile temas ettiðinde
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndTile")
        {
            isOnTile = true;
        }
    }

    // Dice bir Tile'dan çýktýðýnda
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EndTile")
        {
            isOnTile = false;
        }
    }
}
