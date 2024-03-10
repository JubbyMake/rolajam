using Rola.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.Levels
{
    public sealed class LevelHandler : MonoBehaviour
    {
        [SerializeField] private RawImage _blackscreen;

        private LevelWinCondition[] _winConditions;

        private void Awake() =>
            _winConditions = GetComponentsInChildren<LevelWinCondition>();

        public IEnumerator BeginLevel(Action callback)
        {
            var time = 0f;

            yield return new WaitForSeconds(1f);

            while(time < 2f)
            {
                time += Time.deltaTime;

                _blackscreen.color = new Color(
                    _blackscreen.color.r,
                    _blackscreen.color.g,
                    _blackscreen.color.b,
                    Mathf.Lerp(1f, 0f, time / 2f));

                yield return null;
            }

            callback.Invoke();
        }

        public bool EvaluateLevel()
        {
            foreach(var temp in _winConditions)
            {
                if(!temp.Evaluate())
                    return false;
            }

            return true;
        }

        public IEnumerator DecomissionLevel(Action callback)
        {
            LogicInNode.WaitingInput = false;
            LogicOutNode.WaitingInput = false;

            var time = 0f;

            while(time < 1)
            {
                time += Time.deltaTime;

                _blackscreen.color = new Color(
                    _blackscreen.color.r,
                    _blackscreen.color.g,
                    _blackscreen.color.b,
                    Mathf.Lerp(0f, 1f, time / 1f));

                yield return null;
            }

            callback.Invoke();

            yield return null;

            Destroy(gameObject);
        }
    }
}
