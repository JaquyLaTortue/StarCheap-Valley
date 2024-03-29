using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SellTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sellText;

    [SerializeField]
    private Color _sellTextColor;

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
            float tweenDuration = 1f;
            _sellText.text = text;
            _sellText.transform.DOShakePosition(tweenDuration, 10, 10);
            _sellText.DOColor(_sellTextColor, tweenDuration);
            StartCoroutine(SellCD(tweenDuration));
            return;
        }

        _sellText.text = text;
        _sellText.DOColor(_sellTextColor, 0.5f).SetDelay(0.5f);
    }

    private IEnumerator SellCD(float tweenDuration)
    {
        yield return new WaitForSeconds(tweenDuration);
        _sellCd = true;
    }
}
