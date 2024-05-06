namespace GoplayasiaBlazor.Core.Helpers;

using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class ReCaptchaClass
{
    private static IConfiguration _config;
    private List<string> m_ErrorCodes;
    private string PrivateKey;
    public static string Validate(string EncodedResponse)
    {
        var client = new System.Net.WebClient();
        //uat
        string PrivateKey = _config.GetValue<String>("capchasecretkey");

        var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

        var captchaResponse = JsonSerializer.Deserialize<ReCaptchaClass>(GoogleReply);

        return captchaResponse.Success.ToLower();
    }
    public ReCaptchaClass(IConfiguration config)
    {
        _config = config;
    }

    public string Success
    {
        get { return m_Success; }
        set { m_Success = value; }
    }

    private string m_Success;
    public List<string> ErrorCodes
    {
        get { return m_ErrorCodes; }
        set { m_ErrorCodes = value; }
    }


}