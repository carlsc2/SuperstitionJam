using UnityEngine;
using System.Collections;

public class QuestBase : MonoBehaviour {

    public enum State { UKNOWN, STARTED, COMPLETED, TURNED_IN };
    public State currentState;

    public string Description;

    virtual public void CheckConditions(string str){
        try
        {
            throw new UnityException();
        }
        catch (UnityException e){
            Debug.LogError("CheckCondition is not defined!\n");
        }
    }

    virtual public void GiveReward(){
        //change player stat
        //change boss enemy
        //give item to player
        try
        {
            throw new UnityException();
        }
        catch (UnityException e)
        {
            Debug.LogError("GiveReward is not defined!\n");
        }
    }

}
