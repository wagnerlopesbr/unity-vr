using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FireExtinguisherSmokeScript : MonoBehaviour
{
    public ParticleSystem smokeEffect; // Referência ao VFX (Particle System)
    public HandPresence leftHandPresence; // Referência ao script HandPresence para a mão esquerda
    public HandPresence rightHandPresence; // Referência ao script HandPresence para a mão direita
    private bool isSpraying = false; // Estado do jato

    void Start()
    {
        // Certifique-se de que as referências para as mãos não são nulas
        if (leftHandPresence == null)
        {
            leftHandPresence = GetComponentInParent<HandPresence>(); // Se não, tenta pegar o script da mão esquerda
        }

        if (rightHandPresence == null)
        {
            rightHandPresence = GetComponentInParent<HandPresence>(); // Se não, tenta pegar o script da mão direita
        }
    }

    void Update()
    {
        // Verifica se qualquer uma das mãos está pressionando o gatilho
        bool isLeftTriggerPressed = leftHandPresence != null && leftHandPresence.GetTriggerValue() > 0.5f;
        bool isRightTriggerPressed = rightHandPresence != null && rightHandPresence.GetTriggerValue() > 0.5f;

        // Se qualquer uma das mãos estiver pressionando o gatilho, ativa a fumaça
        if (isLeftTriggerPressed || isRightTriggerPressed)
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
