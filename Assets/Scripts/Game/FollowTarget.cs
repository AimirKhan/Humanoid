using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool smoothFollow = true;
    [SerializeField] private float smoothSpeed = .1f;
    
    private void LateUpdate()
    {
        //TODO remade to EveryLateUpdate
        CameraPos();
    }

    private void CameraPos()
    {
        if (smoothFollow)
            transform.DOMove(target.position, smoothSpeed);
        else
            transform.position = target.position;
        //transform.LookAt(cameraTarget);
    }
}
