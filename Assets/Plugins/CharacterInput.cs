using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool tapRequested;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    public void ManualUpdate()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
//#if UNITY_EDITOR || UNITY_STANDALONE
//        #region Standalone Inputs
//        if (Input.GetMouseButtonDown(0))
//        {
//            tapRequested = true;
//            isDragging = true;
//            startTouch = Input.mousePosition;
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            if (tapRequested) { tap = true; }
//            isDragging = false;
//            Reset();
//        }
//        #endregion
//#elif UNITY_ANDROID || UNITY_IOS
//        #region Mobile Inputs
//        if (Input.touchCount > 0)
//        {
//            if (Input.touches[0].phase == TouchPhase.Began)
//            {
//                tapRequested = true;
//                isDragging = true;
//                startTouch = Input.touches[0].position;
//            }
//            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
//            {
//                if (tapRequested) { tap = true; }
//                isDragging = false;
//                Reset();
//            }
//        }
//        #endregion
//#endif

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touchCount > 0) { swipeDelta = Input.touches[0].position - startTouch; }
            else if (Input.GetMouseButton(0)) { swipeDelta = (Vector2)Input.mousePosition - startTouch; }
        }

        //Did we cross the dead zone?
        if (swipeDelta.magnitude > 100)
        {
            tapRequested = false;
            //Which direction are we swiping?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right?
                if (x > 0) { swipeRight = true; }
                else { swipeLeft = true; }
                //x > 0 ? swipeRight = true : swipeLeft = true;
            }
            else
            {
                //Up or down?
                if (y > 0) { swipeUp = true; }
                else { swipeDown = true; }
                // y > 0 ? swipeUp = true : swipeDown = true;
            }
            Reset();
        }
    }

    public void ResetInput()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tapRequested = true;
        isDragging = true;
        startTouch = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tapRequested) { 
            tap = true; 
        }
        isDragging = false;
        Reset();
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }
}
