using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Control the action text.
/// </summary>
public class ActionTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _actionText;

    [SerializeField]
    private Color _actionTextColor;

    [SerializeField]
    private float _shakeDuration = 1f;

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
            _actionText.color = Color.red;
            _actionText.text = text;
            _actionText.transform.DOShakePosition(_shakeDuration, 10, 10);
            _actionText.DOColor(_actionTextColor, _shakeDuration);
            StartCoroutine(BuyCD(_shakeDuration));
            return;
        }

        _actionText.color = Color.black;
        _actionText.text = text;
        _actionText.DOColor(_actionTextColor, 0.5f);
    }

    /// <summary>
    /// The cooldown of the action text to shake again.
    /// </summary>
    /// <param name="tweenDuration">The duration of the Cooldown.</param>
    private IEnumerator BuyCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _buyCd = true;
    }
}
