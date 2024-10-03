using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelConfig;
    [SerializeField] private GameObject painelComo;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirConfig()
    {
        painelMenuInicial.SetActive(false);
        painelConfig.SetActive(true);
    }

    public void FecharConfig()
    {
        painelConfig.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

        public void AbrirComo()
    {
        painelMenuInicial.SetActive(false);
        painelComo.SetActive(true);
    }

    public void FecharComo()
    {
        painelComo.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void Sair()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
