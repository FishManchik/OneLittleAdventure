using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public struct Sentence
    {
        public string characterName;
        public string text;
        public RenderTexture playerIcon;
        public RenderTexture npcIcon;
        public Node.IconType iconType;

        public Sentence(string characterName, string text, RenderTexture npcIcon, RenderTexture playerIcon, Node.IconType iconType)
        {
            this.playerIcon = playerIcon;
            this.npcIcon = npcIcon;
            this.iconType = iconType;
            this.characterName = characterName;
            this.text = text;
        }
    }
}