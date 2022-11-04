using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
namespace Gege
{

    public static class SceneManagement
    {
        public static void LoadSceneWithTransition(string sceneName)
        {
            Scene thisScene = SceneManager.GetActiveScene();
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadSceneOperation.allowSceneActivation = false;
            //TransitionOut.Completed=false;
            GameObject outTransitionAnimationPrefab = GameObject.Instantiate(Resources.Load<GameObject>("TransitionOut"));
            Animator outTransitionAnimation = outTransitionAnimationPrefab.GetComponentInChildren<Animator>();
            loadSceneOperation.completed += (e) =>
            {
                SceneManager.UnloadSceneAsync(thisScene);
            };
            WaitForSceneLoad(loadSceneOperation);

        }
        static async void WaitForSceneLoad(AsyncOperation operation)
        {
            while (!TransitionOut.Completed || operation.progress<.9f)
            {
                await Task.Delay(1);
            }
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                await Task.Delay(1);
            }
            //TransitionIn.Completed = false;
            GameObject inTransitionAnimationPrefab = GameObject.Instantiate(Resources.Load<GameObject>("TransitionIn"));
            Animator inTransitionAnimation = inTransitionAnimationPrefab.GetComponentInChildren<Animator>();
            while (!TransitionIn.Completed)
            {
                await Task.Delay(1);
            }
            GameObject.Destroy(inTransitionAnimation.transform.parent.parent.gameObject);
        }
    }
}
