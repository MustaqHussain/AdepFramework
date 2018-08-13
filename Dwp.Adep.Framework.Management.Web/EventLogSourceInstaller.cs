using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;


namespace Dwp.Adep.Framework.Management.Web
{
    [RunInstaller(true)]
    public partial class EventLogSourceInstaller : System.Configuration.Install.Installer
    {
        private EventLogInstaller customeEventLogInstaller;
        public EventLogSourceInstaller()
        {
            InitializeComponent();
            customeEventLogInstaller = new EventLogInstaller();

            customeEventLogInstaller.Source = "Adep Framework Web Error";
            customeEventLogInstaller.Log = "Application";
            Installers.Add(customeEventLogInstaller);
        }
    }
}
