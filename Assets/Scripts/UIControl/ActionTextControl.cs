using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ActionTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _actionText;

    [SerializeField]
    private Color _actionTextColor;

    [Header("References")]
    [SerializeField]
    private PlantingAndHarvest _planting;

    [SerializeField]

    private List<CropPlot> _cropPlots;

    [SerializeField]
    private List<BuyingZone> _buyingZone;

    private bool _buyCd = true;

    private void Start()
    {
        _planting.OnUpdateUI += UpdateActionText;

        foreach (var item in _cropPlots)
        {
            item.OnUpdateUI += UpdateActionText;
        }

        foreach (var item in _buyingZone)
        {
            item.OnUpdateUI += UpdateActionText;
        }
    }

    /// <summary>
    /// Update the action text.
    /// </summary>
    /// <param name="text">The message that will be displayed.</param>
    /// <param name="shouldShake">If the text should shake.</param>
    private void UpdateActionText(string text, bool shouldShake)
    {
        if (_buyCd && shouldShake)
        {
            _buyCd = false;
            float tweenDuration = 1f;
            _actionText.color = Color.red;
            _actionText.text = text;
            _actionText.transform.DOShakePosition(tweenDuration, 10, 10);
            _actionText.DOColor(_actionTextColor, tweenDuration);
            StartCoroutine(BuyCD(tweenDuration));
            return;
        }

        _actionText.color = Color.black;
        _actionText.text = text;
        _actionText.DOColor(_actionTextColor, 0.5f);
    }

    private IEnumerator BuyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _buyCd = true;
    }
}
