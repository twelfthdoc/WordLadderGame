using System;
using System.Collections.Generic;
using Xunit;
using WordLadderGame.Common;

namespace WordLadderGameTests.Common
{
    public class HelperMethodsTests
    {
        #region IsNullOrEmpty Tests
        [Fact]
        public void IsNullOrEmptyReturnsTrueWhenObjectIsNull()
        {
            List<object> sut = null;
            Exception exception = null;
            var result = false;

            try
            {
                result = sut.IsNullOrEmpty();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.Null(sut);
            Assert.Null(exception);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyReturnsTrueWhenObjectIsEmpty()
        {
            List<object> sut = null;
            Exception exception = null;
            var result = false;

            try
            {
                sut = new List<object>();
                result = sut.IsNullOrEmpty();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.Empty(sut);
            Assert.Null(exception);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyReturnsFalseWhenObjectIsNotEmpty()
        {
            List<object> sut = null;
            Exception exception = null;
            var result = true;

            try
            {
                sut = new List<object> {1};
                result = sut.IsNullOrEmpty();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotEmpty(sut);
            Assert.Null(exception);
            Assert.False(result);
        }

        [Fact]
        public void IsNullOrEmptyThrowsWhenObjectIsNotEnumerable()
        {
            dynamic sut = 1;
            Exception exception = null;
            
            try
            {
                HelperMethods.IsNullOrEmpty(sut);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(sut);
            Assert.NotNull(exception);            
        }
        #endregion
    }
}