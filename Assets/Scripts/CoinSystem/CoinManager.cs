using System.Buffers.Text;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public GameObject door;
    private bool doorDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

        coinText.text = ": " + coinCount.ToString();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinText != null)
            coinText.text = ": " + coinCount.ToString();
    }
    public bool SpendCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }
}
