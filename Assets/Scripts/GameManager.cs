using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool cursorLocked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LockCursor();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!cursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }

#endif
    }

    public void LockCursor()
    {
        cursorLocked = true;

        //Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        //Hide the cursor
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        cursorLocked = false;

        // Unlock the cursor, allowing it to move freely
        Cursor.lockState = CursorLockMode.None;

        // Show the cursor
        Cursor.visible = true;
    }
}
