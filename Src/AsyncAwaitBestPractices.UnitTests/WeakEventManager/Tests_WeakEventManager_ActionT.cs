﻿using System;
using NUnit.Framework;

namespace AsyncAwaitBestPractices.UnitTests
{
    public class Tests_WeakEventManager_ActionT : BaseTest
    {
        #region Constant Fields
        readonly WeakEventManager<string> _actionEventManager = new WeakEventManager<string>();
        #endregion

        #region Events
        public event Action<string> ActionEvent
        {
            add => _actionEventManager.AddEventHandler(value);
            remove => _actionEventManager.RemoveEventHandler(value);
        }
        #endregion

        #region Methods
        [Test]
        public void WeakEventManagerActionT_HandleEvent_ValidImplementation()
        {
            //Arrange
            ActionEvent += HandleDelegateTest;
            bool didEventFire = false;

            void HandleDelegateTest(string message)
            {
                Assert.IsNotNull(message);
                Assert.IsNotEmpty(message);

                didEventFire = true;
                ActionEvent -= HandleDelegateTest;
            }

            //Act
            _actionEventManager.HandleEvent("Test", nameof(ActionEvent));

            //Assert
            Assert.IsTrue(didEventFire);
        }

        [Test]
        public void WeakEventManagerActionT_HandleEvent_InvalidHandleEventEventName()
        {
            //Arrange
            ActionEvent += HandleDelegateTest;
            bool didEventFire = false;

            void HandleDelegateTest(string message)
            {
                Assert.IsNotNull(message);
                Assert.IsNotEmpty(message);

                didEventFire = true;
            }

            //Act
            _actionEventManager.HandleEvent("Test", nameof(TestEvent));

            //Assert
            Assert.False(didEventFire);
            ActionEvent -= HandleDelegateTest;
        }

        [Test]
        public void WeakEventManagerActionT_UnassignedEvent()
        {
            //Arrange
            bool didEventFire = false;

            ActionEvent += HandleDelegateTest;
            ActionEvent -= HandleDelegateTest;
            void HandleDelegateTest(string message)
            {
                Assert.IsNotNull(message);
                Assert.IsNotEmpty(message);

                didEventFire = true;
            }

            //Act
            _actionEventManager.HandleEvent("Test", nameof(ActionEvent));

            //Assert
            Assert.IsFalse(didEventFire);
        }

        [Test]
        public void WeakEventManagerActionT_UnassignedEventManager()
        {
            //Arrange
            var unassignedEventManager = new WeakEventManager();
            bool didEventFire = false;

            ActionEvent += HandleDelegateTest;
            void HandleDelegateTest(string message)
            {
                Assert.IsNotNull(message);
                Assert.IsNotEmpty(message);

                didEventFire = true;
            }

            //Act
            unassignedEventManager.HandleEvent(nameof(ActionEvent));

            //Assert
            Assert.IsFalse(didEventFire);
            ActionEvent -= HandleDelegateTest;
        }

        [Test]
        public void WeakEventManagerActionT_HandleEvent_InvalidHandleEvent()
        {
            //Arrange
            ActionEvent += HandleDelegateTest;
            bool didEventFire = false;

            void HandleDelegateTest(string message)
            {
                Assert.IsNotNull(message);
                Assert.IsNotEmpty(message);

                didEventFire = true;
            }

            //Act

            //Assert
            Assert.Throws<InvalidHandleEventException>(() => _actionEventManager.HandleEvent(this, "Test", nameof(ActionEvent)));
            Assert.IsFalse(didEventFire);
            ActionEvent -= HandleDelegateTest;
        }

        [Test]
        public void WeakEventManagerActionT_AddEventHandler_NullHandler()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.AddEventHandler((Action<string>)null, nameof(ActionEvent)), "Value cannot be null.\nParameter name: action");
        }

        [Test]
        public void WeakEventManagerActionT_AddEventHandler_NullEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.AddEventHandler(s => { var temp = s; }, null), "Value cannot be null.\nParameter name: eventName");
        }

        [Test]
        public void WeakEventManagerActionT_AddEventHandler_EmptyEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.AddEventHandler((Action<string>)null, string.Empty), "Value cannot be null.\nParameter name: eventName");
        }

        [Test]
        public void WeakEventManagerActionT_AddEventHandler_WhitespaceEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.AddEventHandler(s => { var temp = s; }, " "), "Value cannot be null.\nParameter name: eventName");
        }

        [Test]
        public void WeakEventManagerActionT_RemoveventHandler_NullHandler()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.RemoveEventHandler((Action<string>)null), "Value cannot be null.\nParameter name: handler");
        }

        [Test]
        public void WeakEventManagerActionT_RemoveventHandler_NullEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.RemoveEventHandler(s => { var temp = s; }, null), "Value cannot be null.\nParameter name: eventName");
        }

        [Test]
        public void WeakEventManagerActionT_RemoveventHandler_EmptyEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.RemoveEventHandler(s => { var temp = s; }, string.Empty), "Value cannot be null.\nParameter name: eventName");
        }

        [Test]
        public void WeakEventManagerActionT_RemoveventHandler_WhiteSpaceEventName()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _actionEventManager.RemoveEventHandler(s => { var temp = s; }, " "), "Value cannot be null.\nParameter name: eventName");
        }
        #endregion
    }
}
