using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Control the sell text.
/// </summary>
public class SellTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sellText;

    [SerializeField]
    private Color _sellTextColor;

    [SerializeField]
    private float _shakeDuration = 1f;

    [Header("Reference")]
    [SerializeField]
    private Selling _selling;

    private bool _sellCd = true;

    private void Start()
    {
        _selling.OnUpdateUI += UpdateSellText;
    }

    /// <summary>
    /// Update the sell text.
    /// </summary>
    /// <param name="shouldShake">If the text should shake.</param>
    /// <param name="text">The message that will be displayed.</param>
    /// <param name="color">The color in wich the message should be diplayed.</param>
    private void UpdateSellText(bool shouldShake, string text, Color color)
    {
        _sellText.color = color;
        if (!shouldShake && _sellCd)
        {
            _sellCd = false;
            _sellText.text = text;
            _sellText.transform.DOShakePosition(_shakeDuration, 10, 10);
            _sellText.DOColor(_sellTextColor, _shakeDuration);
            StartCoroutine(SellCD(_shakeDuration));
            return;
        }

        _sellText.text = text;
        _sellText.DOColor(_sellTextColor, 0.5f).SetDelay(0.5f);
    }

    /// <summary>
    /// The cooldown of the sell text to shake again.
    /// </summary>
    /// <param name="tweenDuration">The duration of the cooldown</param>
    private IEnumerator SellCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _sellCd = true;
    }
}
