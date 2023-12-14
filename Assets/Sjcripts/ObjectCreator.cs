using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.U2D;
using UnityEditor.Rendering;

public class ObjectCreator : MonoBehaviour
{
    public SpriteAtlas spriteAtlas;
    public GameObject prefab;

    // 변수를 초기화하는 Reset 함수
    private void Start()
    {
        Debug.Log(spriteAtlas.spriteCount);

        for (int i = 0; i < spriteAtlas.spriteCount; i++)
        {
            // 오브젝트 생성
            GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);

            // AssetDatabase를 사용하여 프리팹으로 저장
            string prefabPath = $"Assets/Prefabs/{$"TileSheet_{i}"}.prefab";
            PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);

            // AssetDatabase를 사용하여 프리팩에 접근 
            GameObject prefabInstance = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            // 스프라이트 할당
            Sprite sprite = spriteAtlas.GetSprite($"TileSheet_{i}");
            prefabInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;

            // Save changes to the prefab asset
            PrefabUtility.SavePrefabAsset(prefabInstance);

            Destroy(newObject);
        }
    }
}
