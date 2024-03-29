﻿using System;
using System.IO;
using System.Linq;

using GSendShared.Providers.Internal.Enc;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Middleware;
using Middleware.Accounts;

using SharedPluginFeatures;

namespace GSendService.Internal
{
    public class InitializeDataThread : IConfigureApplicationBuilder
    {
        private const string ApiKey = "fpkd55ff468751343799600792077b4ec69";
        private const string Secret = "4994d3429391b750bf7";

        public void ConfigureApplicationBuilder(in IApplicationBuilder applicationBuilder)
        {
            IAccountProvider accounts = applicationBuilder.ApplicationServices.GetRequiredService<IAccountProvider>();
            IUserSearch userSearch = applicationBuilder.ApplicationServices.GetRequiredService<IUserSearch>();
            IUserApiProvider userApiProvider = applicationBuilder.ApplicationServices.GetRequiredService<IUserApiProvider>();

            long userId = -1;
            Middleware.Users.SearchUser user = userSearch.GetUsers(1, 20000, null, null).Find(u => u.Email.Equals("api.user"));
            if (user == null)
            {
                accounts.CreateAccount("api.user", "api", "user", "simpleApiUser#9876", "", "", "", "", "", "", "", "", "", out userId);
            }
            else
            {
                userId = user.Id;
            }

            string merchantId = userApiProvider.GetMerchantId(userId);

            if (String.IsNullOrEmpty(merchantId))
            {
                userApiProvider.AddApi(userId, ApiKey, Secret);
                merchantId = userApiProvider.GetMerchantId(userId);
            }

            string encryptedData = AesImpl.Encrypt($"{merchantId}#{ApiKey}#{Secret}", Convert.FromBase64String(Environment.GetEnvironmentVariable("gsp")));

            File.WriteAllText(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "api.dat"), encryptedData);
        }
    }
}
