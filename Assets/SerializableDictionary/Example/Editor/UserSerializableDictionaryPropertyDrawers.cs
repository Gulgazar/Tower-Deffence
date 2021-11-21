using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TowerDeffence;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(EnemyTypeControllerDictionary))]
[CustomPropertyDrawer(typeof(TowerTypePrefabDictionary))]
[CustomPropertyDrawer(typeof(ProjectileTypeControllerDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
