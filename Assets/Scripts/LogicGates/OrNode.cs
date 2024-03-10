using Rola.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class OrNode : Node
    {
        [SerializeField] private LogicInNode _in1;
        [SerializeField] private LogicInNode _in2;
        [SerializeField] private bool _isLogical;
        [SerializeField] private bool _exclusive;
        [SerializeField] private LogicOutNode _out;

        public bool IsLogical => _isLogical;
        public bool IsExclusive => _exclusive;

        private void OnEnable()
        {
            if(_in1 != null)
                _in1.OnInputUpdated += OnInputUpdated;

            if(_in2 != null)
                _in2.OnInputUpdated += OnInputUpdated;
        }

        private void OnDisable()
        {
            if(_in1 != null)
                _in1.OnInputUpdated -= OnInputUpdated;

            if(_in2 != null)
                _in2.OnInputUpdated -= OnInputUpdated;
        }

        private void OnInputUpdated()
        {
            if(_exclusive && _in1.GetValue.IsOn && _in2.GetValue.IsOn)
                _value.SetValue(false);
            else
            {
                if(_isLogical)
                    _value.SetValue(_in1.GetValue.IsOn || _in2.GetValue.IsOn);
                else
                    _value.SetValue(_in1.GetValue.Add(_in2.GetValue.Colour));
            }

            _out.UpdateValue(_value);
        }

        public void SetLogical(bool isLogical) => _isLogical = isLogical;

        public void SetExclusive(bool isExclusive) => _exclusive = isExclusive;

        private void OnMouseDown()
        {
            if(_locked)
                UIManager.Instance.EditOrNodeLocked(this);
            else
                UIManager.Instance.EditOrNode(this);
        }
    }
}
