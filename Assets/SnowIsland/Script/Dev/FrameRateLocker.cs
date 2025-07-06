using UnityEngine;
using VContainer.Unity;

namespace TaterGame.SnowIsland.Dev
{
    public class FrameRateLocker: IInitializable
    {
        public void Initialize()
        {
            Application.targetFrameRate = 30;
        }
    }
}