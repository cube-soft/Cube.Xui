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
using System.Collections.Generic;
using System.Threading;

namespace Cube.Xui.Mixin
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableExtension
    ///
    /// <summary>
    /// Bindable に関連する拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class BindableExtension
    {
        #region Methods

        #region ToBindable

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindable
        ///
        /// <summary>
        /// Bindable オブジェクトに変換します。
        /// </summary>
        ///
        /// <param name="src">変換元オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static Bindable<T> ToBindable<T>(this T src) => new Bindable<T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindable
        ///
        /// <summary>
        /// Bindable オブジェクトに変換します。
        /// </summary>
        ///
        /// <param name="src">変換元オブジェクト</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static Bindable<T> ToBindable<T>(this T src,
            SynchronizationContext context) => new Bindable<T>(src) { Context = context };

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindable
        ///
        /// <summary>
        /// BindableCollection オブジェクトに変換します。
        /// </summary>
        ///
        /// <param name="src">変換元オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static BindableCollection<T> ToBindable<T>(this IEnumerable<T> src) =>
            new BindableCollection<T>(src);

        /* ----------------------------------------------------------------- */
        ///
        /// ToBindable
        ///
        /// <summary>
        /// BindableCollection オブジェクトに変換します。
        /// </summary>
        ///
        /// <param name="src">変換元オブジェクト</param>
        /// <param name="context">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public static BindableCollection<T> ToBindable<T>(this IEnumerable<T> src,
            SynchronizationContext context) =>
            new BindableCollection<T>(src) { Context = context };

        #endregion

        #region Raise

        /* ----------------------------------------------------------------- */
        ///
        /// Raise
        ///
        /// <summary>
        /// Raises a PropertyChanged event against the Value property.
        /// </summary>
        ///
        /// <param name="src">Target object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public static void Raise<T>(this Bindable<T> src) =>
            src.RaisePropertyChanged(nameof(src.Value));

        #endregion

        #endregion
    }
}
