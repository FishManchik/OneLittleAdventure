using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InterestPointHandler : MonoBehaviour
{
    [SerializeField] private float _sphereCastSize;
    [SerializeField] private LayerMask _layerMask;

    private GameObject _player;

    void Start()
    {

    }

    void Update()
    {
        if (IsPlayerInZone())
        {
            TurnPlayersHead();
        }
    }

    private bool IsPlayerInZone()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _sphereCastSize, _layerMask);

        if (hits.Length > 0)
        {
            _player = hits[0].gameObject;
            Debug.Log(_player.gameObject);
            return true;
        }
        ResetPlayersHead();
        return false;
    }

    private void TurnPlayersHead()
    {
        _player.GetComponent<Rig>().weight = 1;
    }

    private void ResetPlayersHead()
    {
        _player.GetComponent<Rig>().weight = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sphereCastSize);
    }
}
