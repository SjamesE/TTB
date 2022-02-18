using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Test")]
public class Test : ScriptableObject
{
    public string objectName = "New MyScriptableObject";
    public bool colorIsRandom = false;
    public Color thisColor = Color.white;
    public Vector3[] spawnPoints;
}