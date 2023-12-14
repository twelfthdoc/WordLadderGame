using System;
using System.Collections.Generic;
using Xunit;
using WordLadderGame.Common;

namespace WordLadderGameTests.Common
{
    public class HelperMethodsTests
    {
        #region IsAlpha Tests
        [Theory]
        [InlineData(@"")]
        [InlineData(@"string")]
        public void IsAlphaReturnsTrueWhenStringIsAlphabetic(string sut)
        {
            Exception exception = null;
            bool result = false;

            try
            {
                result = sut.IsAlpha();
            }
            catch(Exception e)
            {
                exception = e;
            }

            Assert.NotNull(sut);
            Assert.Null(exception);
            Assert.True(result);
        }

        [Fact]
        public void IsAlphaReturnsFalseWhenStringHasNumbers()
        {
            var sut = @"12345";
            Exception exception = null;
            bool result = true;

            try
            {
                result = sut.IsAlpha();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(sut);
            Assert.Null(exception);
            Assert.False(result);
        }

        [Fact]
        public void IsAlphaReturnsFalseWhenStringHasSymbols()
        {
            var sut = @"!$%^";
            Exception exception = null;
            bool result = true;

            try
            {
                result = sut.IsAlpha();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(sut);
            Assert.Null(exception);
            Assert.False(result);
        }

        [Fact]
        public void IsAlphaThrowsWhenStringIsNull()
        {
            string sut = null;
            Exception exception = null;
            
            try
            {
                sut.IsAlpha();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.Null(sut);
            Assert.NotNull(exception);
        }

        [Fact]
        public void IsAlphaThrowsWhenObjectIsNotString()
        {
            dynamic sut = new List<int>();
            Exception exception = null;
            
            try
            {
                HelperMethods.IsAlpha(sut);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(sut);
            Assert.NotNull(exception);
        }
        #endregion

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

        #region IsSimilar Tests
        [Fact]
        public void IsSimilarReturnsTrueWhenWordsAreOneLetterApart()
        {
            var first = "span";
            var second = "spat";

            var result = false;
            Exception exception = null;

            try
            {
                result = first.IsSimilar(second);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.Null(exception);
            Assert.NotEqual(first, second);
            Assert.True(result);
        }

        [Fact]
        public void IsSimilarReturnsFalseWhenWordsAreEqual()
        {
            var first = "equal";
            var second = "equal";

            var result = true;
            Exception exception = null;

            try
            {
                result = first.IsSimilar(second);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.Null(exception);
            Assert.Equal(first, second);
            Assert.False(result);
        }

        [Fact]
        public void IsSimilarReturnsFalseWhenWordsAreMoreThanOneLetterApart()
        {
            var first = "first";
            var second = "worst";

            var result = true;
            Exception exception = null;

            try
            {
                result = first.IsSimilar(second);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.Null(exception);
            Assert.NotEqual(first, second);
            Assert.False(result);
        }

        [Fact]
        public void IsSimilarReturnsFalseWhenWordsAreNotEqualLength()
        {
            var first = "first";
            var second = "second";

            var result = true;
            Exception exception = null;

            try
            {
                result = first.IsSimilar(second);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.Null(exception);
            Assert.NotEqual(first, second);
            Assert.False(result);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("first", null)]
        [InlineData(null, "second")]
        public void IsSimilarThrowsWhenEitherWordIsNull(string first, string second)
        {
            Exception exception = null;

            try
            {
                first.IsSimilar(second);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.NotNull(exception);
            Assert.IsType<NullReferenceException>(exception);
        }
        #endregion
    }
}