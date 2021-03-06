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
using System.Threading;
using System.Windows.Controls;
using Cube.Xui.Behaviors;
using NUnit.Framework;

namespace Cube.Xui.Tests.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// PasswordBehaviorTest
    ///
    /// <summary>
    /// Tests for the PasswordBehavior class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    class PasswordBehaviorTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// Executes the test to create, attach, and detach method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Create()
        {
            var view = new PasswordBox();
            var src  = new PasswordBehavior();

            src.Attach(view);
            src.Password = "Behavior";
            Assert.That(view.Password, Is.EqualTo(src.Password).And.EqualTo("Behavior"));

            view.Password = "View";
            Assert.That(src.Password, Is.EqualTo(view.Password).And.EqualTo("View"));
            src.Detach();
        }

        #endregion
    }
}
