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

                _text.color -= new Color(0f, 0f, 0f, 0.01f);
                _backdrop.color -= new Color(0f, 0f, 0f, 0.01f);

                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
