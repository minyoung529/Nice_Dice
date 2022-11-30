using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New DiceData", menuName = "SO/Dice/DiceData")]
public class DiceData : ScriptableObject
{
    // �ֻ������� ���� �����ϴ� ������ �����صδ� �迭 ���� Region 
    #region DiceArray 
    private Vector3[] cubeDice = new Vector3[] { new Vector3(0, 0, 0), new Vector3(90, 0, 0), new Vector3(0, 0, -90), new Vector3(0, 0, 90), new Vector3(-90, 0, 0), new Vector3(180, 0, 0) };
    private Vector3[] tetrahedronDice = new Vector3[] { }; // �����ü
    private Vector3[] octahedronDice = new Vector3[] { }; // ���ȸ�ü
    #endregion

    private List<Vector3[]> diceShapeList = new List<Vector3[]>(); //DiceShape enum�� ������ ������ �־��ٰ�

    public IReadOnlyList<Vector3[]> DiceShapeList => diceShapeList;

#if UNITY_EDITOR

    [ContextMenu("Setting")]
    public void SettingData()
    {
        diceShapeList.Clear();
        diceShapeList.Add(cubeDice);
        diceShapeList.Add(tetrahedronDice);
        diceShapeList.Add(octahedronDice);
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("View")]
    public void ViewData()
    {
        foreach (var item in DiceShapeList)
        {
            string str = "";
            for (int i = 0; i < item.Length; i++)
            {
                str += item[i] + " ";
            }
            Debug.Log(str);
        }
    }
#endif
}