using Rola.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class SwitchNode : Node
    {
        [SerializeField] private LogicInNode _in;
        [SerializeField] private SwitchType _switchType;
        [SerializeField] private LogicInNode _trigger;
        //no changing values
        [SerializeField] private NodeValue _requiredValue;
        [SerializeField] private LogicOutNode _out;

        public NodeValue RequiredValue => _requiredValue;
        public SwitchType SwitchType => _switchType;

        private void OnEnable()
        {
            if(_in != null)
                _in.OnInputUpdated += OnInputUpdated;

            if(_trigger != null)
                _in.OnInputUpdated += OnInputUpdated;
        }

        private void OnDisable()
        {
            if(_in != null)
                _in.OnInputUpdated -= OnInputUpdated;

            if(_trigger != null)
                _in.OnInputUpdated -= OnInputUpdated;
        }

        private void OnInputUpdated()
        {
            switch(_switchType)
            {
                case SwitchType.Bool:
                    if(_trigger.GetValue.IsOn)
                        _value = _in.GetValue;
                    else
                        _value.SetValue(Vector3.zero);
                    break;

                case SwitchType.Exact:
                    if(_trigger.GetValue.IsEqual(_requiredValue))
                        _value = _in.GetValue;
                    else
                        _value.SetValue(Vector3.zero);
                    break;
                    

                case SwitchType.ExactIgnoreZero:
                    if(_trigger.GetValue.IsEqual(_requiredValue, true))
                        _value = _in.GetValue;
                    else
                        _value.SetValue(Vector3.zero);
                    break;

                case SwitchType.Contains:
                    if(_trigger.GetValue.ContainsValue(_requiredValue))
                        _value = _in.GetValue;
                    else
                        _value.SetValue(Vector3.zero);
                    break;
            }

            _out.UpdateValue(_value);
        }

        public void SetSwitchType(int type) => _switchType = (SwitchType)type;

        private void OnMouseDown()
        {
            if(_locked)
                UIManager.Instance.EditSwitchLocked(this);
            else
                UIManager.Instance.EditSwitch(this);
        }
    }

    public enum SwitchType
    {
        Bool,
        Exact,
        ExactIgnoreZero,
        Contains,
    }
}
