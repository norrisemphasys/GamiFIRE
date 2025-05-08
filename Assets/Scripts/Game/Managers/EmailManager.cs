using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UE.Email;

public class EmailManager : MonoSingleton<EmailManager>
{
    // SMTP Credentials
    public static string USERNAME = "norrisemphasys@gmail.com";
    public static string PASSWORD = "F9AA72C45B96548B43680641A0727B744E56";

    public static string HOST = "smtp.elasticemail.com";

    // EmailJS Credentials
    public static string SERVICE_ID = "service_8gnjq1v";
    public static string TEMPLATE_ID = "template_90sxgx1";

    public void SendEmail(string email, string subject, string body)
    {
        Debug.Log("Sending Email");
        // SMTP
        //Email.SendEmail("norrisemphasys@gmail.com", email, subject, body, HOST, USERNAME, PASSWORD);

        // EmailJS
        Email.SendEmailJS(SERVICE_ID, TEMPLATE_ID, subject, "GamiFIRE", System.DateTime.Today.ToString(), body, email);
    }

    private void Update()
    {
#if TEST_EMAIL
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E))
        {
            SendEmail("norris@emphasyscentre.com", "NEWS LETTER", "Thank for subscribing on the GamiFIRE News Letter!");
        }
#endif
    }
}
