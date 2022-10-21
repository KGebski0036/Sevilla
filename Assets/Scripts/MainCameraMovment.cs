using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovment : MonoBehaviour
{
    [SerializeField] float speed;

    private float newYAngle;
    private float oldYAngle;
    public SelectManager selectManager;

    private Vector3 moveVector;
    private CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move((moveVector * speed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            newYAngle = oldYAngle + 90f;
            StartCoroutine(Rotate(newYAngle));
            oldYAngle = newYAngle;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StopAllCoroutines();
            newYAngle = oldYAngle - 90f;
            StartCoroutine(Rotate(newYAngle));
            oldYAngle = newYAngle;
        }

    }

    IEnumerator Rotate(float targetAngle)
    {
        while (transform.rotation.y != targetAngle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), 3f * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        yield return null;
    }
}
