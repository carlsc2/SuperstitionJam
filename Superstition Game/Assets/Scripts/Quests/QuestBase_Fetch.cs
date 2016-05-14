using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestBase_Fetch : QuestBase
{
    public GameObject Player;

    [HideInInspector]
    public InventoryController Inventory;

    public ItemBase RewardItem;

    void Start()
    {
        Inventory = Player.GetComponent<InventoryController>();
    }

    override public void GiveReward()
    {
        if (currentState == QuestBase.State.COMPLETED)
        {
            Inventory.AddItemToInventory(RewardItem);
            currentState = QuestBase.State.TURNED_IN;
        }
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(QuestBase_Fetch))]
public class QuestBase_Fetch_Editor : Editor
{

    QuestBase_Fetch selfScript;
    void OnEnable()
    {
        selfScript = (QuestBase_Fetch)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("test the fuckign fucktion"))
        {
            selfScript.GiveReward();
        }
    }
}
#endif
