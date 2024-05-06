using GoplayasiaBlazor.Core.Global.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace GoPlayAsiaWebApp
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        #region Injected Services
        private ICurrentUser _ICurrentUser;
        private Blazored.SessionStorage.ISessionStorageService _protectedSessionStore;
        #endregion

        #region Local Variables

        #endregion

        #region Implemented Methods

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            await _ICurrentUser.restoreSessionAsync();
            ClaimsIdentity cIdentity;
            if (_ICurrentUser.Token == null)
            {

                cIdentity = new ClaimsIdentity();
            }
            else
            {
                cIdentity = new ClaimsIdentity("Custom");
                //cIdentity.AddClaim(new Claim(ClaimTypes.Name, _ICurrentUser.FullName));
                //cIdentity.AddClaim(new Claim(ClaimTypes.Email, _ICurrentUser.EmailAddress));
                cIdentity.AddClaim(new Claim("token", _ICurrentUser.Token));
                cIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, _ICurrentUser.Id.ToString()));
                cIdentity.AddClaim(new Claim(ClaimTypes.Role, _ICurrentUser.RoleType.ToString()));
            }
            var user = new ClaimsPrincipal(cIdentity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            return await Task.FromResult(new AuthenticationState(user));
        }
        public CustomAuthStateProvider(ICurrentUser ICurrentUser, Blazored.SessionStorage.ISessionStorageService protectedSessionStore)
        {
            _ICurrentUser = ICurrentUser;
            _protectedSessionStore = protectedSessionStore;
        }
        #endregion

        #region Util Methods
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        #endregion

        #region Local Methods
        public async Task MarkUserAsAuthenticated()
        {
            try
            {
                await _ICurrentUser.restoreSessionAsync();
                var cIdentity = new ClaimsIdentity("Custom");
                cIdentity.AddClaim(new Claim(ClaimTypes.Name, _ICurrentUser.FullName));
                cIdentity.AddClaim(new Claim(ClaimTypes.Email, _ICurrentUser.EmailAddress));
                cIdentity.AddClaim(new Claim("token", _ICurrentUser.Token));
                cIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, _ICurrentUser.Id.ToString()));
                cIdentity.AddClaim(new Claim(ClaimTypes.Role, _ICurrentUser.RoleType.ToString()));
                var id = new ClaimsIdentity(cIdentity);
                var user = new ClaimsPrincipal(id);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
            }
            catch (Exception)
            {
                ClaimsIdentity cIdentity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(cIdentity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }


        }
        public async Task MarkUserAsLoggedOut()
        {
            var cIdentity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(cIdentity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            //await _sessionStorageService.RemoveItemAsync("token");
            //await _sessionStorageService.RemoveItemAsync(ClaimTypes.Name);
            //await _sessionStorageService.RemoveItemAsync(ClaimTypes.Email);
            //await _sessionStorageService.RemoveItemAsync(ClaimTypes.NameIdentifier);

        }
        #endregion
    }
}
