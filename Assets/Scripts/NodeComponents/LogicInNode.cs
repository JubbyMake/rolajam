using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    public sealed class LogicInNode : Node
    {
        public event Action OnInputUpdated;

        public static bool WaitingInput = false;
        public static LogicInNode Current = null;

        public bool IsConnected { get; private set; }

        public override void UpdateValue(NodeValue newValue)
        {
            base.UpdateValue(newValue);

            OnInputUpdated?.Invoke();
        }

        private void OnMouseDown()
        {
            if(IsConnected || WaitingInput)
                return;

            WaitingInput = true;
            Current = this;

            if(LogicOutNode.WaitingInput)
            {
                LogicOutNode.Current.CreateWire(this);

                WaitingInput = false;
                Current = null;

                LogicOutNode.WaitingInput = false;
                LogicOutNode.Current = null;
            }
        }

        public void SetConnected(bool connected) => IsConnected = connected;
    }
}
