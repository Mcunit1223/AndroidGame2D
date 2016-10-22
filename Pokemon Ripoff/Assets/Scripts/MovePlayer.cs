using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class MovePlayer : MonoBehaviour {
    public float speed = 5.0f;
    bool canMove;
    Vector3 touchPosition;
    Vector3 startPostition;
    float lastDistance;

    // Use this for initialization
    void Start () {
        touchPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float angle = -Mathf.Atan2(touchPosition.x - transform.position.x, touchPosition.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
        if (Input.GetMouseButtonDown(0))
        {
            canMove = true;
            Vector3 touch = Input.mousePosition;
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 0));
            startPostition = transform.position;
        }
        if (Input.touchCount > 0)
        {
            // The screen has been touched so store the touch
            canMove = true;
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            startPostition = transform.position;
        }
        if (canMove)
        {
            // If the finger is on the screen, move the object smoothly to the touch position
            float currentDistance = Vector3.Distance(transform.position, touchPosition);
            float percentageComplete = 1.1f - currentDistance / Vector3.Distance(startPostition, touchPosition);
            transform.position = Vector3.Lerp(transform.position, touchPosition, percentageComplete);

        }
        if(transform.position == touchPosition)
        {
            canMove = false;
        }
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
