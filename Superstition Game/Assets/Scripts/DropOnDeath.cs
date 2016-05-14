using UnityEngine;
using System.Collections;

public class DropOnDeath : MonoBehaviour
{

    public GameObject item;
    public void OnDestroy()
    {
        Debug.Log("ffffff");
        Instantiate(item,transform.position,Quaternion.identity);
    }

}
