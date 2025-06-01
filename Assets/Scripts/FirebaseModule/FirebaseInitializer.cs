using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace FirebaseModule
{
    public class FirebaseInitializer
    {
        private FirebaseApp _app;
        private readonly string _databaseUri;
        public Task InitializationTask { get; private set; }

        public FirebaseInitializer(string databaseUri)
        {
            _databaseUri = databaseUri;
            InitializationTask = InitializeFirebase();
        }

        private Task InitializeFirebase()
        {
            var tcs = new TaskCompletionSource<bool>();

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _app.Options.DatabaseUrl = new System.Uri(_databaseUri);

                    Debug.Log("Firebase initialized.");
                    tcs.SetResult(true);
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                    tcs.SetException(new System.Exception("Firebase dependencies not available"));
                }
            });

            return tcs.Task;
        }
    }
}