using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SpriteRigController : MonoBehaviour {

    [System.Serializable]
    public class CosmeticSprite {

        public string name;

        public SpriteRenderer spriteRen;

        public Sprite sprite;

        public Vector2 offset;

        //[HideInInspector]
        public int orderInLayer;

        public void ApplyOffsetFromWorld() {
            offset = spriteRen.transform.localPosition;
        }

        public void RefreshOrderInLayer() {
            //orderInLayer = spriteRen.sortingOrder;
            spriteRen.sortingOrder = orderInLayer;
        }
    }

    [System.Serializable]
    public class SpriteBoneBinding {

        public string name;

        public SpriteRigBoneHandler bone;

        public List<CosmeticSprite> cosmeticsList;


        public void ReloadBoneCosmetics() {
            bone.DestroyAllCosmeticSprites();

            foreach (CosmeticSprite cosSprite in cosmeticsList) {
                cosSprite.spriteRen = bone.AddCosmeticSprite(cosSprite.sprite, cosSprite.offset, cosSprite.name);
                
            }
        }

        public void ApplyWorldOffsets() {
            foreach (CosmeticSprite cosSprite in cosmeticsList) {
                cosSprite.ApplyOffsetFromWorld();
            }
        }

        public void ApplySortingOrder() {
            foreach (CosmeticSprite cosSprite in cosmeticsList) {
                //cosSprite.orderInLayer = cosSprite.spriteRen.sortingOrder;
                //cosSprite.spriteRen.sortingOrder = cosSprite.odr
                cosSprite.RefreshOrderInLayer();
            }
        }
    }

    [System.Serializable]
    public class RigSocket {
        public string socketLabel;
        public Transform socketTransform;
        public SpriteRigBoneHandler rigBone;

        public void AttatchToSocket(Transform objectToAttatch) {
            AttatchToSocket(objectToAttatch, Vector3.zero);
        }

        public void AttatchToSocket(Transform objectToAttatch, Vector3 localPosOffset) {

            //get object into position
            objectToAttatch.position = socketTransform.position;
            objectToAttatch.rotation = socketTransform.rotation;

            //parent it so we can do local offsets
            objectToAttatch.parent = socketTransform;

            //adjust local position for lining up
            objectToAttatch.localPosition += localPosOffset;

            if (objectToAttatch.GetComponentInChildren<SpriteRenderer>() != null) {
                objectToAttatch.GetComponentInChildren<SpriteRenderer>().sortingOrder = rigBone.cosmeticSpriteRenderers[0].sortingOrder - 1;
            }
        }

    }

    //public SortingLayer rigSortingLayer;

    public Color boneColor;
    public float boneSize;

    public Color jointColor;
    public float jointSize;

    public Color socketBoneColor;

    public Color primaryFlashColor = Color.red;
    public Color secondaryFlashColor = Color.white;


    public List<SpriteBoneBinding> spriteBones;
    public List<RigSocket> sockets;


    void Awake() {
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (!Application.isPlaying) {
            ApplyCosmeticsOffsetsToRig();
            ApplySortingOrderToRigCosmetics();
        }
	}


    public void AttachObjectToSocket(Transform objectTf, string socketName) {

        //List<string> viableSockets = sockets.Select(x => x.socketLabel).ToList();

        if (!sockets.Select(x => x.socketLabel).Contains(socketName)) { return; }

        sockets.Where(x => x.socketLabel == socketName).ElementAt(0).AttatchToSocket(objectTf);

        //viableSockets[0].AttatchToSocket(objectTf);
    }

    public void ReloadBoneCosmetics() {
        foreach (SpriteBoneBinding binding in spriteBones) {

            binding.ReloadBoneCosmetics();
        }
    }

    public void ApplyCosmeticsOffsetsToRig() {
        foreach (SpriteBoneBinding binding in spriteBones) {
            binding.ApplyWorldOffsets();
        }
    }

    public void ApplySortingOrderToRigCosmetics() {
        foreach (SpriteBoneBinding binding in spriteBones) {
            binding.ApplySortingOrder();
        }
    }

    public SpriteRenderer[] GetCosmeticSpriteRenderers() {
        List<SpriteRenderer> retList = new List<SpriteRenderer>();

        foreach (SpriteBoneBinding binding in spriteBones) {
            foreach (CosmeticSprite cosSprite in binding.cosmeticsList) {
                if (cosSprite.spriteRen != null) {
                    retList.Add(cosSprite.spriteRen);
                }
            }
        }

        return retList.ToArray();
    }

//FLASH ALL SPRITES ON RIG
    public void StartFlashRig(int numFlashes, float timePerFlash) {
        StartCoroutine(FlashRig(numFlashes, timePerFlash, primaryFlashColor, secondaryFlashColor));
    }

    IEnumerator FlashRig(int numFlashes, float timePerFlash, Color upFlashColor, Color downFlashColor) {
        SpriteRenderer[] cosmeticRenderers = GetCosmeticSpriteRenderers();

        while (numFlashes > 0) {
            foreach (SpriteRenderer spriteRen in cosmeticRenderers) {
                spriteRen.color = upFlashColor;
            }

            yield return new WaitForSeconds(timePerFlash / 2.0f);

            foreach (SpriteRenderer spriteRen in cosmeticRenderers) {
                spriteRen.color = downFlashColor;
            }

            yield return new WaitForSeconds(timePerFlash / 2.0f);

            numFlashes -= 1;
        }

        //make sure we set the color back to the original
        foreach (SpriteRenderer spriteRen in cosmeticRenderers) {
            spriteRen.color = Color.white;
        }

        yield return null;
    }


#if UNITY_EDITOR
    //GIZMOS
    void OnDrawGizmos() {
        DrawSkeleton_Gizmo();
    }

    private void DrawSkeleton_Gizmo() {
        
        Color originalColor = Gizmos.color;


        
        //DRAW JOINTS
        Gizmos.color = jointColor;
        foreach (SpriteBoneBinding binding in spriteBones) {
            Gizmos.DrawSphere(binding.bone.transform.position, jointSize);
        }

        //DRAW BONES
        Gizmos.color = boneColor;
        foreach (SpriteBoneBinding binding in spriteBones) {

            foreach (Transform child in binding.bone.transform) {
                //if (!binding.cosmeticsList.Select(x => x.spriteRen.transform).ToList().Contains(child)) {
                if (spriteBones.Select(x => x.bone.transform).Contains(child)) {
                    Gizmos.DrawLine(binding.bone.transform.position, child.position);
                }
            }
        }

        Gizmos.color = socketBoneColor;
        foreach (RigSocket sock in sockets) {
            Gizmos.DrawLine(sock.socketTransform.position, sock.socketTransform.parent.position);
        }
        
        Gizmos.color = originalColor;
        

    }
#endif


}



