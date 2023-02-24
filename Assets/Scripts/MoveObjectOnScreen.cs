using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnScreen : MonoBehaviour
{
    [SerializeField] private GameObject FirstMovingObject;
    [SerializeField] private GameObject SecondMovingObject;
    [SerializeField] private float Speed;

    private Vector3 LeftDownAngleOfScreenForFirstObject;
    private Vector3 LeftUpAngleOfScreenForFirstObject;
    private Vector3 RightUpAngleOfScreenForFirstObject;
    private Vector3 RightDownAngleOfScreenForFirstObject;
    
    private Vector3 LeftDownAngleOfScreenForSecondObject;
    private Vector3 LeftUpAngleOfScreenForSecondObject;
    private Vector3 RightUpAngleOfScreenForSecondObject;
    private Vector3 RightDownAngleOfScreenForSecondObject;
    
    private Vector3[] ArrayAnglesOfScreenForFirstObject;
    private Vector3[] ArrayAnglesOfScreenForSecondObject;

    private float journeyLengthOfFirstObject;
    private float journeyLengthOfSecondObject;

    private float factionJorneyOfFirstObject;
    private float factionJorneyOfSecondObject;

    private float startTimeForFirstObject;
    private float startTimeForSecondObject;

    private float coveredDistanceAnFirstObject;
    private float coveredDistanceAnSecondObject;

    private int nextAngleForFirstObject = 0;
    private int nextAngleForSecondObject = 0;

    private void Awake()
    {
        LeftDownAngleOfScreenForFirstObject = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, Camera.main.farClipPlane));
        RightDownAngleOfScreenForFirstObject = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.1f, Camera.main.farClipPlane));
        LeftUpAngleOfScreenForFirstObject = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, Camera.main.farClipPlane));
        RightUpAngleOfScreenForFirstObject = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, Camera.main.farClipPlane));
    
        LeftDownAngleOfScreenForSecondObject = Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0.25f, Camera.main.farClipPlane));
        RightDownAngleOfScreenForSecondObject = Camera.main.ViewportToWorldPoint(new Vector3(0.75f, 0.25f, Camera.main.farClipPlane));
        LeftUpAngleOfScreenForSecondObject = Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0.75f, Camera.main.farClipPlane));
        RightUpAngleOfScreenForSecondObject = Camera.main.ViewportToWorldPoint(new Vector3(0.75f, 0.75f, Camera.main.farClipPlane));

        ArrayAnglesOfScreenForFirstObject = new Vector3[4]
        {
            LeftDownAngleOfScreenForFirstObject,
            LeftUpAngleOfScreenForFirstObject,
            RightUpAngleOfScreenForFirstObject,
            RightDownAngleOfScreenForFirstObject
        };

        ArrayAnglesOfScreenForSecondObject = new Vector3[4]
        { 
            RightUpAngleOfScreenForSecondObject,
            LeftUpAngleOfScreenForSecondObject,
            LeftDownAngleOfScreenForSecondObject,
            RightDownAngleOfScreenForSecondObject
        };
    }

    private void Start() 
    {
        startTimeForFirstObject = Time.time;
        startTimeForSecondObject = Time.time;

        journeyLengthOfFirstObject = Vector3.Distance(FirstMovingObject.transform.position, ArrayAnglesOfScreenForFirstObject[nextAngleForFirstObject]);
        journeyLengthOfSecondObject = Vector3.Distance(SecondMovingObject.transform.position, ArrayAnglesOfScreenForSecondObject[nextAngleForSecondObject]);
        
        Speed = 1f;
    }

    private void Update()
    {
        MoveObject();
        NextAngleForMove();
    }

    private void MoveObject()
    {
        coveredDistanceAnFirstObject = (Time.time - startTimeForFirstObject) * Speed;
        coveredDistanceAnSecondObject = (Time.time - startTimeForSecondObject) * Speed;

        factionJorneyOfFirstObject = coveredDistanceAnFirstObject / journeyLengthOfFirstObject;
        factionJorneyOfSecondObject = coveredDistanceAnSecondObject / journeyLengthOfSecondObject;

        FirstMovingObject.transform.position = Vector3.Lerp(FirstMovingObject.transform.position, ArrayAnglesOfScreenForFirstObject[nextAngleForFirstObject], factionJorneyOfFirstObject);
        SecondMovingObject.transform.position = Vector3.Lerp(SecondMovingObject.transform.position, ArrayAnglesOfScreenForSecondObject[nextAngleForSecondObject], factionJorneyOfSecondObject); 
    }

    private void NextAngleForMove()
    {
        if (FirstMovingObject.transform.position == ArrayAnglesOfScreenForFirstObject[nextAngleForFirstObject])
        {
            FirstMovingObject.transform.position = ArrayAnglesOfScreenForFirstObject[nextAngleForFirstObject];

            if (nextAngleForFirstObject == ArrayAnglesOfScreenForFirstObject.Length - 1) 
                nextAngleForFirstObject = 0;
            else nextAngleForFirstObject++;

            startTimeForFirstObject = Time.time;
            journeyLengthOfFirstObject = 0;
            journeyLengthOfFirstObject = Vector3.Distance(FirstMovingObject.transform.position, ArrayAnglesOfScreenForFirstObject[nextAngleForFirstObject]); 
        }

        if (SecondMovingObject.transform.position == ArrayAnglesOfScreenForSecondObject[nextAngleForSecondObject])
        {
            SecondMovingObject.transform.position = ArrayAnglesOfScreenForSecondObject[nextAngleForSecondObject];

            if (nextAngleForSecondObject == ArrayAnglesOfScreenForSecondObject.Length - 1) 
                nextAngleForSecondObject = 0;
            else nextAngleForSecondObject++;

            startTimeForSecondObject = Time.time;
            journeyLengthOfSecondObject = 0;
            journeyLengthOfSecondObject = Vector3.Distance(SecondMovingObject.transform.position, ArrayAnglesOfScreenForSecondObject[nextAngleForSecondObject]);
        }
    }
}
