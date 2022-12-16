using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Text infoText;

    public void Active(Dice dice, Vector3 position)
    {
        if (dice == null)
        {
            Inactive();
            return;
        }

        position.z = transform.position.z;
        transform.position = position;

        transform.DOScale(1f, 0.3f);

        StringBuilder info = new StringBuilder();

        if (dice.DiceType != DiceType.Skill)
        {
            info.Append("( ");

            if (dice.DiceType == DiceType.Multiply || dice.DiceType == DiceType.Number)
            {
                List<int> list = dice.numbers.List.ToList();
                info.Append(string.Join(", ", list.OrderBy(x => x)));
            }

            info.Append(" )\n");
        }

        info.Append(dice.DiceDescription);

        infoText.text = info.ToString();
    }

    public void Inactive()
    {
        transform.DOScale(0f, 0.3f);
    }
}