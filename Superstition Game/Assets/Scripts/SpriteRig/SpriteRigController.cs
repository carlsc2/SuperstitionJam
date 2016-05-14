using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteRigController : MonoBehaviour {

    [System.Serializable]
    public class CosmeticSprite {

        public string name;

        public SpriteRenderer spriteRen;

        public Sprite sprite;

        public Vector2 offset;

        public void ApplyOffsetFromWorld() {
            offset = spriteRen.transform.localPosition;
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
    }


    //public SortingLayer rigSortingLayer;

    public Color boneColor;
    public float boneSize;

    public Color jointColor;
    public float jointSize;


    void Awake() {
        
    }

    public List<SpriteBoneBinding> spriteBones;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyRigCosmetics() {

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

    /*
    public void SetSortingLayer(SortingLayer layer) {

        foreach (SpriteBoneBinding binding in spriteBones) {
            binding.spriteRen.sortingLayerName = layer.name;
        }

    }
    */

    /* 
    void OnDrawGizmos() {

        //Gizmos.DrawSphere(transform.position, jointSize);

        //DrawSkeleton_Gizmo();

        Color originalColor = Gizmos.color;

        //DRAW JOINTS
        Gizmos.color = jointColor;

        Debug.Log(spriteBones[0]);
        foreach (SpriteBoneBinding binding in spriteBones) {
            Gizmos.DrawSphere(transform.position, jointSize);
        }

        //DRAW BONES
        Gizmos.color = boneColor;
        foreach (SpriteBoneBinding binding in spriteBones) {

            foreach (Transform child in binding.bone.transform) {
                if (!binding.cosmeticsList.Select(x => x.spriteRen.transform).ToList().Contains(child)) {
                    Gizmos.DrawLine(binding.bone.transform.position, child.position);
                }
            }
        }

        Gizmos.color = originalColor;
    }
    */
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
                if (!binding.cosmeticsList.Select(x => x.spriteRen.transform).ToList().Contains(child)) {
                    Gizmos.DrawLine(binding.bone.transform.position, child.position);
                }
            }
        }

        Gizmos.color = originalColor;

    }    
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteRigController))]
public class SpriteRigController_Editor : Editor {

    SpriteRigController selfScript;

    void OnEnable() {
        selfScript = (SpriteRigController)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        /*
        if (GUILayout.Button("Transferstuff")) {
            foreach (SpriteRigController.SpriteBoneBinding binder in selfScript.spriteBones) {
            }
        }
        */

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
