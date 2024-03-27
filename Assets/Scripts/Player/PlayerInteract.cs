using UnityEngine;

/// <summary>
/// Handle the player interactions and cast the right action
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [Header("Scripts on player")]

    [SerializeField]
    private Selling _selling;

    [SerializeField]
    private PlantingAndHarvest _planting;

    [Header("Scripts Collected")]

    [SerializeField]
    private BuyingZone _buyingZone;

    [SerializeField]
    private string _currentAction;

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    private void Awake()
    {
        PlayerMain.InputsReceiver.OnInteract += Interact;
    }

    /// <summary>
    /// Analyze the current action and interact like it
    /// </summary>
    private void Interact()
    {
        switch (_currentAction)
        {
            case "Buy":
                _buyingZone.Interact();
                break;
            case "Plant":
                _planting.Interact();
                break;
            case "Sell":
                _selling.Interact();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Analyze wich GameObject is in the trigger and set the current action to the right one
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BuyingZone":
                _currentAction = "Buy";
                _buyingZone = other.GetComponent<BuyingZone>();
                _buyingZone.UpdateIndications(true);
                break;
            case "PlantingZone":
                _currentAction = "Plant";
                _planting.DefineCurrentCropPlot(other.GetComponent<CropPlot>());
                break;
            case "SellingZone":
                _currentAction = "Sell";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Reset the action and the current zone when the player is not in the trigger anymore
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "BuyingZone":
                _buyingZone.UpdateIndications(false);
                _buyingZone = null;
                break;
            case "PlantingZone":
                _planting.ResetCropPlot();
                break;
            case "SellingZone":
                break;
            default:
                break;
        }

        _currentAction = string.Empty;
    }
}
