using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecheck : MonoBehaviour
{
    private ControllerDEMO controller_;
    // Start is called before the first frame update
    void Start()
    {
        controller_ = new ControllerDEMO();
        controller_.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller_.Player.Fire.triggered)
        {
            Debug.Log("弾が出たぜペッシ");
        }   
        if (controller_.Player.Move.triggered)
        {
            Debug.Log("動くと決めた時にはすでに動いている");
        }

    }
}
