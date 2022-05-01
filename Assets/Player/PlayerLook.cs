using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 10, _rightFingerIndex, _slerpTime = 0.7f, _handsFollowSlerpTime = 0.9f;
    private float _middleScreenPoint;

    private Vector3 _firstPoint, _secondPoint;

    public float XAngle, YAngle;
    private float _xAngleTemp, _yAngleTemp;

    [SerializeField]
    private Transform _hands;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > _middleScreenPoint && _rightFingerIndex == -1)
                {
                    _rightFingerIndex = touch.fingerId;
                    _firstPoint = touch.position;
                    _xAngleTemp = XAngle;
                    _yAngleTemp = YAngle;
                }
            }

            if (touch.fingerId == _rightFingerIndex)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    _secondPoint = touch.position;
                    XAngle = _xAngleTemp + (_secondPoint.x - _firstPoint.x) * 180 * _sensitivity / Screen.width;
                    YAngle = _yAngleTemp + (_secondPoint.y - _firstPoint.y) * 90 * _sensitivity / Screen.height;

                    YAngle = Mathf.Clamp(YAngle, -90f, 90f);
                    if (XAngle >= 360) XAngle = 0;
                    else if (XAngle < 0) XAngle += 360;
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _rightFingerIndex = -1;
                }
            }
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-YAngle, 0, 0.0f), _slerpTime);
        _hands.rotation = Quaternion.Slerp(_hands.rotation, transform.rotation, _slerpTime);
        _hands.position = Vector3.Lerp(_hands.position, transform.position, _handsFollowSlerpTime);
    }
}
