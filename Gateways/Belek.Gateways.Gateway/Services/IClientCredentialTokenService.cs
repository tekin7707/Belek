using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Gateways.Gateway.Services
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}