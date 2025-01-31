using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit; // Importa para usar XR Grab Interactable

public class FireExtinguisherSmokeScript : MonoBehaviour
{
    public ParticleSystem smokeEffect; // Referência ao VFX (Particle System)
    public XRGrabInteractable grabInteractable; // Referência ao sistema de pegar objetos
    public HandPresence leftHandPresence; // Referência à mão esquerda
    public HandPresence rightHandPresence; // Referência à mão direita
    private bool isSpraying = false; // Estado do jato

    void Start()
    {
        // Verifica se o extintor tem o componente XRGrabInteractable
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        // Se não houver referências para as mãos, tenta pegar automaticamente
        if (leftHandPresence == null)
        {
            leftHandPresence = GetComponentInParent<HandPresence>();
        }

        if (rightHandPresence == null)
        {
            rightHandPresence = GetComponentInParent<HandPresence>();
        }
    }

    void Update()
    {
        // Verifica se o extintor está sendo segurado
        bool isGrabbed = grabInteractable != null && grabInteractable.isSelected;

        // Verifica se alguma das mãos está pressionando o gatilho
        bool isLeftTriggerPressed = leftHandPresence != null && leftHandPresence.GetTriggerValue() > 0.5f;
        bool isRightTriggerPressed = rightHandPresence != null && rightHandPresence.GetTriggerValue() > 0.5f;

        // Só ativa se o extintor estiver segurado E o gatilho pressionado
        if (isGrabbed && (isLeftTriggerPressed || isRightTriggerPressed))
        {
            if (!isSpraying)
            {
                StartSpray();
            }
        }
        else
        {
            if (isSpraying)
            {
                StopSpray();
            }
        }
    }

    private void StartSpray()
    {
        isSpraying = true;
        if (smokeEffect != null)
        {
            smokeEffect.Play(); // Ativa o VFX
            Debug.Log("Jato de fumaça ativado");
        }
    }

    private void StopSpray()
    {
        isSpraying = false;
        if (smokeEffect != null)
        {
            smokeEffect.Stop(); // Para o VFX
            Debug.Log("Jato de fumaça desativado");
        }
    }
}
