using DG.Tweening;
using UnityEngine;

public class MagneticAlignmentHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform finalRotation;

    private GameObject objectToMagnetize;
    private bool isOccupied;

    void Update()
    {
        foreach (var hit in Physics.OverlapSphere(transform.position, 3f, layerMask))
        {
            objectToMagnetize = hit.gameObject;

            if (!isOccupied)
            {
                MagnetizeToPosition();
            }
        }
    }

    private void MagnetizeToPosition()
    {
        if (objectToMagnetize)
        {
            objectToMagnetize.transform.DOMove(transform.position, 0.1f).OnComplete(() => objectToMagnetize.transform.DORotate(finalRotation.rotation.eulerAngles, 0.5f));

            if (objectToMagnetize.transform.position.y <= transform.position.y + 0.3f)
            {
                ResetObject();
                isOccupied = true;
            }
        }
    }

    private void ResetObject()
    {
        objectToMagnetize.gameObject.layer = 0;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Destroy(objectToMagnetize.GetComponent<Rigidbody>());
    }

    void OnDrawGizmos()
    {
        finalRotation = transform;
    }
}
