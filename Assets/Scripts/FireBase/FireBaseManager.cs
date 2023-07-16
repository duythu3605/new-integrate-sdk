using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;


public class FireBaseManager : MonoBehaviour
{
    private GameManager _gameManager;
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SignOutButton()
    {
        auth.SignOut();
        _gameManager._uIManager._uILogin.ShowUI();
        _gameManager._uIManager._uILogin.ClearLoginFeilds();
        _gameManager._uIManager._uIRegister.ClearRegisterFeilds();

    }
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(_gameManager._uIManager._uiUserData._inputUserName.text));
        StartCoroutine(UpdateUsernameDatabase(_gameManager._uIManager._uiUserData._inputUserName.text));

        StartCoroutine(UpdateXp(int.Parse(_gameManager._uIManager._uiUserData._inputExp.text)));
        StartCoroutine(UpdateKills(int.Parse(_gameManager._uIManager._uiUserData._inputKills.text)));
        StartCoroutine(UpdateDeaths(int.Parse(_gameManager._uIManager._uiUserData._inputDeaths.text)));
    }
    //Function for the scoreboard button
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    //Function for the login button
    public void LoginButton(string _email, string _pass)
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(_email, _pass));
    }
    //Function for the register button
    public void RegisterButton(string _email, string _pass,string _repass, string _userName)
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(_email, _pass,_repass, _userName));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            _gameManager._uIManager._uILogin.Notice(message, true);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            _gameManager._uIManager._uILogin.Notice("Logged In", false);
            //StartCoroutine(LoadUserData());

            yield return new WaitForSeconds(2);

            _gameManager._uIManager._uiUserData._inputUserName.text = User.DisplayName;
            _gameManager._uIManager._uiUserData.ShowUI();
            _gameManager._uIManager._uILogin.Notice("", false);
            _gameManager._uIManager._uILogin.ClearLoginFeilds();
            _gameManager._uIManager._uIRegister.ClearRegisterFeilds();
        }
    }

    private IEnumerator Register(string _email, string _password,string _rePassword ,string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            _gameManager._uIManager._uILogin.Notice("Missing Username", true);
        }
        else if (_password != _rePassword)
        {
            //If the password does not match show a warning
            _gameManager._uIManager._uILogin.Notice("Password Does Not Match!", true);
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                _gameManager._uIManager._uILogin.Notice(message, true);
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        //FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        //AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        _gameManager._uIManager._uILogin.Notice("Username Set Failed!", true);
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        _gameManager._uIManager._uIRegister.HideUI();
                        _gameManager._uIManager._uILogin.ShowUI();
                        _gameManager._uIManager._uILogin.ClearLoginFeilds();
                        _gameManager._uIManager._uIRegister.ClearRegisterFeilds();

                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateXp(int _xp)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").SetValueAsync(_xp);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }

    private IEnumerator UpdateKills(int _kills)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("kills").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateDeaths(int _deaths)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(_deaths);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            _gameManager._uIManager._uiUserData._inputExp.text = "0";
            _gameManager._uIManager._uiUserData._inputKills.text = "0";
            _gameManager._uIManager._uiUserData._inputDeaths.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            _gameManager._uIManager._uiUserData._inputExp.text = snapshot.Child("xp").Value.ToString();
            _gameManager._uIManager._uiUserData._inputKills.text = snapshot.Child("kills").Value.ToString();
            _gameManager._uIManager._uiUserData._inputDeaths.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    private IEnumerator LoadScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("kills").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in _gameManager._uIManager._uIScore.scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int kills = int.Parse(childSnapshot.Child("kills").Value.ToString());
                int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());
                int xp = int.Parse(childSnapshot.Child("xp").Value.ToString());

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(_gameManager._uIManager._uIScore.scoreElement, _gameManager._uIManager._uIScore.scoreboardContent);
                scoreboardElement.GetComponent<ScoreBoard>().SetData(username, kills, deaths, xp);
            }

            //Go to scoareboard screen
            _gameManager._uIManager._uIScore.ShowUI();
        }
    }
}
