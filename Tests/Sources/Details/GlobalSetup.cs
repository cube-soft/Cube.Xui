﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using Cube.Mixin.Logging;
using NUnit.Framework;
using System.Reflection;
using System.Windows;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// GlobalSetup
    ///
    /// <summary>
    /// Provides functionality to run at the beginning of the NUnit.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [SetUpFixture]
    public class GlobalSetup
    {
        /* ----------------------------------------------------------------- */
        ///
        /// OneTimeSetup
        ///
        /// <summary>
        /// Invokes the setup only once.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger.Configure();
            BindingLogger.Configure();
            _ = Logger.ObserveTaskException();
            _ = Application.Current.ObserveUiException();
            Logger.Info(typeof(GlobalSetup), Assembly.GetExecutingAssembly());
        }
    }
}
