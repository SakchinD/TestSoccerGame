using System;
using Zenject;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolController : MonoBehaviour
{
    [Serializable]
    public struct KeyValue
    {
        public string ObjectName;
        public Ball Object;
    }
    [SerializeField] private List<KeyValue> _objects = new();

    private Dictionary<string, List<Ball>> _poolsDictionary = new();
    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer di)
    {
        _diContainer = di;
    }

    public Ball GetPooledObject(string ObjectName)
    {
        if (_poolsDictionary.TryGetValue(ObjectName, out var objects)
            && objects.Any(x => !x.gameObject.activeInHierarchy))
        {
            return objects.First(x => !x.gameObject.activeInHierarchy);
        }

        return CreateObject(ObjectName);
    }

    private Ball CreateObject(string objectName)
    {
        var obj = _objects
            .FirstOrDefault(item => item.ObjectName == objectName);

        if (obj.Object)
        {
            if (!_poolsDictionary.ContainsKey(objectName))
            {
                _poolsDictionary.Add(objectName, new List<Ball>());
            }
            var pooledObject = _diContainer
                .InstantiatePrefabForComponent<Ball>(obj.Object);
            pooledObject.transform.SetParent(transform);
            pooledObject.gameObject.SetActive(false);
            _poolsDictionary[objectName].Add(pooledObject);
            return pooledObject;
        }
        
        Debug.LogError($"Don't find object with name: {objectName}");
        return null;
    }
}