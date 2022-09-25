using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle, Ready, Tracking
    }
    private State state
    {
        set
        {
            switch(value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    private Transform target;

    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;//���������� ���� �ӵ��� �̵����̾��°�?
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5f;

    private float roundReadyZoomSize = 14.5f;
    private float readyShotZoomSize = 5f;
    private float trackingZoomSize = 10f;

    private float lastZoomSpeed;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();//�ڽ��� ������ ������Ʈ�� ������
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref lastMovingVelocity, smoothTime); //�ɸ��� �ð� �ȿ� �������ϰ� �۵��� �̾��ش�
        transform.position = smoothPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if(target!=null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
