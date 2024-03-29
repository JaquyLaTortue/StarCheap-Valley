using DG.Tweening;
using TMPro;
using UnityEngine;

public class SeedTextControl : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _seedText;

    [SerializeField]
    private Color _seedTextColor;

    private void Start()
    {
        PlayerInventory.Instance.OnCurrentSeedUpdated += UpdateSeedText;
        UpdateSeedText("No seed in inventory");
    }

    /// <summary>
    /// Update the seed text.
    /// </summary>
    /// <param name="currentseed">The message that will be displayed.</param>
    private void UpdateSeedText(string currentseed)
    {
        _seedText.color = Color.black;
        _seedText.text = currentseed;
        _seedText.transform.DOShakePosition(0.5f, 0.1f, 10, 90, false);
        _seedText.DOColor(_seedTextColor, 0.5f);
    }
}
