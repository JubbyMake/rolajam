using Rola.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class ValueInNode : Node
    {
        [SerializeField] private LogicInNode _in;
        [SerializeField] private SwitchType _type;
        [SerializeField] private NodeValue _requiredValue;

        public SwitchType Type => _type;
        public NodeValue RequiredValue => _requiredValue;

        public bool IsCorrect
        {
            get
            {
                if(_in == null)
                    return false;

                switch(_type)
                {
                    case SwitchType.Bool:
                        if(_in.GetValue.IsOn)
                        {
                            UpdateValue(_in.GetValue);
                            return true;
                        }
                        else return false;

                    case SwitchType.Exact:
                        if(_in.GetValue.IsEqual(_requiredValue))
                        {
                            UpdateValue(_in.GetValue);
                            return true;
                        }
                        else return false;

                    case SwitchType.ExactIgnoreZero:
                        if(_in.GetValue.IsEqual(_requiredValue, true))
                        {
                            UpdateValue(_in.GetValue);
                            return true;
                        }
                        else
                            return false;

                    case SwitchType.Contains:
                        if(_in.GetValue.ContainsValue(_requiredValue))
                        {
                            UpdateValue(_in.GetValue);
                            return true;
                        }
                        else
                            return false;
                }

                //this never happens
                return false;
            }
        }

        private void OnMouseDown() => UIManager.Instance.EditValueIn(this);
    }
}
