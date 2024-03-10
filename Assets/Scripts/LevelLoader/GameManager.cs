using Rola.Nodes;
using Rola.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rola.Levels
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelHandler[] _levels;
        //to save
        //no saving for jam(it doesnt do anything)
        [SerializeField] private int _levelsCompleted;

        [Header("Globals")]
        [SerializeField] private GameObject _wirePrefab;
        [SerializeField] private GameObject _lockPrefab;
        [SerializeField] private Volume _winVolume;

        private LevelHandler _loadedLevel;
        private int _currentLevel = 1;
        private List<ValueOutNode> _registeredNodes = new();
        private bool _canUnload;

        [Header("I AM TESTINTESTING")]
        [SerializeField] private int STARTLEVELTESTTEST;

        public static GameManager Instance { get; private set; }
        public GameObject GetWirePrefab => _wirePrefab;
        public GameObject GetLockPrefab => _lockPrefab;

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(Instance);

            Instance = this;
            _winVolume.weight = 0f;
            _canUnload = true;
        }

        //unity is trash
        /*
        private void OnEnable()
        {
            if(Instance == null)
                Instance = this;
        }

        private void Start()
        {
            if(Instance == null)
                Instance = this;
        }
        */

        [ContextMenu("LOAD GUY")]
        public void DOTHINGTEST() => LoadLevel(STARTLEVELTESTTEST);

        //the strongest input system
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                LoadLevel(_currentLevel);

            if(Input.GetKeyDown(KeyCode.Return) && _loadedLevel != null)
                EvaluateCurrentLevel();

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(UIManager.Instance.OnEscPressed())
                    return;

                if(_canUnload)
                    EndGame();
            }

#if UNITY_EDITOR
            return;
#endif

            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                EndGame();
                Application.Quit();
            }
        }

        public void LoadLevel(int level)
        {
            if(!_canUnload)
                return;

            _canUnload = false;

            if(_loadedLevel != null)
            {
                StartCoroutine(_loadedLevel.DecomissionLevel(() =>
                {
                    try
                    {
                        _loadedLevel = Instantiate(_levels[level - 1]);
                        StartCoroutine(_loadedLevel.BeginLevel(() => _canUnload = true));
                        _currentLevel = level;
                    }
                    catch
                    {
                        EndGame();
                        return;
                    }
                }));
                return;
            }

            try
            {
                _loadedLevel = Instantiate(_levels[level - 1]);
                StartCoroutine(_loadedLevel.BeginLevel(() => _canUnload = true));
                _currentLevel = level;
            }
            catch
            {
                EndGame();
                return;
            }
        }

        public void EvaluateCurrentLevel()
        {
            if(_loadedLevel == null || !_canUnload)
                return;

            foreach(var temp in _registeredNodes)
                temp.StartUpdateChain();

            if(_loadedLevel.EvaluateLevel())
                StartCoroutine(CurrentLevelComplete());
        }

        public void EndGame()
        {
            if(!_canUnload)
                return;

            UIManager.Instance.DisableAll();

            if(_loadedLevel != null)
                StartCoroutine(_loadedLevel.DecomissionLevel(() =>
                {
                    _canUnload = true;
                    _loadedLevel = null;
                    UIManager.Instance.OpenLevelMenu();
                }));
        }

        private IEnumerator CurrentLevelComplete()
        {
            Debug.Log("LEVEL WIN");
            UIManager.Instance.DisableAll();
            _canUnload = false;

            var time = 0f;

            while(time < 1)
            {
                time += Time.deltaTime;

                _winVolume.weight = Mathf.Lerp(0f, 1f, time / 1f);

                yield return null;
            }

            time = 0;

            while(time < 1f)
            {
                time += Time.deltaTime;

                _winVolume.weight = Mathf.Lerp(1f, 0f, time / 1f);

                yield return null;
            }

            StartCoroutine(_loadedLevel.DecomissionLevel(() =>
            {
                _canUnload = true;
                _loadedLevel = null;
                LoadLevel(++_currentLevel);
            }));
            /*
            if(_currentLevel >= _levelsCompleted)
            _levelsCompleted++;
            */
        }

        public void RegisterNode(ValueOutNode node)
        {
            if(!_registeredNodes.Contains(node))
                _registeredNodes.Add(node);
        }

        public void UnegisterNode(ValueOutNode node)
        {
            if(_registeredNodes.Contains(node))
                _registeredNodes.Remove(node);
        }
    }
}
