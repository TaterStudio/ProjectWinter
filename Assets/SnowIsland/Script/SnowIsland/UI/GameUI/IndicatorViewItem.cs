using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    [UxmlElement]
    public partial class IndicatorViewItem: VisualElement
    {
        public IndicatorViewItem()
        {
            this.style.width = new StyleLength(80);
            this.style.height = new StyleLength(80);
            this.style.position = Position.Absolute;
        }

        public void SetPosition(float x,float y)
        {
            this.style.left = x;
            this.style.top = y;
        }

        public void SetImage(Sprite sprite)
        {
            style.backgroundImage = new StyleBackground(sprite);
        }
    }
}