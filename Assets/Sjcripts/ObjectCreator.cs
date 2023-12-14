using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.U2D;
using UnityEditor.Rendering;

public class ObjectCreator : MonoBehaviour
{
    public SpriteAtlas spriteAtlas;
    public GameObject prefab;

    // ������ �ʱ�ȭ�ϴ� Reset �Լ�
    private void Start()
    {
        Debug.Log(spriteAtlas.spriteCount);

        for (int i = 0; i < spriteAtlas.spriteCount; i++)
        {
            // ������Ʈ ����
            GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);

            // AssetDatabase�� ����Ͽ� ���������� ����
            string prefabPath = $"Assets/Prefabs/{$"TileSheet_{i}"}.prefab";
            PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);

            // AssetDatabase�� ����Ͽ� �����ѿ� ���� 
            GameObject prefabInstance = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            // ��������Ʈ �Ҵ�
            Sprite sprite = spriteAtlas.GetSprite($"TileSheet_{i}");
            prefabInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;

            // Save changes to the prefab asset
            PrefabUtility.SavePrefabAsset(prefabInstance);

            Destroy(newObject);
        }
    }
}
