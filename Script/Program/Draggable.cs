using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform dragArea;
    private Vector3 offset;
    public bool isDragging;
    
    private bool recentlyDragged;
    
    private Vector3 initialPressPosition;
    private float dragThreshold = 5f;
    private float dragCoolDown = 0.1f;
    private float lastDragTime;

    public float TimeSinceLastDrag { get { return Time.time - lastDragTime; } }

    public bool WasRecentlyDragged()
    {
        return (Time.time - lastDragTime) < dragCoolDown; // 200 milliseconds cooldown
    }

// Assuming the code block is from the OnPointerDown method

public void OnPointerDown(PointerEventData eventData)
{
    RectTransform targetRectTransform = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
    
    if (targetRectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(targetRectTransform, eventData.position, eventData.pressEventCamera))
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos))
        {
            // Direct calculation of offset without adjustments for pivot
            offset = transform.position - globalMousePos;
            initialPressPosition = globalMousePos;
            isDragging = false; // Initially, do not consider it as dragging
        }
    }
}

public void OnDrag(PointerEventData eventData)
{
    if (!isDragging && Vector3.Distance(initialPressPosition, eventData.position) > dragThreshold)
    {
        isDragging = true; // Only start dragging if movement exceeds the threshold
    }

    if (isDragging)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos))
        {
            transform.position = globalMousePos + offset;
        }
    }
}


    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            lastDragTime = Time.time; // Record end time of drag
        }
        isDragging = false; // Stop dragging
    }
}