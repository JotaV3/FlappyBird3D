using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;

    public static void LoadScene(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        // certifica que o jogo esteja despausado quando trocar de cena
        Time.timeScale = 1f;

        // carrega uma tela de loading antes da cena definitiva
        SceneManager.LoadScene(Loader.Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static bool TryGetScene(Scene scene)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(scene.ToString()))
        {// se a cena ativa for igual a scene
            return true;
        }
        // se não
        return false;
    }
}
