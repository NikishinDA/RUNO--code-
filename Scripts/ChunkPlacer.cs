using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Chunk _chunkPrefab;
    [SerializeField] private Chunk[] _firstPrefab;
    [SerializeField] private Chunk _finalPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _chunkNumber;
    [SerializeField] private int _levelLength;
    //[SerializeField] private int _levelLengthMultiplier;
    //[SerializeField] private int _levelSpeedMultiplier;

    private List<Chunk> _spawnedChunks;
    private bool _finishSpawned = false;
    private int _currentLength = 4;

    private void Awake()
    {
        _spawnedChunks = new List<Chunk>();
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }
    private void Start()
    {
        //_levelLength += _levelLengthMultiplier * PlayerPrefs.GetInt("level");
        _currentLength = _firstPrefab.Length;
        foreach (Chunk ch in _firstPrefab)
        {
            _spawnedChunks.Add(ch);
        }
    }

    private void Update()
    {
        if ((!_finishSpawned) && (_playerTransform.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - _spawnDistance))
        {
            SpawnChunk();
        }

    }
    private void SpawnChunk()
    {
        Chunk newChunk;
        if (_currentLength < _levelLength)
        {
            newChunk = Instantiate(_chunkPrefab);
        }
        else
        {
            newChunk = Instantiate(_finalPrefab);
            _finishSpawned = true;
        }
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        _spawnedChunks.Add(newChunk);
        _currentLength++;
        if (_spawnedChunks.Count > _chunkNumber)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }
    private void OnGameStart(GameStartEvent obj)
    {
        //_levelLength = obj.LevelSetLength;
    }
}
