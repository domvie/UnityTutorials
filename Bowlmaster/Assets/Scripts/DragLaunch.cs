using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour
{

    Ball ballComponent;
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    bool isDragging = false;

    private void Start()
    {
        ballComponent = GetComponent<Ball>();
    }

    public void DragStart()
    {
        // Capture time & pos. of drag start
        dragStart = Input.mousePosition;
        startTime = Time.time;
        isDragging = true;
    }

    public void MoveStart(float amount)
    {
        var ballObj = FindObjectOfType<Ball>();
        if (!ballComponent.inPlay)
        {
            ballComponent.transform.Translate(new Vector3(amount, 0, 0));
        }
    }

    public void DragEnd()
    {
        isDragging = false;
        // Launch the ball
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;

        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
        ballComponent.Launch(launchVelocity);
    }
}
