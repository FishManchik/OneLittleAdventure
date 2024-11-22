using DG.Tweening;
using UnityEngine;

public class DragDropHandlerOld : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject point;

    private Collider colliderHit;
    private Collider previousHit;
    private bool dragging;

    private void Start()
    {
        EventManager.TriedToInteract += HandleDragging;
    }

    void Update()
    {
        HandleDragging();

        if (dragging)
        {
            MoveObjectToPoint();
            RotateObject();
        }
    }

    private bool IsAbleToDrag()
    {
        foreach (var hit in Physics.OverlapSphere(transform.position, 3f, layerMask))
        {
            if (hit == previousHit)
            {
                colliderHit = null;
                return false;
            }

            if (IsWithinAngle(hit.transform.position) && !dragging)
            {
                colliderHit = hit;
                return true;
                break;
            }
        }
        return false;
    }

    private bool IsWithinAngle(Vector3 position)
    {
        float angle = Vector3.Angle(transform.forward, position - transform.position);
        return angle <= 60f;
    }

    private void HandleDragging()
    {
        if (IsAbleToDrag())
        {
            dragging = true;
            previousHit = null;
            FreezeTransform(dragging);
        }
        else
        {
            dragging = false;
            previousHit = colliderHit;
            FreezeTransform(dragging);
        }
    }

    private void MoveObjectToPoint()
    {
        colliderHit.transform.DOMove(point.transform.position, 0.1f);
    }

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
            colliderHit.transform.Rotate(new Vector3(0, transform.rotation.y, 0), 1f);

        /*
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        colliderHit.transform.Rotate(new Vector3(0, 0, scroll * 120f));
        */
    }

    private void FreezeTransform(bool freeze)
    {
        Rigidbody rb = colliderHit.gameObject.GetComponent<Rigidbody>();

        rb.useGravity = !freeze;
        rb.freezeRotation = freeze;
        rb.isKinematic = freeze;
    }
}
