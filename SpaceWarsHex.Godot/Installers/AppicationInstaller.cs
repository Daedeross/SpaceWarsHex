//using Injact;
//using SpaceWarHex.Bridges;

//namespace SpaceWarHex.Installers
//{
//    public class AppicationInstaller : Installer
//    {
//        public DiContainer Container => _container;

//        public AppicationInstaller(DiContainer container) : base(container)
//        {
//        }

//        public override void InstallBindings()
//        {
//            Container.Bind<IGodotHexMath, HexMath>()
//                .FromInstance(new HexMath(128f))
//                .Immediate()
//                .Finalise();
//        }
//    }
//}
