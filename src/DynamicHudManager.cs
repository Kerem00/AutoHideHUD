using GlobalEnums;
using UnityEngine;

public class DynamicHudManager : MonoBehaviour
{
    public float enemyDetectRadius = 15f;
    private int enemyLayerMask = 1 << 11;

    private HudCanvas hudScript;
    private Vector3 originalPosition;
    private bool hasSavedPosition = false;

    private float checkTimer = 0f;

    void Update()
    {
        if (UIManager.instance != null && UIManager.instance.uiState != UIState.PLAYING) return;

        if (hudScript == null)
        {
            hudScript = FindFirstObjectByType<HudCanvas>();

            if (hudScript != null)
            {
                originalPosition = hudScript.transform.localPosition;
                hasSavedPosition = true;
                Debug.Log("[AutoHideHUD] Locked onto HUD and saved its anchor position!");
            }
            return;
        }

        checkTimer += Time.deltaTime;
        if (checkTimer < 0.25f) return;
        checkTimer = 0f;

        bool enemiesNearby = CheckForEnemies();

        if (hasSavedPosition)
        {
            if (enemiesNearby)
            {
                hudScript.transform.localPosition = originalPosition;
            }
            else
            {
                hudScript.transform.localPosition = new Vector3(10000f, 10000f, 0f);
            }
        }
    }

    bool CheckForEnemies()
    {
        if (HeroController.instance == null) return false;

        Vector2 playerPos = HeroController.instance.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos, enemyDetectRadius, enemyLayerMask);

        return colliders.Length > 0;
    }
}
