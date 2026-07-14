using GlobalEnums;
using UnityEngine;

public class DynamicHudManager : MonoBehaviour
{
    public float enemyDetectRadius = 15f;
    private int enemyLayerMask = 1 << 11;
    private float checkTimer = 0f;

    void Update()
    {
        if (UIManager.instance != null && UIManager.instance.uiState != UIState.PLAYING) return;

        checkTimer += Time.deltaTime;
        if (checkTimer < 0.25f) return;
        checkTimer = 0f;

        HudGlobalHide.IsHidden = !CheckForEnemies();
    }

    bool CheckForEnemies()
    {
        if (HeroController.instance == null) return false;

        Vector2 playerPos = HeroController.instance.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos, enemyDetectRadius, enemyLayerMask);

        return colliders.Length > 0;
    }
}
