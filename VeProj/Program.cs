using System.Security.Cryptography;
using Autofac;
using Security;
using Security.Hashing;
using Security.HMAC;
using Security.HOTP;
using Security.TOTP;
using System;
using HMACSHA1 = Security.HMAC.HMACSHA1;
using SHA1 = Security.Hashing.SHA1;

namespace VeProj
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = SetupContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IUserCredentialService>();
                string password = service.GeneratePassword(123, DateTime.Now);

                Console.WriteLine("Generated password is: {0}", password);
                Console.ReadLine();
            }
        }

        private static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserCredentialService>().As<IUserCredentialService>();
            builder.RegisterType<HMACSHA1>().As<IHMAC>();
            builder.RegisterType<HOTP>().As<IHOTP>();
            builder.RegisterType<TOTP>().As<ITOTP>();
            builder.RegisterType<SHA1>().As<IHasing>();

            return builder.Build();
        }
    }
}
