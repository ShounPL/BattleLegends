using UnityEngine;

namespace BattleLegends
{
    public class ShopScreenController : MonoBehaviour
    {
        private const int PanelWidth = 900;
        private const int PanelHeight = 500;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Sklep", GUILayout.Height(36f));

            if (ShopSystem.Instance == null)
            {
                GUILayout.Label("Brak sklepu");
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(340f));

            foreach (var item in ShopSystem.Instance.shopItems)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(item.itemName, GUILayout.Width(180f));
                GUILayout.Label(item.rarity.ToString(), GUILayout.Width(80f));
                GUILayout.Label(item.cost + " zł", GUILayout.Width(70f));

                if (GUILayout.Button("Kup", GUILayout.Width(120f)))
                {
                    bool success = ShopSystem.Instance.TryBuy(item.itemId);
                    if (!success)
                    {
                        Debug.Log("Brak środków lub błąd kupna.");
                    }
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            if (GUILayout.Button("Powrót", GUILayout.Height(36f)))
            {
                SceneLoader.LoadScene("MainMenu");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
