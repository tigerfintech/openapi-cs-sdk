using NUnit.Framework;
using TigerOpenAPI.Common;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies every constructor of <see cref="TigerApiException"/>
  /// correctly populates <c>ErrCode</c>, <c>ErrMsg</c>, and <c>TigerApiCode</c>.
  /// </summary>
  [TestFixture]
  public class TigerApiExceptionTest
  {
    /// <summary>Default constructor leaves all fields at their default values.</summary>
    [Test]
    public void DefaultConstructor_LeavesErrCodeZeroAndErrMsgNull()
    {
      TigerApiException ex = new TigerApiException();

      Assert.That(ex.ErrCode, Is.EqualTo(0));
      Assert.That(ex.ErrMsg, Is.Null);
      Assert.That(ex.TigerApiCode, Is.Null);
    }

    /// <summary>String constructor sets ErrMsg to the supplied message.</summary>
    [Test]
    public void StringConstructor_SetsErrMsgToMessage()
    {
      TigerApiException ex = new TigerApiException("boom");

      Assert.That(ex.ErrCode, Is.EqualTo(0));
      Assert.That(ex.ErrMsg, Is.EqualTo("boom"));
      Assert.That(ex.Message, Is.EqualTo("boom"));
      Assert.That(ex.TigerApiCode, Is.Null);
    }

    /// <summary>int+string constructor sets ErrCode and ErrMsg.</summary>
    [Test]
    public void IntAndStringConstructor_SetsErrCodeAndErrMsg()
    {
      TigerApiException ex = new TigerApiException(1000, "common param error");

      Assert.That(ex.ErrCode, Is.EqualTo(1000));
      Assert.That(ex.ErrMsg, Is.EqualTo("common param error"));
      Assert.That(ex.Message, Is.EqualTo("1000:common param error"));
      Assert.That(ex.TigerApiCode, Is.Null);
    }

    /// <summary>TigerApiCode constructor sets ErrCode, ErrMsg and TigerApiCode.</summary>
    [Test]
    public void TigerApiCodeConstructor_SetsErrCodeErrMsgAndTigerApiCode()
    {
      TigerApiException ex = new TigerApiException(TigerApiCode.COMMON_PARAM_ERROR);

      Assert.That(ex.ErrCode, Is.EqualTo(1000));
      Assert.That(ex.ErrMsg, Is.EqualTo("common param error"));
      Assert.That(ex.Message, Is.EqualTo("1000:common param error"));
      Assert.That(ex.TigerApiCode, Is.SameAs(TigerApiCode.COMMON_PARAM_ERROR));
    }

    /// <summary>TigerApiCode + params constructor formats the message template with args.</summary>
    [Test]
    public void TigerApiCodeAndParams_FormatsMessageTemplateWithArgs()
    {
      TigerApiException ex =
        new TigerApiException(TigerApiCode.HTTP_COMMON_PARAM_ERROR, "symbol");

      Assert.That(ex.ErrCode, Is.EqualTo(10000));
      Assert.That(ex.ErrMsg, Is.EqualTo("client common param error(symbol)"));
      Assert.That(ex.Message, Is.EqualTo("10000:client common param error(symbol)"));
      Assert.That(ex.TigerApiCode, Is.SameAs(TigerApiCode.HTTP_COMMON_PARAM_ERROR));
    }

    /// <summary>TigerApiCode with two format placeholders fills both {0} and {1}.</summary>
    [Test]
    public void TigerApiCodeAndTwoParams_FormatsBothPlaceholders()
    {
      TigerApiException ex =
        new TigerApiException(TigerApiCode.HTTP_BIZ_PARAM_RANGE_ERROR, "page_size", "max_size");

      Assert.That(ex.ErrCode, Is.EqualTo(10103));
      Assert.That(ex.ErrMsg,
        Is.EqualTo("client biz param error ('page_size' cannot be greater than 'max_size')"));
      Assert.That(ex.TigerApiCode, Is.SameAs(TigerApiCode.HTTP_BIZ_PARAM_RANGE_ERROR));
    }
  }
}
