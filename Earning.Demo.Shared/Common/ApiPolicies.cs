using System;
using Polly;

namespace Earning.Demo.Shared.Common
{
    public static class ApiPolicies
    {
        public static Policy[] Create()
        {
            return new Policy[] {
                Policy.Handle<Exception>().Fallback(() => {
                }),
                Policy.Handle<InvalidOperationException>().Fallback(() => {
                })
            };
        }
    }
}
