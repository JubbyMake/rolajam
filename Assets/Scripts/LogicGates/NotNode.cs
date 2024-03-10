using Rola.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class NotNode : Node
    {
        [SerializeField] private LogicInNode _in;
        [SerializeField] private NotType _type;
        [SerializeField] private LogicOutNode _out;

        public NotType Type => _type;

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
            switch(_type)
            {
                case NotType.Bool:
                    _value.SetValue(!_in.GetValue.IsOn);
                    break;

                case NotType.Invert:
                    _value.SetValue(Vector3.one - _in.GetValue.Colour);
                    break;

                case NotType.InvertIgnoreZero:
                    _value.SetValue(new Vector3(
                        _in.GetValue.Red == 0f ? 0f : 1f - _in.GetValue.Red,
                        _in.GetValue.Green == 0f ? 0f : 1f - _in.GetValue.Green,
                        _in.GetValue.Blue == 0f ? 0f : 1f - _in.GetValue.Blue));
                    break;
            }

            _out.UpdateValue(_value);
        }

        public void SetNotType(int type) => _type = (NotType)type;

        private void OnMouseDown()
        {
            if(_locked)
                UIManager.Instance.EditNotNodeLocked(this);
            else
                UIManager.Instance.EditNotNode(this);
        }
    }

    public enum NotType
    {
        Bool,
        Invert,
        InvertIgnoreZero,
    }
}
