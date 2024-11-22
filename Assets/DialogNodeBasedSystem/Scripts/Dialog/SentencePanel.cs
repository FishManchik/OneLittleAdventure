using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static cherrydev.Node;
using System;

namespace cherrydev
{
    public class SentencePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogNameText;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private RawImage dialogPlayerIcon;
        [SerializeField] private RawImage dialogNpcIcon;
        [SerializeField] private Node.IconType dialogCharacterIconType;
        private const string IsSpeaking = "IsSpeaking";

        /// <summary>
        /// Setting dialogText max visible characters to zero
        /// </summary>
        public void ResetDialogText()
        {
            dialogText.maxVisibleCharacters = 0;
        }

        /// <summary>
        /// Set dialog text max visible characters to dialog text length
        /// </summary>
        /// <param name="text"></param>
        public void ShowFullDialogText(string text)
        {
            dialogText.maxVisibleCharacters = text.Length;
        }

        /// <summary>
        /// Assigning dialog name text, character image sprite and dialog text
        /// </summary>
        /// <param name="name"></param>
        public void Setup(string name, string text, RenderTexture playerIcon, RenderTexture npcIcon,Node.IconType iconType)
        {
            dialogNameText.text = name;
            dialogText.text = text;

            dialogPlayerIcon.texture = playerIcon;
            dialogNpcIcon.texture = npcIcon;
            dialogCharacterIconType = iconType;

            dialogPlayerIcon.rectTransform.DOScale(new Vector3(-2f, 2f, 2f), 0.5f).SetEase(Ease.OutBack);
            dialogNpcIcon.rectTransform.DOScale(new Vector3(2f, 2f, 2f), 0.5f).SetEase(Ease.OutBack);

            switch (iconType)
            {
                case IconType.PlayerIcon:
                    dialogPlayerIcon.rectTransform.DOScale(new Vector3(-3.5f, 3.5f, 3.5f), 0.5f).SetEase(Ease.OutBack);
                    FindIconObject(dialogPlayerIcon.texture).SetTrigger(IsSpeaking);
                    break;
                case IconType.NpcIcon:
                    dialogNpcIcon.rectTransform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), 0.5f).SetEase(Ease.OutBack);
                    FindIconObject(dialogNpcIcon.texture).SetTrigger(IsSpeaking);
                    break;
            }
        }

        private Animator FindIconObject(Texture icon)
        {
            foreach (Camera renderingCamera in Camera.allCameras)
            {
                if (renderingCamera.targetTexture == icon)
                {
                    return renderingCamera.transform.GetChild(0).GetComponent<Animator>();
                }
            }

            return null;
        }

        /// <summary>
        /// Increasing max visible characters
        /// </summary>
        public void IncreaseMaxVisibleCharacters()
        {
            dialogText.maxVisibleCharacters++;
        }
    }
}