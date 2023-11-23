using UnityEngine;

public static class InputManager
{
    private static Controls _ctrls;

    private static Vector3 _mousePos;

    private static ProjectileWeapon projectileWeapon;

    private static Camera cam;

    

    public static Ray GetCameraRay()
    {
        return cam.ScreenPointToRay(_mousePos);
    }

    public static void Init(Player p)
    {
        _ctrls = new();

        cam = Camera.main;
        
        _ctrls.Permenanet.Enable();

        _ctrls.InGame.Shoot.performed += ctx =>
        {
            p.Shoot(); //shoots
        };

        _ctrls.InGame.Reload.started += ctx =>
        {
            p.Reload(); //reloads
            Debug.Log("Reloaded in InputManager :3");
        };

        _ctrls.Permenanet.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };

        _ctrls.InGame.Change.started += ctx =>
        {
            p.Change();
        };
    }


    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}
