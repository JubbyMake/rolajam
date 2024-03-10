using Rola.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    public sealed class LogicOutNode : Node
    {
        [SerializeField] private Wire _outWire;

        public static bool WaitingInput = false;
        public static LogicOutNode Current = null;

        private void Awake()
        {
            if(_outWire != null)
                _outWire.Init(transform.position);
        }

        public override void UpdateValue(NodeValue newValue)
        {
            base.UpdateValue(newValue);

            if(_outWire != null)
                _outWire.UpdateValue(newValue);
        }

        private void OnMouseDown()
        {
            if(_outWire != null || WaitingInput)
                return;

            WaitingInput = true;
            Current = this;

            if(LogicInNode.WaitingInput)
            {
                CreateWire(LogicInNode.Current);

                WaitingInput = false;
                Current = null;

                LogicInNode.WaitingInput = false;
                LogicInNode.Current = null;
            }
        }

        public void CreateWire(LogicInNode next)
        {
            var temp = Instantiate(GameManager.Instance.GetWirePrefab,
                transform)
                .GetComponent<Wire>();

            temp.Init(next, transform.position, next.transform.position);

            _outWire = temp;
            _outWire.UpdateValue(_value);
        }
    }
}
