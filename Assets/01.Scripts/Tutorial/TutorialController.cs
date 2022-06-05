using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorials;
    [SerializeField] private Image tutorialImage;
    private CanvasGroup tutorialCanvas;
    private int curIndex = 0;

    private WaitForSeconds tutorialDelay = new WaitForSeconds(4f);

    void Start()
    {
        tutorialCanvas = GetComponent<CanvasGroup>();

        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, () => PlayTutorial(true));
        EventManager.StartListening(Constant.GAME_START_EVENT, () => PlayTutorial(false));

        tutorialCanvas.gameObject.SetActive(false);
    }

    private void PlayTutorial(bool isClear)
    {
        if (curIndex >= tutorials.Length) return;
        if (isClear && curIndex == 0) return;

        if (GameManager.CurrentChapter == tutorials[curIndex].targetChapter &&
            GameManager.CurrentStage == tutorials[curIndex].targetStage)
        {
            tutorialCanvas.gameObject.SetActive(true);
            StartCoroutine(TutorialCoroutine());
        }
    }

    private IEnumerator TutorialCoroutine()
    {
        tutorialImage.sprite = tutorials[curIndex].tutorialImages[0];

        tutorialCanvas.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        Sprite[] sprites = tutorials[curIndex].tutorialImages;
        for (int i = 0; i < sprites.Length; i++)
        {
            tutorialImage.sprite = sprites[i];

            TutorialAction action = tutorials[curIndex].tutorialActions.Find(x => x.order == i + 1);
            Component component = null;

            if (action != null)
            {
                component = gameObject.AddComponent(Type.GetType(action.actionName));
            }

            yield return tutorialDelay;

            if (component)
            {
                Destroy(component);
            }
        }

        tutorialCanvas.DOFade(0f, 1f).OnComplete(() =>
         tutorialCanvas.gameObject.SetActive(false)
        );
        curIndex++;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, () => PlayTutorial(true));
        EventManager.StopListening(Constant.GAME_START_EVENT, () => PlayTutorial(false));
    }
}