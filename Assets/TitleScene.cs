using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScene : MonoBehaviour
{
    public Image emoji;
    public Image zizi;
    public Image hasNoIdea;
    public Image[] fadedImages;

    private Transform titleTransform;

    public AudioClip enterClip;
    public CanvasGroup lobbyCanvas;

    void Start()
    {
        titleTransform = zizi.transform.parent;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(MoveIcon);
        sequence.AppendInterval(2f);
        sequence.AppendCallback(OnEnterIcon);
        sequence.AppendInterval(2f);
        sequence.AppendCallback(ArrangeTitle);
        sequence.AppendInterval(2f);
        sequence.AppendCallback(ActiveLobby);
    }

    private void MoveIcon()
    {
        emoji.transform.DOMove(fadedImages[0].transform.position, 2f);
    }

    private void OnEnterIcon()
    {
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, enterClip);

        foreach (Image image in fadedImages)
        {
            image.DOFade(0f, 2f);
        }

        hasNoIdea.DOFade(1f, 2f);
        emoji.transform.DOScale(0f, 0.5f);
    }

    private void ArrangeTitle()
    {
        titleTransform.DOLocalMoveX(-110f, 1f);
    }

    private void ActiveLobby()
    {
        lobbyCanvas.DOFade(1f, 1f);
    }
}
