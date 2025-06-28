using System;
using HeathenEngineering.Events;
using HeathenEngineering.UX;
using TaterGame.SnowIsland.Shared;
using UniRx;
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
        private FixedSizeQueue<string> m_FixedSizeQueue;
        private TextField m_ConsoleInput;
        private Label m_ConsoleOutput;
        private EventCenter m_EventCenter;
        private ClientState m_ClientState;
        private void Awake()
        {
            m_ClientState = ApplicationScope.Resolve<ClientState>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_FixedSizeQueue = new FixedSizeQueue<string>(11);
            m_Root = m_UIDocument.rootVisualElement;
            m_ConsoleInput = m_Root.Q<TextField>();
            m_ConsoleInput.RegisterValueChangedCallback(OnSubmit);
            m_ConsoleOutput = m_Root.Q<Label>();
            m_EventCenter.Log2Console.Subscribe(Log2Console).AddTo(this);
        }

        private void Log2Console(string str)
        {
            m_FixedSizeQueue.Enqueue(str);
            m_ConsoleOutput.text = string.Join("\n", m_FixedSizeQueue);
        }

        private void OnSubmit(UnityEngine.UIElements.ChangeEvent<string> evt)
        {
            m_ConsoleInput.SetValueWithoutNotify(string.Empty);
            m_EventCenter.Log2Console.OnNext($"{m_ClientState.LocalPlayer.name}: "+evt.newValue);
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
        }
    }
}