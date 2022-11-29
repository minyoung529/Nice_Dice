using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] flowerPrefabs;
    [SerializeField] private int flowerCount = 0;
    readonly float size = 8f;

    void Start()
    {
        for (int i = 0; i < flowerCount; i++)
        {
            float x = Random.Range(2f, size);
            float z = Random.Range(-size, size);

            Ray ray = new Ray(new Vector3(x, 5f, z), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitinfo, 10f, LayerMask.GetMask("Bottom")))
            {
                GameObject prefab = flowerPrefabs[Random.Range(0, flowerPrefabs.Length)];
                Vector3 pos = new Vector3(x, hitinfo.point.y, z);
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, hitinfo.normal);

                Vector3 size = prefab.transform.localScale;
                size.x /= transform.parent.localScale.x;
                size.y /= transform.parent.localScale.y;
                size.z /= transform.parent.localScale.z;

                Instantiate(prefab, pos, rot, transform.parent).transform.localScale = size;
            }
            else i--;
        }
    }
}
