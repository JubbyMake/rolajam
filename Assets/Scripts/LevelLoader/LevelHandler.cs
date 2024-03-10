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

        public async void BeginLevel()
        {
            var time = 0;

            await Task.Delay(1000);

            while(time < 2000)
            {
                time += 20;

                _blackscreen.color = new Color(
                    _blackscreen.color.r,
                    _blackscreen.color.g,
                    _blackscreen.color.b,
                    Mathf.Lerp(1f, 0f, time / 2000f));

                await Task.Delay(20);
            }
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

        public async void DecomissionLevel()
        {
            var time = 0;

            while(time < 1000)
            {
                time += 20;

                _blackscreen.color = new Color(
                    _blackscreen.color.r,
                    _blackscreen.color.g,
                    _blackscreen.color.b,
                    Mathf.Lerp(0f, 1f, time / 1000f));

                await Task.Delay(20);
            }

            Destroy(gameObject);
        }
    }
}
