using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _joystick;

    [SerializeField]
    private float _speed;

    private Rigidbody _rb;
    private float _joystickSize;

    [SerializeField]
    private Animator _handsAnim;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joystickSize = _joystick.GetComponent<RectTransform>().sizeDelta.x;
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.forward * (_joystick.localPosition.y / _joystickSize * _speed) + transform.right * (_joystick.localPosition.x / _joystickSize * _speed);
        Vector3 newPos = new Vector3(pos.x, _rb.velocity.y, pos.z);
        _rb.velocity = newPos;

        bool isWalking = Vector3.Distance(_joystick.localPosition, Vector3.zero) > 0.01f;
        
        _handsAnim.SetBool("walk", isWalking);
        
        bool isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.1f);
        
        _handsAnim.SetBool("air", !isGrounded);
    }
}
