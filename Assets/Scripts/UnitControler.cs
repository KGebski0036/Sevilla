using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControler : MonoBehaviour
{
    private Vector3 movePosition;
    private MoveVelocity moveVelocity;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource src;
    public bool selected;
    void Awake()
    {
        movePosition = transform.position;
        moveVelocity = GetComponent<MoveVelocity>();
    }
    void Update()
    {
        Vector3 moveDirection = (movePosition - transform.position).normalized;

        if (Vector3.Distance(movePosition, transform.position) < 1f)
            moveDirection = Vector3.zero;

        movePosition = transform.position;



        if (selected)
        {
            anim.SetFloat("Speed", Math.Max(Math.Abs(Input.GetAxis("MHorizontal")) , Math.Abs(Input.GetAxis("MVertical"))));
            moveVelocity.Velocity = new Vector3(Input.GetAxis("MHorizontal"), 0, Input.GetAxis("MVertical")).normalized;
            if (moveVelocity.Velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVelocity.Velocity), Time.deltaTime * 4f);
                
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
}
