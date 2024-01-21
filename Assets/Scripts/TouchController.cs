using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class TouchController : MonoBehaviour
{
    public GeneralController general;
    public PlinkoLevel plinko;
    public Vector3 touchPos;
    public Vector3 touchPosWorld;

    public bool blocked;

    public void Update()
    {
        if (!general.paused)
        {
            touchInput();
        }
    }

   public void touchInput()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouch();
            }
            if (Input.GetMouseButton(0))
            {
                touchPos = Input.mousePosition;
                Vector3 mousePosCorrected = new Vector3(touchPos.x, touchPos.y - screenHeight / 2, 10);
                touchPosWorld = Camera.main.ScreenToWorldPoint(mousePosCorrected);
                continueTouch();
            }
            if (Input.GetMouseButtonUp(0))
            {
                endTouch();
            }

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startTouch();
                    break;
                case TouchPhase.Moved:
                    touchPos = touch.position;
                    Vector3 mousePosCorrected = new Vector3(touchPos.x, touchPos.y - screenHeight / 2, 10);
                    touchPosWorld = Camera.main.ScreenToWorldPoint(mousePosCorrected);
                    continueTouch();
                    break;
                case TouchPhase.Ended:
                    endTouch();

                    break;

            }
        }
    }

    public void startTouch()
    {
        if (!general.paused)
        {
            if (!blocked)
            {
                if (!plinko.isChoosingAngle && general.numberOfBalls > 0)
                {
                    plinko.trajectoryRenderer.positionCount = 0;
                    plinko.isChoosingAngle = true;
                }
            }
        }
    }
    public void continueTouch()
    {
        if (!general.paused)
        {
            if (plinko.isChoosingAngle)
            {

                plinko.direction = (touchPosWorld - plinko.ballHolder.transform.position);
                plinko.direction.y = plinko.launchForceMagnitude;
                plinko.rotateHolder();
                plinko.DisplayTrajectory();
            }
        }
    }

    public void endTouch()
    {
        if (!general.paused)
        {
            if (!general.ui.settingScreen.activeSelf)
            {
                blocked = false;
            }
            if (plinko.isChoosingAngle && !blocked)
            {
                plinko.isChoosingAngle = false;

                general.numberOfBalls -= 1;
                general.ballstocollecttext.text = general.numberOfBalls.ToString("0");

                if (general.numberOfBalls == 0)
                {
                    StartCoroutine(general.loseornot());
                }

                plinko.LaunchBall();
                plinko.trajectoryRenderer.positionCount = 0;
            }
        }
    }

    
}
