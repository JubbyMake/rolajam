using Rola.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class ValueAbsorbNode : Node
    {
        [SerializeField] private LogicInNode _in;
        [SerializeField] private NodeValue _toAbsorb;
        [SerializeField] private LogicOutNode _out;

        public NodeValue ToAbsorb => _toAbsorb;

        public bool IsCorrect => _in.GetValue.ContainsValue(_toAbsorb);

        private void OnEnable()
        {
            if(_in != null)
                _in.OnInputUpdated += OnInputUpdated;
        }

        private void OnDisable()
        {
            if(_in != null)
                _in.OnInputUpdated -= OnInputUpdated;
        }

        private void OnInputUpdated()
        {
            //what am i looking at
            var outValue = new NodeValue(Vector3.zero);

            if(_in.GetValue.ContainsValue(_toAbsorb))
            {
                //imagine extensions

                outValue.SetValue(new Vector3(
                    _in.GetValue.Red - _toAbsorb.Red,
                    _in.GetValue.Green - _toAbsorb.Green,
                    _in.GetValue.Blue - _toAbsorb.Blue));

                _value.SetValue(_toAbsorb.Colour);
            }
            else
                _value.SetValue(Vector3.zero);

            _out.UpdateValue(outValue);
        }

        private void OnMouseDown() => UIManager.Instance.EditAbsorb(this);
    }
}
