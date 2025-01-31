using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para manipular UI (Texto)

public class BonfireScript : MonoBehaviour
{
    public ParticleSystem fireParticleSystem; // Referência ao Particle System do fogo
    public ParticleSystem smokeEffect; // Referência ao Particle System da fumaça
    public GameObject afterCanvas;

    private float collisionTime = 0f; // Tempo de colisão
    private bool isCollidingWithSmoke = false; // Se está colidindo com a fumaça

    void Start()
    {
        // Garante que o Canvas começa invisível
        if (afterCanvas != null)
        {
            afterCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("O GameObject afterCanvas não foi atribuído no Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é a fumaça
        if (other.CompareTag("Smoke"))
        {
            isCollidingWithSmoke = true;
            collisionTime = 0f; // Resetar o tempo de colisão
            Debug.Log("Fumaça entrou em colisão");
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Verifica se ainda está colidindo com a fumaça
        if (isCollidingWithSmoke && other.CompareTag("Smoke"))
        {
            collisionTime += Time.deltaTime; // Aumenta o tempo de colisão

            // Se a colisão durar 5 segundos, desabilita o fogo
            if (collisionTime >= 5f)
            {
                DisableFireEffect();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Se a fumaça sair do bonfire, reseta a colisão
        if (other.CompareTag("Smoke"))
        {
            isCollidingWithSmoke = false;
            collisionTime = 0f; // Resetar o tempo
            Debug.Log("Fumaça saiu da colisão");
        }
    }

    private void DisableFireEffect()
    {
        // Desabilitar o Particle System do fogo
        if (fireParticleSystem != null)
        {
            fireParticleSystem.Stop(); // Para o efeito de fogo
            Debug.Log("Fogo desabilitado devido à fumaça");
            
            // Iniciar o tempo de espera e mostrar o texto "PARABÉNS!" após 2 segundos
            StartCoroutine(ShowCongratulationsText());
        }
    }

    private IEnumerator ShowCongratulationsText()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(2f);

        // Exibe o texto "PARABÉNS!"
        if (afterCanvas != null)
        {
            afterCanvas.SetActive(true);
            Debug.Log("Texto PARABÉNS! visível");
        }
        else
        {
            Debug.LogError("Referência ao texto não está atribuída!");
        }
    }
}
