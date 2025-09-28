using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public List<GameObject> drop = new List<GameObject>();
    public int dropCount = 3;
    
    //选择随机掉落的序号
    private GameObject GetRandomDrop() {
        int random = Random.Range(0, drop.Count);
        return drop[random];
    }
    //生成掉落物
    public void DropOccur(Vector3 position) {
        for (int i = 0; i < dropCount; i++) {
            GameObject dropIndex = GetRandomDrop();
            if (dropIndex != null) {
                Vector3 dropPos = position+new Vector3(Random.Range(-1f,1f),0.5f,Random.Range(-1f,1f));
                Instantiate(dropIndex,dropPos,Quaternion.identity);
            }
        }
    }
}
