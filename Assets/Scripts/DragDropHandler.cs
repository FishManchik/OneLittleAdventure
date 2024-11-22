using UnityEngine;
using DG.Tweening;

public class DragDropHandler : PlayerRoot
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _point;
    private Transform _objectToDrag;

    private void OnEnable()
    {
        EventManager.TriedToInteract += CheckForObject;
    }

    private void OnDisable()
    {
        EventManager.TriedToInteract -= CheckForObject;
    }

    private void Update()
    {
        if (_objectToDrag != null)
        {
            MoveObjectToPoint();
            RotateObject();
        }
    }

    private void CheckForObject()
    {
        if (_objectToDrag != null)
        {
            DropObject();
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, 5f, _layerMask);
        if (hits.Length > 0)
        {
            _objectToDrag = hits[0].transform;

            if (_objectToDrag == null)
            {
                DropObject();
                return;
            }

            LiftObject();
        }
    }

    private void LiftObject()
    {
        if (_objectToDrag == null) return;
        FreezeRigidbody(_objectToDrag, true);
        MoveObjectToPoint();
    }

    private void DropObject()
    {
        if (_objectToDrag == null) return;
        FreezeRigidbody(_objectToDrag, false);
        _objectToDrag = null;
    }

    private void MoveObjectToPoint()
    {
        if (_objectToDrag == null || _point == null) return;

        _objectToDrag.DOMove(_point.transform.position, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
        });
    }

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.R) && _objectToDrag != null)
        {
            _objectToDrag.Rotate(Vector3.back, 1f);
        }
    }

    private void FreezeRigidbody(Transform obj, bool freeze)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = !freeze;
            rb.isKinematic = freeze;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
// Привет, Max! Hav ар ю? Неден? Вхи? Blin