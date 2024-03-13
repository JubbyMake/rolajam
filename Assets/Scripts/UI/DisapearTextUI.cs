using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public sealed class DisapearTextUI : MonoBehaviour
    {
        [SerializeField] private float _displayTimeS;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RawImage _backdrop;

        private void OnEnable() => StartCoroutine(DoOnEnable());

        private IEnumerator DoOnEnable()
        {
            yield return new WaitForSeconds(_displayTimeS);

            var time = 0f;

            while(time < 1f && gameObject != null)
            {
                time += Time.deltaTime;

                _text.color = new Color(_text.color.r,
                    _text.color.g,
                    _text.color.b,
                    Mathf.Lerp(1f, 0f, time / 1f));

                _backdrop.color = new Color(_backdrop.color.r,
                    _backdrop.color.g,
                    _backdrop.color.b,
                    Mathf.Lerp(1f, 0f, time / 1f));

                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
