using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameManager : BaseGameManager
{
    protected override void AddManagers()
    {
        managers.Add(AuthenticationManager.Instance);
        managers.Add(UIManager.Instance);
        managers.Add(SoundManager.Instance);
    }

    protected override void InitializeManagerForce()
    {
    }

    protected override void OnInit()
    {
    }
}
