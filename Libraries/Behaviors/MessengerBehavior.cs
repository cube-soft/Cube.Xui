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
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Interactivity;

namespace Cube.Xui.Behaviors
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerBehavior(T)
    ///
    /// <summary>
    /// Messenger オブジェクトに登録するための Behavior クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class MessengerBehavior<T> : Behavior<DependencyObject>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Messenger
        ///
        /// <summary>
        /// Messenger オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IMessenger Messenger
        {
            get => _messenger;
            set
            {
                if (_messenger == value) return;
                if (_messenger != null) _messenger.Unregister<T>(AssociatedObject);

                _messenger = value;
                if (_messenger == null) return;
                _messenger.Register<T>(AssociatedObject, e => Invoke(e));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MessengerProperty
        ///
        /// <summary>
        /// Messenger を保持するための DependencyProperty です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static readonly DependencyProperty MessengerProperty =
            DependencyProperty.RegisterAttached(
                nameof(Messenger),
                typeof(IMessenger),
                typeof(MessengerBehavior<T>),
                new PropertyMetadata((s, e) =>
                {
                    if (s is MessengerBehavior<T> b) b.Messenger = e.NewValue as IMessenger;
                })
            );

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// 処理を実行します。
        /// </summary>
        ///
        /// <param name="e">パラメータ</param>
        ///
        /* ----------------------------------------------------------------- */
        protected abstract void Invoke(T e);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// OnAttached
        ///
        /// <summary>
        /// 要素へ接続された時に実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnAttached()
        {
            base.OnAttached();
            Messenger.Register<T>(AssociatedObject, e => Invoke(e));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnDetaching
        ///
        /// <summary>
        /// 要素から解除された時に実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnDetaching()
        {
            Messenger.Unregister<T>(AssociatedObject);
            base.OnDetaching();
        }

        #endregion

        #region Fields
        private IMessenger _messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default;
        #endregion
    }
}