#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteRigController))]
public class SpriteRigController_Editor : Editor {

    SpriteRigController selfScript;

    void OnEnable() {
        selfScript = (SpriteRigController)target;
        //EditorApplication.CallbackFunction callbackDel;
        //callbackDel = selfScript.ApplySortingOrderToRigCosmetics();
    }

    void OnGUI() {
        Debug.Log("Blomasd;lkj");
    }
    
    /*
    public override void DrawPreview(Rect previewArea) {
        base.DrawPreview(previewArea);
    }
    public override bool HasPreviewGUI() {
        //return base.HasPreviewGUI();
        return true;
    }
    */
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        /*
        if (GUILayout.Button("Transferstuff")) {
            foreach (SpriteRigController.SpriteBoneBinding binder in selfScript.spriteBones) {
            }
        }
        */

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Update Rig Cosmetics")) {
                UpdateCosmeticRig();
            }
        
            if (GUILayout.Button("Apply World Offsets to Cosmetics")) {
                selfScript.ApplyCosmeticsOffsetsToRig();
            }
        EditorGUILayout.EndHorizontal();
    }

    private void UpdateCosmeticRig() {

        selfScript.ReloadBoneCosmetics();
        
    }

}

/*
[CustomPropertyDrawer(typeof(SpriteRigController.SpriteBoneBinding))]
public class SpriteBoneBinding_PropertyDrawer : PropertyDrawer {

}
*/

/*
[CustomPropertyDrawer(typeof(SpriteRigController.CosmeticSprite))]
public class CosmeticSprite_PropertyDrawer : PropertyDrawer {



}
*/

#endif
