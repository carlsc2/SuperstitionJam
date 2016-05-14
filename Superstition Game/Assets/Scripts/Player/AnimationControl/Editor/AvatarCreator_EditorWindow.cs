using UnityEngine;
using UnityEditor;
using System.Collections;

public class AvatarCreator_EditorWindow : EditorWindow {

    [MenuItem("Avatar/Builder")]
    static void Init() {
        AvatarCreator_EditorWindow window = (AvatarCreator_EditorWindow)EditorWindow.GetWindow(typeof(AvatarCreator_EditorWindow));
    }

    void OnGUI() {



    }
	
}
