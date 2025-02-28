using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_ButtonController : MonoBehaviour
{
    public void OnButtonClick()
    {
        Lur_GameManager.Instance.SetScriptsState(true, true); // Activar `scriptA`, desactivar `scriptB`
    }
}
