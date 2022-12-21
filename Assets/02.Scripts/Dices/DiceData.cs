using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New DiceData", menuName = "SO/Dice/DiceData")]
public class DiceData : ScriptableObject
{
    // �ֻ������� ���� �����ϴ� ������ �����صδ� �迭 ���� Region 
    #region DiceArray
    private Vector3[] cubeDice = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(90, 0, 0), new Vector3(0, 0, -90), new Vector3(0, 0, 90), new Vector3(-90, 0, 0), new Vector3(180, 0, 0) };
    private Vector3[] tetrahedronDice = new Vector3[4] { new Vector3(0f, 0f, 0f), new Vector3(125f, 0, -52f), new Vector3(180f, 0f, 68f), new Vector3(300f, 0f, 125f) }; // �����ü
    private Vector3[] octahedronDice = new Vector3[] { }; // ���ȸ�ü
    #endregion

    private Dictionary<int, Vector3[]> diceShapeDict = new Dictionary<int, Vector3[]>(); //DiceShape enum�� ������ ������ �־��ٰ�

    public IReadOnlyDictionary<int, Vector3[]> DiceShapeDict => diceShapeDict;

    [ContextMenu("Setting")]
    public void SettingData()
    {
        diceShapeDict.Clear();
        diceShapeDict.Add((int)DiceShape.Cube, cubeDice);
        diceShapeDict.Add((int)DiceShape.Tetrahedron, tetrahedronDice);
        diceShapeDict.Add((int)DiceShape.Octahedron, octahedronDice);
        //EditorUtility.SetDirty(this);
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
    }

#if UNITY_EDITOR
    [ContextMenu("View")]
    public void ViewData()
    {
        foreach (var item in DiceShapeDict)
        {
            string str = "";
            str += item.Key.ToString() + " ";
            for (int i = 0; i < item.Value.Length; i++)
            {
                str += item.Value[i] + " ";
            }
            Debug.Log(str);
        }
    }
#endif
}