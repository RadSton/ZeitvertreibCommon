namespace vip.zeitvertreib
{
    using System.ComponentModel;
    using System.IO;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {

        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }
    }
}