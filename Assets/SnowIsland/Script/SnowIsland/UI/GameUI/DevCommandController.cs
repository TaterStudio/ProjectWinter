using System;
using HeathenEngineering.Events;
using HeathenEngineering.UX;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    public class DevCommandController: MonoBehaviour
    {
        [SerializeField]
        private UIDocument m_UIDocument;

        [SerializeField]
        private CommandDirector m_CommandDirector;

        private VisualElement m_Root;
        private TextField m_TextField;

        private void Awake()
        {
            m_Root = m_UIDocument.rootVisualElement;
            m_TextField = m_Root.Q<TextField>();
            m_TextField.RegisterValueChangedCallback(OnSubmit);
        }

        private void OnSubmit(UnityEngine.UIElements.ChangeEvent<string> evt)
        {
            m_TextField.SetValueWithoutNotify(string.Empty);
            m_CommandDirector.ParseInput(evt.newValue);
        }

        public void CommandRaised(CommandData data)
        {
            if (!data.isValid)
            {
                return;
            }

            Debug.Log("Detected event: '" + data.command.name + "' with the arguments of '" + data.arguments + "'");

            switch (data.command)
            {
                case StringGameEvent stringGameEvent:
                    stringGameEvent.Invoke(data.arguments);
                    break;
                case PlayerAnimationEvent playerAnimationEvent:
                    var arg = data.arguments.Split(' ');
                    if (!float.TryParse(arg[1], out var speed))
                    {
                        Debug.LogError("第二个参数不为float");
                        break;
                    }
                    playerAnimationEvent.Invoke(new PlayerAnimationEventData()
                    {
                        State = arg[0],
                        Parameter1 = speed
                    });
                    break;
                default:
                    data.command.Invoke();
                    break;
            }
            // if (data.command.GetType() != typeof(StringGameEvent))
            // {
            //     //Example of invoking the command without an argument
            //     data.command.Invoke(this);
            // }
            // else
            // {
            //     //Example of invoking the command with an argument
            //     ((StringGameEvent)data.command).Invoke(this, data.arguments);
            // }
        }
    }
}