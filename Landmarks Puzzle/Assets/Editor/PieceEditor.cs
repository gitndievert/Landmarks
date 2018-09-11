using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PuzzlePiece))]
public class PieceEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var piece = (PuzzlePiece)target;
        if (GUILayout.Button("Set Finished Position"))
        {
            piece.SetFinalPosition();
        }        
    } 	
}
