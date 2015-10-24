using Cirrious.CrossCore.IoC;
using Translyte.Core.Parse;

namespace Translyte.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            ParseAdapter.InitializeApp();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.AuthenticationViewModel>();
            
        }
    }
}