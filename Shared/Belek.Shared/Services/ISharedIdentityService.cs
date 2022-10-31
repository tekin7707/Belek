using System;
using System.Collections.Generic;
using System.Text;

namespace Belek.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}