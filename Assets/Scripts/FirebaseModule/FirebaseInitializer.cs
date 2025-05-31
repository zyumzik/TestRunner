using Firebase.Extensions;

namespace FirebaseModule
{
    public class FirebaseInitializer
    {
        private Firebase.FirebaseApp _app;
        private readonly string _databaseUri;

        public FirebaseInitializer(string databaseUri)
        {
            _databaseUri = databaseUri;
            
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    _app = Firebase.FirebaseApp.DefaultInstance;
            
                    // Set a flag here to indicate whether Firebase is ready to use by your app.

                    _app.Options.DatabaseUrl = new System.Uri(_databaseUri);
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}