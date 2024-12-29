using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject loadingBg;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 0.5f);
    }

    // Update is called once per frame
    private IEnumerator Load()
    {
        yield return new WaitForSeconds(3);
        Destroy(loadingBg);
        Destroy(gameObject);
    }
}
