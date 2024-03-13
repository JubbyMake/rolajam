using Rola.Levels;
using Rola.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public sealed class LevelSelectorMenu : MonoBehaviour
    {
        [SerializeField] private RawImage _image;

        private void OnEnable() => StartCoroutine(DoThing());

        public void OnLevelSelected(int level)
        {
            GameManager.Instance.LoadLevel(level);

            UIManager.Instance.DisableAll();
            UIManager.DoButtonSound();
        }

        private IEnumerator DoThing()
        {
            while(gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(2f);

                if(_image.color.a < 0.005)
                    yield break;

                _image.color -= new Color(0f, 0f, 0f, 0.004f);
                _image.rectTransform.localPosition += new Vector3(0f, 0.05f, 0f);
            }
        }
    }
}
