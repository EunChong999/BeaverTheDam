using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private float initTime;
    private WaitForSeconds waitForInit;

    // 풀 클래스
    [System.Serializable]
    public class Pool
    {
        // 태그
        public string tag;

        // 프리펩
        public GameObject prefeb;

        // 크기
        public int size;
    }

    #region singleton

    // 클래스의 인스턴스
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // 풀 리스트
    [SerializeField]
    private List<Pool> pools;

    // 이름과 큐가 쌍을 이루는 딕셔너리 
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        waitForInit = new WaitForSeconds(initTime);

        // 풀 초기화
        StartCoroutine(InitPool());
    }

    IEnumerator InitPool()
    {
        yield return waitForInit;

        // 풀 딕셔너리에 인스턴스 생성
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
            pool.size = BuildingManager.instance.buildingCount;

        // 풀 리스트 안에 있는 풀 꺼내기
        foreach (Pool pool in pools)
        {
            // 오브젝트 풀에 인스턴스 생성
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // 프레펩 생성 반복
            for (int i = 0; i < pool.size; i++)
            {
                // 생성된 객체 저장
                GameObject obj = Instantiate(pool.prefeb);

                // 객체 비활성화
                obj.SetActive(false);

                // 오브젝트 풀에 객체 저장
                objectPool.Enqueue(obj);
            }

            // 풀 딕셔너리에 태그와 함께 오브젝트 풀 저장
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // 풀 딕셔너리에 해당 태그가 없는 경우
        if (!poolDictionary.ContainsKey(tag))
        {
            // 경고 메시지 출력
            //Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");

            // 널값 반환
            return null;
        }

        // 생성 오브젝트에 풀 저장
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // 생성 오브젝트 활성화
        objectToSpawn.SetActive(true);

        // 생성 오브젝트 위치 설정
        objectToSpawn.transform.position = position;

        // 생성 오브젝트 회전 설정
        objectToSpawn.transform.rotation = rotation;

        // 생성 오브젝트의 함수 참조 및 저장
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        // 풀링된 오브젝트에 대한 함수가 할당되었을 경우
        //if (pooledObj != null)
        //{
        //    pooledObj.OnObjectSpawn();
        //}

        // 생성 오브젝트를 풀 딕셔너리에 저장
        poolDictionary[tag].Enqueue(objectToSpawn);

        // 생성 오브젝트 반환
        return objectToSpawn;
    }
}
