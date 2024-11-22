using DG.Tweening;
using UnityEngine;

public class PlayerReturner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _spawnpoint;

    private Transform _player;

    void Awake()
    {
        // Optional: You can find the player transform here if it's a child or find it through other means.
        // _player = FindObjectOfType<Player>().transform; // Example if you have a Player script.
    }

    private void ReturnPlayerToSpawnPoint()
    {
        if (_player != null)
        {
            Debug.Log("Returning player to spawn point");
            _player.DOMove(_spawnpoint.position, 1f);
            _player = null; // Reset the player reference after moving
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.transform.CompareTag("Player")) // Ensure your player has the "Player" tag
        {
            _player = collision.transform; // Store the player's transform
            ReturnPlayerToSpawnPoint(); // Move the player to the spawn point upon collision
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmoSize = transform.localScale;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up, gizmoSize);
    }
}
