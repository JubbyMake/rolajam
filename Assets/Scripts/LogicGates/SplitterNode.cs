using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class SplitterNode : Node
    {
        [SerializeField] private LogicInNode _in;
        [SerializeField] private LogicOutNode _out1;
        [SerializeField] private LogicOutNode _out2;

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
            _value = _in.GetValue;

            _out1.UpdateValue(_value);
            _out2.UpdateValue(_value);
        }
    }
}
