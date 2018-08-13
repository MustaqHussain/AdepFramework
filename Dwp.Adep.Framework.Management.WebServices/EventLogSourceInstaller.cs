using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;


namespace Dwp.Adep.Framework.Management.WebServices
{
    [RunInstaller(true)]
    public partial class EventLogSourceInstaller : System.Configuration.Install.Installer
    {
        
        private EventLogInstaller customeEventLogInstaller;
        
        public EventLogSourceInstaller()
        {
            InitializeComponent();
            customeEventLogInstaller = new EventLogInstaller();

            customeEventLogInstaller.Source = "Adep Framework Web Services Error";
            customeEventLogInstaller.Log = "Application";
            Installers.Add(customeEventLogInstaller);
        
        }
    }
}
