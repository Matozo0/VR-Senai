using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public float min_distanciaVisao = 1.0f;
    public float max_distanciaVisao = 10.0f;
    public Camera vrCamera;
    public LayerMask LayerInterativa;
    public float tempoParaStay = 2f;
    public float tempoParaOrbitar = 30f;
    public float tempoOlhandoParaBaixo = 5f;
    public float velocidadeOrbita = 20f;

    private IInterfaceObjetos interfaceObj;
    private float focusTime;
    private bool isOrbiting;
    private float olharBaixoTime;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    private GameObject objetoBatido;

    void Start()
    {
        initialCameraPosition = vrCamera.transform.position;
        initialCameraRotation = vrCamera.transform.rotation;
    }

    void Update()
    {
        if (isOrbiting)
        {
            OrbitarObjeto();
            DetectarOlharParaBaixo();
        }
        else
        {
            DetectarObj();
        }
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
                focusTime = 0f;
            }
            else
            {
                focusTime += Time.deltaTime;

                if (focusTime >= tempoParaStay)
                {
                    interfaceObj.OnFocoStay();
                }

                if (focusTime >= tempoParaOrbitar)
                {
                    StartOrbitar();
                    objetoBatido = hit.transform.gameObject;
                }
            }
        }
        else
        {
            if (interfaceObj != null)
            {
                interfaceObj.OnFocoExit();
                interfaceObj = null;
                focusTime = 0f;
            }
        }
    }

    private void StartOrbitar()
    {
        isOrbiting = true;
        focusTime = 0f;
    }

    private void StopOrbitar()
    {
        isOrbiting = false;
        olharBaixoTime = 0f;
        gameObject.transform.position = initialCameraPosition;
        gameObject.transform.rotation = initialCameraRotation;
    }

    private void OrbitarObjeto()
    {
        if (interfaceObj != null)
        {
            gameObject.transform.RotateAround(objetoBatido.transform.position, Vector3.up, velocidadeOrbita * Time.deltaTime);
        }
    }

    private void DetectarOlharParaBaixo()
    {
        if (vrCamera.transform.eulerAngles.x > 45 && vrCamera.transform.eulerAngles.x < 315)
        {
            olharBaixoTime += Time.deltaTime;

            if (olharBaixoTime >= tempoOlhandoParaBaixo)
            {
                StopOrbitar();
            }
        }
        else
        {
            olharBaixoTime = 0f;
        }
    }
}
