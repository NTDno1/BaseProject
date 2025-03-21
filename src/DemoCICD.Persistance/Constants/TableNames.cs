﻿namespace DemoCICD.Persistance.Constants;
internal class TableNames
{
    internal const string Actions = nameof(Actions);
    internal const string Functions = nameof(Functions);
    internal const string ActionInFunctions = nameof(ActionInFunctions);
    internal const string Permissions = nameof(Permissions);

    internal const string AppUsers = nameof(AppUsers);
    internal const string AppRoles = nameof(AppRoles);
    internal const string AppUserRoles = nameof(AppUserRoles);

    internal const string AppUserClaims = nameof(AppUserClaims); // IdentityUserClaim
    internal const string AppRoleClaims = nameof(AppRoleClaims); // IdentityRoleClaim
    internal const string AppUserLogins = nameof(AppUserLogins); // IdentityRoleClaim
    internal const string AppUserTokens = nameof(AppUserTokens); // IdentityUserToken

    // *********** Singular Nouns ***********
    internal const string Product = nameof(Product);
}
