using Cysharp.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public interface IPrivacyPolicyController
{
    string PrivacyPolicy { get; }
    UniTask LoadPrivacyPolicyAsync();
}
public class PrivacyPolicyController : IPrivacyPolicyController, IDisposable
{
    private readonly string _url = "https://aviabaffishpic.com/politic/";

    private CancellationTokenSource _cancellationTokenSource;

    private int _loadAttempt;
    private string _privacyPolicy;

    public string PrivacyPolicy => _privacyPolicy;

    public PrivacyPolicyController()
    {
        LoadPrivacyPolicyAsync().Forget();
    }

    public void Dispose()
    {
        ReleaseCancellationToken();
    }

    public async UniTask LoadPrivacyPolicyAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            var webRequest = await UnityWebRequest.Get(_url)
                .SendWebRequest()
                .WithCancellation(_cancellationTokenSource.Token);

            if (webRequest != null)
            {
                string html = webRequest.downloadHandler.text;
                ReleaseCancellationToken();
                _privacyPolicy = StripHtmlTags(html);
                Debug.Log("Policy Load complite");
                return;
            }

            _privacyPolicy = null;
            ReleaseCancellationToken();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);

            _privacyPolicy = null;
            ReleaseCancellationToken();

            if (_loadAttempt < 3)
            {
                _loadAttempt++;
                LoadPrivacyPolicyAsync().Forget();
                return;
            }
        }
    }

    private string StripHtmlTags(string html)
    {
        string htmlRemoved = Regex.Replace(html, "<title>.*?</title>", string.Empty, RegexOptions.IgnoreCase);
        htmlRemoved = Regex.Replace(htmlRemoved, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        htmlRemoved = Regex.Replace(htmlRemoved, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        htmlRemoved = Regex.Replace(htmlRemoved, "<(?!\\/p\\b).*?>", string.Empty);
        htmlRemoved = Regex.Replace(htmlRemoved, "<p>", "");
        htmlRemoved = Regex.Replace(htmlRemoved, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        htmlRemoved = Regex.Replace(htmlRemoved, @"</p>", "<br><br>");
        htmlRemoved = Regex.Replace(htmlRemoved, @"\s{2,}", " ");
        htmlRemoved = Regex.Replace(htmlRemoved, @"^\s+|\s+$", "", RegexOptions.Multiline);
        htmlRemoved = System.Net.WebUtility.HtmlDecode(htmlRemoved);
        return htmlRemoved;
    }

    private void ReleaseCancellationToken()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }
}
