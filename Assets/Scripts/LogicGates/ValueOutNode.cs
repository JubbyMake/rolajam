using Rola.Levels;
using Rola.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class ValueOutNode : Node
    {
        [SerializeField] private LogicOutNode _out;
        [SerializeField] private NodeValue _default;

        [Header("TESTINGTESTINGTESTING")]
        [SerializeField] private NodeValue TESTCHANGER;

        private void Awake()
        {
            base.UpdateValue(_default);

            if(_locked)
            {
                var temp = Instantiate(GameManager.GetLockPrefab, null);

                temp.transform.parent = transform;
                temp.transform.position = transform.position;

                temp.transform.LookAt(transform.position + new Vector3(0f, 0f, -100f));
            }
        }

        private void OnEnable() => GameManager.Instance.RegisterNode(this);

        private void OnDisable() => GameManager.Instance.UnegisterNode(this);

        public override void UpdateValue(NodeValue newValue)
        {
            base.UpdateValue(newValue);

            if(_out != null)
                _out.UpdateValue(_value);
        }

        public void StartUpdateChain()
        {
            /*
#if UNITY_EDITOR
            _value = TESTCHANGER;
#endif
            */
            _out.UpdateValue(_value);
        }

        public void SetValue(Vector3 value) => _value.SetValue(value);

        private void OnMouseDown()
        {
            if(_locked)
                UIManager.Instance.EditValueOutLocked(this);
            else
                UIManager.Instance.EditValueOut(this);
        }
    }
}
