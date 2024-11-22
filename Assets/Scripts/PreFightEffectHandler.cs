using UnityEngine;

public class PreFightEffectHandler : MonoBehaviour
{
    private const string IsAppear = "IsAppear";

    private Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        AutomaticInputHandler.PreFightEffectEvent += SetState;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnderEnemy();
    }

    public void SetState(Collider hit, bool IsInRadius)
    {
        GetComponent<Animator>().SetBool(IsAppear, IsInRadius);
        enemyTransform = hit.transform;
    }

    private void MoveUnderEnemy()
    {
        if (enemyTransform != null)
            this.gameObject.transform.position = new Vector3(enemyTransform.position.x, 0.03f, enemyTransform.position.z);
    }
}
