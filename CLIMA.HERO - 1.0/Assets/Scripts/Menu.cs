using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Este script controla as ações do menu, incluindo o carregamento de cenas e a saída da aplicação.

public class Menu : MonoBehaviour
{
    // Método para carregar uma cena com base no nome fornecido.
    public void LoadScenes(string cena)
    {
        // Carrega a cena especificada.
        SceneManager.LoadScene(cena);
    }

    // Método para sair da aplicação.
    public void Quit()
    {
        // Encerra a aplicação. Isso funciona apenas no build final e não no Editor.
        Application.Quit();
    }
}

