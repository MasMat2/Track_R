using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using TrackrAPI.Helpers;

//A simple C# class to post messages to a Slack channel
//Note: This class uses the Newtonsoft Json.NET serializer available via NuGet
public class SlackClient
{
    private readonly Uri _uri;
    private readonly Encoding _encoding = new UTF8Encoding();
    private string urlFrontend;
    private string environment;

    public SlackClient(string urlWithAccessToken)
    {
        _uri = new Uri(urlWithAccessToken);
    }

    //Post a message using simple strings
    public void PostMessage(string text, string username = null, string errorMessage = null)
    {
        Payload payload = new Payload()
        {
            Username = username,
            Text = text,
            ErrorMessage = errorMessage
        };

        PostMessage(payload);
    }

    //Post a message using a Payload object
    public void PostMessage(Payload payload)
    {
        GetVariables();
        
        var formattedStackTrace = ProcessStackTrace(payload.ErrorMessage);
        

        string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        string formattedErrorMessage = string.Join("", formattedStackTrace.Select(line => line.Replace("\\", "\\\\").Replace("\"", "\\\"").TrimEnd('\r')));
        var truncatedJson = TruncateJson(formattedErrorMessage);

        var formattedJson = truncatedJson.Replace("{0}", environment)
                                            .Replace("{1}", dateTimeNow)
                                            .Replace("{2}", payload.Text)
                                            .Replace("{3}", urlFrontend);


        using (WebClient client = new())
        {
            NameValueCollection data = new()
            {
                ["payload"] = formattedJson
            };

            var response = client.UploadValues(_uri, "POST", data);

            string responseText = _encoding.GetString(response);
        }

    }

    private static string[] ProcessStackTrace(string stackTraceText)
    {
        string[] stackTraceLines = stackTraceText.Split('\n');
        return stackTraceLines;
    }

    /// <summary>
    /// Truncates the given JSON template to a maximum length, ensuring it fits within the specified character limit.
    /// </summary>
    /// <param name="errorMessage">The JSON template to truncate.</param>
    /// <returns>The truncated JSON string.</returns>
    public static string TruncateJson(string errorMessage)
    {
        var fixedJsonStart = GeneralConstant.SlackJsonMessageStart;
        var fixedJsonEnd = GeneralConstant.SlackJsonMessageEnd;

        var maxCharacters = 3000 + fixedJsonStart.Length + fixedJsonEnd.Length; // 3000 es el numero maximo de caracteres para un texto en la api de slack

        var stackTraceText = errorMessage.Replace("\\", "\\\\").Replace("\"", "\\\"");

        var availableLength = maxCharacters - fixedJsonStart.Length - fixedJsonEnd.Length;

        if (stackTraceText.Length > availableLength)
        {
            stackTraceText = stackTraceText[..availableLength];
        }

        var truncatedJson = fixedJsonStart + stackTraceText + fixedJsonEnd;
        return truncatedJson;
    }

    private void GetVariables()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");


        urlFrontend = builder.Build().GetSection("AppSettings:UrlFrontEnd").Value;
        environment = builder.Build().GetSection("Slack:NombreAmbiente").Value;
        
    }


}

//This class serializes into the Json payload required by Slack Incoming WebHooks
public class Payload
{
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("text")]
    public string Text { get; set; }
    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }
 
}