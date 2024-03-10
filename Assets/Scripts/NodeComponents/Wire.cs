using Rola.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rola.Nodes
{
    [RequireComponent(typeof(NodeShaderHandler))]
    public sealed class Wire : Node
    {
        [SerializeField] private LogicInNode _nextNode;
        [SerializeField] private Transform _plugStart;
        [SerializeField] private Transform _plugEnd;
        [SerializeField] private Transform _wireStart;
        [SerializeField] private Transform _wireEnd;

        //stops infite loop when updating
        private bool _updatedRecently;
        private bool _animating = false;

        private void Awake()
        {
            if(_locked)
            {
                var temp = Instantiate(GameManager.Instance.GetLockPrefab, transform);
                temp.transform.position += Vector3.up * 2f;
            }

            if(_nextNode != null && _nextNode.IsConnected)
            {
                RemoveWire();
                return;
            }

            if(_nextNode != null)
                _nextNode.SetConnected(true);
        }

        public void Init(LogicInNode next, Vector3 pos1, Vector3 pos2)
        {
            _nextNode = next;
            _nextNode.SetConnected(true);

            transform.position = Vector3.Lerp(pos1, pos2, 0.5f);
            transform.LookAt(pos2);

            _plugStart.position = pos1;
            _plugEnd.position = pos2;
            _plugStart.LookAt(pos2);
            _plugEnd.LookAt(pos1);


            _wireStart.position = pos1;
            _wireStart.LookAt(pos2);
            _wireEnd.SetPositionAndRotation(pos2, _wireStart.rotation);

            AnimateIn();
        }

        public void Init(Vector3 pos1)
        {
            transform.position = Vector3.Lerp(pos1, _nextNode.transform.position, 0.5f);
            transform.LookAt(_nextNode.transform.position);

            _plugStart.position = pos1;
            _plugEnd.position = _nextNode.transform.position;
            _plugStart.LookAt(_nextNode.transform.position);
            _plugEnd.LookAt(pos1);

            _wireStart.position = pos1;
            _wireStart.LookAt(_nextNode.transform.position);
            _wireEnd.SetPositionAndRotation(_nextNode.transform.position,
                _wireStart.rotation);

            AnimateIn();
        }

        public override async void UpdateValue(NodeValue newValue)
        {
            if(_updatedRecently)
                return;

            _updatedRecently = true;

            base.UpdateValue(newValue);

            if(_nextNode != null)
                _nextNode.UpdateValue(newValue);

            await Task.Delay(100);

            _updatedRecently = false;
        }

        public void RemoveWire()
        {
            if(_locked)
                return;

            if(_nextNode != null)
            {
                _nextNode.UpdateValue(_value.SetValue(false));
                _nextNode.SetConnected(false);
            }

            Destroy(gameObject);
        }

        private void OnMouseDown() => RemoveWire();

        //how is it so bad
        private async void AnimateIn()
        {
            _animating = true;

            _ = WaitCallback(200, () => _animating = false);

            transform.position = transform.position + new Vector3(0f, 2f, 0f);

            while(_animating && gameObject != null)
            {
                await Task.Delay(20);

                transform.position = transform.position - new Vector3(0f, 0.2f, 0f);
            }
        }

        private async Task WaitCallback(int delayms, Action callback)
        {
            await Task.Delay(delayms);

            callback.Invoke();
        }
    }
}
