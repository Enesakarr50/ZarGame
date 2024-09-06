using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject diceMesh;
    [SerializeField] int speed = 300;
    bool isMoving = false;

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

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        inputControl = new InputControl();
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

    private void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startTouchPosition = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            endTouchPosition = Mouse.current.position.ReadValue();
            Vector2 swipeDelta = endTouchPosition - startTouchPosition;

            if (swipeDelta.magnitude >= minSwipeDistance)
            {
                swipeDelta.Normalize();
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0 && !blockRight) StartCoroutine(I_Roll(Vector3.right));
                    else if (swipeDelta.x < 0 && !blockLeft) StartCoroutine(I_Roll(Vector3.left));
                }
                else
                {
                    if (swipeDelta.y > 0 && !blockForward) StartCoroutine(I_Roll(Vector3.forward));
                    else if (swipeDelta.y < 0 && !blockBack) StartCoroutine(I_Roll(Vector3.back));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("block left"))
            blockLeft = true;
        else if (other.gameObject.CompareTag("block right"))
            blockRight = true;
        else if (other.gameObject.CompareTag("block forward"))
            blockForward = true;
        else if (other.gameObject.CompareTag("block back"))
            blockBack = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("block left"))
            blockLeft = false;
        else if (other.gameObject.CompareTag("block right"))
            blockRight = false;
        else if (other.gameObject.CompareTag("block forward"))
            blockForward = false;
        else if (other.gameObject.CompareTag("block back"))
            blockBack = false;
    }

    public void ResetMovingConstrains()
    {
        blockBack = false;
        blockForward = false;
        blockLeft = false;
        blockRight = false;
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

    IEnumerator I_Roll(Vector3 direction)
    {
        if (isMoving) yield break;

        isMoving = true;

        onPlayerMove.Invoke();
        sound.Play();

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    IEnumerator I_WaitForInputController(bool isOn)
    {
        while (inputControl == null) yield return null;

        ToggleActiveState(isOn);
    }
}
