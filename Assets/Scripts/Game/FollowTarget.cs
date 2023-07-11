using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Humanoid;
using UnityEngine;
using Zenject;

public class FollowTarget : MonoBehaviour
{
    [Inject] private IPlayerValues playerValues;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraOffsetTarget;
    [SerializeField] private bool smoothFollow = true;
    [SerializeField] private float smoothSpeed = .1f;
    [SerializeField] private Vector3 panOffset;

    private void Awake()
    {
        playerValues.PlayerCamera = playerCamera;

        cameraOffsetTarget.position += panOffset;
    }

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
    }
}
