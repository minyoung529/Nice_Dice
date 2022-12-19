using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomMachine : MonoBehaviour
{
    #region MACHINE
    readonly Vector3 ORIGIANL_POS = new Vector3(3.5f, 5f, 0f);
    readonly Vector3 TARGET_POS = new Vector3(3.5f, -2.62f, 0f);
    #endregion  // MACHINE

    #region DICE
    [SerializeField] private Transform diceSpawn;
    private readonly Vector3 DICE_SCALE = Vector3.one * 0.2f;

    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] lastPointa;
    #endregion // DICE

    [SerializeField]
    private ParticleSystem particle;

    [Header("Sound")]
    [SerializeField]
    private AudioClip appearSound;

    [SerializeField]
    private AudioClip diceSound;
    [SerializeField]
    private AudioClip specialDiceSound;

    void Awake()
    {
        transform.position = ORIGIANL_POS;
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, Active);
    }

    private void Active()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(TARGET_POS, 1f).SetEase(Ease.InFlash));
        seq.AppendCallback(() =>
        {
            particle.Play();
            SoundManager.Instance.PlayOneshot(appearSound);
        });

        seq.Append(GameManager.Instance.MainCam.transform.DOShakePosition(0.4f));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => GameManager.Instance.MainCam.MoveDrawPos());
        seq.AppendInterval(1.5f);
        seq.AppendCallback(() =>
        {
            StopAllCoroutines();
            StartCoroutine(DrawCoroutine());
            EventManager.TriggerEvent(Define.ON_START_DRAW);
        });
    }

    private void Inactive()
    {
        StopAllCoroutines();
        transform.DOMove(ORIGIANL_POS, 1f).SetEase(Ease.OutFlash);

        EventManager.TriggerEvent(Define.ON_END_DRAW);
    }

    private IEnumerator DrawCoroutine()
    {
        for (int i = 0; i < Define.DICE_SELECT_COUNT; i++)
        {
            Dice diceData = GameManager.Instance.Dice.SelectedDice[i];

            if (diceData.DiceType == DiceType.Number)
            {
                SoundManager.Instance.PlayOneshot(diceSound);
            }
            else
            {
                SoundManager.Instance.PlayOneshot(specialDiceSound);
            }

            GameObject dice = Instantiate(diceData.DicePrefab, diceSpawn.position, Quaternion.identity, transform);
            dice.transform.localScale = DICE_SCALE;

            dice.AddComponent<RandomDiceObject>().Init(wayPoints, lastPointa[i]);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
        Inactive();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, Active);
    }
}
