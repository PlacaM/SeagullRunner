using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taller;

namespace Taller
{
    [System.Serializable]
    public class TouchResult
    {
        //posicion inicial donde toca la pantalla
        public Vector2 startScreenPoint;

        //posicion final donde toca la pantalla
        public Vector2 endScreenPoint;

        //posicion inicial en el mundo
        public Vector2 startWorldPoint;

        //posicion final en el mundo
        public Vector2 endWorldPoint;

        //posicion final en el mundo
        public Vector2 endViewportPoint;

        public Vector2 startViewportPoint;

        //  distancia del trazado en la pantalla
        public float swipeScreenDistance;

        //distancia del trazado en el mundo
        public float swipeWorldDistance;

        //  direccion del trazado en la pantalla
        public Vector2 swipeScreenDirection;

        //direccion del trazado en el mundo
        public Vector2 swipeWorldDirection;

        /// <summary>
        /// Si quieres saber cual de las 4 direcciones retorna un vector normalizado
        /// </summary>
        public Vector3 direction4WayVector;

        /// <summary>
        /// Si quieres saber cual de las 4 direcciones retorna un Enum
        /// </summary>
        public EDirections direction4Way;

        public TouchResult()
        {
            swipeScreenDirection = startViewportPoint = endViewportPoint = swipeWorldDirection = startScreenPoint = endWorldPoint = endWorldPoint = startWorldPoint = Vector2.zero;
            direction4Way = EDirections.NONE;
        }

        public void Reset()
        {
            swipeScreenDirection = startViewportPoint = endViewportPoint = swipeWorldDirection = startScreenPoint = endWorldPoint = endWorldPoint = startWorldPoint = Vector2.zero;
            direction4Way = EDirections.NONE;
            swipeScreenDistance = swipeWorldDistance = 0;
        }
    }

    public delegate void TouchEventGeneral();

    public delegate void TouchEventResult(TouchResult swipeResult);

    public static class EventTouch
    {
        public static TouchEventGeneral OnTouchBegin;
        public static TouchEventResult OnSwipeFinished, OnDrag, OnDragBegin, OnDragFinished;

        private static TouchResult swipeResult;
        public static float touchThreshold = 0.1f;

        private static void TouchDetector()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                //   OnTouchBegin?.Invoke();

                TouchBegin(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                TouchEnd(Input.mousePosition);
            }
#endif
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    TouchBegin(touch.position);
                }
                else
                if (touch.phase == TouchPhase.Ended)
                {
                    TouchEnd(touch.position);
                }
                else
                if (touch.phase == TouchPhase.Canceled)
                {
                    TouchEnd(touch.position);
                }
            }
        }

        private static void TouchBegin(Vector2 screenTouchPosition)
        {
            if (swipeResult == null)
                swipeResult = new TouchResult();

            swipeResult.Reset();
            swipeResult.startScreenPoint = screenTouchPosition;

            if (Camera.main != null)
            {
                Vector3 screenRec = new Vector3(screenTouchPosition.x, screenTouchPosition.y, Camera.main.transform.position.z);

                swipeResult.startWorldPoint = Camera.main.ScreenToWorldPoint(screenRec);
                swipeResult.startViewportPoint = (Camera.main.ScreenToViewportPoint(screenTouchPosition));
            }
            swipeResult.startScreenPoint = screenTouchPosition;

            Debug.LogFormat(" start {0}", swipeResult.startScreenPoint.ToString());
            Debug.LogFormat(" screenTouchPosition {0}", screenTouchPosition.ToString());
            // GetTouchStatus(screenTouchPosition);
            OnDragBegin?.Invoke(swipeResult);
            OnTouchBegin?.Invoke();
        }

        private static void GetTouchStatus(Vector2 screenTouchPosition)
        {
            if (swipeResult == null) return;
            swipeResult.endScreenPoint = screenTouchPosition;

            if (Camera.main != null)
            {
                Vector3 screenRec = new Vector3(screenTouchPosition.x, screenTouchPosition.y, Camera.main.transform.position.z);

                swipeResult.endWorldPoint = Camera.main.ScreenToWorldPoint(screenRec);
            }
            swipeResult.endViewportPoint = (Camera.main.ScreenToViewportPoint(screenTouchPosition));

            swipeResult.swipeScreenDirection = swipeResult.endScreenPoint - swipeResult.startScreenPoint;
            swipeResult.swipeScreenDistance = swipeResult.swipeScreenDirection.magnitude;

            swipeResult.swipeWorldDirection = swipeResult.endWorldPoint - swipeResult.startWorldPoint;
            swipeResult.swipeWorldDistance = swipeResult.swipeWorldDirection.magnitude;
            Vector2 viewportDirection = swipeResult.endViewportPoint - swipeResult.startViewportPoint;

            if (viewportDirection.magnitude < touchThreshold)
            {
                swipeResult.direction4Way = EDirections.NONE;
                swipeResult.direction4WayVector = Vector2.zero;
            }
            else
            if (Mathf.Abs(swipeResult.swipeScreenDirection.x) > Mathf.Abs(swipeResult.swipeScreenDirection.y))
            {
                if (swipeResult.swipeScreenDirection.x > 0)
                {
                    swipeResult.direction4Way = EDirections.RIGHT;
                    swipeResult.direction4WayVector = Vector2.right;
                }
                else
                {
                    swipeResult.direction4Way = EDirections.LEFT;
                    swipeResult.direction4WayVector = Vector2.left;
                }
            }
            else
            {
                if (swipeResult.swipeScreenDirection.y > 0)
                {
                    swipeResult.direction4Way = EDirections.UP;
                    swipeResult.direction4WayVector = Vector2.up;
                }
                else
                {
                    swipeResult.direction4Way = EDirections.DOWN;
                    swipeResult.direction4WayVector = Vector2.down;
                }
            }
        }

        private static void TouchDrag(Vector2 screenTouchPosition)
        {
            if (swipeResult == null) return;
            if (OnDrag == null)
            {
                return;
            }
            Debug.Log("dragging");
            // GetTouchStatus(screenTouchPosition);
            OnDrag?.Invoke(swipeResult);
        }

        private static void TouchEnd(Vector2 screenTouchPosition)
        {
            if (swipeResult == null)
            {
                Debug.LogError("swipeResult == null");
                return;
            };

            GetTouchStatus(screenTouchPosition);

            OnSwipeFinished?.Invoke(swipeResult);
            OnDragFinished?.Invoke(swipeResult);
            swipeResult.Reset();
        }

        // Update is called once per frame
        public static void ExternalUpdate()
        {
            TouchDetector();
        }
    }

    public class Helper
    {
        public static void TransformFlipZ(ref Transform transform)
        {
            transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z * -1);
        }
    }
}