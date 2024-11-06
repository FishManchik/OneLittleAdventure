using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Friend : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NPCConversation conversation;

    private const string IsSitting = "IsSitting";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForCollisions();
    }

    public void CheckForCollisions()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= 15)
        {
            Debug.Log("Можно начинать разговор");
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayAnimation();
            }
        }
    }

    public void PlayAnimation()
    {
        if (conversation)
        {
            FindAnyObjectByType(typeof(AutomaticInputHandler)).GetComponent<Animator>().SetBool(IsSitting, true);
            Invoke(nameof(StartCutscene), 2f);
        }
    }

    public void StartCutscene()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
