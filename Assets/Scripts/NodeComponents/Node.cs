using Rola.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rola.Nodes
{
    public abstract class Node : MonoBehaviour
    {
        [SerializeField] protected bool _locked;

        protected NodeValue _value;

        public NodeValue GetValue => _value;
        public bool IsLocked => _locked;

        private void Awake()
        {
            if(_locked)
            {
                var temp = Instantiate(GameManager.GetLockPrefab, null);

                temp.transform.parent = transform;
                temp.transform.position = transform.position;

                temp.transform.LookAt(transform.position + new Vector3(0f, 0f, -100f));
            }
        }

        public virtual void UpdateValue(NodeValue newValue)
        {
            /*
            if(_updatedRecently)
                return;

            _updatedRecently = true;
            */

            _value = newValue;

            /*
            await Task.Delay(100);

            _updatedRecently = false;
            */
        }

        public void SetLock(bool _isLocked) => _locked = _isLocked;
    }

    [System.Serializable]
    public struct NodeValue
    {
        [Range(0f, 1f)]
        [SerializeField] private float _red;
        [Range(0f, 1f)]
        [SerializeField] private float _green;
        [Range(0f, 1f)]
        [SerializeField] private float _blue;

        public Vector3 Colour => new Vector3(_red, _green, _blue);
        public float Red => _red;
        public float Green => _green;
        public float Blue => _blue;
        public bool IsOn => _red != 0f || _green != 0f || _blue != 0f;

        public NodeValue(Vector3 colour)
        {
            _red = colour.x;
            _green = colour.y;
            _blue = colour.z;

            Round();
        }

        public NodeValue SetValue(bool value)
        {
            _red = _green = _blue = value ? 1f : 0f;

            return this;
        }

        public NodeValue SetValue(Vector3 rgb)
        {
            _red = rgb.x;
            _green = rgb.y;
            _blue = rgb.z;

            Round();
            return this;
        }

        public bool IsEqual(NodeValue other, bool ignoreOtherZero = false)
        {
            Round();

            if(ignoreOtherZero)
            {
                if(other.Red != 0f && _red != other.Red)
                    return false;

                if(other.Green != 0f && _green != other.Green)
                    return false;

                if(other.Blue != 0f && _blue != other.Blue)
                    return false;

                return true;
            }

            if(_red != other.Red)
                return false;

            if(_green != other.Green)
                return false;

            if(_blue != other.Blue)
                return false;

            return true;
        }

        public bool ContainsValue(NodeValue other)
        {
            Round();

            if(_red < other.Red)
                return false;

            if(_green < other.Green)
                return false;

            if(_blue < other.Blue)
                return false;

            return true;
        }

        public Vector3 Add(Vector3 other)
        {
            Round();

            return new Vector3(
                Mathf.Min(_red + other.x, 1f),
                Mathf.Min(_green + other.y, 1f),
                Mathf.Min(_blue + other.z, 1f));
        }

        private void Round()
        {
            _red = (float)Math.Round(_red, 1);
            _green = (float)Math.Round(_green, 1);
            _blue = (float)Math.Round(_blue, 1);
        }
    }
}
