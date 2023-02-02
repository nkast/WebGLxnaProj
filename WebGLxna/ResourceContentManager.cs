using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework.Content;


namespace WebGLxna
{
    public class ResourceContentManager : ContentManager
    {
        public ResourceContentManager(IServiceProvider servicesProvider)
           : base(servicesProvider)
        {

        }

        protected override Stream OpenStream(string assetName)
        {
            Assembly asm = this.GetType().Assembly;            
            var asmName = asm.GetName().Name;

            var assetFullPath = Path.Combine(asmName, RootDirectory, assetName);
            assetFullPath = assetFullPath.Replace('/','.');
            Stream stream = asm.GetManifestResourceStream(assetFullPath+ ".xnb");
            if (stream == null)
                stream = base.OpenStream(assetName);

            return stream;
        }
    }
}
