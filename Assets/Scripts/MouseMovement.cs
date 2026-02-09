using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    float MouseSensitivity;
    [SerializeField]
    Transform PlayerTransform;
    float YInvert;
    float YRecoil;
    float XRecoil;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        
       
            float xAx = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime + XRecoil;
            PlayerTransform.Rotate(PlayerTransform.up * xAx);

            float yAx = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime + YRecoil;
            YInvert -= yAx;
            YInvert = Mathf.Clamp(YInvert, -90, 90);
            transform.localRotation = Quaternion.Euler(YInvert, 0, 0);
        


    }

    public void RecoilSniper(float y,float x)
    {
        YRecoil = y;
        XRecoil = x;
    }
}
