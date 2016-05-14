using UnityEngine;
using System.Collections;

public class DropOnDeath : MonoBehaviour
{

    public GameObject item;
    public void OnDestroy()
    {
        Instantiate(item,transform.position,Quaternion.identity);
    }

}
