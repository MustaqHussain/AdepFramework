﻿using System;

namespace ManagementConsole.Server.IoC.ServiceLocation
{
    public class SimpleServiceLocator
    {
        public static void SetServiceLocatorProvider(ISimpleServiceLocator simpleServiceLocator)
        {
            Instance = simpleServiceLocator;
        }

        public static ISimpleServiceLocator Instance { get; private set; }
    }
}
