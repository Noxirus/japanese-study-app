using UnityEngine;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] EMenu menu;

    public void SelectMenu()
    {
        MenuManager.GetInstance().SelectMenu(menu);
    }
}
