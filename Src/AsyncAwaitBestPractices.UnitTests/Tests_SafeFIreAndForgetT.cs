﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AsyncAwaitBestPractices.UnitTests
{
    public class Tests_SafeFireAndForgetT : BaseTest
    {
        [SetUp]
        public void BeforeEachTest()
        {
            SafeFireAndForgetExtensions.Initialize(false);
            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(null);
        }

        [TearDown]
        public void AfterEachTest()
        {
            SafeFireAndForgetExtensions.Initialize(false);
            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(null);
        }

        [Test]
        public async Task SafeFireAndForget_HandledException()
        {
            //Arrange
            NullReferenceException exception = null;

            //Act
            NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(onException: ex => exception = ex);
            await NoParameterTask();
            await NoParameterTask();

            //Assert
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_NoParams()
        {
            //Arrange
            Exception exception = null;
            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception = ex);

            //Act
            NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget();
            await NoParameterTask();
            await NoParameterTask();

            //Assert
            Assert.IsNotNull(exception);

            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(null);
        }

        [Test]
        public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_WithParams()
        {
            //Arrange
            Exception exception1 = null;
            NullReferenceException exception2 = null;
            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception1 = ex);

            //Act
            NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(onException: ex => exception2 = ex);
            await NoParameterTask();
            await NoParameterTask();

            //Assert
            Assert.IsNotNull(exception1);
            Assert.IsNotNull(exception2);

            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(null);
        }
    }
}
