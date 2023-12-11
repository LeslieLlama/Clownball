using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RopeScript : MonoBehaviour
{
    [SerializeField] private Transform transPoint1, transPoint2, transPoint3;
    [SerializeField] private Transform aimingPoint;
    [SerializeField] private LineRenderer lr;
    private EdgeCollider2D edgeCollider2D;

    [SerializeField] private GameObject playerBall;
    private Clownball clownBall;

    //slingshot variables
    [SerializeField] private Vector3 origin;
    [SerializeField] private float forceConstant = 200;
    [SerializeField] private Vector3 anchor;
    [SerializeField] private bool invertControls;
    [SerializeField] private bool directControl;

    //references
    [SerializeField] private Rigidbody2D rb2d;

    //events 
    public static event Action<float> OnSlingshotRelease;

    private bool mouseDown;
    private bool mouseHeld;
    private bool mouseUp;
    private bool ballConfirmedForLaunch;
    Vector3 mousePosition;

    private void OnEnable()
    {
        Collector.OnPlayerOutOfBounds += ResetPlayerPosition;
    }
    private void OnDisable()
    {
        Collector.OnPlayerOutOfBounds -= ResetPlayerPosition;
    }

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        clownBall = playerBall.GetComponent<Clownball>();
    }

    void Update()
    {
        mouseDown = (Input.GetMouseButtonDown(0));
        //if (mouseDown) { print("mousedown input accepted"); }
        mouseHeld = (Input.GetMouseButton(0));
        mouseUp = (Input.GetMouseButtonUp(0));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ballConfirmedForLaunch = (clownBall.ballConnected == true && mouseUp);

        if (mouseDown)
        {
            anchor = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
        if (mouseUp)
        {
            OnSlingshotRelease((origin.y - rb2d.gameObject.transform.position.y));
        }
        if (ballConfirmedForLaunch)
        {
            float shootForce =  MathF.Pow((aimingPoint.position.y - rb2d.gameObject.transform.position.y), 3.3f);
            playerBall.GetComponent<Rigidbody2D>().AddForce(shootForce * (aimingPoint.position - rb2d.gameObject.transform.position));
            StartCoroutine(IntangibilityTimer(0.25f));
        }
    }

    private void FixedUpdate()
    {
        lr.positionCount = 3;
        lr.SetPosition(0, transPoint1.position);
        lr.SetPosition(1, transPoint2.position);
        lr.SetPosition(2, transPoint3.position);
        SetEdgeCollider(lr);
        DragBall();
    }

    void ResetPlayerPosition()
    {
        playerBall.transform.position = aimingPoint.position;
    }

    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider2D.SetPoints(edges);
    }

    void DragBall()
    {
        if (mouseHeld)
        {
            Vector3 hook = new Vector3(mousePosition.x, mousePosition.y, 0);
            Vector3 heading;
            if (directControl)
            {
                transPoint2.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                return;
            }
            if (invertControls == false)
            {
                heading = (anchor - hook);
            }
            else
            {
                heading = (hook - anchor);
            }
            Vector3 appliedDifference = origin - heading;
            transPoint2.position = new Vector3(appliedDifference.x, appliedDifference.y, 0);
        }
        else
        {
            SlingshotActive();
        }
    }

    void SlingshotActive()
    {
        if (rb2d.gameObject.transform.position != origin)
        {
            rb2d.AddForce(forceConstant * (origin - rb2d.gameObject.transform.position));
            rb2d.velocity *= 0.3f;
        }
        /*if (transPoint2.position != origin)
        {
            var step = forceConstant * Time.deltaTime; // calculate distance to move
            transPoint2.position = Vector3.MoveTowards(transPoint2.position, origin, step);
        }
        */
    }

    private IEnumerator IntangibilityTimer(float time)
    {
        edgeCollider2D.enabled = false;
        yield return new WaitForSeconds(time);
        edgeCollider2D.enabled = true;
    }


}
