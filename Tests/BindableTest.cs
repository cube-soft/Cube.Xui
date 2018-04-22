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
using NUnit.Framework;
using System.Threading;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableTest
    ///
    /// <summary>
    /// Bindable のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class BindableTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// RaiseCollectionChanged
        ///
        /// <summary>
        /// PropertyChanged イベントの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true,  ExpectedResult = 4)]
        [TestCase(false, ExpectedResult = 2)]
        public int RaisePropertyChanged(bool redirect)
        {
            var ctx = SynchronizationContext.Current;
            using (var src = new Bindable<Person>(null, redirect, ctx))
            {
                var count = 0;
                Assert.That(src.HasValue, Is.False);

                src.PropertyChanged += (s, e) => ++count;
                var value = new Person();
                src.Value = value;
                Assert.That(src.HasValue, Is.True);

                value.Name = "Jack";
                value.Age  = 20;

                return count;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable_Context
        ///
        /// <summary>
        /// SynchronizationContext を指定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Bindable_Context()
        {
            var value = new Person();
            using (var src = new Bindable<Person>(value, true, new SynchronizationContext()))
            {
                var count = 0;
                src.PropertyChanged += (s, e) => ++count;
                src.Value = value;

                value.Name = "Jack";
                value.Age  = 20;

                Assert.That(count, Is.EqualTo(2));
            }
        }

        #endregion
    }
}
