using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public float min_distanciaVisao = 1.0f;
    public float max_distanciaVisao = 10.0f;
    public Camera vrCamera;
    public LayerMask LayerInterativa;

    private IInterfaceObjetos interfaceObj;

    void Update()
    {
        DetectarObj();
    }

    void DetectarObj()
    {
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, max_distanciaVisao, LayerInterativa))
        {
            IInterfaceObjetos interactable = hit.collider.GetComponent<IInterfaceObjetos>();

            if (interactable == null) return;
           
            if (interfaceObj != interactable)
            {
                interfaceObj?.OnFocoExit();
                interfaceObj = interactable;
                interfaceObj.OnFocoEnter();
            }
            else
            {
                interfaceObj.OnFocoStay();
            }
        }
        else
        {
            if (interfaceObj != null)
            {
                interfaceObj.OnFocoExit();
                interfaceObj = null;
            }
        }
    }
}
