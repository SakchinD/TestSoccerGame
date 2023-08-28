using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PolicyScreen : MonoBehaviour
{ 
    [SerializeField] private TMP_Text _policyText;

    private IPrivacyPolicyController _policyController;

    [Inject]
    private void Construct(IPrivacyPolicyController policyController)
    {
        _policyController = policyController;
    }

    private void OnEnable()
    {
        GetPrivacyPolicyAsync();
    }

    private void GetPrivacyPolicyAsync()
    {
        var text = _policyController.PrivacyPolicy;
        if (text == null)
        {
            _policyController.LoadPrivacyPolicyAsync()
                .ContinueWith(GetPrivacyPolicyAsync)
                .Forget();
            return;
        }

        _policyText.text = text;
    }
}
