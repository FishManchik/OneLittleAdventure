using UnityEngine;
using UnityEngine.UI;
using cherrydev;

public class TestDialogStarter : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private Text dialogError;

    private void Start()
    {
        dialogBehaviour.BindExternalFunction("Test", DebugExternal);
        dialogBehaviour.OnSentenceNodeActiveWithParameter += WriteError;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBehaviour.StartDialog(dialogGraph);
        }
    }

    private void WriteError(string s, string s2, RenderTexture t, RenderTexture t2, Node.IconType iconType)
    {
        dialogError.text = "Ёмм тип ну кароч - " + iconType.ToString();
    }

    private void DebugExternal()
    {
        Debug.Log("External function works!");
    }
}