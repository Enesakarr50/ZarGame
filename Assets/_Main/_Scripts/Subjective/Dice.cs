using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Assertions.Must;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject diceMesh;
    [SerializeField] int speed = 300;
    public bool isMoving = false;

    [SerializeField] bool blockLeft;
    [SerializeField] bool blockRight;
    [SerializeField] bool blockForward;
    [SerializeField] bool blockBack;

    [HideInInspector]
    public UnityEvent onPlayerMove;

    AudioSource sound;
    InputControl inputControl;


    Vector2 startTouchPosition;
    Vector2 endTouchPosition;
    float minSwipeDistance = 50f;

    public Vector3 LastDir;

    public bool isSliding = false;
    public bool CanSlide = true;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        inputControl = new InputControl(); ;

    }

    private void Start()
    {
        inputControl.Dice.Enable();

        inputControl.Dice.Forward.performed += (c) => {
            if (!blockForward) StartCoroutine(I_Roll(Vector3.forward));
        };

        inputControl.Dice.Backward.performed += (c) => {
            if (!blockBack) StartCoroutine(I_Roll(Vector3.back));
        };

        inputControl.Dice.Right.performed += (c) => {
            if (!blockRight) StartCoroutine(I_Roll(Vector3.right));
        };

        inputControl.Dice.Left.performed += (c) => {
            if (!blockLeft) StartCoroutine(I_Roll(Vector3.left));
        };


    } 




    private Vector3 iceDirection = Vector3.zero; // Yönü saklamak için bir deðiþken

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ice") && !isSliding)
        {
            StartCoroutine(SlideOnIce(LastDir, other.gameObject));


        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        CanSlide = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        CanSlide = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("block left"))
        {
            blockLeft = true;

        }


        else if (other.gameObject.CompareTag("block right"))
        {
            blockRight = true;

        }

        else if (other.gameObject.CompareTag("block forward"))
        {
            blockForward = true;

        }

        else if (other.gameObject.CompareTag("block back"))
        {
            blockBack = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("block left"))
        {
            blockLeft = false;

        }


        else if (other.gameObject.CompareTag("block right"))
        {
            blockRight = false;

        }

        else if (other.gameObject.CompareTag("block forward"))
        {
            blockForward = false;

        }

        else if (other.gameObject.CompareTag("block back"))
        {
            blockBack = false;

        }
    }

    public void ResetMovingConstrains()
    {
        blockBack = false;
        blockForward = false;
        blockLeft = false;
        blockRight = false;
    }

    IEnumerator SlideOnIce(Vector3 direction, GameObject IceTile)
    {
        LastDir = direction;
        isMoving = true;
        isSliding = true;

        onPlayerMove.Invoke();
        sound.Play();

        yield return new WaitForSeconds(0.2f);

        float remainingWay = IceTile.GetComponent<IceTile>().SlideCount;

        while (remainingWay > 0 && CanSlide)
        {
            transform.position += direction / 100;
            remainingWay -= 0.01f;

            // Duvara çarpýldýðýnda kaymayý durdur
            if (blockLeft || blockRight || blockForward || blockBack)
            {
                Debug.Log("Wall hit, stopping slide.");
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        isMoving = false;
        isSliding = false;
    }



    public void GoingAnim()
    {
        Sequence s = DOTween.Sequence();
        s.Append(diceMesh.transform.DOScale(Vector3.one * 1.05f, 0.1f));
        s.Append(diceMesh.transform.DOScale(Vector3.zero, 0.4f));
    }

    public void CommingAnim(bool activeOnComplete = true)
    {
        Sequence s = DOTween.Sequence();
        s.Append(diceMesh.transform.DOScale(Vector3.one * 1.05f, 0.4f));
        s.Append(diceMesh.transform.DOScale(Vector3.one, 0.1f));

        if (activeOnComplete)
            s.AppendCallback(() => {
                ToggleActiveState(true);
            });
    }

    public void ToggleActiveState(bool isOn)
    {
        if (inputControl == null)
        {
            StartCoroutine(I_WaitForInputController(isOn));
            return;
        }

        if (isOn) inputControl.Dice.Enable();
        else inputControl.Dice.Disable();
    }

    public void ResetGraphicsScale()
    {
        diceMesh.transform.localScale = Vector3.zero;
    }

    public IEnumerator I_Roll(Vector3 direction)
    {
        if (isMoving || isSliding)
        {
            Debug.Log("Cannot roll, dice is already moving!");
            yield break;
        }

        LastDir = direction;
        isMoving = true;

        onPlayerMove.Invoke();
        sound.Play();

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction.normalized / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);

            // Rotate around the center
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);

            // Manually set the y position to 0 after rotating
            Vector3 currentPosition = transform.position;
            currentPosition.y = 0; // Y eksenini sabitle

            transform.position = currentPosition;

            remainingAngle -= rotationAngle;
            yield return null;
        }

        // Snap the x and z positions to the nearest integer value
        Vector3 finalPosition = transform.position;
        finalPosition.x = Mathf.Round(finalPosition.x); // Snap X to nearest integer
        finalPosition.z = Mathf.Round(finalPosition.z); // Snap Z to nearest integer

        // Apply the new snapped position
        transform.position = finalPosition;

        isMoving = false;
    }


    IEnumerator I_WaitForInputController(bool isOn)
    {
        while (inputControl == null) yield return null;

        ToggleActiveState(isOn);
    }
}