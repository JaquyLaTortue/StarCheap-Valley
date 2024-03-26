using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private BuyingZone _buying;

    [SerializeField]
    private Selling _selling;

    [SerializeField]
    private Planting _planting;

    [SerializeField]
    private string _currentAction;

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    private void Awake()
    {
        PlayerMain.InputsReceiver.OnInteract += Interact;
    }

    private void Interact()
    {
        switch (_currentAction)
        {
            case "Buy":
                _buying.Interact();
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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BuyingZone":
                _currentAction = "Buy";
                _buying = other.GetComponent<BuyingZone>();
                break;
            case "Planting":
                _currentAction = "Plant";
                _planting = other.GetComponent<Planting>();
                break;
            case "SellingZone":
                _currentAction = "Sell";
                _selling = other.GetComponent<Selling>();
                break;
            default:
                break;
        }

        Debug.Log(_currentAction);
    }

    private void OnTriggerExit(Collider other)
    {
        _currentAction = string.Empty;
    }
}
