using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : MonoBehaviour
    {
        [HideInInspector] public string NameSceneLoading;
        public void LoadScene(LoadSceneMode mode = LoadSceneMode.Additive)
        {
            Debug.Log("Start loading scene");
            SceneManager.LoadSceneAsync(NameSceneLoading, mode).AsAsyncOperationObservable()
                .Do((operation) =>
                {
                    //checkProgress
                })
                .Subscribe((_) =>
                {
                    //end loading
                }).AddTo(this);
        }

        public void SimpleLoadScene() => LoadScene(LoadSceneMode.Single);
    }
}