using Rola.Nodes;
using Rola.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

        [SerializeField]
        private LevelHandler _loadedLevel;
        private int _currentLevel = 1;
        private List<ValueOutNode> _registeredNodes = new();

        [Header("I AM TESTINTESTING")]
        [SerializeField] private int STARTLEVELTESTTEST;

        public static GameManager Instance { get; private set; }
        public GameObject GetWirePrefab => _wirePrefab;
        public GameObject GetLockPrefab => _lockPrefab;

        private async void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(Instance);

            Instance = this;

            await Task.Delay(100);

            UIManager.Instance.OpenLevelMenu();
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
                if(!UIManager.Instance.OnEscPressed())
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

        public async void LoadLevel(int level)
        {
            if(_loadedLevel != null)
                _loadedLevel.DecomissionLevel();

            //unlucky
            while(_loadedLevel != null)
                await Task.Delay(10);

            try
            {
                _loadedLevel = Instantiate(_levels[level - 1]);
                _loadedLevel.BeginLevel();
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
            if(_loadedLevel == null)
                return;

            foreach(var temp in _registeredNodes)
                temp.StartUpdateChain();

            if(_loadedLevel.EvaluateLevel())
                CurrentLevelComplete();
        }

        public void EndGame()
        {
            if(_loadedLevel != null)
                _loadedLevel.DecomissionLevel();

            UIManager.Instance.OpenLevelMenu();
        }

        private void CurrentLevelComplete()
        {
            Debug.Log("LEVEL WIN");

            _loadedLevel.DecomissionLevel();

            /*
            if(_currentLevel >= _levelsCompleted)
                _levelsCompleted++;
            */

            LoadLevel(++_currentLevel);

            UIManager.Instance.DisableAll();
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
