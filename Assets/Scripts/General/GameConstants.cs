//#define USE_TEST_DB

public static class GameConstants
{
#if USE_TEST_DB
    public static string DB_URL = "https://emphasysgamifier-default-rtdb.europe-west1.firebasedatabase.app";
#else
    public static string DB_URL = "https://gamifiredb-default-rtdb.europe-west1.firebasedatabase.app";
#endif
    public static string SIGN_UP_URL = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    public static string SIGN_IN_URL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
    public static string EMAIL_VETIFICATION_URL = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
    public static string GET_USER_DATA_URL = "https://identitytoolkit.googleapis.com/v1/accounts:lookup?key=";
    public static string SEND_PASSWORD_RESET_EMAIL_URL = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
    public static string CONFIRM_PASSWORD_RESET_URL = "https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=";

    public static string USERS_DB_URL = DB_URL + "/users";
    public static string WEB_API_KEY = "AIzaSyD_mZ44fKkkFyhTkWR7XZf5WKBKZF-mK90";

    public static string USERS_BADGE_URL = DB_URL + "/badges";
    public static string USERS_SURVEY_URL = DB_URL + "/surveys";

    public static string BADGE_URL = "https://dcadmin.gear-up.nl";
    public static string BADGE_TOKEN_URL = BADGE_URL + "/api/token";
    public static string BADGE_CREDENTIAL_URL = BADGE_URL + "/api/create-credential";
}
